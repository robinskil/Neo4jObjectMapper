using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoObjectMapperTests
{
    public abstract class TestEngine
    {
        protected IDriver Driver { get; set; } = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
        protected bool FlightIsEqual(Flight a, Flight b)
        {
            if (a.FlightId == b.FlightId)
            {
                if (a.Airline == null && b.Airline == null)
                {
                    return true;
                }
                else
                {
                    return AirlineIsEqual(a.Airline, b.Airline);
                }
            }
            return false;
        }

        protected bool AirlineIsEqual(Airline a, Airline b)
        {
            return a.AirlineId == b.AirlineId;
        }

        protected bool AirportIsEqual(Airport a, Airport b)
        {
            if (a.AirportId == b.AirportId && a.AirportName == b.AirportName)
            {
                if (a.IncomingFlights == null && a.OutgoingFlights == null && b.IncomingFlights == null && b.OutgoingFlights == null)
                {
                    return true;
                }
                else
                {
                    bool result = true;
                    if (a.IncomingFlights != null && b.IncomingFlights != null)
                    {
                        foreach (var aIncFlight in a.IncomingFlights)
                        {
                            if (!(b.IncomingFlights.Any(bIncFlight => FlightIsEqual(aIncFlight, bIncFlight))))
                            {
                                result = false;
                            }
                        }
                    }
                    if (a.OutgoingFlights != null && b.OutgoingFlights != null)
                    {
                        foreach (var aOutFlight in a.OutgoingFlights)
                        {
                            if (!(b.OutgoingFlights.Any(bOutFlight => FlightIsEqual(aOutFlight, bOutFlight))))
                            {
                                result = false;
                            }
                        }
                    }
                    return result;
                }
            }
            return false;
        }

        protected bool CityIsEqual(City a, City b)
        {
            if (a.CityName == b.CityName && a.CityId == b.CityId)
            {
                if (a.Airports == null && b.Airports == null)
                {
                    return true;
                }
                else
                {
                    foreach (var aAirport in a.Airports)
                    {
                        if (!(b.Airports.Any(bAirport => AirportIsEqual(aAirport, bAirport))))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        protected bool IsEqual(Country a, Country b)
        {
            if (a.CountryID == b.CountryID && a.CountryName == b.CountryName)
            {
                if (a.Cities == null && b.Cities == null)
                {
                    return true;
                }
                else
                {
                    foreach (var cityA in a.Cities)
                    {
                        if (!(b.Cities.Any(cityB => CityIsEqual(cityA, cityB))))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
