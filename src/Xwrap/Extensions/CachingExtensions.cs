namespace Xwrap.Extensions
{
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using Caching;

	internal static class CachingExtensions
    {
        public static void RemoveCacheEntries(this ConcurrentDictionary<string, SitecoreMemoryCacheEntry> cacheEntries, string key)
        {
	        cacheEntries.TryRemove(key, out _);
        }

        public static void RemoveCacheEntries(this ConcurrentDictionary<string, SitecoreMemoryCacheEntry> cacheEntries, IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                cacheEntries.RemoveCacheEntries(key);
            }
        }
    }
}
