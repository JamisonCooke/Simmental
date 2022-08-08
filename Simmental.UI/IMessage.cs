namespace Simmental.Interfaces
{
    public interface IMessage
    {
        string MessageText { get; }
        int TurnNo { get; }
    }
}