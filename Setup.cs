using AircraftParkingPlanning.Model;
using System.Security.Cryptography.X509Certificates;

namespace AircraftParkingPlanning
{
  public class Setup
  {
    public List<Flight> Flights { get; set; }
    public List<Aircraft> AircraftList { get; set; }

    public List<ParkingArea> ParkingAreas { get; set; }

    public Setup()
    {
      CreateAirport();
      CreateAircaftList();
      CreateFlights();
    }

    private void CreateFlights()
    {
      Flights = new List<Flight>();
      Flights.Add(new Flight
      {
        Aircraft = Aircraft("PHNXT"),
        StartDateTime = DateTime.Parse("1 Jan 2023 12:00"),
        EndDateTime = DateTime.Parse("2 Jan 2023 08:00"),
        ParkingSpot = ParkingSpot("N1")
      });
      Flights.Add(new Flight
      {
        Aircraft = Aircraft("9HLTT"),
        StartDateTime = DateTime.Parse("1 Jan 2023 10:00"),
        EndDateTime = DateTime.Parse("3 Jan 2023 12:00"),
        ParkingSpot = ParkingSpot("N2")
      });
      Flights.Add(new Flight
      {
        Aircraft = Aircraft("YUPRJ"),
        StartDateTime = DateTime.Parse("2 Jan 2023 08:30"),
        EndDateTime = DateTime.Parse("3 Jan 2023 12:00"),
        ParkingSpot = ParkingSpot("N1")
      });
      Flights.Add(new Flight
      {
        Aircraft = Aircraft("N123T"),
        StartDateTime = DateTime.Parse("1 Jan 2023 14:30"),
        EndDateTime = DateTime.Parse("1 Jan 2023 20:00"),
        ParkingSpot = ParkingSpot("S1")
      });
      Flights.Add(new Flight
      {
        Aircraft = Aircraft("NCDFT"),
        StartDateTime = DateTime.Parse("1 Jan 2023 09:30"),
        EndDateTime = DateTime.Parse("4 Jan 2023 09:00"),
        ParkingSpot = ParkingSpot("S2")
      });
      Flights.Add(new Flight
      {
        Aircraft = Aircraft("PHNXT"),
        StartDateTime = DateTime.Parse("3 Jan 2023 13:00"),
        EndDateTime = DateTime.Parse("4 Jan 2023 15:00"),
        ParkingSpot = ParkingSpot("N1")
      });
      Flights.Add(new Flight
      {
        Aircraft = Aircraft("ERZ2"),
        StartDateTime = DateTime.Parse("2 Jan 2023 09:30"),
        EndDateTime = DateTime.Parse("3 Jan 2023 09:00"),
        ParkingSpot = ParkingSpot("S3")
      });
    }

    private void CreateAirport()
    {
      ParkingAreas = new List<ParkingArea>
      {
        new ParkingArea
        {
          Name = "North",
          ParkingSpots = new List<ParkingSpot>
          {
            new ParkingSpot { Name = "N1", FootprintSqm = 700 },
            new ParkingSpot { Name = "N2", FootprintSqm = 700 },
            new ParkingSpot { Name = "N3", FootprintSqm = 1400 },
            new ParkingSpot { Name = "N4", FootprintSqm = 1000 },
            new ParkingSpot { Name = "N5", FootprintSqm = 1000 },
            new ParkingSpot { Name = "N6", FootprintSqm = 1500 },
          }
        },
        new ParkingArea
        {
          Name = "South",
          ParkingSpots = new List<ParkingSpot>
          {
            new ParkingSpot { Name = "S1", FootprintSqm = 700 },
            new ParkingSpot { Name = "S2", FootprintSqm = 700 },
            new ParkingSpot { Name = "S3", FootprintSqm = 1400 },
            new ParkingSpot { Name = "S4", FootprintSqm = 1000 },
            new ParkingSpot { Name = "S5", FootprintSqm = 1000 },
            new ParkingSpot { Name = "S6", FootprintSqm = 1500 },
            new ParkingSpot { Name = "S7", FootprintSqm = 4500 },
          }
        },
      };
    }

    private ParkingSpot ParkingSpot(string name)
    {
      return ParkingAreas.SelectMany(a => a.ParkingSpots).Where(a => a.Name == name).Single();
    }

    private void CreateAircaftList()
    {
      AircraftList = new List<Aircraft>
      {
        new Aircraft {RegistrationCode = "PHNXT", FootprintSqm = 350 },
        new Aircraft {RegistrationCode = "9HLTT", FootprintSqm = 600 },
        new Aircraft {RegistrationCode = "YUPRJ", FootprintSqm = 420 },
        new Aircraft {RegistrationCode = "N123T", FootprintSqm = 550},
        new Aircraft {RegistrationCode = "NCDFT", FootprintSqm = 780 },
        new Aircraft {RegistrationCode = "TTPB", FootprintSqm = 490 },
        new Aircraft {RegistrationCode = "ZZZZ", FootprintSqm = 1000 },
        new Aircraft {RegistrationCode = "ERZ1", FootprintSqm = 1000 },
        new Aircraft {RegistrationCode = "ERZ2", FootprintSqm = 3000 },
      };
    }

    private Aircraft Aircraft(string registrationCode)
    {
      return AircraftList.Single(a => a.RegistrationCode == registrationCode);
    }
        public double GetAvailableSpace()
        {
            double totalOccupiedSqm = ParkingAreas.SelectMany(a => a.ParkingSpots).Where(s => s.Flight != null)
                                    .Select(s => s.FootprintSqm).Sum();
            double totalSurfaceSqm = ParkingAreas.SelectMany(a => a.ParkingSpots).Select(s => s.FootprintSqm).Sum();
            return totalSurfaceSqm - totalOccupiedSqm;
        }

        public List<ParkingSpot> GetAvailableSpots(DateTime dateTime)
        {
            List<ParkingSpot> availableSpots = ParkingAreas.SelectMany(a => a.ParkingSpots)
                                                .Where(s => s.Flight == null).ToList();
            return availableSpots;
        }

        public List<ParkingSpot> GetOccupiedSpots(DateTime dateTime)
        {
            List<ParkingSpot> occupiedSpots = ParkingAreas.SelectMany(a => a.ParkingSpots)
                                                .Where(s => s.Flight != null &&
                                                s.Flight.StartDateTime <= dateTime &&
                                                s.Flight.EndDateTime >= dateTime).ToList();
            return occupiedSpots;
        }

        public bool AssignParkingSpot(Flight flight, ParkingSpot spot)
        {
            if (flight.Aircraft.FootprintSqm > spot.FootprintSqm)
            {
                // Show warning if the aircraft footprint is larger than the parking spot
                return false;
            }

            if (spot.Flight != null)
            {
                // Show warning if the parking spot is already occupied
                return false;
            }

            spot.Flight = flight;
            flight.ParkingSpot = spot;
            return true;
        }
    }
}

