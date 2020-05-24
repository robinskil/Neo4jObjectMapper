using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jObjectMapper
{
    public class NeoTransactionContext : NeoContext, ITransactionContext
    {
        private readonly IAsyncTransaction transaction;

        internal NeoTransactionContext(IDriver driver, NeoContextEngine executingEngine, IAsyncTransaction transaction) : base(driver, executingEngine)
        {
            this.transaction = transaction;
        }

        protected override async Task<List<IRecord>> GetRecords(string cypherQuery)
        {
            var result = await transaction.RunAsync(cypherQuery);
            return await result.ToListAsync();
        }
        protected override async Task<List<IRecord>> GetRecords(string cypherQuery, IDictionary<string, object> parameters)
        {
            var result = await transaction.RunAsync(cypherQuery, parameters);
            return await result.ToListAsync();
        }
        protected override async Task<IRecord> GetRecord(string cypherQuery)
        {
            IRecord record = null;
            var result = await transaction.RunAsync(cypherQuery);
            if (await result.FetchAsync())
            {
                record = result.Current;
            }
            return record;
        }
        protected override async Task<IRecord> GetRecord(string cypherQuery, IDictionary<string, object> parameters)
        {
            IRecord record = null;
            var result = await transaction.RunAsync(cypherQuery, parameters);
            if (await result.FetchAsync())
            {
                record = result.Current;
            }
            return record;
        }
        protected override async Task<IResultSummary> RunQuery(Query query)
        {
            var cursor = await transaction.RunAsync(query);
            return await cursor.ConsumeAsync();
        }
        protected override async Task<IResultSummary> RunQuery(string cypherQuery)
        {
            var cursor = await transaction.RunAsync(cypherQuery);
            return await cursor.ConsumeAsync();
        }
        protected override async Task<IResultSummary> RunQuery(string cypherQuery, IDictionary<string, object> parameters)
        {
            var session = Driver.AsyncSession();
            var cursor = await session.RunAsync(cypherQuery, parameters);
            return await cursor.ConsumeAsync();
        }

        public async Task CommitTransaction()
        {
            await transaction.CommitAsync();
        }
        public async Task RollbackTransaction()
        {
            await transaction.RollbackAsync();
        }
    }
}
