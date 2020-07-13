using System.Threading.Tasks;
using LobsterAdventure.Domain;

namespace LobsterAdventure.AppServices
{
  public interface IUserAdventureSessionRepository
  {
    Task<UserAdventureSession> GetAsync(string userId, string adventureId);
    Task SaveAsync(UserAdventureSession userAdventureSession);
  }
}