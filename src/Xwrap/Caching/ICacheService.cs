namespace Xwrap.Caching
{
	using System;
	using System.Collections.Generic;

	public interface ICacheService : IDisposable
    {
	    T Get<T>(string key);
	    IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);
	    bool Remove(string key);
	    void RemoveAll(IEnumerable<string> keys);
	    void FlushAll();
		bool Add<T>(string key, T value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = false);
        bool Set<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan), bool clearOnPublish = false);
    }
}
