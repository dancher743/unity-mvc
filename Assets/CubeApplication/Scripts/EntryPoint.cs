using CubeApplication.Controllers;
using CubeApplication.Models;
using CubeApplication.Views;
using Mvc;
using UnityEngine;

namespace CubeApplication
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CubeView cubeView;

        [SerializeField]
        private UIView uiView;

        private CubeController cubeController;
        private UIController uiController;

        void Start()
        {
            cubeController = ControllerManager.CreateController<CubeController>(cubeView, new CubeModel());
            uiController = ControllerManager.CreateController<UIController>();
        }

        private void OnDestroy()
        {
            ControllerManager.RemoveController(cubeController);
            ControllerManager.RemoveController(uiController);
        }
    }
}