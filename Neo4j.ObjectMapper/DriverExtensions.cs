using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neo4j.ObjectMapper
{
    public static class DriverExtensions
    {
        public static async Task<T> QueryDefault<T>(this IDriver driver, Query query)
            where T : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefault<T>(query);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryDefault<T>(this IDriver driver, string cypherQuery)
            where T : class, new()
        {
            return await QueryDefault<T>(driver, new Query(cypherQuery));
        }
        public static async Task<T> QueryDefault<T>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters)
            where T : class, new()
        {
            return await QueryDefault<T>(driver, new Query(cypherQuery, parameters));
        }
        public static async Task<T> QueryDefault<T>(this IDriver driver, string cypherQuery, object parameters)
            where T : class, new()
        {
            return await QueryDefault<T>(driver, new Query(cypherQuery, parameters));
        }

        public static async Task<T> QueryCachedDefault<T>(this IDriver driver, Query query, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            T result = default;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefault<T>(query, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryCachedDefault<T>(this IDriver driver, string cypherQuery, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await QueryCachedDefault<T>(driver, new Query(cypherQuery), expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefault<T>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await QueryCachedDefault<T>(driver, new Query(cypherQuery, parameters), expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefault<T>(this IDriver driver, string cypherQuery, object parameters, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await QueryCachedDefault<T>(driver, new Query(cypherQuery, parameters), expirationTime, forceRefresh);
        }

        public static async Task<T> QueryDefaultInclude<T, T1>(this IDriver driver, Query query, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultInclude<T, T1>(query, map);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryDefaultInclude<T, T1>(this IDriver driver, string cypherQuery, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<T> QueryDefaultInclude<T, T1, T2>(this IDriver driver, Query query, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultInclude<T, T1, T2>(query, map);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3>(this IDriver driver, Query query, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultInclude<T, T1, T2, T3>(query, map);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T3, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryDefaultInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3, T4>(this IDriver driver, Query query, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultInclude(query, map);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultInclude(new Query(cypherQuery), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<T> QueryDefaultInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T3, T4, T> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<T> QueryCachedDefaultInclude<T, T1>(this IDriver driver, Query query, Func<T, T1, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultInclude<T, T1>(query, map, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1>(this IDriver driver, string cypherQuery, Func<T, T1, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1>(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2>(this IDriver driver, Query query, Func<T, T1, T2, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultInclude(query, map, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2>(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3>(this IDriver driver, Query query, Func<T, T1, T2, T3, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultInclude(query, map, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T3, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2, T3>(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2, T3>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T3, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2, T3>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3, T4>(this IDriver driver, Query query, Func<T, T1, T2, T3, T4, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            T result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultInclude(query, map, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T3, T4, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2, T3, T4>(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T4, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2, T3, T4>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<T> QueryCachedDefaultInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T3, T4, T> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultInclude<T, T1, T2, T3, T4>(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultiple<T>(this IDriver driver, Query query)
            where T : class, new()
        {
            IList<T> result = new List<T>();
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultMultiple<T>(query);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultiple<T>(this IDriver driver, string cypherQuery)
            where T : class, new()
        {
            return await QueryDefaultMultiple<T>(driver, new Query(cypherQuery));
        }
        public static async Task<IList<T>> QueryDefaultMultiple<T>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters)
            where T : class, new()
        {
            return await QueryDefaultMultiple<T>(driver, new Query(cypherQuery, parameters));
        }
        public static async Task<IList<T>> QueryDefaultMultiple<T>(this IDriver driver, string cypherQuery, object parameters)
            where T : class, new()
        {
            return await QueryDefaultMultiple<T>(driver, new Query(cypherQuery, parameters));
        }

        public static async Task<IList<T>> QueryCachedDefaultMultiple<T>(this IDriver driver, Query query, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            IList<T> result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultMultiple<T>(query, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultiple<T>(this IDriver driver, string cypherQuery, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await driver.QueryCachedDefaultMultiple<T>(new Query(cypherQuery), expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultiple<T>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await driver.QueryCachedDefaultMultiple<T>(new Query(cypherQuery, parameters), expirationTime, forceRefresh);

        }
        public static async Task<IList<T>> QueryCachedDefaultMultiple<T>(this IDriver driver, string cypherQuery, object parameters, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
        {
            return await driver.QueryCachedDefaultMultiple<T>(new Query(cypherQuery, parameters), expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1>(this IDriver driver, Query query, Func<T, T1, IList<T>> map)
             where T : class, new()
             where T1 : class, new()
        {
            IList<T> result = new List<T>();
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultMultipleInclude(query, map);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1>(this IDriver driver, string cypherQuery, Func<T, T1, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1>(this IDriver driver, Query query, Func<T, T1, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            IList<T> result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultMultipleInclude(query, map, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1>(this IDriver driver, string cypherQuery, Func<T, T1, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2>(this IDriver driver, Query query, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            IList<T> result = new List<T>();
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultMultipleInclude(query, map);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2>(this IDriver driver, string cypherQuery, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2>(this IDriver driver, Query query, Func<T, T1, T2, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            IList<T> result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultMultipleInclude(query, map, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2>(this IDriver driver, string cypherQuery, Func<T, T1, T2, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3>(this IDriver driver, Query query, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            IList<T> result = new List<T>();
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultMultipleInclude(query, map);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T3, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3>(this IDriver driver, Query query, Func<T, T1, T2, T3, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            IList<T> result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultMultipleInclude(query, map, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T3, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T3, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3, T4>(this IDriver driver, Query query, Func<T, T1, T2, T3, T4, IList<T>> map)
           where T : class, new()
           where T1 : class, new()
           where T2 : class, new()
           where T3 : class, new()
           where T4 : class, new()
        {
            IList<T> result = new List<T>();
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryDefaultMultipleInclude(query, map);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T3, T4, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T4, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }
        public static async Task<IList<T>> QueryDefaultMultipleInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T3, T4, IList<T>> map)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryDefaultMultipleInclude(new Query(cypherQuery, parameters), map);
        }

        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3, T4>(this IDriver driver, Query query, Func<T, T1, T2, T3, T4, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            IList<T> result;
            var session = driver.AsyncSession();
            try
            {
                result = await session.QueryCachedDefaultMultipleInclude(query, map, expirationTime, forceRefresh);
            }
            finally
            {
                await session.CloseAsync();
            }
            return result;
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, Func<T, T1, T2, T3, T4, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, IDictionary<string, object> parameters, Func<T, T1, T2, T3, T4, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }
        public static async Task<IList<T>> QueryCachedDefaultMultipleInclude<T, T1, T2, T3, T4>(this IDriver driver, string cypherQuery, object parameters, Func<T, T1, T2, T3, T4, IList<T>> map, TimeSpan expirationTime = default, bool forceRefresh = false)
            where T : class, new()
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            return await driver.QueryCachedDefaultMultipleInclude(new Query(cypherQuery, parameters), map, expirationTime, forceRefresh);
        }

    }
}
