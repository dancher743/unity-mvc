namespace Mvc
{
    public class Controller<TView, TModel> : IController
    {
        protected TView view;
        protected TModel model;
    }
}