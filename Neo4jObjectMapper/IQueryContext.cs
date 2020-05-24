using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neo4jObjectMapper
{
    public interface IQueryContext
    {
        Task<TResult> QueryDefault<TResult>(string cypherQuery) where TResult : class, new();
        Task<TResult> QueryDefault<TResult>(string cypherQuery, IDictionary<string, object> parameters) where TResult : class, new();
        Task<IEnumerable<TResult>> QueryMultiple<TResult>(string cypherQuery) where TResult : class, new();
        Task<IEnumerable<TResult>> QueryMultiple<TResult>(string cypherQuery, IDictionary<string, object> parameters) where TResult : class, new();

        Task<TResult> QueryDefaultIncludeable<TResult, TInclude>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TResult> mapFunc) where TResult : class, new() where TInclude : class, new();
        Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TResult> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new();
        Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2, TInclude3>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TInclude3, TResult> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new() where TInclude3 : class, new();
        Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2, TInclude3, TInclude4>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, TResult> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new() where TInclude3 : class, new() where TInclude4 : class, new();
        
        Task<TResult> QueryDefaultIncludeable<TResult, TInclude>(string cypherQuery, Func<TResult, TInclude, TResult> mapFunc) where TResult : class, new() where TInclude : class, new();
        Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, Func<TResult, TInclude, TInclude2, TResult> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new();
        Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2, TInclude3>(string cypherQuery, Func<TResult, TInclude, TInclude2, TInclude3, TResult> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new() where TInclude3 : class, new();
        Task<TResult> QueryDefaultIncludeable<TResult, TInclude, TInclude2, TInclude3, TInclude4>(string cypherQuery, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, TResult> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new() where TInclude3 : class, new() where TInclude4 : class, new();

        Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new();
        Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new();
        Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2, TInclude3>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TInclude3, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new() where TInclude3 : class, new();
        Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2, TInclude3, TInclude4>(string cypherQuery, IDictionary<string, object> parameters, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new() where TInclude3 : class, new() where TInclude4 : class, new();

        Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude>(string cypherQuery, Func<TResult, TInclude, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new();
        Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2>(string cypherQuery, Func<TResult, TInclude, TInclude2, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new();
        Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2, TInclude3>(string cypherQuery, Func<TResult, TInclude, TInclude2, TInclude3, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new() where TInclude3 : class, new();
        Task<IEnumerable<TResult>> QueryMultipleIncludeable<TResult, TInclude, TInclude2, TInclude3, TInclude4>(string cypherQuery, Func<TResult, TInclude, TInclude2, TInclude3, TInclude4, IEnumerable<TResult>> mapFunc) where TResult : class, new() where TInclude : class, new() where TInclude2 : class, new() where TInclude3 : class, new() where TInclude4 : class, new();
        
        Task<IEnumerable<IRecord>> GetRecordsAsync(string cypherQuery, IDictionary<string, object> parameters);
        Task<IEnumerable<NOMDataRecord>> GetNeoDataRecordsAsync(string cypherQuery, IDictionary<string, object> parameters);
    }
}
