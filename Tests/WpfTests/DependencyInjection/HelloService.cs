using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTests.DependencyInjection
{
    public interface IHelloService
    {
        string SayHello();
    }

    public class HelloService : IHelloService
    {
        private ISettingsService _settingsService;
        private IDateTimeService _dateTimeService;

        public HelloService(ISettingsService settingsService, IDateTimeService dateTimeService)
        {
            _settingsService = settingsService;
            _dateTimeService = dateTimeService;
        }

        public string SayHello()
        {
            return $"Hello, this is your value: {_settingsService.GetValue()}. DateTime: {_dateTimeService.DateTime}";
        }
    }
}
