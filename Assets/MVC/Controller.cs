namespace Mvc
{
    public class Controller<TView, TModel> : IController
        where TView : IView
        where TModel : IModel
    {
        protected TView view;
        protected TModel model;
    }
}