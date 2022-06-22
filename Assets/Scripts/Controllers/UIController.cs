using CubeApplication.Event;
using CubeApplication.View;

namespace CubeApplication.Controllers
{
    public class UIController : IController
    {
        UIView view;

        public UIController()
        {
            view = UnityEngine.Object.FindObjectOfType<UIView>();
        }

        public void Dispose()
        {
        }

        public void ReceiveEvent(IControllerEvent controllerEvent)
        {
            switch (controllerEvent)
            {
                case CubeColorEvent colorEvent:
                    view.ColorText.text = colorEvent.Color;
                    break;

            }
        }
    }
}