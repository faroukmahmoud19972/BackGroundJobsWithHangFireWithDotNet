using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BackGroundJobsWithHangFireWithDotNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //BackgroundJob.Enqueue(()=>SendMsg("Farouk@outlook.com"));

            //Console.WriteLine(DateTime.Now);
            //BackgroundJob.Schedule(() => SendMsg("Farouk@outlook.com"), TimeSpan.FromMinutes(1));

            RecurringJob.AddOrUpdate(() => SendMsg("Farouk@outlook.com"), Cron.Minutely);


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [ApiExplorerSettings(IgnoreApi =true)]
        public void SendMsg(string email)
        {
            Console.WriteLine($"Email Sent to {email} at {DateTime.Now}");
        }
    }
}
