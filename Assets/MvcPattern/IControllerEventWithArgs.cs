namespace MvcPattern
{
    public interface IControllerEventWithArgs<TArg> : IControllerEvent
    {
        void Update(TArg arg);
    }
}