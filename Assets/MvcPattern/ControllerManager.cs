using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcPattern
{
    public class ControllerManager
    {
        private Dictionary<Type, IController> controllers;

        private static ControllerManager instance;

        private static ControllerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ControllerManager();
                }

                return instance;
            }
        }

        private ControllerManager()
        {
            controllers = new Dictionary<Type, IController>();
        }

        public static TController CreateController<TController>() where TController : IController, new()
        {
            return Instance.CreateControllerInstance<TController>();
        }

        public static void RemoveController<TController>() where TController : IController
        {
            Instance.RemoveControllerInstance<TController>();
        }

        public static TController GetController<TController>() where TController : IController
        {
            return Instance.GetControllerInctance<TController>();
        }

        public static void DispatchEvent<TController>(IControllerEvent controllerEvent) where TController : IController
        {
            Instance.DispatchEventInstance<TController>(controllerEvent);
        }

        public static void DispatchEventAll(IControllerEvent controllerEvent, bool isReverseOrder = false)
        {
            Instance.DispatchEventAllInstance(controllerEvent, isReverseOrder);
        }

        private TController CreateControllerInstance<TController>() where TController : IController, new()
        {
            TController controller = GetControllerInctance<TController>();

            if (controller == null)
            {
                controller = new TController();
                controllers.Add(controller.GetType(), controller);
            }

            return controller;
        }

        private void RemoveControllerInstance<TController>() where TController : IController
        {
            TController controller = GetController<TController>();

            if (controller != null)
            {
                controller.Dispose();
                controllers.Remove(controller.GetType());
            }
        }

        private TController GetControllerInctance<TController>() where TController : IController
        {
            TController controller = default;

            bool isExist = controllers.TryGetValue(typeof(TController), out IController cachedController);

            if (isExist && cachedController is TController)
            {
                controller = (TController)cachedController;
            }

            return controller;
        }

        private void DispatchEventInstance<TController>(IControllerEvent controllerEvent) where TController : IController
        {
            GetController<TController>()?.ReceiveEvent(controllerEvent);
        }

        private void DispatchEventAllInstance(IControllerEvent controllerEvent, bool isReverseOrder = false)
        {
            IEnumerable<IController> controllers =
                isReverseOrder ?
                this.controllers.Values.Reverse() :
                this.controllers.Values;

            foreach (var controller in controllers)
            {
                controller?.ReceiveEvent(controllerEvent);
            }
        }
    }
}