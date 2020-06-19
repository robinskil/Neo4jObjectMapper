using Neo4j.Driver;
using Neo4j.ObjectMapper.Tests.Models;
using Neo4j.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Neo4j.ObjectMapper.Tests
{
    public class QueryDefaultTests : TestBase
    {
        [Fact]
        public async Task TestQueryDefault()
        {
            Person person = GeneratePerson();
            await InsertPerson(person);
            var resultPerson = await Driver.QueryDefault<Person>($"MATCH (p:Person {{ Id: '{person.Id.ToString()}'}} ) RETURN p");
            Assert.Equal<Person>(person, resultPerson);
        }

        [Fact]
        public async Task TestQueryDefaultParameters()
        {
            Person person = GeneratePerson();
            await InsertPerson(person);
            var parameters = new Dictionary<string, object>();
            parameters.Add("Id", person.Id);
            var resultPerson = await Driver.QueryDefault<Person>("MATCH (p:Person { Id: $Id} ) RETURN p",parameters);
            Assert.Equal<Person>(person, resultPerson);
        }

        [Fact]
        public async Task TestQueryDefaultAnonymousObject()
        {
            Person person = GeneratePerson();
            await InsertPerson(person);
            var resultPerson = await Driver.QueryDefault<Person>("MATCH (p:Person { Id: $Id }) RETURN p",new { Id = person.Id });
            Assert.Equal<Person>(person, resultPerson);
        }

        [Fact]
        public async Task TestQueryDefaultNoResult()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("Id", Guid.Empty.ToString());
            var resultPerson = await Driver.QueryDefault<Person>("MATCH (p:Person { Id: $Id} ) RETURN p", parameters);
            Assert.Null(resultPerson);
        }

        [Fact]
        public async Task TestQueryCachedDefault()
        {
            Person person = GeneratePerson();
            await InsertPerson(person);
            var resultPerson = await Driver.QueryCachedDefault<Person>($"MATCH (p:Person {{ Id: '{person.Id.ToString()}'}} ) RETURN p");
            Assert.Equal<Person>(person, resultPerson);
        }

        [Fact]
        public async Task TestQueryCachedDefaultParameters()
        {
            Person person = GeneratePerson();
            await InsertPerson(person);
            var parameters = new Dictionary<string, object>();
            parameters.Add("Id", person.Id);
            var resultPerson = await Driver.QueryCachedDefault<Person>("MATCH (p:Person { Id: $Id} ) RETURN p", parameters);
            Assert.Equal<Person>(person, resultPerson);
        }

        [Fact]
        public async Task TestQueryDefaulCachedtAnonymousObject()
        {
            Person person = GeneratePerson();
            await InsertPerson(person);
            var resultPerson = await Driver.QueryCachedDefault<Person>("MATCH (p:Person { Id: $Id }) RETURN p", new { Id = person.Id });
            Assert.Equal<Person>(person, resultPerson);
        }

        [Fact]
        public async Task TestQueryDefaultCachedNoResult()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("Id", Guid.Empty.ToString());
            await Assert.ThrowsAsync<Exception>(async() => await Driver.QueryCachedDefault<Person>("MATCH (p:Person { Id: $Id} ) RETURN p", parameters));
        }
    }
}
