using CubeApplication.Controllers;
using CubeApplication.Models;
using CubeApplication.Views;
using MvcPattern;
using UnityEngine;

namespace CubeApplication
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CubeView cubeView;

        [SerializeField]
        private UIView uiView;

        void Start()
        {
            ControllerManager.CreateController<CubeController>(cubeView, new CubeModel());
            ControllerManager.CreateController<UIController>();
        }

        private void OnDestroy()
        {
            ControllerManager.RemoveController<CubeController>();
            ControllerManager.RemoveController<UIController>();
        }
    }
}