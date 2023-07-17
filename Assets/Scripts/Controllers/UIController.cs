using CubeApplication.Events;
using CubeApplication.Models;
using CubeApplication.Views;
using MvcPattern;
using UnityEngine;

namespace CubeApplication.Controllers
{
    public class UIController : IController, IEventReceivable, ICleareable
    {
        private readonly UIView view;
        private readonly UIModel model;

        public UIController()
        {
            view = Object.FindObjectOfType<UIView>();
            model = new UIModel();
            model.ColorTextChanged += OnModelColorTextChanged;
        }

        public void Clear()
        {
            model.ColorTextChanged -= OnModelColorTextChanged;
        }

        public void ReceiveEvent(IControllerEvent controllerEvent)
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