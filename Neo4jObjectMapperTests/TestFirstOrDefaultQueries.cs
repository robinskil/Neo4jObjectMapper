using Neo4jObjectMapper;
using Neo4jObjectMapper.Advanced;
using System;
using System.Collections.Generic;
using Xunit;

namespace NeoObjectMapperTests
{
    public class TestFirstOrDefaultQueries : TestEngine
    {
        [Fact]
        public async void TestSingleQuery()
        {
            var expected = new Country()
            {
                CountryID = "186",
                CountryName = "Russia"
            };
            INeoContext context = new NeoContext(Driver);
            Country result = await context.QueryDefault<Country>("MATCH(n:Country {countryName:'Russia'}) return n", new CacheConfig(true,TimeSpan.FromMinutes(20)));
            Country result2 = await context.QueryDefault<Country>("MATCH(n:Country {countryName:'Russia'}) return n", new CacheConfig(true,TimeSpan.FromMinutes(20)));
            Assert.NotNull(result);
            Assert.True(IsEqual(result, expected));
        }
        [Fact]
        public async void TestSingleQueryParameter()
        {
            var expected = new Country()
            {
                CountryID = "186",
                CountryName = "Russia"
            };
            var context = new NeoContext(Driver);
            var parameters = new Dictionary<string, object>();
            parameters.Add("Country", "Russia");
            var result = await context.QueryDefault<Country>("MATCH(n:Country {countryName:$Country}) return n", parameters, new CacheConfig(true, TimeSpan.FromMinutes(20),true));
            var result2 = await context.QueryDefault<Country>("MATCH(n:Country {countryName:$Country}) return n", parameters, new CacheConfig(true, TimeSpan.FromMinutes(20), true));
            Assert.NotNull(result);
            Assert.True(IsEqual(result, expected));
        }
        [Fact]
        public async void TestDefault()
        {
            var context = new NeoContext(Driver);
            var parameters = new Dictionary<string, object>();
            parameters.Add("Country", "NotExist");
            var result = await context.QueryDefault<Country>("MATCH(n:Country {countryName:$Country}) return n", parameters);
            Assert.Null(result);
        }

        [Fact]
        public async void TestQuerySingleInclude()
        {
            var expected = new Country()
            {
                CountryID = "186",
                CountryName = "Russia",
                Cities = new List<City>()
                {
                    new City()
                    {
                        CityId = "2654",
                        CityName = "Sochi"
                    },
                    new City()
                    {
                        CityId = "2675",
                        CityName = "Kazan"
                    },
                    new City()
                    {
                        CityId = "2657",
                        CityName = "Chelyabinsk"
                    }
                }
            };
            var countryHolder = new Dictionary<string, Country>();
            var parameters = new Dictionary<string, object>();
            parameters.Add("Country", "Russia");
            var context = new NeoContext(Driver);
            var result = await context.QueryDefaultIncludeable<Country, City>("match(n:Country {countryName:$Country})<-[e:EXISTS_IN]-(c:City) return n,c", parameters,
                (country, city) =>
                {
                    if (!countryHolder.ContainsKey(country.CountryID))
                    {
                        countryHolder.Add(country.CountryID, country);
                        country.Cities = new List<City>();
                    }
                    countryHolder[country.CountryID].Cities.Add(city);
                    return countryHolder[country.CountryID];
                }
            );
            Assert.NotNull(result);
            Assert.True(IsEqual(result, expected));
        }

        [Fact]
        public async void TestQueryDoubleInclude()
        {
            var expected = new Country()
            {
                CountryID = "216",
                CountryName = "United States",
                Cities = new List<City>()
                {
                    new City()
                    {
                        CityId = "3308",
                        CityName = "Chicago",
                        Airports = new List<Airport>()
                        {
                            new Airport()
                            {
                                AirportId = "3830",
                                AirportName = "Chicago O'Hare International Airport"
                            },
                            new Airport()
                            {
                                AirportId = "3747",
                                AirportName = "Chicago Midway International Airport"
                            }
                        }
                    },
                }
            };
            var countryHolder = new Dictionary<string, Country>();
            var cityHolder = new Dictionary<string, City>();
            var parameters = new Dictionary<string, object>
            {
                { "Country", "United States" },
                { "City", "Chicago" }
            };
            var context = new NeoContext(Driver);
            var result = await context.QueryDefaultIncludeable<Country, City, Airport>("match(n:Country {countryName:$Country})<-[e:EXISTS_IN]-(c:City {cityName:$City})<-[:IS_IN]-(a:Airport) return n,c,a", parameters,
                (country, city, airport) =>
                {
                    if (!countryHolder.ContainsKey(country.CountryID))
                    {
                        countryHolder.Add(country.CountryID, country);
                        country.Cities = new List<City>();
                    }
                    if (!cityHolder.ContainsKey(city.CityId))
                    {
                        cityHolder.Add(city.CityId, city);
                        countryHolder[country.CountryID].Cities.Add(city);
                        city.Airports = new List<Airport>();
                    }
                    cityHolder[city.CityId].Airports.Add(airport);
                    return countryHolder[country.CountryID];
                }
            );
            Assert.NotNull(result);
            Assert.True(IsEqual(result, expected));
        }

