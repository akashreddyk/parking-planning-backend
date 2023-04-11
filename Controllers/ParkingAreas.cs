using AircraftParkingPlanning.Model;
using Microsoft.AspNetCore.Mvc;

namespace AircraftParkingPlanning.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ParkingAreasController : ControllerBase
  {    

    private readonly ILogger<ParkingAreasController> _logger;
    private readonly Setup setup;

    public ParkingAreasController(ILogger<ParkingAreasController> logger, Setup s)
    {
      setup = s;
      _logger = logger;
    }

    [HttpGet(Name = "GetParkingAreas")]
    public List<ParkingArea> Get()
    {
      return setup.ParkingAreas;
    }

    [HttpGet("total-available-space", Name = "GetTotalAvailableSpace")]
    public double TotalAvailableSpace()
    {
        return setup.GetAvailableSpace();
    }

  }
}