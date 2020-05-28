using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace Neo4jObjectMapper
{
    public class NeoContext : INeoContext
    {
        public IDriver Driver { get; }

        private readonly NeoContextEngine engine;
        protected readonly Action<SessionConfigBuilder> sessionBuilder;

        public NeoContext(IDriver driver)
        {
            this.Driver = driver;
            engine = new NeoContextEngine();
        }

        public NeoContext(IDriver driver, Action<SessionConfigBuilder> sessionBuilder)
        {
            Driver = driver;
            this.sessionBuilder = sessionBuilder;
        }

        protected NeoContext(IDriver driver ,NeoContextEngine executingEngine)
        {
            Driver = driver;
            engine = executingEngine;
        }

        private IAsyncSession StartSession()
        {
            return sessionBuilder != null ? Driver.AsyncSession(sessionBuilder) : Driver.AsyncSession();
        }

        #region Executers
        protected virtual async Task ExecuteRawQuery(Query query)
        {
            var session = StartSession();
            try
            {
                await session.RunAsync(query);
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        protected virtual async Task<List<IRecord>> GetRecords(Query query)
        {
            var session = StartSession();
            List<IRecord> records;
            try
            {
                var result = await session.RunAsync(query);
                records = await result.ToListAsync();
            }
            finally
            {
                await session.CloseAsync();
            }
            return records;
        }
        protected virtual async Task<IRecord> GetRecord(Query query)
        {
            var session = StartSession();
            IRecord record = null;
            try
            {
                var result = await session.RunAsync(query);
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
            var session = StartSession();
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
        #endregion
        #region RawQueryExecuting
        public async Task ExecuteQuery(string cypherQuery)
        {
            await ExecuteRawQuery(new Query(cypherQuery));
        }
        public async Task ExecuteQuery(string cypherQuery, IDictionary<string, object> parameters)
        {
            parameters = engine.ParameterConverter(parameters);
            await ExecuteRawQuery(new Query(cypherQuery,parameters));
        }
        #endregion
        #region Querying
        public async Task<TResult> QueryDefault<TResult>(string cypherQuery) where TResult : class, new()
        {
            TResult resultVal = default;
            IRecord record = await GetRecord(new Query(cypherQuery));
            if (record != null)
            {
                resultVal = engine.ConvertRecordToObject<TResult>(record);
            }
            return resultVal;
        }
        public async Task<TResult> QueryDefault<TResult>(string cypherQuery, IDictionary<string, object> parameters) where TResult : class, new()
        {
            TResult resultVal = default;
            parameters = engine.ParameterConverter(parameters);
            IRecord record = await GetRecord(new Query(cypherQuery,parameters));
            if (record != null)
            {
                resultVal = engine.ConvertRecordToObject<TResult>(record);
            }
            return resultVal;
        }

        public async Task<IEnumerable<TResult>> QueryMultiple<TResult>(string cypherQuery) where TResult : class, new()
        {
            var resultList = new List<TResult>();
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                foreach (var record in records)
                {
                    resultList.Add(engine.ConvertRecordToObject<TResult>(record));
                }
            }
            return resultList;
        }
        public async Task<IEnumerable<TResult>> QueryMultiple<TResult>(string cypherQuery, IDictionary<string, object> parameters) where TResult : class, new()
        {
            var resultList = new List<TResult>();
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery,parameters));
            if (records != null)
            {
                foreach (var record in records)
                {
                    resultList.Add(engine.ConvertRecordToObject<TResult>(record));
                }
            }
            return resultList;
        }

        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TResult> mapFunc) where TResult : class, new() where TInclude : class, new()
        {
            TResult resultObj = default;
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery,parameters));
            if (records != null)
            {
                resultObj = engine.ConvertRecordToObjects<TResult, TInclude>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            TResult resultObj = default;
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
            if (records != null)
            {
                resultObj = engine.ConvertRecordToObjects<TResult, TInclude, TInclude2>(records, mapFunc);
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
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
            if (records != null)
            {
                resultObj = engine.ConvertRecordToObjects<TResult, TInclude, TInclude2, TInclude3>(records, mapFunc);
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
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
            if (records != null)
            {
                resultObj = engine.ConvertRecordToObjects<TResult, TInclude, TInclude2, TInclude3, TInclude4>(records, mapFunc);
            }
            return resultObj;
        }

        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude>(string cypherQuery, Func<TResult, TInclude, TResult> mapFunc) where TResult : class, new() where TInclude : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                resultObj = engine.ConvertRecordToObjects<TResult, TInclude>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, Func<TResult, TInclude, TInclude2, TResult> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            TResult resultObj = default;
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                resultObj = engine.ConvertRecordToObjects<TResult, TInclude, TInclude2>(records, mapFunc);
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
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                resultObj = engine.ConvertRecordToObjects<TResult, TInclude, TInclude2, TInclude3>(records, mapFunc);
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
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                resultObj = engine.ConvertRecordToObjects<TResult, TInclude, TInclude2, TInclude3, TInclude4>(records, mapFunc);
            }
            return resultObj;
        }

        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
            if (records != null)
            {
                resultObj = engine.ConvertRecordsToObjects<TResult, TInclude>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
            if (records != null)
            {
                resultObj = engine.ConvertRecordsToObjects<TResult, TInclude, TInclude2>(records, mapFunc);
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
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
            if (records != null)
            {
                resultObj = engine.ConvertRecordsToObjects<TResult, TInclude, TInclude2, TInclude3>(records, mapFunc);
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
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
            if (records != null)
            {
                resultObj = engine.ConvertRecordsToObjects<TResult, TInclude, TInclude2, TInclude3, TInclude4>(records, mapFunc);
            }
            return resultObj;
        }

        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude>(string cypherQuery, Func<TResult, TInclude, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                resultObj = engine.ConvertRecordsToObjects<TResult, TInclude>(records, mapFunc);
            }
            return resultObj;
        }
        public async Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, Func<TResult, TInclude, TInclude2, IEnumerable<TResult>> mapFunc)
            where TResult : class, new()
            where TInclude : class, new()
            where TInclude2 : class, new()
        {
            IEnumerable<TResult> resultObj = default;
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                resultObj = engine.ConvertRecordsToObjects<TResult, TInclude, TInclude2>(records, mapFunc);
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
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                resultObj = engine.ConvertRecordsToObjects<TResult, TInclude, TInclude2, TInclude3>(records, mapFunc);
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
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            if (records != null)
            {
                resultObj = engine.ConvertRecordsToObjects<TResult, TInclude, TInclude2, TInclude3, TInclude4>(records, mapFunc);
            }
            return resultObj;
        }
        
        public async Task<IEnumerable<IRecord>> GetRecordsAsync(string cypherQuery)
        {
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            return records;
        }
        public async Task<IEnumerable<IRecord>> GetRecordsAsync(string cypherQuery, IDictionary<string, object> parameters)
        {
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
            return records;
        }
        public async Task<IEnumerable<NOMDataRecord>> GetNeoDataRecordsAsync(string cypherQuery)
        {
            List<NOMDataRecord> neoDataRecords = new List<NOMDataRecord>();
            List<IRecord> records = await GetRecords(new Query(cypherQuery));
            foreach (var record in records)
            {
                neoDataRecords.Add(engine.ConvertRecordToNeoRecord(record));
            }
            return neoDataRecords;
        }
        public async Task<IEnumerable<NOMDataRecord>> GetNeoDataRecordsAsync(string cypherQuery, IDictionary<string, object> parameters)
        {
            List<NOMDataRecord> neoDataRecords = new List<NOMDataRecord>();
            parameters = engine.ParameterConverter(parameters);
            List<IRecord> records = await GetRecords(new Query(cypherQuery, parameters));
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
            return engine.CreateTFromEntity<T>(node);
        }
        #endregion
        #region Updating
        public async Task<IResultSummary> Update<T>(string matchQuery, IDictionary<string,object> parameters,string matchQueryResultVariable,T obj)
            where T : class, new()
        {
            IResultSummary resultSummary = await RunQuery(engine.CreateSetNodeQuery<T>(matchQuery,parameters,matchQueryResultVariable, obj));
            return resultSummary;
        }
        #endregion
        #region Inserting
        public async Task<IResultSummary> Insert(string cypherQuery)
        {
            IResultSummary resultSummary = await RunQuery(new Query(cypherQuery));
            return resultSummary;
        }
        public async Task<IResultSummary> Insert(string cypherQuery,IDictionary<string,object> parameters)
        {
            parameters = engine.ParameterConverter(parameters);
            IResultSummary resultSummary = await RunQuery(new Query(cypherQuery, parameters));
            return resultSummary;
        }
        public async Task<IResultSummary> InsertNode<TNode>(TNode node)
        {
            IResultSummary resultSummary = await RunQuery(engine.CreateInsertNodeQuery<TNode>(node));
            return resultSummary;
        }
        public async Task<IResultSummary> InsertNodes<TNode>(IEnumerable<TNode> node)
        {
            IResultSummary resultSummary = await RunQuery(engine.CreateInsertNodesQuery<TNode>(node));
            return resultSummary;
        }
        public async Task<IResultSummary> InsertNodeWithRelation<TNode, TRelation, TNode2>(TNode nodeFrom, TRelation relation , TNode2 nodeTo)
        {
            IResultSummary resultSummary = await RunQuery(engine.CreateInsertNodesWithRelationQuery<TNode, TRelation, TNode2>(nodeFrom, relation, nodeTo));
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
            var session = StartSession();
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
            var session = StartSession();
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
            var session = StartSession();
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
