using System;
using System.IO;
using System.Threading;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using SeleniumContinousAutomation.Services.Driver;
using SeleniumContinousAutomation.Services.Navigation;
using SeleniumContinousAutomation.Services.RestApi;

namespace SeleniumContinousAutomation
{
    public class Program
    {
        #region Variables
        public static IConfigurationRoot _configuration;
        private static System.Timers.Timer _timer;
        private static NavigationService _navigation;

        #endregion

        private static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Information()
                   .WriteTo.Logger(l => l
                       .Filter.ByIncludingOnly(e => e
                       .Level == LogEventLevel.Information)
                       .WriteTo.Console()
                       .WriteTo.File($"../automationlogs/{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}.log"))
                   .WriteTo.Logger(l => l
                       .Filter.ByIncludingOnly(e => e
                       .Level == LogEventLevel.Error)
                       .WriteTo.File($"../errorlogs/{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}.log"))
                   .CreateLogger();


            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _navigation = serviceProvider.GetService<NavigationService>();

            _configuration = builder.Build();

            ConfiguraTimer();

            Thread.Sleep(Timeout.Infinite);

        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog())
                    .AddScoped<DriverMethods>()
                    .AddScoped<RestApiService>()
                    .AddTransient<NavigationService>();
        }
        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }

        #region timer

        public static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Stop();

                var automationSelector = int.Parse(_configuration["NavigationSelector"]);

                switch (automationSelector)
                {
                    case 1:
                        _navigation.Navigation1();
                        _timer.Start();
                        break;
                    case 2:
                        _navigation.Navigation2();
                        _timer.Start();
                        break;
                }
            }
            catch
            {
                _timer.Start();
            }
        }

        #endregion

        #region 

        public static void ConfiguraTimer()
        {
            _timer = new System.Timers.Timer(500);
            _timer.Elapsed += timer_Elapsed;
            _timer.Start();
        }

        #endregion



    }
}

