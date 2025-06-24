using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webapi.Controllers
{
    [ApiController]
    [Route("weatherforecast")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} GET.");
            string cnxstr = Startup.Configuration["Database:fcm-test03-cnx"];
            string forecast_qry = "SELECT TOP 5[Date],[TemperatureC],[TemperatureF],[Summary] FROM[dbo].[WeatherForecast] WHERE[Date] >= @Date ORDER BY[Date] ASC";

            List<WeatherForecast> forecasts = new List<WeatherForecast>();

            using (SqlConnection cnx = new SqlConnection(cnxstr))
            {
                try
                {
                    _logger.LogInformation($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} Opening DB connection.");
                    cnx.Open();

                    SqlCommand forecast_cmd = new SqlCommand(forecast_qry, cnx);
                    forecast_cmd.Parameters.Add("@Date", SqlDbType.DateTime2);
                    forecast_cmd.Parameters["@Date"].SqlValue = DateTime.UtcNow;

                    _logger.LogInformation($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} About to execute reader.");
                    SqlDataReader forecast_reader = forecast_cmd.ExecuteReader();

                    if (forecast_reader.HasRows)
                    {
                        _logger.LogInformation($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} Query returned with rows.");
                        while (forecast_reader.Read())
                        {
                            DateTime date = forecast_reader.GetDateTime(0);
                            int celsius = forecast_reader.GetInt32(1);
                            string summary = forecast_reader.GetString(3);
                            WeatherForecast f = new WeatherForecast() { Date = date, TemperatureC = celsius, Summary = summary };
                            forecasts.Add(f);
                            _logger.LogInformation($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")}Data: {f}");
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} Query returned with NO rows.");
                    }

                    forecast_reader.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} {ex.Message}.");
                    _logger.LogError($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} {ex.StackTrace}.");
                    _logger.LogError($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} {ex.Source}.");

                    var inner = ex.InnerException;
                    while (inner != null)
                    {
                        _logger.LogError($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} {new string('=',31)} Inner  Exception {new string('=', 31)}");
                        _logger.LogError($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} {inner.Message}.");
                        _logger.LogError($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} {inner.StackTrace}.");
                        _logger.LogError($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} {inner.Source}.");

                        inner = inner.InnerException;
                    }
                    throw;
                }

            }

            _logger.LogInformation($"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff")} Done!");

            return forecasts;
        }
    }
}
