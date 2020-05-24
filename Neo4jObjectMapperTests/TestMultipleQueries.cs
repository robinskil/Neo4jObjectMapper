using Neo4j.Driver;
using Neo4jObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NeoObjectMapperTests
{
    public class TestMultipleQueries : TestEngine
    {
        [Fact]
        public async void TestMultipleQuery()
        {
            var expectedDic = new Dictionary<string, Country>()
            {
                {"186" , new Country(){ CountryID = "186" , CountryName = "Russia" } },
                {"216" , new Country(){ CountryID = "216" , CountryName = "United States" } }
            };
            var context = new NeoContext(Driver);
            var result = await context.QueryMultiple<Country>("match(n:Country) WHERE n.countryID IN ['186','216'] return n");
            Assert.NotNull(result);
            Assert.True(result.Count() == expectedDic.Count);
            foreach (var country in result)
            {
                Assert.True(IsEqual(country, expectedDic[country.CountryID]));
            }
        }

        [Fact]
        public async void TestMultipleQueryInclude()
        {
            var expectedDic = new Dictionary<string, Country>()
            {
                {"186" , new Country(){
                    CountryID = "186" ,
                    CountryName = "Russia" ,
                    Cities = new List<City>() {
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
                    }
                },
                {"15" , new Country(){
                    CountryID = "15" ,
                    CountryName = "Germany",
                    Cities = new List<City>()
                        {
                            new City()
                            {
                                CityId = "325",
                                CityName = "Berlin"
                            },
                        }
                    }
                }
            };
            var parameters = new Dictionary<string, object>
            {
                { "rsID", "186" },
                { "geID", "15" }
            };
            var countryHolder = new Dictionary<string, Country>();
            var context = new NeoContext(Driver);
            var result = await context.QueryMultipleIncludeable<Country, City>("match(n:Country)<-[:EXISTS_IN]-(c:City) WHERE n.countryID IN [$geID,$rsID] return n,c", parameters,
                (country, city) =>
                {
                    if (!countryHolder.ContainsKey(country.CountryID))
                    {
                        countryHolder.Add(country.CountryID, country);
                        country.Cities = new List<City>();
                    }
                    countryHolder[country.CountryID].Cities.Add(city);
                    return countryHolder.Values;
                }
            );
            Assert.NotNull(result);
            Assert.True(result.Count() == expectedDic.Count);
            foreach (var country in result)
            {
                Assert.True(IsEqual(country, expectedDic[country.CountryID]));
            }
        }

        [Fact]
        public async void TestGetRecords()
        {
            var expectedDic = new Dictionary<string, Country>()
            {
                {"186" , new Country(){ CountryID = "186" , CountryName = "Russia" } },
                {"216" , new Country(){ CountryID = "216" , CountryName = "United States" } }
            };
            var resultDic = new Dictionary<string, Country>();
            var context = new NeoContext(Driver);
            var result = await context.GetRecordsAsync("match(n:Country) WHERE n.countryID IN ['186','216'] return n");
            Assert.NotNull(result);
            foreach (var countryResult in result)
            {
                var nodeVal = countryResult.Values.First();
                var node = nodeVal.Value as INode;
                var country = context.ConvertNodeToT<Country>(node);
                resultDic.Add(country.CountryID, country);
            }
            foreach (var country in expectedDic.Values)
            {
                Assert.True(IsEqual(country, resultDic[country.CountryID]));
            }
        }
        [Fact]
        public async void TestGetRecordsWithParameters()
        {
            var expectedDic = new Dictionary<string, Country>()
            {
                {"186" , new Country(){ CountryID = "186" , CountryName = "Russia" } },
                {"216" , new Country(){ CountryID = "216" , CountryName = "United States" } }
            };
            var resultDic = new Dictionary<string, Country>();
            var context = new NeoContext(Driver);
            var result = await context.GetRecordsAsync("match(n:Country) WHERE n.countryID IN [$p1,$p2] return n", new Dictionary<string, object>()
            {
                { "p1" , "186"},
                { "p2" , "216"}
            });
            Assert.NotNull(result);
            foreach (var countryResult in result)
            {
                var nodeVal = countryResult.Values.First();
                var node = nodeVal.Value as INode;
                var country = context.ConvertNodeToT<Country>(node);
                resultDic.Add(country.CountryID, country);
            }
            foreach (var country in expectedDic.Values)
            {
                Assert.True(IsEqual(country, resultDic[country.CountryID]));
            }
        }
        [Fact]
        public async void TestGetNOMRecordsDynamicWithParameters()
        {
            var expectedDic = new Dictionary<string, Country>()
            {
                {"186" , new Country(){ CountryID = "186" , CountryName = "Russia" } },
                {"216" , new Country(){ CountryID = "216" , CountryName = "United States" } }
            };
            var resultDic = new Dictionary<string, Country>();
            var context = new NeoContext(Driver);
            var result = await context.GetNeoDataRecordsAsync("match(n:Country) WHERE n.countryID IN [$p1,$p2] return n", new Dictionary<string, object>()
            {
                { "p1" , "186"},
                { "p2" , "216"}
            });
            Assert.NotNull(result);
            foreach (var neoRecord in result)
            {
                Country c = new Country()
                {
                    CountryID = neoRecord["n"].countryID,
                    CountryName = neoRecord["n"].countryName
                };
                resultDic.Add(c.CountryID, c);
            }
            foreach (var country in expectedDic.Values)
            {
                Assert.True(IsEqual(country, resultDic[country.CountryID]));
            }
        }
        [Fact]
        public async void TestGetNOMRecordsDynamic()
        {
            var expectedDic = new Dictionary<string, Country>()
            {
                {"186" , new Country(){ CountryID = "186" , CountryName = "Russia" } },
                {"216" , new Country(){ CountryID = "216" , CountryName = "United States" } }
            };
            var resultDic = new Dictionary<string, Country>();
            var context = new NeoContext(Driver);
            var result = await context.GetNeoDataRecordsAsync("match(n:Country) WHERE n.countryID IN ['186','216'] return n");
            Assert.NotNull(result);
            foreach (var neoRecord in result)
            {
                Country c = new Country()
                {
                    CountryID = neoRecord["n"].countryID,
                    CountryName = neoRecord["n"].countryName
                };
                resultDic.Add(c.CountryID, c);
            }
            foreach (var country in expectedDic.Values)
            {
                Assert.True(IsEqual(country, resultDic[country.CountryID]));
            }
        }
        [Fact]
        public async void TestGetNOMRecordsConvertToTWithParameters()
        {
            var expectedDic = new Dictionary<string, Country>()
            {
                {"186" , new Country(){ CountryID = "186" , CountryName = "Russia" } },
                {"216" , new Country(){ CountryID = "216" , CountryName = "United States" } }
            };
            var resultDic = new Dictionary<string, Country>();
            var context = new NeoContext(Driver);
            var result = await context.GetNeoDataRecordsAsync("match(n:Country) WHERE n.countryID IN [$p1,$p2] return n", new Dictionary<string, object>()
            {
                { "p1" , "186"},
                { "p2" , "216"}
            });
            Assert.NotNull(result);
            foreach (var neoRecord in result)
            {
                Country c = neoRecord.GetAndConvert<Country>("n");
                resultDic.Add(c.CountryID, c);
            }
            foreach (var country in expectedDic.Values)
            {
                Assert.True(IsEqual(country, resultDic[country.CountryID]));
            }
        }
        [Fact]
        public async void TestGetNOMRecordsGetSinglePropertiesWithParameters()
        {
            var expectedDic = new Dictionary<string, Country>()
            {
                {"186" , new Country(){ CountryID = "186" , CountryName = "Russia" } },
                {"216" , new Country(){ CountryID = "216" , CountryName = "United States" } }
            };
            var resultDic = new Dictionary<string, Country>();
            var context = new NeoContext(Driver);
            var result = await context.GetNeoDataRecordsAsync("match(n:Country) WHERE n.countryID IN [$p1,$p2] return n", new Dictionary<string, object>()
            {
                { "p1" , "186"},
                { "p2" , "216"}
            });
            Assert.NotNull(result);
            foreach (var neoRecord in result)
            {
                Country c = new Country()
                {
                    CountryID = neoRecord.GetSingleProperty<string>("n", "countryID"),
                    CountryName = (string)neoRecord.GetSingleProperty("n", "countryName")
                };
                resultDic.Add(c.CountryID, c);
            }
            foreach (var country in expectedDic.Values)
            {
                Assert.True(IsEqual(country, resultDic[country.CountryID]));
            }
        }
    }
}
