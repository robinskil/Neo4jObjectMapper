using System.Collections.Generic;

namespace NeoObjectMapperTests
{
    public class City
    {
        public string CityId { get; set; }
        public string CountryId { get; set; }
        public string CityName { get; set; }
        public Country Country { get; set; }
        public ICollection<Airport> Airports { get; set; }
    }
}
