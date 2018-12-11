namespace Xwrap.Caching
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Cache service
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface ICacheService : IDisposable
	{
		/// <summary>
		/// Gets the cached entity by specified key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		T Get<T>(string key);

		/// <summary>
		/// Gets all cached entities by specified keys.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="keys">The keys.</param>
		/// <returns></returns>
		IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);

		/// <summary>
		/// Removes the cached entity by specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		bool Remove(string key);

		/// <summary>
		/// Removes all cached entities by specified keys.
		/// </summary>
		/// <param name="keys">The keys.</param>
		void RemoveAll(IEnumerable<string> keys);

		/// <summary>
		/// Flushes all.
		/// </summary>
		void FlushAll();

		/// <summary>
		/// Adds the cache entity with specified key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <param name="expiresIn">The expires in.</param>
		/// <param name="clearOnPublish">if set to <c>true</c> [clear on publish].</param>
		/// <returns></returns>
		bool Add<T>(string key, T value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = false);

		/// <summary>
		/// Sets the cache entity with specified key.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <param name="expiresIn">The expires in.</param>
		/// <param name="clearOnPublish">if set to <c>true</c> [clear on publish].</param>
		/// <returns></returns>
		bool Set<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan), bool clearOnPublish = false);
	}
}
