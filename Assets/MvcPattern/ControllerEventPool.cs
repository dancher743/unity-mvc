using System;
using System.Collections.Generic;

namespace MvcPattern
{
    public static class ControllerEventPool
    {
        private static readonly Dictionary<Type, IControllerEvent> controllerEvents = new Dictionary<Type, IControllerEvent>();

        public static TEvent CreateEvent<TEvent, TArg>(TArg arg) where TEvent : IControllerEvent
        {
            IControllerEvent controllerEvent = GetEvent<TEvent>();

            if (controllerEvent == null)
            {
                controllerEvent = (IControllerEvent)Activator.CreateInstance(typeof(TEvent), new object[] { arg });
                controllerEvents.Add(typeof(TEvent), controllerEvent);
            }
            else if (controllerEvent is IControllerEventWithArgs<TArg>)
            {
                (controllerEvent as IControllerEventWithArgs<TArg>).Update(arg);
            }

            return (TEvent)controllerEvent;
        }

        public static TEvent CreateEvent<TEvent>() where TEvent : IControllerEvent, new()
        {
            TEvent controllerEvent = GetEvent<TEvent>();

            if (controllerEvent == null)
            {
                controllerEvent = new TEvent();
                controllerEvents.Add(typeof(TEvent), controllerEvent);
            }

            return controllerEvent;
        }

        private static TControllerEvent GetEvent<TControllerEvent>()
        {
            TControllerEvent controllerEvent = default;

            bool isExist = controllerEvents.TryGetValue(typeof(TControllerEvent), out IControllerEvent cachedEvent);

            if (isExist && cachedEvent is TControllerEvent)
            {
                controllerEvent = (TControllerEvent)cachedEvent;
            }

            return controllerEvent;
        }
    }
}