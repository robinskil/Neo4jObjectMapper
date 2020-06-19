using Neo4j.Driver;
using Neo4j.ObjectMapper.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neo4j.ObjectMapper.Tests
{
    public abstract class TestBase
    {
        protected IDriver Driver { get; set; } = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));

        protected Person GeneratePerson()
        {
            return new Person()
            {
                Age = 50,
                DateOfBirth = DateTime.Now.AddYears(-30),
                Id = Guid.NewGuid(),
                Name = "Neo",
                Salary = 4722.99,
            };
        }

        protected async Task InsertPerson(Person p)
        {
            var session = Driver.AsyncSession();
            var parameters = new Dictionary<string, object>()
            {
                { "Age" , p.Age },
                { "DateOfBirth", p.DateOfBirth },
                { "Id" , p.Id.ToString() },
                { "Name" , p.Name },
                { "Salary" , p.Salary }
            };
            try
            {
                await session.RunAsync("CREATE (p:Person{ Id : $Id , Age : $Age , DateOfBirth : $DateOfBirth , Name : $Name , Salary : $Salary })", parameters);
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }
}
