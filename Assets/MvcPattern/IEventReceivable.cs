namespace ModelViewController
{
    public interface IEventReceivable
    {
        void ReceiveEvent<TEventData>(TEventData data) where TEventData : struct;
    }
}