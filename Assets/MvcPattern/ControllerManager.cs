using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcPattern
{
    public static class ControllerManager
    {
        private static readonly Dictionary<Type, IController> controllers = new Dictionary<Type, IController>();

        public static TController CreateController<TController>() where TController : IController, new()
        {
            TController controller = GetController<TController>();

            if (controller == null)
            {
                controller = new TController();
                controllers.Add(controller.GetType(), controller);
            }

            return controller;
        }

        public static void RemoveController<TController>() where TController : IController
        {
            TController controller = GetController<TController>();

            if (controller != null)
            {
                (controller as ICleareable)?.Clear();
                controllers.Remove(controller.GetType());
            }
        }

        public static void DispatchEvent<TController, TControllerEvent>(TControllerEvent controllerEvent) 
            where TController : IController, IEventReceivable
            where TControllerEvent : struct
        {
            GetController<TController>()?.ReceiveEvent(controllerEvent);
        }

        public static void DispatchEventAll<TControllerEvent>(TControllerEvent controllerEvent, bool isReverseOrder = false) where TControllerEvent : struct
        {
            var controllers = ControllerManager.controllers.Values;

            if (isReverseOrder)
            {
                controllers.Reverse();
            }

            foreach (var controller in controllers)
            {
                (controller as IEventReceivable)?.ReceiveEvent(controllerEvent);
            }
        }

        private static TController GetController<TController>() where TController : IController
        {
            TController controller = default;

            bool isExist = controllers.TryGetValue(typeof(TController), out IController cachedController);

            if (isExist && cachedController is TController)
            {
                controller = (TController)cachedController;
            }

            return controller;
        }
    }
}