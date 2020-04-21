namespace Xwrap.Caching
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Sitecore.Caching;

	internal class SitecoreCacheService : CustomCache, ICacheService
	{
		public SitecoreCacheService(string name)
			: base(name, global::Sitecore.Configuration.Settings.Caching.DefaultDataCacheSize)
		{
			global::Sitecore.Events.Event.Subscribe("publish:end", this.OnPublishEnd);
			global::Sitecore.Events.Event.Subscribe("publish:end:remote", this.OnPublishEnd);
		}

		public SitecoreCacheService(ICache innerCache) : base(innerCache)
		{
			global::Sitecore.Events.Event.Subscribe("publish:end", this.OnPublishEnd);
			global::Sitecore.Events.Event.Subscribe("publish:end:remote", this.OnPublishEnd);
		}

		public bool Set<T>(string key, T value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = false)
		{
			this.RemoveEntry(key);
			this.SetObject(key, new SitecoreCacheEntry(value, expiresIn, clearOnPublish));

			return true;
		}

		public T Get<T>(string key)
		{
			var entry = this.GetObject(key) as SitecoreCacheEntry;

			if (entry?.Value is T entryValue)
			{
				if (entry.IsExpired)
				{
					this.RemoveEntry(key);
					return default(T);
				}

				return entryValue;
			}

			return default(T);
		}

		public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
		{
			return keys.ToDictionary(k => k, this.Get<T>);
		}

		public new bool Remove(string key)
		{
			this.RemoveEntry(key);
			return false;
		}

		public void RemoveAll(IEnumerable<string> keys)
		{
			foreach (var key in keys)
			{
				this.Remove(key);
			}
		}

		public void FlushAll()
		{
			this.Clear();
		}

		public bool Add<T>(string key, T value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = false)
		{
			return this.Set<T>(key, value, expiresIn, false);
		}

		public void Dispose()
		{
		}

		private void OnPublishEnd(object sender, EventArgs e)
		{
			var keys = this.InnerCache?.GetCacheKeys();

			foreach (var key in keys ?? Enumerable.Empty<string>())
			{
				var entry = this.GetObject(key) as SitecoreCacheEntry;

				if (entry != null && (entry.ClearOnPublish || entry.IsExpired))
				{
					this.RemoveEntry(key);
				}
			}
		}

		internal void RemoveEntry(string key)
		{
			var entry = this.GetObject(key) as SitecoreCacheEntry;

			if (entry != null)
			{
				base.Remove(key);

				if (entry?.Value != null && entry.Value is IDisposable disposable)
				{
					disposable?.Dispose();
				}
			}
		}
	}
}