namespace ModelViewController
{
    public interface IMessageReceivable
    {
        void ReceiveMessage<TMessageData>(TMessageData data) where TMessageData : struct;
    }
}