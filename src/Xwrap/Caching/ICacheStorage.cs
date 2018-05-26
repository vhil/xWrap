using System.Collections.Concurrent;

namespace Xwrap.Caching
{
	internal interface ICacheStorage
    {
        ConcurrentDictionary<string, SitecoreMemoryCacheEntry> Entries { get; }
        void Clear();
    }
}
