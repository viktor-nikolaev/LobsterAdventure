using System.Text.Json;
using System.Threading.Tasks;
using LobsterAdventure.AppServices;
using LobsterAdventure.Domain;
using StackExchange.Redis;

namespace LobsterAdventure.Infrastructure
{
  public class UserAdventureSessionRepository : IUserAdventureSessionRepository
  {
    private const string RedisHashKey = "UserAdventureSessions";
    private readonly IDatabase _redis;

    public UserAdventureSessionRepository(IDatabase redis)
    {
      _redis = redis;
    }

    public async Task<UserAdventureSession> GetAsync(string userId, string adventureId)
    {
      var key = GetKey(userId, adventureId);
      byte[] bytes = await _redis.HashGetAsync(RedisHashKey, key);

      return JsonSerializer.Deserialize<UserAdventureSession>(bytes);
    }

    public async Task SaveAsync(UserAdventureSession userAdventureSession)
    {
      var userId = userAdventureSession.UserId;
      var adventureId = userAdventureSession.AdventureId;

      var key = GetKey(userId, adventureId);

      var bytes = JsonSerializer.SerializeToUtf8Bytes(userAdventureSession);

      await _redis.HashSetAsync(RedisHashKey, key, bytes);
    }

    private static string GetKey(string userId, string adventureId)
    {
      return $"{userId}_{adventureId}";
    }
  }
}