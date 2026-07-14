using Microsoft.Extensions.Caching.Memory;
using WorldRank.Application.Interfaces;
namespace WorldRank.Infrastructure.Caching;
using Microsoft.Extensions.Logging;

public class MemoryCacheStore : ICache
{
	private readonly IMemoryCache _cache;
	private readonly ILogger<MemoryCacheStore> _logger;

	public MemoryCacheStore(IMemoryCache cache, ILogger<MemoryCacheStore> logger)
	{
		_cache = cache;
		_logger = logger;
	}

	public bool TryGet<T>(string key, out T? value)
	{
		var hit = _cache.TryGetValue(key, out value);
		_logger.LogInformation(hit ? "Cache HIT: {Key}" : "Cache MISS: {Key}", key);
		return hit;
	}

	public void Set<T>(string key, T value, TimeSpan ttl)
	{
		_cache.Set(key, value, ttl);
		_logger.LogInformation("Cache SET: {Key} (ttl {Ttl}s)", key, ttl.TotalSeconds);
	}

	public void Remove(string key)
	{
		_cache.Remove(key);
		_logger.LogInformation("Cache REMOVE: {Key}", key);
	}
}