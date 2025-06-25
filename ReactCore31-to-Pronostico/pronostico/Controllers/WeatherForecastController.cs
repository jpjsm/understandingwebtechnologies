using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;

namespace pronostico.Controllers
{
    [ApiController]
    [Route("/")]
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

        [HttpGet("weatherforecast")]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            UriBuilder pronosticoBackendUriB = new UriBuilder(Program.EnvironmentVariables["pronosticoBackend_BaseAddress"]);
            pronosticoBackendUriB.Path = "weatherforecast";

            string json = string.Empty;
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(pronosticoBackendUriB.Uri);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Status: {response.StatusCode} code: {(int)response.StatusCode}");
                }
                json = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrWhiteSpace(json))
                {
                    throw new ApplicationException("No content in response from backend.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}\nHResult: {ex.HResult}");
            }

            List<WeatherForecast> weatherForecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(json);
            return weatherForecast;
        }

        [HttpGet("hello")]
        public  string GetHello()
        {
            return "Hola Mundo!!";
        }
    }
}
