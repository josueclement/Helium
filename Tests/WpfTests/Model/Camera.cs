using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTests.Model
{
    public abstract class Camera
    {
        public abstract object GetImage();
    }

    public abstract class VisionCamera : Camera
    {
        protected static int Count { get; set; }

        public string ConnectionId { get; set; }
        public virtual CameraParameters CameraParameters { get; set; }

        public abstract void Connect();
    }

    public class SimulationCamera : VisionCamera
    {
        public SimulationCamera()
        {
            ConnectionId = $"SIMUCAM({Count++:6})";
            CameraParameters = new TelecentricCameraParameters
            {
                Width = 4096.0,
                Height = 2048.0,
                K = 1.0,
                S = 12.0,
                Mag = 1.0
            };
        }

        public override CameraParameters CameraParameters { get; set; }

        public override void Connect()
        { }

        public override object GetImage()
        {
            return null;
        }
    }

    public class DahengCamera : VisionCamera
    {
        public DahengCamera()
        {
            ConnectionId = $"DAHENG({Count++:6})";
            CameraParameters = new TelecentricCameraParameters
            {
                Width = 3446.0,
                Height = 1980.0,
                K = 3.9993340,
                S = 123887.0,
                Mag = 1.5
            };
        }

        public override void Connect()
        { }

        public override object GetImage()
        {
            return null;
        }
    }
}
