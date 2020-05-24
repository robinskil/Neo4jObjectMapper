using System.Collections.Generic;

namespace NeoObjectMapperTests
{
    public class Airport
    {
        public string AirportId { get; set; }
        public string AirportName { get; set; }
        public string CityId { get; set; }
        public City City { get; set; }
        public ICollection<Flight> OutgoingFlights { get; set; }
        public ICollection<Flight> IncomingFlights { get; set; }
    }
}
