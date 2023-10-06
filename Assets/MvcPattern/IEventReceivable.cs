namespace MvcPattern
{
    public interface IEventReceivable
    {
        void ReceiveEvent<TControllerEvent>(TControllerEvent controllerEvent) where TControllerEvent : struct;
    }
}