namespace NeoObjectMapperTests
{
    public class Airline
    {
        public string AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string CountryId { get; set; }
        public Country CountryOfAirline { get; set; }
    }
}
