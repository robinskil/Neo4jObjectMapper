using Neo4j.ObjectMapper.Tests.Models;
using Neo4j.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Neo4j.ObjectMapper.Tests
{
    public class QueryMultipleDefaultTests : TestBase
    {
        [Fact]
        public async Task TestQueryMultipleDefault()
        {
            Person person = GeneratePerson();
            Person person2 = GeneratePerson();
            await InsertPerson(person);
            await InsertPerson(person2);
            List<Person> persons = new List<Person>()
            {
                person,
                person2
            };
            var resultPerson = await Driver.QueryDefaultMultiple<Person>($"MATCH (p:Person) RETURN p");
            foreach (var personItem in persons)
            {
                Assert.True(resultPerson.Contains(personItem));
            }
        }

        [Fact]
        public async Task TestQueryMultipleDefaultParameters()
        {
            Person person = GeneratePerson();
            Person person2 = GeneratePerson();
            await InsertPerson(person);
            await InsertPerson(person2);
            List<Person> persons = new List<Person>()
            {
                person,
                person2
            };
            var parameters = new Dictionary<string, object>();
            parameters.Add("Name", "Neo");
            var resultPerson = await Driver.QueryDefaultMultiple<Person>("MATCH (p:Person { Name: $Name} ) RETURN p", parameters);
            foreach (var personItem in persons)
            {
                Assert.True(resultPerson.Contains(personItem));
            }
        }

        [Fact]
        public async Task TestQueryMultipleDefaultAnonymousObject()
        {
            Person person = GeneratePerson();
            Person person2 = GeneratePerson();
            await InsertPerson(person);
            await InsertPerson(person2);
            List<Person> persons = new List<Person>()
            {
                person,
                person2
            };
            var parameters = new Dictionary<string, object>();
            parameters.Add("Name", "Neo");
            var resultPerson = await Driver.QueryDefaultMultiple<Person>("MATCH (p:Person { Name: $Name} ) RETURN p", new { Name = "Neo"});
            foreach (var personItem in persons)
            {
                Assert.True(resultPerson.Contains(personItem));
            }
        }

        [Fact]
        public async Task TestQueryMultipleDefaultNoResult()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("Id", Guid.Empty.ToString());
            var resultPerson = await Driver.QueryDefaultMultiple<Person>("MATCH (p:Person { Id: $Id} ) RETURN p", parameters);
            Assert.Empty(resultPerson);
        }

        [Fact]
        public async Task TestQueryMultipleCachedDefault()
        {
            Person person = GeneratePerson();
            Person person2 = GeneratePerson();
            await InsertPerson(person);
            await InsertPerson(person2);
            List<Person> persons = new List<Person>()
            {
                person,
                person2
            };
            var resultPerson = await Driver.QueryCachedDefaultMultiple<Person>($"MATCH (p:Person) RETURN p", forceRefresh: true);
            foreach (var personItem in persons)
            {
                Assert.True(resultPerson.Contains(personItem));
            }
        }

        [Fact]
        public async Task TestQueryMultipleCachedDefaultParameters()
        {
            Person person = GeneratePerson();
            Person person2 = GeneratePerson();
            await InsertPerson(person);
            await InsertPerson(person2);
            List<Person> persons = new List<Person>()
            {
                person,
                person2
            };
            var parameters = new Dictionary<string, object>();
            parameters.Add("Name", "Neo");
            var resultPerson = await Driver.QueryCachedDefaultMultiple<Person>("MATCH (p:Person { Name: $Name} ) RETURN p", parameters , forceRefresh: true);
            foreach (var personItem in persons)
            {
                Assert.True(resultPerson.Contains(personItem));
            }
        }

        [Fact]
        public async Task TestQueryMultipleDefaulCachedtAnonymousObject()
        {
            Person person = GeneratePerson();
            Person person2 = GeneratePerson();
            await InsertPerson(person);
            await InsertPerson(person2);
            List<Person> persons = new List<Person>()
            {
                person,
                person2
            };
            var parameters = new Dictionary<string, object>();
            parameters.Add("Name", "Neo");
            var resultPerson = await Driver.QueryCachedDefaultMultiple<Person>("MATCH (p:Person { Name: $Name} ) RETURN p", new { Name = "Neo" },forceRefresh: true);
            foreach (var personItem in persons)
            {
                Assert.True(resultPerson.Contains(personItem));
            }
        }

        [Fact]
        public async Task TestQueryDefaultCachedNoResult()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("Id", Guid.Empty.ToString());
            var resultPerson = await Driver.QueryCachedDefaultMultiple<Person>("MATCH (p:Person { Id: $Id} ) RETURN p", parameters, forceRefresh: true);
            Assert.Empty(resultPerson);
        }

        [Fact]
        public async Task TestQueryCachingMechanism()
        {
            await InsertPerson(GeneratePerson());
            await InsertPerson(GeneratePerson());
            var parameters = new Dictionary<string, object>();
            parameters.Add("Name", "Neo");
            var resultPerson = await Driver.QueryCachedDefaultMultiple<Person>("MATCH (p:Person { Name: $Name} ) RETURN p", new { Name = "Neo" }, forceRefresh: true);
            await InsertPerson(GeneratePerson());
            var resultPerson2 = await Driver.QueryCachedDefaultMultiple<Person>("MATCH (p:Person { Name: $Name} ) RETURN p", new { Name = "Neo" });
            Assert.True(resultPerson.Count == resultPerson2.Count);
            foreach (var personItem in resultPerson2)
            {
                Assert.True(resultPerson.Contains(personItem));
            }
        }
    }
}
