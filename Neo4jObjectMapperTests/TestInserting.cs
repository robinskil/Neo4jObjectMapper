using Neo4j.Driver;
using Neo4jObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NeoObjectMapperTests
{
    public class TestInserting : TestEngine
    {
        [Fact]
        public async void TestQueryMultiple()
        {
            var persons = new List<Person>()
            {
                new Person()
                {
                    Age = 50,
                    DateOfBirth = DateTime.Now.AddYears(-50),
                    Id = Guid.NewGuid(),
                    Name = "neo",
                    Salary = 5400.77,
                },
                new Person()
                {
                    Age = 50,
                    DateOfBirth = DateTime.Now.AddYears(-50),
                    Id = Guid.NewGuid(),
                    Name = "neo",
                    Salary = 5400.77,
                }
            };
            var context = new NeoContext(Driver);
            IResultSummary resultExecuting = await context.InsertNodes<Person>(persons);
            IEnumerable<Person> personsResult = await context.QueryMultiple<Person>("MATCH (p:Person) return p");
            await context.ExecuteQuery("MATCH (p:Person) DETACH DELETE p");
        }
        [Fact]
        public async void TestInsertQuery()
        {
            var person = new Person()
            {
                Age = 50,
                DateOfBirth = DateTime.Now.AddYears(-50),
                Id = Guid.NewGuid(),
                Name = "neo",
                Salary = 5400.77,
            };
            var context = new NeoContext(Driver);
            IResultSummary resultExecuting = await context.InsertNode<Person>(person);
            var resultPerson = await context.QueryDefault<Person>("MATCH (p:Person) RETURN p");
            Assert.True(resultExecuting.QueryType == Neo4j.Driver.QueryType.WriteOnly);
            Assert.Equal<Person>(person, resultPerson);
            await context.ExecuteQuery("MATCH (p:Person { Name : 'neo' }) DETACH DELETE p");
        }
        [Fact]
        public async void TestInsertQueryParameters()
        {
            var personGuid = Guid.NewGuid();
            var person = new Person()
            {
                Age = 50,
                DateOfBirth = DateTime.Now.AddYears(-50),
                Id = personGuid,
                Name = "neo",
                Salary = 5400.77,
            };
            var expectedCountry = new Country() { CountryID = "555", CountryName = "NOM COUNTRY" };
            var context = new NeoContext(Driver);
            var parameters = new Dictionary<string, object>();
            parameters.Add("p1", "neo");
            parameters.Add("p2", personGuid.ToString());
            var resultExecuting = await context.Insert("CREATE (:Person {  Name : $p1 , Id: $p2 })", parameters);
            var resultPerson = await context.QueryDefault<Person>("MATCH (p:Person { Name: $p1 , Id: $p2 }) RETURN p", parameters);
            Assert.True(resultExecuting.QueryType == Neo4j.Driver.QueryType.WriteOnly);
            Assert.True(person.Id == resultPerson.Id);
            await context.ExecuteQuery("MATCH (p:Person) DETACH DELETE p");
        }
        [Fact]
        public async void TestInsertQueryGeneric()
        {
            var expectedCountry = new Country() { CountryID = "555", CountryName = "NOM COUNTRY" };
            var context = new NeoContext(Driver);
            var resultExecuting = await context.InsertNode<Country>(expectedCountry);
            var resultCountry = await context.QueryDefault<Country>("MATCH (n:Country { CountryID: '555' }) RETURN n");
            Assert.True(resultExecuting.QueryType == Neo4j.Driver.QueryType.WriteOnly);
            Assert.True(IsEqual(expectedCountry, resultCountry));
            await context.ExecuteQuery("MATCH (n:Country { CountryID: '555' }) DETACH DELETE n");
        }

        [Fact]
        public async void TestInsertMultipleQueryGeneric()
        {
            var expectedCountries = new Dictionary<string, Country>()
            {
                {"555", new Country() { CountryID = "555", CountryName = "NOM COUNTRY" } },
                {"556", new Country() { CountryID = "556", CountryName = "NOM COUNTRY2" }},
            };
            var context = new NeoContext(Driver);
            var resultExecuting = await context.InsertNodes<Country>(expectedCountries.Values);
            var resultCountry = await context.QueryMultiple<Country>("match(n:Country) WHERE n.CountryID IN ['555','556'] return n");
            Assert.True(resultExecuting.QueryType == Neo4j.Driver.QueryType.WriteOnly);
            Assert.True(expectedCountries.Count == resultCountry.Count());
            foreach (var resCountry in resultCountry)
            {
                Assert.True(IsEqual(resCountry, expectedCountries[resCountry.CountryID]));
            }
            await context.ExecuteQuery("MATCH (n:Country) WHERE n.CountryID IN ['555','556'] DETACH DELETE n");
        }

        [Fact]
        public async void TestInsertNodesWithRelationGeneric()
        {
            var country = new Country() { CountryID = "555", CountryName = "NOM COUNTRY" };
            var city = new City() { CityId = "5555", CityName = "NOM CITY" };
            country.Cities = new List<City>()
            {
                city
            };
            var context = new NeoContext(Driver);
            var resultExecuting = await context.InsertNodeWithRelation<City, EXISTS_IN, Country>(city, new EXISTS_IN(), country);
            var resultCountry = await context.QueryDefaultIncludeable<Country, City>("MATCH (n:Country { CountryID: '555' })<-[e:EXISTS_IN]-(c:City) return n,c",
                (country, city) =>
                {
                    country.Cities = new List<City>() { city };
                    return country;
                }
            );
            Assert.True(resultExecuting.QueryType == Neo4j.Driver.QueryType.WriteOnly);
            Assert.True(IsEqual(country, resultCountry));
            await context.ExecuteQuery("MATCH (n:Country { CountryID: '555' }) DETACH DELETE n");
            await context.ExecuteQuery("MATCH (n:City { CityId: '5555' }) DETACH DELETE n");
        }

        [Fact]
        public async void TestInsertNodesWithRelation()
        {
            var person = new Person()
            {
                Age = 50,
                DateOfBirth = DateTime.Now.AddYears(-50),
                Id = Guid.NewGuid(),
                Name = "neo",
                Salary = 5400.77,
                Owns = new List<Owns>()
                {
                    new Owns()
                    {
                        OwnedFrom = DateTime.Now.AddYears(-2),
                        OwnedTill = DateTime.Now.AddYears(-1),
                        House = new House()
                        {
                            Address = "test address",
                            Age = 150
                        }
                    }
                }
            };
            INeoContext context = new NeoContext(Driver);
            IResultSummary resultExecuting = await context.InsertNodeWithRelation<Person, Owns, House>(person, person.Owns.First(), person.Owns.First().House);
            Dictionary<Guid, Person> personContainer = new Dictionary<Guid, Person>();
            var resultPerson = await context.QueryDefaultIncludeable<Person, Owns, House>("MATCH (p:Person { Name: 'neo' })-[o:Owns]->(h:House) return p,o,h",
                (p, o, h) =>
                {
                    if (!personContainer.ContainsKey(p.Id))
                    {
                        personContainer.Add(p.Id, p);
                        p.Owns = new List<Owns>();
                    }
                    personContainer[p.Id].Owns.Add(o);
                    o.House = h;
                    return personContainer[p.Id];
                }
            );
            await context.ExecuteQuery("MATCH (n:Person { Name: 'neo' }) DETACH DELETE n");
            await context.ExecuteQuery("MATCH (p:House {Address: 'test address'}) DETACH DELETE p");
            Assert.Equal<Person>(person, resultPerson);
        }

        [Fact]
        public async void TestInsertRelation()
        {
            var country = new Country() { CountryID = "555", CountryName = "NOM COUNTRY" };
            var city = new City() { CityId = "5555", CityName = "NOM CITY" };
            country.Cities = new List<City>()
            {
                city
            };
            var context = new NeoContext(Driver);
            await context.InsertNode<Country>(country);
            await context.InsertNode<City>(city);
            var resultRelEx = await context.InsertRelation<EXISTS_IN>(
                "MATCH (n:Country { CountryID: '555' }) " +
                "MATCH (c:City { CityId: '5555' })", "c", "n", new EXISTS_IN());
            var resultCountry = await context.QueryDefaultIncludeable<Country, City>("MATCH (n:Country { CountryID: '555' })<-[e:EXISTS_IN]-(c:City) return n,c",
                (country, city) =>
                {
                    country.Cities = new List<City>() { city };
                    return country;
                }
            );
            Assert.True(resultRelEx.QueryType == Neo4j.Driver.QueryType.WriteOnly);
            Assert.True(IsEqual(country, resultCountry));
            await context.ExecuteQuery("MATCH (n:Country { CountryID: '555' }) DETACH DELETE n");
            await context.ExecuteQuery("MATCH (n:City { CityId: '5555' }) DETACH DELETE n");
        }
    }
}
