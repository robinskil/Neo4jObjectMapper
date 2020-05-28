using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jObjectMapper
{
    public interface IUpdateContext
    {
        Task<IResultSummary> Update<T>(string matchQuery, IDictionary<string, object> parameters, string matchQueryResultVariable, T obj);
    }
}
