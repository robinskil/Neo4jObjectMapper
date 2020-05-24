using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace Neo4jObjectMapper
{
    public class NeoContext : INeoContext
    {
        public IDriver Driver { get; }

        private readonly NeoContextEngine engine;

        public NeoContext(IDriver driver)
        {
            this.Driver = driver;
            engine = new NeoContextEngine();
        }
        protected NeoContext(IDriver driver ,NeoContextEngine executingEngine)
        {
            Driver = driver;
            engine = executingEngine;
        }
        #region Executers
        protected virtual async Task ExecuteRawQuery(string cypherQuery)
        {
            var session = Driver.AsyncSession();
            try
            {
                await session.RunAsync(cypherQuery);
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        protected virtual async Task<List<IRecord>> GetRecords(string cypherQuery, IDictionary<string, object> parameters)
        {
            var session = Driver.AsyncSession();
            List<IRecord> records;
            try
            {
                var result = await session.RunAsync(cypherQuery, parameters);
                records = await result.ToListAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return records;
        }
        protected virtual async Task<List<IRecord>> GetRecords(string cypherQuery)
        {
            var session = Driver.AsyncSession();
            List<IRecord> records;
            try
            {
                var result = await session.RunAsync(cypherQuery);
                records = await result.ToListAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return records;
        }
        protected virtual async Task<IRecord> GetRecord(string cypherQuery)
        {
            var session = Driver.AsyncSession();
            IRecord record = null;
            try
            {
                var result = await session.RunAsync(cypherQuery);
                if (await result.FetchAsync())
                {
                    record = result.Current;
                }
            }
            finally
            {
                await session.CloseAsync();
            }
            return record;
        }
        protected virtual async Task<IRecord> GetRecord(string cypherQuery, IDictionary<string, object> parameters)
        {
            var session = Driver.AsyncSession();
            IRecord record = null;
            try
            {
                var result = await session.RunAsync(cypherQuery, parameters);
                if (await result.FetchAsync())
                {
                    record = result.Current;
                }
            }
            finally
            {
                await session.CloseAsync();
            }
            return record;
        }
        protected virtual async Task<IResultSummary> RunQuery(Query query)
        {
            IResultSummary resultSummary;
            var session = Driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync(query);
                resultSummary = await cursor.ConsumeAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return resultSummary;
        }
        protected virtual async Task<IResultSummary> RunQuery(string cypherQuery)
        {
            IResultSummary resultSummary;
            var session = Driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync(cypherQuery);
                resultSummary = await cursor.ConsumeAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return resultSummary;
        }
        protected virtual async Task<IResultSummary> RunQuery(string cypherQuery, IDictionary<string, object> parameters)
        {
            IResultSummary resultSummary;
            var session = Driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync(cypherQuery, parameters);
                resultSummary = await cursor.ConsumeAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return resultSummary;
        }
        #endregion
        #region RawQueryExecuting
        public async Task ExecuteQuery(string cypherQuery)
        {
            await ExecuteRawQuery(cypherQuery);
        }
        #endregion
        #region Querying
        public async Task<TResult> QueryDefault<TResult>(string cypherQuery) where TResult : class, new()
        {
            TResult resultVal = default;
            IRecord record = await GetRecord(cypherQuery);
            if (record != null)
            {
                resultVal = engine.ConvertNodeToSingleObject<TResult>(record);
            }
            return resultVal;
        }
        public async Task<TResult> QueryDefault<TResult>(string cypherQuery, IDictionary<string, object> parameters) where TResult : class, new()
        {
            TResult resultVal = default;
            IRecord record = await GetRecord(cypherQuery,parameters);
            if (record != null)
            {
                resultVal = engine.ConvertNodeToSingleObject<TResult>(record);
            }
            return resultVal;
        }

        public async Task<IEnumerable<TResult>> QueryMultiple<TResult>(string cypherQuery) where TResult : class, new()
        {
            var resultList = new List<TResult>();
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                foreach (var record in records)
                {
                    resultList.Add(engine.ConvertNodeToSingleObject<TResult>(record));
                }
            }
            return resultList;
        }
        public async Task<IEnumerable<TResult>> QueryMultiple<TResult>(string cypherQuery, IDictionary<string, object> parameters) where TResult : class, new()
        {
            var resultList = new List<TResult>();
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                foreach (var record in records)
                {
                    resultList.Add(engine.ConvertNodeToSingleObject<TResult>(record));
                }
            }
            return resultList;
        }

        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TResult> mapFunc) where TResult : class, new() where TInclude : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2, TInclude3>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TInclude3, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2, TInclude3, TInclude4>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
            where TInclude4 : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3, TInclude4>(records, mapFunc);
            }
            return resultObj;
        }

        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude>(string cypherQuery, Func<TResult, TInclude, TResult> mapFunc) where TResult : class, new() where TInclude : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, Func<TResult, TInclude, TInclude2, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2, TInclude3>(string cypherQuery, Func<TResult, TInclude, TInclude2, TInclude3, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2, TInclude3, TInclude4>(string cypherQuery, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
            where TInclude4 : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3, TInclude4>(records, mapFunc);
            }
            return resultObj;
        }

        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2, TInclude3>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TInclude3, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2, TInclude3, TInclude4>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
            where TInclude4 : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery, parameters);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3, TInclude4>(records, mapFunc);
            }
            return resultObj;
        }

        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude>(string cypherQuery, Func<TResult, TInclude, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, Func<TResult, TInclude, TInclude2, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2, TInclude3>(string cypherQuery, Func<TResult, TInclude, TInclude2, TInclude3, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2, TInclude3, TInclude4>(string cypherQuery, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
            where TInclude3 : class, new()
            where TInclude4 : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(cypherQuery);
            if (records != null)
            {
                resultObj = engine.ConvertNodeToObjectsAndMap<TResult, TInclude, TInclude2, TInclude3, TInclude4>(records, mapFunc);
            }
            return resultObj;
        }
        
        public async Task<IEnumerable<IRecord>> GetRecordsAsync(string cypherQuery)
        {
            List<IRecord> records = await GetRecords(cypherQuery);
            return records;
        }
        public async Task<IEnumerable<IRecord>> GetRecordsAsync(string cypherQuery, IDictionary<string, object> parameters)
        {
            List<IRecord> records = await GetRecords(cypherQuery,parameters);
            return records;
        }
        public async Task<IEnumerable<NOMDataRecord>> GetNeoDataRecordsAsync(string cypherQuery)
        {
            List<NOMDataRecord> neoDataRecords = new List<NOMDataRecord>();
            List<IRecord> records = await GetRecords(cypherQuery);
            foreach (var record in records)
            {
                neoDataRecords.Add(engine.ConvertRecordToNeoRecord(record));
            }
            return neoDataRecords;
        }
        public async Task<IEnumerable<NOMDataRecord>> GetNeoDataRecordsAsync(string cypherQuery, IDictionary<string, object> parameters)
        {
            List<NOMDataRecord> neoDataRecords = new List<NOMDataRecord>();
            List<IRecord> records = await GetRecords(cypherQuery,parameters);
            foreach (var record in records)
            {
                neoDataRecords.Add(engine.ConvertRecordToNeoRecord(record));
            }
            return neoDataRecords;
        }
        #endregion
        #region Helper methods
        public T ConvertNodeToT<T>(INode node) where T : class, new()
        {
            return engine.CreateTFromNode<T>(node);
        }
        #endregion
        #region Inserting
        public async Task<IResultSummary> Insert(string cypherQuery)
        {
            IResultSummary resultSummary = await RunQuery(cypherQuery);
            return resultSummary;
        }
        public async Task<IResultSummary> Insert(string cypherQuery,IDictionary<string,object> parameters)
        {
            IResultSummary resultSummary = await RunQuery(cypherQuery, parameters);
            return resultSummary;
        }
        public async Task<IResultSummary> InsertNode<TNode>(TNode node)
        {
            IResultSummary resultSummary = await RunQuery(engine.CreateInsertQuery<TNode>(node));
            return resultSummary;
        }
        public async Task<IResultSummary> InsertNodes<TNode>(IEnumerable<TNode> node)
        {
            IResultSummary resultSummary = await RunQuery(engine.CreateInsertQuery<TNode>(node));
            return resultSummary;
        }
        public async Task<IResultSummary> InsertNodeWithRelation<TNode, TRelation, TNode2>(TNode node, TRelation relation , TNode2 node2)
        {
            IResultSummary resultSummary = await RunQuery(engine.CreateInsertQuery<TNode, TRelation, TNode2>(node, relation, node2));
            return resultSummary;
        }
        public async Task<IResultSummary> InsertRelation<TRelation>(string cypherMatchQuery, string variableNodeFrom, string variableNodeTo, TRelation relation)
        {
            IResultSummary resultSummary = await RunQuery(engine.CreateInsertRelationQuery<TRelation>(cypherMatchQuery, variableNodeFrom, variableNodeTo, relation));
            return resultSummary;
        }
        #endregion
        #region Transactions
        public async Task InsertWithWriteTransaction(Action<IAsyncTransaction> transactionFunctionBlock)
        {
            var session = Driver.AsyncSession();
            try
            {
                await session.WriteTransactionAsync((tx) =>
                {
                    return new Task(() => {
                        transactionFunctionBlock(tx);
                    });
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async Task ReadTransaction(Action<IAsyncTransaction> transactionFunctionBlock)
        {
            var session = Driver.AsyncSession();
            try
            {
                await session.ReadTransactionAsync((tx) =>
                {
                    return new Task(() => {
                        transactionFunctionBlock(tx);
                    });
                });
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async Task UseTransaction(Action<ITransactionContext> transactionBody)
        {
            var session = Driver.AsyncSession();
            try
            {
                var transaction = await session.BeginTransactionAsync();
                transactionBody(new NeoTransactionContext(Driver, engine , transaction));
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        #endregion
    }
}
