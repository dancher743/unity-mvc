using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelViewController
{
    public static class ControllerManager
    {
        private static readonly Dictionary<string, IController> controllers = new();

        public static TController CreateController<TController>(params object[] data) where TController : IController
        {
            var controller = GetController<TController>(out string key);

            if (controller == null)
            {
                controller = (TController)Activator.CreateInstance(typeof(TController), data);
                controllers.Add(key, controller);
            }
            return controller;
        }

        public static void RemoveController<TController>(TController controller) where TController : IController
        {
            var key = GetKey(controller.GetType());

            if (controllers.ContainsKey(key))
            {
                (controller as ICleareable)?.Clear();
                controllers.Remove(key);
            }
        }

        public static void DispatchMessageTo<TController, TMessageData>(TMessageData data)
            where TController : IController, IMessageReceivable
            where TMessageData : struct
        {
            var controller = GetController<TController>(out _);

            if (controller is not null and IMessageReceivable receivable)
            {
                receivable.ReceiveMessage(data);
            }
        }

        public static void DispatchMessageToAll<TMessageData>(TMessageData data, bool isInReverseOrder = false) where TMessageData : struct
        {
            var controllers = ControllerManager.controllers.Values;

            if (isInReverseOrder)
            {
                controllers.Reverse();
            }

            foreach (var receivable in controllers.Cast<IMessageReceivable>())
            {
                receivable.ReceiveMessage(data);
            }
        }

        private static TController GetController<TController>(out string key) where TController : IController
        {
            TController controller = default;

            key = GetKey(typeof(TController));

            if (controllers.ContainsKey(key))
            {
                controller = (TController)controllers[key];
            }

            return controller;
        }

        private static string GetKey(Type type)
        {
            return type.Name;
        }
    }
}