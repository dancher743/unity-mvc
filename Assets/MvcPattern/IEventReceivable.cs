namespace MvcPattern
{
    public interface IEventReceivable
    {
        void ReceiveEvent(IControllerEvent controllerEvent);
    }
}