using CubeApplication.Event;
using CubeApplication.Managers;
using CubeApplication.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeApplication.Controllers
{
    public class CubeController : IController
    {
        private CubeView view; 

        public CubeController()
        {
            view = UnityEngine.Object.FindObjectOfType<CubeView>();

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
        }

        private void RemoveEventHandlers()
        {
            view.OnMouseDownEvent -= ViewOnMouseDownEvent;
        }

        private void ViewOnMouseDownEvent()
        {
            Color color = Random.ColorHSV();

            view.MeshRenderer.material.color = color;

            ControllerManager.DispatchEvent<UIController>(ControllerEventPool.CreateEvent<CubeColorEvent, string>(color.ToString()));
        }
    }
}