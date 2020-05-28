using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neo4jObjectMapper.Advanced
{
    public class QueryEquality : IEqualityComparer<Query>
    {
        public bool Equals(Query x, Query y)
        {
            if(x != null && y != null && x.Text == y.Text && x.Parameters.Count == y.Parameters.Count)
            {
                if(x.Parameters == null && y.Parameters == null)
                {
                    return true;
                }
                else if(x.Parameters != null && y.Parameters != null)
                {
                    foreach (var parameter in x.Parameters)
                    {
                        if (!y.Parameters.ContainsKey(parameter.Key))
                        {
                            return false;
                        }
                        if (!y.Parameters[parameter.Key].Equals(parameter.Value))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public int GetHashCode(Query obj)
        {
            return obj.GetHashCode();
        }
    }
}
