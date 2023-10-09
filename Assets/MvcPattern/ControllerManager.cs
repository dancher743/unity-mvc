using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcPattern
{
    public static class ControllerManager
    {
        private static readonly Dictionary<Type, IController> controllers = new();

        public static TController CreateController<TController>(params object[] data) where TController : IController
        {
            var controller = GetController<TController>(out Type key);

            if (controller == null)
            {
                controller = (TController)Activator.CreateInstance(typeof(TController), data);
                controllers.Add(key, controller);
            }

            return controller;
        }

        public static void RemoveController<TController>() where TController : IController
        {
            var controller = GetController<TController>(out Type key);

            if (controller != null)
            {
                (controller as ICleareable)?.Clear();
                controllers.Remove(key);
            }
        }

        public static void DispatchEvent<TController, TControllerEvent>(TControllerEvent controllerEvent) 
            where TController : IController, IEventReceivable
            where TControllerEvent : struct
        {
            var controller = GetController<TController>(out _);

            if (controller is not null and IEventReceivable receivable)
            {
                receivable.ReceiveEvent(controllerEvent);
            }
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

        private static TController GetController<TController>(out Type key) where TController : IController
        {
            TController controller = default;

            key = GetKey<TController>();

            if (controllers.ContainsKey(key))
            {
                controller = (TController)controllers[key];
            }

            return controller;
        }

        private static Type GetKey<TController>() where TController : IController
        {
            return typeof(TController);
        }
    }
}