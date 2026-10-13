namespace ROBOT.Services
{
    // Kare kare bulunan nesneleri takip eder ve ölçümlerini yumuşatır: konum, boyut, açı ve güven yüzdesi
    // her karede birkaç piksel/yüzde oynadığından çerçeve titrer; burada üstel ortalama (EMA) ile sabitlenir.
    // Ayrıca bir nesne tek karede kaybolursa çerçeve hemen silinmez, kısa süre tutulur.
    public class DetectionTracker
    {
        private const float PositionAlpha = 0.25f;   // 0'a yaklaştıkça daha yavaş/sakin
        private const float ScoreAlpha = 0.12f;
        private const float PositionDeadband = 1.5f; // piksel; bundan küçük oynamalar yok sayılır
        private const float AngleDeadband = 1.5f;    // derece
        private const float MatchDistance = 80f;     // aynı nesne sayılacak en büyük merkez uzaklığı (piksel)
        private const int MinHits = 3;               // çizilmeden önce görülmesi gereken kare sayısı
        private const int MaxMisses = 10;            // kaybolduktan sonra çerçevenin tutulacağı kare sayısı

        // "Otomatik" şekilde daire/dikdörtgen arası gidip gelmesin diye iki eşikli geçiş.
        private const double CircleEnter = 0.84;
        private const double CircleLeave = 0.76;

        private class Track
        {
            public string ClassName = "";
            public bool Matched;
            public float Score;
            public ObjectShape ShapeMode;
            public double Circularity;
            public bool IsCircle;
            public float CircleX, CircleY, Radius;
            public float RectX, RectY, RectWidth, RectHeight, RectAngle;
            public int Hits;
            public int Misses;
        }

        private readonly object _lock = new();
        private readonly List<Track> _tracks = new();

        public void Reset()
        {
            lock (_lock) _tracks.Clear();
        }

        // O karede bulunan ham nesneleri alır, yumuşatılmış çizilecek nesneleri döner.
        public List<Detection> Update(IReadOnlyList<Detection> raw)
        {
            lock (_lock)
            {
                var updated = new HashSet<Track>();

                foreach (var detection in raw)
                {
                    var track = FindNearestTrack(detection, updated);

                    if (track == null)
                    {
                        track = NewTrack(detection);
                        _tracks.Add(track);
                    }
                    else
                    {
                        ApplyDetection(track, detection);
                    }

                    updated.Add(track);
                }

                foreach (var track in _tracks)
                {
                    if (!updated.Contains(track))
                        track.Misses++;
                }

                _tracks.RemoveAll(t => t.Misses > MaxMisses);

                return _tracks
                    .Where(t => t.Hits >= MinHits)
                    .Select(ToDetection)
                    .ToList();
            }
        }

        private Track? FindNearestTrack(Detection detection, HashSet<Track> alreadyUpdated)
        {
            Track? best = null;
            float bestDistance = MatchDistance;

            foreach (var track in _tracks)
            {
                if (alreadyUpdated.Contains(track)) continue;

                float dx = track.RectX - detection.RectX;
                float dy = track.RectY - detection.RectY;
                float distance = MathF.Sqrt(dx * dx + dy * dy);

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = track;
                }
            }

            return best;
        }

        private static Track NewTrack(Detection d) => new()
        {
            ClassName = d.ClassName,
            Matched = d.Matched,
            Score = d.ScorePercent,
            ShapeMode = d.ShapeMode,
            Circularity = d.Circularity,
            IsCircle = d.IsCircle,
            CircleX = d.CircleX,
            CircleY = d.CircleY,
            Radius = d.Radius,
            RectX = d.RectX,
            RectY = d.RectY,
            RectWidth = d.RectWidth,
            RectHeight = d.RectHeight,
            RectAngle = d.RectAngle,
            Hits = 1
        };

        private static void ApplyDetection(Track t, Detection d)
        {
            t.Hits++;
            t.Misses = 0;

            t.ClassName = d.ClassName;
            t.Matched = d.Matched;
            t.ShapeMode = d.ShapeMode;

            t.Score = Smooth(t.Score, d.ScorePercent, ScoreAlpha, 0);
            t.Circularity = t.Circularity + 0.25 * (d.Circularity - t.Circularity);

            t.CircleX = Smooth(t.CircleX, d.CircleX, PositionAlpha, PositionDeadband);
            t.CircleY = Smooth(t.CircleY, d.CircleY, PositionAlpha, PositionDeadband);
            t.Radius = Smooth(t.Radius, d.Radius, PositionAlpha, PositionDeadband);

            t.RectX = Smooth(t.RectX, d.RectX, PositionAlpha, PositionDeadband);
            t.RectY = Smooth(t.RectY, d.RectY, PositionAlpha, PositionDeadband);
            t.RectWidth = Smooth(t.RectWidth, d.RectWidth, PositionAlpha, PositionDeadband);
            t.RectHeight = Smooth(t.RectHeight, d.RectHeight, PositionAlpha, PositionDeadband);
            t.RectAngle = SmoothAngle(t.RectAngle, d.RectAngle);

            t.IsCircle = t.ShapeMode switch
            {
                ObjectShape.Circle => true,
                ObjectShape.Rectangle => false,
                _ => t.IsCircle ? t.Circularity > CircleLeave : t.Circularity > CircleEnter
            };
        }

        private static float Smooth(float current, float target, float alpha, float deadband)
        {
            if (Math.Abs(target - current) < deadband) return current;
            return current + alpha * (target - current);
        }

        // Açı 0-180 arasında döngüseldir (179° ile 1° birbirine 2° uzaklıktadır).
        private static float SmoothAngle(float current, float target)
        {
            float delta = ((target - current + 90) % 180 + 180) % 180 - 90;
            if (Math.Abs(delta) < AngleDeadband) return current;

            float result = current + PositionAlpha * delta;
            return ((result % 180) + 180) % 180;
        }

        private static Detection ToDetection(Track t) => new()
        {
            ClassName = t.ClassName,
            Matched = t.Matched,
            ScorePercent = t.Score,
            ShapeMode = t.ShapeMode,
            Circularity = t.Circularity,
            IsCircle = t.IsCircle,
            CircleX = t.CircleX,
            CircleY = t.CircleY,
            Radius = t.Radius,
            RectX = t.RectX,
            RectY = t.RectY,
            RectWidth = t.RectWidth,
            RectHeight = t.RectHeight,
            RectAngle = t.RectAngle
        };
    }
}
