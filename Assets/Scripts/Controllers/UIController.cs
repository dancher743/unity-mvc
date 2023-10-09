using CubeApplication.Events;
using CubeApplication.Models;
using CubeApplication.Views;
using ModelViewController;
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

        void IEventReceivable.ReceiveEvent<TEventData>(TEventData data)
        {
            switch (data)
            {
                case CubeColorData cubeColorData:
                    model.ColorText = cubeColorData.Color.ToString();
                    break;

            }
        }

        private void OnModelColorTextChanged(string text)
        {
            view.ColorText = text;
        }
    }
}