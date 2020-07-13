namespace LobsterAdventure.Domain
{
  public sealed class Choice
  {
    public static readonly Choice DeadEnd = new Choice();

    public Choice(string caption, string option, Choice left, Choice right)
    {
      Caption = caption;
      Option = option;
      Left = left;
      Right = right;
    }

    private Choice()
    {
      Caption = Option = string.Empty;
    }

    public string Caption { get; }
    public string Option { get; }
    public Choice? Left { get; }
    public Choice? Right { get; }
    public bool IsDeadEnd => Left == DeadEnd && Right == DeadEnd;
  }
}