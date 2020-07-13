using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace LobsterAdventure.AppServices
{
  public class MakeChoiceCommand : IRequest
  {
    public MakeChoiceCommand(string userId, string adventureId, ChoiceType choiceType)
    {
      UserId = userId;
      AdventureId = adventureId;
      ChoiceType = choiceType;
    }

    public string UserId { get; }
    public string AdventureId { get; }
    public ChoiceType ChoiceType { get; }
  }

  public enum ChoiceType
  {
    Left = 0,
    Right = 1
  }

  internal class MakeChoiceHandler : IRequestHandler<MakeChoiceCommand>
  {
    private readonly IUserAdventureSessionRepository _userAdventureSessionRepository;

    public MakeChoiceHandler(IUserAdventureSessionRepository userAdventureSessionRepository)
    {
      _userAdventureSessionRepository = userAdventureSessionRepository;
    }

    public async Task<Unit> Handle(MakeChoiceCommand command, CancellationToken ct)
    {
      var session = await _userAdventureSessionRepository.GetAsync(command.UserId, command.AdventureId);

      if (command.ChoiceType == ChoiceType.Left)
      {
        session.ChooseLeft();
      }
      else
      {
        session.ChooseRight();
      }

      await _userAdventureSessionRepository.SaveAsync(session);

      return Unit.Value;
    }
  }
}