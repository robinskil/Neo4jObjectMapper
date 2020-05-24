using Neo4jObjectMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace NeoObjectMapperTests
{
    public class TestTransaction : TestEngine
    {
        [Fact]
        public async void TestCommitTransaction()
        {
            var expectedCountry = new Country() { CountryID = "555", CountryName = "NOM COUNTRY" };
            var context = new NeoContext(Driver);
            await context.UseTransaction((transaction) =>
            {
                transaction.Insert("CREATE (:Country { CountryID: '555' , countryName: 'NOM COUNTRY'})").GetAwaiter().GetResult();
                var shouldBeNull = context.QueryDefault<Country>("MATCH (n:Country { CountryID: '555' }) RETURN n").GetAwaiter().GetResult();
                Assert.Null(shouldBeNull);
                transaction.CommitTransaction().GetAwaiter().GetResult();
            });
            var resultCountry = await context.QueryDefault<Country>("MATCH (n:Country { CountryID: '555' }) RETURN n");
            Assert.True(IsEqual(expectedCountry, resultCountry));
            await context.ExecuteQuery("MATCH (n:Country { CountryID: '555' }) DETACH DELETE n");
        }
        [Fact]
        public async void TestRollbackTransaction()
        {
            var expectedCountry = new Country() { CountryID = "555", CountryName = "NOM COUNTRY" };
            var context = new NeoContext(Driver);
            await context.UseTransaction((transaction) =>            
            {
                transaction.Insert("CREATE (:Country { CountryID: '555' , countryName: 'NOM COUNTRY'})").GetAwaiter().GetResult();
                var shouldBeNull = context.QueryDefault<Country>("MATCH (n:Country { CountryID: '555' }) RETURN n").GetAwaiter().GetResult();
                Assert.Null(shouldBeNull);
                var resultCountry = transaction.QueryDefault<Country>("MATCH (n:Country { CountryID: '555' }) RETURN n").GetAwaiter().GetResult();
                Assert.True(IsEqual(expectedCountry, resultCountry));
                transaction.RollbackTransaction().GetAwaiter().GetResult();
            });
            var shouldBeNullAgain = await context.QueryDefault<Country>("MATCH (n:Country { CountryID: '555' }) RETURN n");
            Assert.Null(shouldBeNullAgain);
            await context.ExecuteQuery("MATCH (n:Country { CountryID: '555' }) DETACH DELETE n");
        }
    }
}