        [Fact]
        public async void TestQueryTripleInclude()
        {
            var expected = new Country()
            {
                CountryID = "216",
                CountryName = "United States",
                Cities = new List<City>()
                {
                    new City()
                    {
                        CityId = "3308",
                        CityName = "Chicago",
                        Airports = new List<Airport>()
                        {
                            new Airport()
                            {
                                AirportId = "3830",
                                AirportName = "Chicago O'Hare International Airport",
                                IncomingFlights = new List<Flight>()
                                {
                                    new Flight()
                                    {
                                        FlightId = "8879"
                                    },
                                    new Flight()
                                    {
                                        FlightId = "9999"
                                    }
                                }
                            },
                            new Airport()
                            {
                                AirportId = "3747",
                                AirportName = "Chicago Midway International Airport",
                                IncomingFlights = new List<Flight>()
                                {
                                    new Flight()
                                    {
                                        FlightId = "8858"
                                    }
                                }
                            }
                        }
                    },
                }
            };
            var countryHolder = new Dictionary<string, Country>();
            var cityHolder = new Dictionary<string, City>();
            var airportHolder = new Dictionary<string, Airport>();
            var parameters = new Dictionary<string, object>
            {
                { "Country", "United States" },
                { "City", "Chicago" }
            };
            var context = new NeoContext(Driver);
            var result = await context.QueryDefaultIncludeable<Country, City, Airport, Flight>("match(n:Country {countryName:$Country})<-[e:EXISTS_IN]-(c:City {cityName:$City})<-[:IS_IN]-(a:Airport)<-[:DESTINATION]-(f:Flight) return n,c,a,f", parameters,
                (country, city, airport, incFlight) =>
                {
                    if (!countryHolder.ContainsKey(country.CountryID))
                    {
                        countryHolder.Add(country.CountryID, country);
                        country.Cities = new List<City>();
                    }
                    if (!cityHolder.ContainsKey(city.CityId))
                    {
                        cityHolder.Add(city.CityId, city);
                        countryHolder[country.CountryID].Cities.Add(city);
                        city.Airports = new List<Airport>();
                    }
                    if (!airportHolder.ContainsKey(airport.AirportId))
                    {
                        airportHolder.Add(airport.AirportId, airport);
                        cityHolder[city.CityId].Airports.Add(airport);
                        airport.IncomingFlights = new List<Flight>();
                        airport.OutgoingFlights = new List<Flight>();
                    }
                    airportHolder[airport.AirportId].IncomingFlights.Add(incFlight);
                    return countryHolder[country.CountryID];
                }
            );
            Assert.NotNull(result);
            Assert.True(IsEqual(result, expected));
        }

        [Fact]
        public async void TestQueryQuadrupleInclude()
        {
            var expected = new Country()
            {
                CountryID = "216",
                CountryName = "United States",
                Cities = new List<City>()
                {
                    new City()
                    {
                        CityId = "3308",
                        CityName = "Chicago",
                        Airports = new List<Airport>()
                        {
                            new Airport()
                            {
                                AirportId = "3830",
                                AirportName = "Chicago O'Hare International Airport",
                                IncomingFlights = new List<Flight>()
                                {
                                    new Flight()
                                    {
                                        FlightId = "8879",
                                        Airline = new Airline()
                                        {
                                            AirlineId = "137"
                                        }
                                    },
                                    new Flight()
                                    {
                                        FlightId = "9999",
                                        Airline = new Airline()
                                        {
                                            AirlineId = "137"
                                        }
                                    }
                                }
                            },
                            new Airport()
                            {
                                AirportId = "3747",
                                AirportName = "Chicago Midway International Airport",
                                IncomingFlights = new List<Flight>()
                                {
                                    new Flight()
                                    {
                                        FlightId = "8858",
                                        Airline = new Airline()
                                        {
                                            AirlineId = "137"
                                        }
                                    }
                                }
                            }
                        }
                    },
                }
            };
            var countryHolder = new Dictionary<string, Country>();
            var cityHolder = new Dictionary<string, City>();
            var airportHolder = new Dictionary<string, Airport>();
            var flightHolder = new Dictionary<string, Flight>();
            var parameters = new Dictionary<string, object>
            {
                { "Country", "United States" },
                { "City", "Chicago" }
            };
            var context = new NeoContext(Driver);
            var result = await context.QueryDefaultIncludeable<Country, City, Airport, Flight, Airline>("match(n:Country {countryName:$Country})<-[e:EXISTS_IN]-(c:City {cityName:$City})<-[:IS_IN]-(a:Airport)<-[:DESTINATION]-(f:Flight)-[:OF]->(al:Airline) return n,c,a,f,al", parameters,
                (country, city, airport, incFlight, airline) =>
                {
                    if (!countryHolder.ContainsKey(country.CountryID))
                    {
                        countryHolder.Add(country.CountryID, country);
                        country.Cities = new List<City>();
                    }
                    if (!cityHolder.ContainsKey(city.CityId))
                    {
                        cityHolder.Add(city.CityId, city);
                        countryHolder[country.CountryID].Cities.Add(city);
                        city.Airports = new List<Airport>();
                    }
                    if (!airportHolder.ContainsKey(airport.AirportId))
                    {
                        airportHolder.Add(airport.AirportId, airport);
                        cityHolder[city.CityId].Airports.Add(airport);
                        airport.IncomingFlights = new List<Flight>();
                    }
                    if (!flightHolder.ContainsKey(incFlight.FlightId))
                    {
                        flightHolder.Add(incFlight.FlightId, incFlight);
                        airportHolder[airport.AirportId].IncomingFlights.Add(incFlight);
                        incFlight.Airline = airline;
                    }
                    return countryHolder[country.CountryID];
                }
            );
            Assert.NotNull(result);
            Assert.True(IsEqual(result, expected));
        }
    }
}
