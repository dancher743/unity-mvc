using CubeApplication.Events;
using CubeApplication.Models;
using CubeApplication.Views;
using MvcPattern;
using UnityEngine;

namespace CubeApplication.Controllers
{
    public class UIController : Controller<UIView, UIModel>, IEventReceivable, ICleareable
    {
        public UIController()
        {
            view = Object.FindObjectOfType<UIView>();

            model = new UIModel();
            model.ColorTextChanged += OnModelColorTextChanged;
        }

        void ICleareable.Clear()
        {
            model.ColorTextChanged -= OnModelColorTextChanged;
        }

        void IEventReceivable.ReceiveEvent<TControllerEvent>(TControllerEvent controllerEvent)
        {
            switch (controllerEvent)
            {
                case CubeColorEvent colorEvent:
                    model.ColorText = colorEvent.Color;
                    break;

            }
        }

        private void OnModelColorTextChanged(string text)
        {
            view.ColorText = text;
        }
    }
}