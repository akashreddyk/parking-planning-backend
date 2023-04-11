namespace AircraftParkingPlanning.Model
{
  public class ParkingSpot
  {
    public string Name { get; set; }
    public double FootprintSqm { get; set; }
    public Flight Flight { get; set; }
    }
}
