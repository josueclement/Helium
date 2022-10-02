using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTests.Model
{
    public abstract class CameraParameters
    {
        public double Mag { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class TelecentricCameraParameters : CameraParameters
    {
        public double K { get; set; }
        public double S { get; set; }
    }
}
