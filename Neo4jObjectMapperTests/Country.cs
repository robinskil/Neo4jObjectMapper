using System;
using System.Collections.Generic;
using System.Text;

namespace NeoObjectMapperTests
{
    public class Country
    {
        public string CountryID { get; set; }
        public string CountryName { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
