using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItemService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly WeatherForecast[] _wf = new WeatherForecast[]
        {
            new WeatherForecast(){ link=1, name = "Prueba1", summary = "Resumen Prueba 1", year = 2001, country="US", price=1000, description="Descripcion 1"},
            new WeatherForecast(){ link=2, name = "Prueba2", summary = "Resumen Prueba 2", year = 2002, country="UK", price=2000, description="Descripcion 2"},
            new WeatherForecast(){ link=3, name = "Prueba3", summary = "Resumen Prueba 3", year = 2003, country="ES", price=3000, description="Descripcion 3"},
            new WeatherForecast(){ link=4, name = "Prueba4", summary = "Resumen Prueba 4", year = 2004, country="FR", price=4000, description="Descripcion 4"},
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _wf;
        }
    }
}
