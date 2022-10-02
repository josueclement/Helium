using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTests.DependencyInjection
{
    public interface IDateTimeService
    {
        DateTime DateTime { get; }
    }

    public class DateTimeService : IDateTimeService
    {

        public DateTimeService()
        {
            DateTime = DateTime.Now;
        }

        public DateTime DateTime { get; private set; }
    }
}
