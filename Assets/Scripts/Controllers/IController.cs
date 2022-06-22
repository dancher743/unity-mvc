namespace CubeApplication.Controllers
{
    public interface IController : IDisposableManaged
    {
        void ReceiveEvent(IControllerEvent controllerEvent);
    }
}