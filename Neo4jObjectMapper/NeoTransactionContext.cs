using Neo4j.Driver;
using Neo4jObjectMapper.Advanced;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jObjectMapper
{
    public class NeoTransactionContext : NeoContext, ITransactionContext
    {
        private readonly IAsyncTransaction transaction;

        internal NeoTransactionContext(IDriver driver, NeoContextEngine executingEngine , Neo4jCache neo4JCache, IAsyncTransaction transaction) : base(driver, executingEngine , neo4JCache)
        {
            this.transaction = transaction;
        }
        protected override async Task ExecuteRawQuery(Query query)
        {
            await transaction.RunAsync(query);
        }
        protected override async Task<List<IRecord>> GetRecords(Query query)
        {
            var result = await transaction.RunAsync(query);
            return await result.ToListAsync();
        }

        protected override async Task<IRecord> GetRecord(Query query)
        {
            IRecord record = null;
            var result = await transaction.RunAsync(query);
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
