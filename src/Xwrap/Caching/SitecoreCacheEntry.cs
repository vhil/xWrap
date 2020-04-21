namespace Xwrap.Caching
{
	using System;
	using System.IO;
	using System.Runtime.Serialization.Formatters.Binary;
	using Sitecore.Caching;

	public class SitecoreCacheEntry : ICacheable
	{
		private long dataLength = 0;
		
		public bool ClearOnPublish { get; protected set; }
		
		/// <summary>
		/// .ctor
		/// </summary>
		/// <param name="value"></param>
		/// <param name="expiresIn"></param>
		/// <param name="clearOnPublish"></param>
		public SitecoreCacheEntry(object value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = true)
		{
			if (expiresIn == default(TimeSpan))
			{
				expiresIn = TimeSpan.FromDays(365);
			}

			this.Value = value;
			this.ExpiresAt = DateTime.UtcNow.Add(expiresIn);
			this.ClearOnPublish = clearOnPublish;
		}

		internal DateTime ExpiresAt { get; }

		internal object Value { get; }

		/// <summary>
		/// tries to calculate object size in memory.
		/// </summary>
		/// <returns></returns>
		public long GetDataLength()
		{
			try
			{
				if (dataLength == 0 && (this.Value?.GetType().IsSerializable ?? false))
				{
					using (var s = new MemoryStream())
					{
						var formatter = new BinaryFormatter();
						formatter.Serialize(s, this.Value);
						dataLength = s.Length;
					}
				}

				return this.dataLength;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}

		public bool Cacheable { get; set; }
		public bool Immutable => false;

		public event DataLengthChangedDelegate DataLengthChanged;
		public bool IsExpired => this.ExpiresAt < DateTime.UtcNow;
	}
}
