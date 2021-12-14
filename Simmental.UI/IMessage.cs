namespace Simmental.UI
{
    public interface IMessage
    {
        string MessageText { get; }
        int TurnNo { get; }
    }
}