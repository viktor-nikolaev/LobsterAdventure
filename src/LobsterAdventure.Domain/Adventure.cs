namespace LobsterAdventure.Domain
{
  public sealed class Adventure
  {
    public Adventure(string id, string name, Choice startingChoice)
    {
      Id = id;
      Name = name;
      StartingChoice = startingChoice;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public Choice StartingChoice { get; set; }
  }
}