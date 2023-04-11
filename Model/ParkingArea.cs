namespace AircraftParkingPlanning.Model
{
  public class ParkingArea
  {
    public string Name { get; set; }
    public List<ParkingSpot> ParkingSpots { get; set; }

    public double TotalSurfaceSqm => ParkingSpots.Select(s => s.FootprintSqm).Sum();
  }
}
