using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using LobsterAdventure.AppServices;
using LobsterAdventure.Domain;
using StackExchange.Redis;

namespace LobsterAdventure.Infrastructure
{
  public class AdventureRepository : IAdventureRepository
  {
    private const string RedisHashKey = "Adventures";
    private readonly IDatabase _redis;

    public AdventureRepository(IDatabase redis)
    {
      _redis = redis;
    }

    public async Task<Adventure> GetAsync(string adventureId)
    {
      byte[] bytes = await _redis.HashGetAsync(RedisHashKey, adventureId);
      return JsonSerializer.Deserialize<Adventure>(bytes);
    }

    public async IAsyncEnumerable<Adventure> GetAllAsync()
    {
      await foreach (var entry in _redis.HashScanAsync(RedisHashKey))
      {
        byte[] bytes = entry.Value;
        yield return JsonSerializer.Deserialize<Adventure>(bytes);
      }
    }
  }
}