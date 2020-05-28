using Neo4j.Driver;
using System.Collections.Generic;

namespace Neo4jObjectMapper.Advanced
{
    public class Neo4jQuery : Query
    {
        public bool CacheForTheseParameters { get; }
        public Neo4jQuery(string text) : base(text)
        {
        }

        public Neo4jQuery(string text, IDictionary<string, object> parameters, bool cacheForTheseParameters = true) : base(text, parameters)
        {
            CacheForTheseParameters = cacheForTheseParameters;
        }
    }
}
