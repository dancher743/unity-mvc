using CubeApplication.Event;
using CubeApplication.Models;
using CubeApplication.View;
using MvcPattern;
using UnityEngine;

namespace CubeApplication.Controllers
{
    public class CubeController : IController
    {
        private CubeView view;
        private CubeModel model;

        public CubeController()
        {
            view = Object.FindObjectOfType<CubeView>();
            model = new CubeModel();

            AddEventHandlers();
        }

        public void Dispose()
        {
            RemoveEventHandlers();
        }

        public void ReceiveEvent(IControllerEvent controllerEvent)
        {
        }

        private void AddEventHandlers()
        {
            view.OnMouseDownEvent += ViewOnMouseDownEvent;
            model.ColorChanged += OnModelColorChanged;
        }

        private void RemoveEventHandlers()
        {
            view.OnMouseDownEvent -= ViewOnMouseDownEvent;
            model.ColorChanged -= OnModelColorChanged;
        }

        private void ViewOnMouseDownEvent()
        {
            model.ChangeColor();
        }

        private void OnModelColorChanged(Color color)
        {
            view.SetColor(color);
            ControllerManager.DispatchEvent<UIController>(ControllerEventPool.CreateEvent<CubeColorEvent, string>(color.ToString()));
        }
    }
}