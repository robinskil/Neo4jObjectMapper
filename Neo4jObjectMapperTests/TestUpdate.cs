using Neo4j.Driver;
using Neo4jObjectMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NeoObjectMapperTests
{
    public class TestUpdate : TestEngine
    {
        [Fact]
        public async void TestUpdateNode()
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
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Id" , personGuid}
            };
            var context = new NeoContext(Driver);
            IResultSummary resultExecuting = await context.InsertNode<Person>(person);
            person.Age = 55;
            await context.Update<Person>("MATCH (p:Person { Id : $Id} )", parameters, "p", person);
            var resultPerson = await context.QueryDefault<Person>("MATCH (p:Person { Id : $Id} ) return p", parameters);
            Assert.Equal<Person>(person, resultPerson);
            await context.ExecuteQuery("MATCH (p:Person) DETACH DELETE p");
        }
    }
}
