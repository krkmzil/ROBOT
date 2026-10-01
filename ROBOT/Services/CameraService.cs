using System.Runtime.InteropServices;
using AForge.Video;
using AForge.Video.DirectShow;

namespace ROBOT.Services
{
    public enum CameraProperty
    {
        Brightness = 0,
        Contrast = 1,
        Hue = 2,
        Saturation = 3,
        Sharpness = 4,
        Gamma = 5,
        ColorEnable = 6,
        WhiteBalance = 7,
        BacklightCompensation = 8,
        Gain = 9
    }

    [Flags]
    internal enum CameraPropertyFlags
    {
        Auto = 0x0001,
        Manual = 0x0002
    }

    // Windows'un standart DirectShow arayüzü — AForge bunu içermiyor, kendimiz tanımlıyoruz.
    [ComImport]
    [Guid("C6E13360-30AC-11D0-A18C-00A0C9118956")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAMVideoProcAmp
    {
        [PreserveSig]
        int GetRange(CameraProperty property, out int min, out int max, out int stepSize, out int defaultValue, out CameraPropertyFlags flags);

        [PreserveSig]
        int Set(CameraProperty property, int value, CameraPropertyFlags flags);

        [PreserveSig]
        int Get(CameraProperty property, out int value, out CameraPropertyFlags flags);
    }

    public class CameraService
    {
        private VideoCaptureDevice? _device;
        private IAMVideoProcAmp? _procAmp;

        public bool IsConnected => _device != null && _device.IsRunning;

        // Yeni bir kare geldiğinde tetiklenir (arka plan thread'inden gelir!)
        public event Action<Bitmap>? FrameReceived;

        public string[] GetAvailableCameras()
        {
            var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            var names = new string[devices.Count];
            for (int i = 0; i < devices.Count; i++)
                names[i] = devices[i].Name;
            return names;
        }

        public bool Connect(string deviceName)
        {
            if (IsConnected) return true;

            var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FilterInfo? match = null;
            foreach (FilterInfo info in devices)
            {
                if (info.Name == deviceName)
                {
                    match = info;
                    break;
                }
            }

            if (match == null) return false;

            try
            {
                _device = new VideoCaptureDevice(match.MonikerString);
                _device.NewFrame += Device_NewFrame;
                _device.Start();

                // Parlaklık/Karşıtlık/Keskinlik/Doygunluk kontrolü için ayrı, hafif bir COM bağlantısı.
                try
                {
                    object filterObj = Marshal.BindToMoniker(match.MonikerString);
                    _procAmp = filterObj as IAMVideoProcAmp;
                }
                catch
                {
                    _procAmp = null;
                }

                return true;
            }
            catch
            {
                _device = null;
                return false;
            }
        }

        public void Disconnect()
        {
            if (_procAmp != null)
            {
                Marshal.ReleaseComObject(_procAmp);
                _procAmp = null;
            }

            if (_device == null) return;

            try
            {
                _device.NewFrame -= Device_NewFrame;
                if (_device.IsRunning)
                {
                    _device.SignalToStop();
                    _device.WaitForStop();
                }
            }
            catch { }
            finally
            {
                _device = null;
            }
        }

        private void Device_NewFrame(object sender, NewFrameEventArgs e)
        {
            var frameCopy = (Bitmap)e.Frame.Clone();
            FrameReceived?.Invoke(frameCopy);
        }

        public bool TryGetPropertyRange(CameraProperty property, out int min, out int max, out int defaultValue)
        {
            min = 0;
            max = 0;
            defaultValue = 0;

            if (_procAmp == null) return false;

            try
            {
                int hr = _procAmp.GetRange(property, out min, out max, out _, out defaultValue, out _);
                return hr == 0;
            }
            catch
            {
                return false;
            }
        }

        public int GetProperty(CameraProperty property)
        {
            if (_procAmp == null) return 0;

            try
            {
                _procAmp.Get(property, out int value, out _);
                return value;
            }
            catch
            {
                return 0;
            }
        }

        public void SetProperty(CameraProperty property, int value)
        {
            if (_procAmp == null) return;

            try
            {
                _procAmp.Set(property, value, CameraPropertyFlags.Manual);
            }
            catch { }
        }
    }
}
