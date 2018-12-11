namespace Xwrap.Caching
{
	using System;
	using System.Collections.Concurrent;
	using System.Linq;
	using Extensions;
	using Sitecore.Caching;
	using Sitecore.Configuration;

	internal class SitecoreCacheStorage : CustomCache, ICacheStorage
	{
		private const string CacheKey = "Xwrap.Caching";

		public SitecoreCacheStorage(string cacheName)
			: base(cacheName, Settings.Caching.DefaultDataCacheSize)
		{
			global::Sitecore.Events.Event.Subscribe("publish:end", this.OnPublishEnd);
			global::Sitecore.Events.Event.Subscribe("publish:end:remote", this.OnPublishEnd);
		}

		public ConcurrentDictionary<string, SitecoreMemoryCacheEntry> Entries
		{
			get
			{
				if (!(this.GetObject(CacheKey) is ConcurrentDictionary<string, SitecoreMemoryCacheEntry> entries))
				{
					entries = new ConcurrentDictionary<string, SitecoreMemoryCacheEntry>();
					this.SetObject(CacheKey, entries);
				}

				return entries;
			}
		}

		private void OnPublishEnd(object sender, EventArgs e)
		{
			var keys = this.Entries
				.Where(x => x.Value?.ClearOnPublish ?? false)
				.Select(x => x.Key)
				.ToArray();

			this.Entries.RemoveCacheEntries(keys);
		}
	}
}
