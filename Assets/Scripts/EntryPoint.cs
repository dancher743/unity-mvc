using CubeApplication.Controllers;
using MvcPattern;
using UnityEngine;

namespace CubeApplication
{
    public class EntryPoint : MonoBehaviour
    {
        void Start()
        {
            ControllerManager.CreateController<CubeController>();
            ControllerManager.CreateController<UIController>();
        }

        private void OnDestroy()
        {
            ControllerManager.RemoveController<CubeController>();
            ControllerManager.RemoveController<UIController>();
        }
    }
}