namespace MvcPattern
{
    public interface IController
    {
        void ReceiveEvent(IControllerEvent controllerEvent);
    }
}