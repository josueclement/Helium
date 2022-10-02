using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTests.Model
{
    public abstract class Sensor
    {
        public Sensor()
        {
            SensorId = Guid.NewGuid().ToString();
        }

        public abstract string SensorName { get; }
        public string SensorId { get; set; }
    }

    public class GrabImageSensor : Sensor
    {
        public override string SensorName => "I'm the GrabImage";
    }

    public class ZffSensor : Sensor
    {
        public override string SensorName => "I'm the Zff";
    }
}
