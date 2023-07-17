using CubeApplication.Events;
using CubeApplication.Models;
using CubeApplication.Views;
using MvcPattern;
using UnityEngine;

namespace CubeApplication.Controllers
{
    public class CubeController : IController, ICleareable
    {
        private readonly CubeView view;
        private readonly CubeModel model;

        public CubeController()
        {
            view = Object.FindObjectOfType<CubeView>();
            model = new CubeModel();

            AddEventHandlers();
        }

        public void Clear()
        {
            RemoveEventHandlers();
        }

        public void ReceiveEvent(IControllerEvent controllerEvent)
        {
        }

        private void AddEventHandlers()
        {
            view.Clicked += OnViewClicked;
            model.ColorChanged += OnModelColorChanged;
        }

        private void RemoveEventHandlers()
        {
            view.Clicked -= OnViewClicked;
            model.ColorChanged -= OnModelColorChanged;
        }

        private void OnViewClicked()
        {
            model.ChangeColor();
        }

        private void OnModelColorChanged(Color color)
        {
            view.Color = color;
            ControllerManager.DispatchEvent<UIController>(ControllerEventPool.CreateEvent<CubeColorEvent, string>(color.ToString()));
        }
    }
}