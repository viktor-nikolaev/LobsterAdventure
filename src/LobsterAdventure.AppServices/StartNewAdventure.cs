using System.Threading;
using System.Threading.Tasks;
using LobsterAdventure.Domain;
using MediatR;

namespace LobsterAdventure.AppServices
{
  public class StartNewAdventureCommand : IRequest
  {
    public StartNewAdventureCommand(string userId, string adventureId)
    {
      UserId = userId;
      AdventureId = adventureId;
    }

    public string UserId { get; }
    public string AdventureId { get; }
  }

  internal class StartNewAdventureHandler : IRequestHandler<StartNewAdventureCommand>
  {
    private readonly IAdventureRepository _adventureRepository;
    private readonly IUserAdventureSessionRepository _userAdventureSessionRepository;

    public StartNewAdventureHandler(IAdventureRepository adventureRepository,
      IUserAdventureSessionRepository userAdventureSessionRepository)
    {
      _adventureRepository = adventureRepository;
      _userAdventureSessionRepository = userAdventureSessionRepository;
    }

    public async Task<Unit> Handle(StartNewAdventureCommand command, CancellationToken ct)
    {
      var adventure = await _adventureRepository.GetAsync(command.AdventureId);

      var session = new UserAdventureSession(command.UserId, adventure);

      await _userAdventureSessionRepository.SaveAsync(session);

      return Unit.Value;
    }
  }
}