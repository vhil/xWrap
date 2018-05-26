namespace Xwrap.Caching
{
	using System;

	internal class SitecoreMemoryCacheEntry
	{
		public bool ClearOnPublish { get; protected set; }

		public SitecoreMemoryCacheEntry(object value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = true)
		{
			if (expiresIn == default(TimeSpan))
			{
				expiresIn = TimeSpan.FromDays(365);
			}

			this.Value = value;
			this.ExpiresAt = DateTime.UtcNow.Add(expiresIn);
			this.ClearOnPublish = clearOnPublish;
		}

		internal DateTime ExpiresAt { get; private set; }

		internal object Value { get; private set; }
	}
}
