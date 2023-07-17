namespace MvcPattern
{
    public interface IController : ICleareable
    {
        void ReceiveEvent(IControllerEvent controllerEvent);
    }
}