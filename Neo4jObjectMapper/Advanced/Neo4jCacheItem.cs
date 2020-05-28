using System;

namespace Neo4jObjectMapper.Advanced
{
    public class Neo4jCacheItem
    {
        public TimeSpan TimeToLive { get; }
        public DateTime TimeAdded { get; }
        public object CachedObject { get; }
        public Neo4jCacheItem(TimeSpan timeToLive, DateTime timeAdded , object cachedObject)
        {
            TimeToLive = timeToLive;
            TimeAdded = timeAdded;
            CachedObject = cachedObject;
        }
    }
}
