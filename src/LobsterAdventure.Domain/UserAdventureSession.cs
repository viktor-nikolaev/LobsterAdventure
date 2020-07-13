using System;

namespace LobsterAdventure.Domain
{
  public sealed class UserAdventureSession
  {
    public UserAdventureSession(string userId, Adventure adventure)
    {
      UserId = userId;
      AdventureId = adventure.Id;
      CurrentChoice = adventure.StartingChoice;
    }

    public string UserId { get; }
    public string AdventureId { get; }
    public Choice CurrentChoice { get; private set; }

    public void ChooseLeft()
    {
      CurrentChoice = CurrentChoice.Left ?? throw new InvalidOperationException();
    }

    public void ChooseRight()
    {
      CurrentChoice = CurrentChoice.Right ?? throw new InvalidOperationException();
    }
  }
}