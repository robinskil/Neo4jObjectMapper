using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jObjectMapper
{
    public interface IExecuteContext
    {
        Task ExecuteQuery(string cypherQuery);
        Task ExecuteQuery(string cypherQuery, IDictionary<string, object> parameters);
    }
}
