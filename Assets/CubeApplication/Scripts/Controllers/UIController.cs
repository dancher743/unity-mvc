using CubeApplication.Messages;
using CubeApplication.Models;
using CubeApplication.Views;
using Mvc;
using UnityEngine;

namespace CubeApplication.Controllers
{
    public class UIController : Controller<UIView, UIModel>, IMessageReceivable, ICleareable
    {
        public UIController()
        {
            view = Object.FindObjectOfType<UIView>();

            model = new UIModel();
            model.ColorChanged += OnModelColorChanged;
        }

        void ICleareable.Clear()
        {
            model.ColorChanged -= OnModelColorChanged;
        }

        void IMessageReceivable.ReceiveMessage<TMessageData>(TMessageData data)
        {
            switch (data)
            {
                case CubeColorData cubeColorData:
                    model.Color = cubeColorData.Color;
                    break;

            }
        }

        private void OnModelColorChanged(string text)
        {
            view.ColorText = text;
        }
    }
}