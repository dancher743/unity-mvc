namespace CubeApplication.Controllers
{
    public interface IControllerEventWithArgs<TArg> : IControllerEvent
    {
        void Update(TArg arg);
    }
}