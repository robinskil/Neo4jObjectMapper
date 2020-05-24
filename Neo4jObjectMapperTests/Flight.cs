namespace NeoObjectMapperTests
{
    public class Flight
    {
        public string FlightId { get; set; }
        public string SourceAirportId { get; set; }
        public string DestinationAirportId { get; set; }
        public string AirlineId { get; set; }
        public Airport SourceAirport { get; set; }
        public Airport DestinationAirport { get; set; }
        public Airline Airline { get; set; }
    }
}
