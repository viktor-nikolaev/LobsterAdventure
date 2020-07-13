using System.Collections.Generic;
using System.Threading.Tasks;
using LobsterAdventure.Domain;

namespace LobsterAdventure.AppServices
{
  public interface IAdventureRepository
  {
    Task<Adventure> GetAsync(string adventureId);
    IAsyncEnumerable<Adventure> GetAllAsync();
  }
}