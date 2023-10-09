using CubeApplication.Messages;
using CubeApplication.Models;
using CubeApplication.Views;
using ModelViewController;
using UnityEngine;

namespace CubeApplication.Controllers
{
    public class CubeController : Controller<CubeView, CubeModel>, ICleareable
    {
        public CubeController(CubeView view, CubeModel model)
        {
            this.view = view;
            this.model = model;

            view.Clicked += OnViewClicked;
            model.ColorChanged += OnModelColorChanged;
        }

        void ICleareable.Clear()
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

            ControllerManager.DispatchMessageTo<UIController, CubeColorData>(new CubeColorData(color));
        }
    }
}