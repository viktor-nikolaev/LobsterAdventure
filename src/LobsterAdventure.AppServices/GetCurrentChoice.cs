using System.Threading;
using System.Threading.Tasks;
using LobsterAdventure.Domain;
using MediatR;

namespace LobsterAdventure.AppServices
{
  public class GetCurrentChoiceQuery : IRequest<GetCurrentChoiceResult>
  {
    public GetCurrentChoiceQuery(string userId, string adventureId)
    {
      UserId = userId;
      AdventureId = adventureId;
    }

    public string UserId { get; }
    public string AdventureId { get; }
  }

  public class GetCurrentChoiceResult
  {
    public GetCurrentChoiceResult(Choice choice)
    {
      Caption = choice.Caption;
      LeftOption = choice.Left?.Option ?? string.Empty;
      RightOption = choice.Right?.Option ?? string.Empty;
      IsDeadEnd = choice.IsDeadEnd;
    }

    public string Caption { get; }
    public string LeftOption { get; }
    public string RightOption { get; }
    public bool IsDeadEnd { get; }
  }

  internal class GetCurrentChoiceHandler : IRequestHandler<GetCurrentChoiceQuery, GetCurrentChoiceResult>
  {
    private readonly IUserAdventureSessionRepository _userAdventureSessionRepository;

    public GetCurrentChoiceHandler(IUserAdventureSessionRepository userAdventureSessionRepository)
    {
      _userAdventureSessionRepository = userAdventureSessionRepository;
    }

    public async Task<GetCurrentChoiceResult> Handle(GetCurrentChoiceQuery query, CancellationToken ct)
    {
      var userId = query.UserId;
      var adventureId = query.AdventureId;

      var session = await _userAdventureSessionRepository.GetAsync(userId, adventureId);

      return new GetCurrentChoiceResult(session.CurrentChoice);
    }
  }
}