using Neo4j.Driver;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Neo4jObjectMapper.Advanced
{
    public class Neo4jCache
    {
        private readonly IDictionary<Neo4jQuery, Neo4jCacheItem> cacheContainer;
        private readonly IDictionary<Neo4jQuery, Neo4jCacheItem> cacheContainerWithParameters;
        public Neo4jCache()
        {
            cacheContainer = new ConcurrentDictionary<Neo4jQuery, Neo4jCacheItem>(new Neo4jQueryTextEquality());
            cacheContainerWithParameters = new ConcurrentDictionary<Neo4jQuery, Neo4jCacheItem>(new Neo4jQueryTextAndParameterEquality());
        }

        public void ClearCache()
        {
            cacheContainer.Clear();
            cacheContainerWithParameters.Clear();
        }

        public void ForceRefreshOnItem(Neo4jQuery query)
        {
            cacheContainer.Remove(query);
            cacheContainerWithParameters.Remove(query);
        }

        public void AddToCache(Neo4jQuery query, TimeSpan timeToLive, object obj)
        {
            if (query.CacheForTheseParameters)
            {
                cacheContainerWithParameters.Add(query, new Neo4jCacheItem(timeToLive, DateTime.Now, obj));
            }
            else
            {
                cacheContainer.Add(query, new Neo4jCacheItem(timeToLive, DateTime.Now, obj));
            }
        }

        public bool TryGet<T>(Neo4jQuery query, out T obj)
            where T : class,new()
        {
            obj = default;
            if (cacheContainerWithParameters.ContainsKey(query))
            {
                var cacheItem = cacheContainerWithParameters[query];
                if (cacheItem.TimeAdded.Add(cacheItem.TimeToLive) > DateTime.Now)
                {
                    obj = (T)cacheItem.CachedObject;
                    return true;
                }
                cacheContainerWithParameters.Remove(query);
            }
            if (cacheContainer.ContainsKey(query))
            {
                var cacheItem = cacheContainer[query];
                if(cacheItem.TimeAdded.Add(cacheItem.TimeToLive) > DateTime.Now)
                {
                    obj = (T)cacheItem.CachedObject;
                    return true;
                }
                cacheContainer.Remove(query);
            }
            return false;
        }

        public bool ContainsCachedQueryResult(Neo4jQuery query)
        {
            return cacheContainer.ContainsKey(query) || cacheContainerWithParameters.ContainsKey(query);
        }
    }
}
