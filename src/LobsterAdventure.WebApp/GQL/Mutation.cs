using System.Threading.Tasks;
using HotChocolate;
using LobsterAdventure.AppServices;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LobsterAdventure.WebApp.GQL
{
  public class Mutation
  {
    public async Task<Query> StartNewAdventure(string adventureId,
      [Service] HttpContextAccessor httpContextAccessor,
      [Service] IMediator mediator)
    {
      var userId = httpContextAccessor.GetUserId();
      await mediator.Send(new StartNewAdventureCommand(userId, adventureId));

      return new Query();
    }

    public async Task<Query> ChooseLeft(string adventureId,
      [Service] HttpContextAccessor httpContextAccessor,
      [Service] IMediator mediator)
    {
      var userId = httpContextAccessor.GetUserId();

      await mediator.Send(new MakeChoiceCommand(userId, adventureId, ChoiceType.Left));

      return new Query();
    }

    public async Task<Query> ChooseRight(string adventureId,
      [Service] HttpContextAccessor httpContextAccessor,
      [Service] IMediator mediator)
    {
      var userId = httpContextAccessor.GetUserId();

      await mediator.Send(new MakeChoiceCommand(userId, adventureId, ChoiceType.Right));

      return new Query();
    }
  }
}