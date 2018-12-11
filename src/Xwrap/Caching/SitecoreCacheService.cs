namespace Xwrap.Caching
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Extensions;

	internal class SitecoreCacheService : ICacheService
	{
		protected ICacheStorage CacheStorage;
		protected bool IsDisposed;

		public SitecoreCacheService(ICacheStorage cacheStorage)
		{
			this.CacheStorage = cacheStorage;
		}

		public virtual bool Add<T>(string key, T value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = false)
		{
			if (this.CacheStorage.Entries.TryGetValue(key, out _))
			{
				return false;
			}

			return this.CacheStorage.Entries.TryAdd(key, new SitecoreMemoryCacheEntry(value, expiresIn, clearOnPublish));
		}

		public virtual bool Set<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan), bool clearOnPublish = false)
		{
			var cacheEntry = new SitecoreMemoryCacheEntry(value, expiresIn, clearOnPublish);

			if (this.CacheStorage.Entries.TryGetValue(key, out _))
			{
				this.CacheStorage.Entries[key] = cacheEntry;
				return true;
			}

			return this.CacheStorage.Entries.TryAdd(key, cacheEntry);
		}

		public virtual T Get<T>(string key)
		{
			var obj = this.Get(key);

			if (obj != null && obj is T)
			{
				return (T)obj;
			}

			return default(T);
		}

		public virtual IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
		{
			return keys.Select(key => new { Key = key, Value = this.Get<T>(key) }).ToDictionary(k => k.Key, v => v.Value);
		}

		public virtual bool Remove(string key)
		{
			this.CacheStorage.Entries.RemoveCacheEntries(key);
			return true;
		}

		public virtual void RemoveAll(IEnumerable<string> keys)
		{
			foreach (var key in keys)
			{
				this.Remove(key);
			}
		}

		public virtual void FlushAll()
		{
			this.CacheStorage.Entries.RemoveCacheEntries(this.CacheStorage.Entries.Keys.ToArray());
			this.CacheStorage.Clear();
		}

		public virtual void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.FlushAll();

				this.IsDisposed = true;
			}
		}

		protected virtual object Get(string key)
		{
			if (!this.CacheStorage.Entries.TryGetValue(key, out var memoryCacheEntry))
			{
				return null;
			}

			if (memoryCacheEntry.ExpiresAt < DateTime.UtcNow)
			{
				this.CacheStorage.Entries.TryRemove(key, out memoryCacheEntry);
				return null;
			}

			return memoryCacheEntry.Value;
		}
	}
}