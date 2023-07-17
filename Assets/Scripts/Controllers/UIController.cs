using CubeApplication.Events;
using CubeApplication.Views;
using MvcPattern;
using UnityEngine;

namespace CubeApplication.Controllers
{
    public class UIController : IController, IEventReceivable
    {
        private readonly UIView view;

        public UIController()
        {
            view = Object.FindObjectOfType<UIView>();
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