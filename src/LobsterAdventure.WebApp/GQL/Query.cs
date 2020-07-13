using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using LobsterAdventure.AppServices;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LobsterAdventure.WebApp.GQL
{
  public class Query
  {
    public async Task<GetCurrentChoiceResult> GetCurrentChoice(string adventureId,
      [Service] HttpContextAccessor httpContextAccessor, [Service] IMediator mediator)
    {
      var userId = httpContextAccessor.GetUserId();
      return await mediator.Send(new GetCurrentChoiceQuery(userId, adventureId));
    }

    public async Task<IReadOnlyList<GetAdventuresResult.Adventure>> GetAdventures([Service] IMediator mediator)
    {
      var getAdventuresResult = await mediator.Send(new GetAdventuresQuery());
      return getAdventuresResult.Adventures;
    }
  }
}