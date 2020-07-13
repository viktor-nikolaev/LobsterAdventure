using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace LobsterAdventure.AppServices
{
  public class GetAdventuresQuery : IRequest<GetAdventuresResult>
  {
  }

  public class GetAdventuresResult
  {
    public GetAdventuresResult(IReadOnlyList<Adventure> adventures)
    {
      Adventures = adventures;
    }

    public IReadOnlyList<Adventure> Adventures { get; }

    public class Adventure
    {
      public Adventure(string id, string caption)
      {
        Id = id;
        Caption = caption;
      }

      public string Id { get; }
      public string Caption { get; }
    }
  }

  internal class GetAdventuresHandler : IRequestHandler<GetAdventuresQuery, GetAdventuresResult>
  {
    private readonly IAdventureRepository _repository;

    public GetAdventuresHandler(IAdventureRepository repository)
    {
      _repository = repository;
    }

    public async Task<GetAdventuresResult> Handle(GetAdventuresQuery query, CancellationToken ct)
    {
      // In a real-world application I would split adventure name
      
      var adventures = await _repository
        .GetAllAsync()
        .Select(x => new GetAdventuresResult.Adventure(x.Id, x.Name))
        .ToListAsync(ct);

      return new GetAdventuresResult(adventures);
    }
  }
}