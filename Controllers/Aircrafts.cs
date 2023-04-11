using AircraftParkingPlanning.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AircraftParkingPlanning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AircraftsController : ControllerBase
    {
        private readonly ILogger<AircraftsController> _logger;
        private readonly Setup setup;

        public AircraftsController(ILogger<AircraftsController> logger, Setup s)
        {
            setup = s;
            _logger = logger;
        }

        [HttpGet(Name = "GetAircrafts")]
        public List<Aircraft> Get()
        {
            return setup.AircraftList;
        }
    }
}

