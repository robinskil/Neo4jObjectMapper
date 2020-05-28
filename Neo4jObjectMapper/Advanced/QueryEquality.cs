using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neo4jObjectMapper.Advanced
{
    public class Neo4jQueryTextEquality : IEqualityComparer<Neo4jQuery>
    {
        public bool Equals(Neo4jQuery x, Neo4jQuery y)
        {
            if(x != null && y != null && x.Text == y.Text)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Neo4jQuery obj)
        {
            return obj.Text.GetHashCode();
        }
    }

    public class Neo4jQueryTextAndParameterEquality : IEqualityComparer<Neo4jQuery>
    {
        public bool Equals(Neo4jQuery x, Neo4jQuery y)
        {
            if (x != null && y != null && x.Text == y.Text)
            {
                if (x.Parameters != null && y.Parameters != null && x.Parameters.Count == y.Parameters.Count)
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
            }
            return false;
        }

        public int GetHashCode(Neo4jQuery obj)
        {
            int hashCode = obj.Text.GetHashCode();
            foreach (var parameter in obj.Parameters)
            {
                hashCode = hashCode ^ parameter.Key.GetHashCode();
                hashCode = hashCode ^ parameter.Value.GetHashCode();
            }
            return hashCode;
        }
    }
}
