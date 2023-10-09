namespace MvcPattern
{
    public class Controller<TView, TModel> : IController
    {
        protected TView view;
        protected TModel model;
    }
}