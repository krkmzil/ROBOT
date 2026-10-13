namespace ROBOT.Services
{
    // Bir karede bulunan nesnenin ölçümleri ve çizilecek çerçevesi. Hem daire hem döndürülmüş
    // dikdörtgen ölçüleri tutulur; hangisinin çizileceğini IsCircle belirler.
    public class Detection
    {
        public string ClassName { get; init; } = "";
        public bool Matched { get; init; }
        public float ScorePercent { get; init; }

        // Öğretilirken seçilen şekil; Auto ise yuvarlaklık oranına bakılır.
        public ObjectShape ShapeMode { get; init; } = ObjectShape.Auto;
        public double Circularity { get; init; }
        public bool IsCircle { get; init; }

        // Nesneyi saran en küçük daire.
        public float CircleX { get; init; }
        public float CircleY { get; init; }
        public float Radius { get; init; }

        // Nesneyi saran en küçük döndürülmüş dikdörtgen: RectWidth uzun kenar, RectAngle uzun kenarın
        // yatayla açısı (0-180 derece).
        public float RectX { get; init; }
        public float RectY { get; init; }
        public float RectWidth { get; init; }
        public float RectHeight { get; init; }
        public float RectAngle { get; init; }

        public string Label => Matched ? $"{ClassName} %{ScorePercent:0}" : "";

        // Dikdörtgenin dört köşesi, sırayla bir çokgen çizilecek şekilde.
        public PointF[] GetRectCorners()
        {
            double radians = RectAngle * Math.PI / 180.0;
            float ux = (float)Math.Cos(radians) * RectWidth / 2;
            float uy = (float)Math.Sin(radians) * RectWidth / 2;
            float vx = -(float)Math.Sin(radians) * RectHeight / 2;
            float vy = (float)Math.Cos(radians) * RectHeight / 2;

            return new[]
            {
                new PointF(RectX - ux - vx, RectY - uy - vy),
                new PointF(RectX + ux - vx, RectY + uy - vy),
                new PointF(RectX + ux + vx, RectY + uy + vy),
                new PointF(RectX - ux + vx, RectY - uy + vy)
            };
        }
    }
}
