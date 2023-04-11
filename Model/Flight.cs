namespace AircraftParkingPlanning.Model
{
  public class Flight
  {
    public Aircraft Aircraft { get; set; }
    public ParkingSpot ParkingSpot { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
  }
}
