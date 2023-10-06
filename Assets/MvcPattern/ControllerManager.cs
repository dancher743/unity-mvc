using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcPattern
{
    public static class ControllerManager
    {
        private static readonly Dictionary<Type, IController> controllers = new();

        public static TController CreateController<TController>() where TController : IController, new()
        {
            var controller = GetController<TController>();

            if (controller == null)
            {
                var controllerKey = GetControllerKey<TController>();
                controller = new TController();

                controllers.Add(controllerKey, controller);
            }

            return controller;
        }

        public static void RemoveController<TController>() where TController : IController
        {
            var controller = GetController<TController>();

            if (controller != null)
            {
                (controller as ICleareable)?.Clear();

                var controllerKey = GetControllerKey<TController>();
                controllers.Remove(controllerKey);
            }
        }

        public static void DispatchEvent<TController, TControllerEvent>(TControllerEvent controllerEvent) 
            where TController : IController, IEventReceivable
            where TControllerEvent : struct
        {
            GetController<TController>()?.ReceiveEvent(controllerEvent);
        }

        public static void DispatchEventAll<TControllerEvent>(TControllerEvent controllerEvent, bool isInReverseOrder = false) where TControllerEvent : struct
        {
            var controllers = ControllerManager.controllers.Values;

            if (isInReverseOrder)
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
            var controllerKey = GetControllerKey<TController>();

            if (controllers.ContainsKey(controllerKey))
            {
                controller = (TController)controllers[controllerKey]; 
            }

            return controller;
        }

        private static Type GetControllerKey<TController>() where TController : IController
        {
            return typeof(TController);
        }
    }
}