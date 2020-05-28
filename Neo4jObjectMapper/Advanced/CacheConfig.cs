using System;
using System.Collections.Generic;
using System.Text;

namespace Neo4jObjectMapper.Advanced
{
    public class CacheConfig
    {
        public TimeSpan DurationInCache { get; }
        public bool ShouldCache { get; }
        public bool CacheOnlyWithTheseParameters { get; }
        public CacheConfig(bool shouldCache, TimeSpan durationInCache, bool cacheOnlyWithTheseParameters = false)
        {
            ShouldCache = shouldCache;
            DurationInCache = durationInCache;
            CacheOnlyWithTheseParameters = cacheOnlyWithTheseParameters;
        }
    }
}
