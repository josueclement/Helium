using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTests.Model
{
    public abstract class SystemApp
    {
        public string Name { get; set; }
    }

    public class MyAppSystem : SystemApp
    {
        public MyAppSystem()
        {
            Name = "My app";
            Camera1 = new SimulationCamera();
            Camera2 = new DahengCamera();

            Sensors = new List<Sensor>()
            {
                new GrabImageSensor(),
                new ZffSensor(),
                new GrabImageSensor()
            };
        }

        public string AppPath { get; set; } = "C:\\MyApp";
        public string AppDataPath { get; set; } = "C:\\ProgramData\\MyApp";
        public int DecimalNumbers { get; } = 3;
        public double TheValue { get; set; } = 12.0;

        public Camera Camera1 { get; set; }
        public Camera Camera2 { get; set; }

        public IEnumerable<Sensor> Sensors { get; private set; }
    }
}
