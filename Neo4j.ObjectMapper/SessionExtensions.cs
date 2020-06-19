using Neo4j.Driver;
using Neo4j.QueryCaching;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neo4j.ObjectMapper
{
    public static class SessionExtensions
    {
        private static void ConvertParameterTypes(IDictionary<string, object> parameters)
        {
            var keys = new List<string>(parameters.Keys);
            foreach (var key in keys)
            {
                if (parameters[key].GetType() == typeof(Guid))
                {
                    Guid guid = (Guid)parameters[key];
                    parameters[key] = guid.ToString();
                }
            }
        }

        public static async Task<T> QueryDefault<T>(this IAsyncQueryRunner queryRunner, Query query)
            where T : class, new()
        {
            T result = default;
            ConvertParameterTypes(query.Parameters);
            var record = await queryRunner.GetRecordAsync(query);
            if (record != null)
            {
                result = record.ConvertFirstRecordValueTo<T>();
            }
            return result;
        }
        public static async Task<T> QueryDefault<T>(this IAsyncQueryRunner queryRunner, string cypherQuery)
            where T : class, new()
        {
            return await queryRunner.QueryDefault<T>(new Query(cypherQuery));
        }
        public static async Task<T> QueryDefault<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters)
            where T : class, new()
        {
            return await queryRunner.QueryDefault<T>(new Query(cypherQuery, parameters));
        }
        public static async Task<T> QueryDefault<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters)
            where T : class, new()
        {
            return await queryRunner.QueryDefault<T>(new Query(cypherQuery, parameters));
        }

        public static async Task<T> QueryCachedDefault<T>(this IAsyncQueryRunner queryRunner, Query query, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            T result;
            ConvertParameterTypes(query.Parameters);
            result = await queryRunner.GetCachedResultAsync<T>(async (cursor) =>
            {
                if (await cursor.FetchAsync())
                {
                    return cursor.Current.ConvertFirstRecordValueTo<T>();
                }
                throw new Exception("No result found.");
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<T> QueryCachedDefault<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await queryRunner.QueryCachedDefault<T>(new Query(cypherQuery), expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefault<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await queryRunner.QueryCachedDefault<T>(new Query(cypherQuery, parameters), expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefault<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await queryRunner.QueryCachedDefault<T>(new Query(cypherQuery, parameters), expirationTime, forceRefresh);
        }

        public static async Task<T> QueryDefaultInclude<T, T1>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            T result = default;
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            foreach (var record in records)
            {
                result = record.MapRecordTo<T, T1>(map);
            }
            return result;
        }
        public static async Task<T> QueryDefaultInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<T> QueryDefaultInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            T result = default;
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            foreach (var record in records)
            {
                result = record.MapRecordTo<T, T1, T2>(map);
            }
            return result;
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            T result = default;
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            foreach (var record in records)
            {
                result = record.MapRecordTo<T, T1, T2, T3>(map);
            }
            return result;
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            T result = default;
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            foreach (var record in records)
            {
                result = record.MapRecordTo<T, T1, T2, T3, T4>(map);
            }
            return result;
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<T> QueryCachedDefaultInclude<T, T1>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            T result;
            ConvertParameterTypes(query.Parameters);
            result = await queryRunner.GetCachedResultAsync<T>(async (cursor) =>
            {
                var records = await cursor.ToListAsync();
                T funcResult = default;
                foreach (var record in records)
                {
                    funcResult = record.MapRecordTo(map);
                }
                return funcResult;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1>(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            T result;
            ConvertParameterTypes(query.Parameters);
            result = await queryRunner.GetCachedResultAsync<T>(async (cursor) =>
            {
                var records = await cursor.ToListAsync();
                T funcResult = default;
                foreach (var record in records)
                {
                    funcResult = record.MapRecordTo(map);
                }
                return funcResult;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2>(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T3, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            T result;
            ConvertParameterTypes(query.Parameters);
            result = await queryRunner.GetCachedResultAsync<T>(async (cursor) =>
            {
                var records = await cursor.ToListAsync();
                T funcResult = default;
                foreach (var record in records)
                {
                    funcResult = record.MapRecordTo(map);
                }
                return funcResult;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T3, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2, T3>(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2, T3>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T3, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2, T3>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T3, T4, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            T result;
            ConvertParameterTypes(query.Parameters);
            result = await queryRunner.GetCachedResultAsync<T>(async (cursor) =>
            {
                var records = await cursor.ToListAsync();
                T funcResult = default;
                foreach (var record in records)
                {
                    funcResult = record.MapRecordTo(map);
                }
                return funcResult;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T3, T4, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2, T3, T4>(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T4, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2, T3, T4>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T3, T4, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultInclude<T, T1, T2, T3, T4>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        #region Query Default Multiple
        public static async Task<IList<T>> QueryDefaultMultiple<T>(this IAsyncQueryRunner queryRunner, Query query)
            where T : class, new()
        {
            IList<T> result = new List<T>();
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            if (records != null)
            {
                foreach (var record in records)
                {
                    result.Add(record.ConvertFirstRecordValueTo<T>());
                }
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultiple<T>(this IAsyncQueryRunner queryRunner, string cypherQuery)
            where T : class, new()
        {
            return await queryRunner.QueryDefaultMultiple<T>(new Query(cypherQuery));
        }
        public static async Task<IList<T>> QueryDefaultMultiple<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters)
            where T : class, new()
        {
            return await queryRunner.QueryDefaultMultiple<T>(new Query(cypherQuery, parameters));
        }
        public static async Task<IList<T>> QueryDefaultMultiple<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters)
            where T : class, new()
        {
            return await queryRunner.QueryDefaultMultiple<T>(new Query(cypherQuery, parameters));
        }

        public static async Task<IList<T>> QueryCachedDefaultMultiple<T>(this IAsyncQueryRunner queryRunner, Query query, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            ConvertParameterTypes(query.Parameters);
            var result = await queryRunner.GetCachedResultAsync<List<T>>(async (cursor) =>
            {
                var resultList = new List<T>();
                var records = await cursor.ToListAsync();
                foreach (var record in records)
                {
                    resultList.Add(record.ConvertFirstRecordValueTo<T>());
                }
                return resultList;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultiple<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultiple<T>(new Query(cypherQuery), expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultiple<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultiple<T>(new Query(cypherQuery, parameters), expirationTime, forceRefresh);

        }
        public static async Task<IList<T>> QueryCachedDefaultMultiple<T>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultiple<T>(new Query(cypherQuery, parameters), expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
        {
            IList<T> result = default;
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            foreach (var record in records)
            {
                result = record.MapRecordTo<T, T1>(map);
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            ConvertParameterTypes(query.Parameters);
            var result = await queryRunner.GetCachedResultAsync<IList<T>>(async (cursor) =>
            {
                IList<T> resultList = new List<T>();
                var records = await cursor.ToListAsync();
                foreach (var record in records)
                {
                    resultList = record.MapRecordTo(map);
                }
                return resultList;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            IList<T> result = default;
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            foreach (var record in records)
            {
                result = record.MapRecordTo(map);
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            ConvertParameterTypes(query.Parameters);
            var result = await queryRunner.GetCachedResultAsync<IList<T>>(async (cursor) =>
            {
                IList<T> resultList = new List<T>();
                var records = await cursor.ToListAsync();
                foreach (var record in records)
                {
                    resultList = record.MapRecordTo(map);
                }
                return resultList;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            IList<T> result = default;
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            foreach (var record in records)
            {
                result = record.MapRecordTo(map);
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T3, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            ConvertParameterTypes(query.Parameters);
            var result = await queryRunner.GetCachedResultAsync<IList<T>>(async (cursor) =>
            {
                IList<T> resultList = new List<T>();
                var records = await cursor.ToListAsync();
                foreach (var record in records)
                {
                    resultList = record.MapRecordTo(map);
                }
                return resultList;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T3, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T3, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T3, T4, IList<T>> map)
           where T : class, new()
           where T1 : class, new()
           where T2 : class, new()
           where T3 : class, new()
           where T4 : class, new()
        {
            IList<T> result = default;
            ConvertParameterTypes(query.Parameters);
            var records = await queryRunner.GetAllRecordsAsync(query);
            foreach (var record in records)
            {
                result = record.MapRecordTo(map);
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T3, T4, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T4, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T3, T4, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, Query query, Func<T, T1, T2, T3, T4, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            ConvertParameterTypes(query.Parameters);
            var result = await queryRunner.GetCachedResultAsync<IList<T>>(async (cursor) =>
            {
                IList<T> resultList = new List<T>();
                var records = await cursor.ToListAsync();
                foreach (var record in records)
                {
                    resultList = record.MapRecordTo(map);
                }
                return resultList;
            }, query, expirationTime, forceRefresh);
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, Func<T, T1, T2, T3, T4, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T4, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3, T4>(this IAsyncQueryRunner queryRunner, string cypherQuery, object parameters, Func<T, T1, T2, T3, T4, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await queryRunner.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        #endregion
        /// <summary>
        /// Gets a single record from the query runner.
        /// </summary>
        /// <param name="queryRunner"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<IRecord> GetRecordAsync(this IAsyncQueryRunner queryRunner, Query query)
        {
            var resultCursor = await queryRunner.RunAsync(query);
            if (await resultCursor.FetchAsync())
            {
                return resultCursor.Current;
            }
            return null;
        }
        public static async Task<List<IRecord>> GetAllRecordsAsync(this IAsyncQueryRunner queryRunner, Query query)
        {
            var resultCursor = await queryRunner.RunAsync(query);
            return await resultCursor.ToListAsync();
        }
        public static async Task<T> GetCachedResultAsync<T>(this IAsyncQueryRunner queryRunner, Func<IResultCursor, Task<T>> mapper, Query query, TimeSpan expirationTime = default, bool forceRefresh = false)
        {
            return await queryRunner.RunCachedAsync<T>(query, mapper, expirationTime, forceRefresh);
        }
    }
}
