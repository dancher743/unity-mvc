namespace MvcPattern
{
    public interface IController : IDisposableManaged
    {
        void ReceiveEvent(IControllerEvent controllerEvent);
    }
}