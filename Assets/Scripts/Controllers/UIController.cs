using CubeApplication.Messages;
using CubeApplication.Models;
using CubeApplication.Views;
using ModelViewController;
using UnityEngine;

namespace CubeApplication.Controllers
{
    public class UIController : Controller<UIView, UIModel>, IMessageReceivable, ICleareable
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

        void IMessageReceivable.ReceiveMessage<TMessageData>(TMessageData data)
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