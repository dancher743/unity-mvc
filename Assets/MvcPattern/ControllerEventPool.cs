using System;
using System.Collections.Generic;

namespace MvcPattern
{
    public class ControllerEventPool
    {
        private Dictionary<Type, IControllerEvent> controllerEvents;

        private static ControllerEventPool instance;

        private static ControllerEventPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ControllerEventPool();
                }

                return instance;
            }
        }

        private ControllerEventPool()
        {
            controllerEvents = new Dictionary<Type, IControllerEvent>();
        }

        public static TEvent CreateEvent<TEvent, TArg>(TArg arg) where TEvent : IControllerEvent
        {
            return Instance.CreateEventInstance<TEvent, TArg>(arg);
        }

        public static TEvent CreateEvent<TEvent>() where TEvent : IControllerEvent, new()
        {
            return Instance.CreateEventInstance<TEvent>();
        }

        public static void Clear()
        {
            Instance.controllerEvents.Clear();
        }

        private TEvent CreateEventInstance<TEvent, TArg>(TArg arg) where TEvent : IControllerEvent
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

        private TEvent CreateEventInstance<TEvent>() where TEvent : IControllerEvent, new()
        {
            TEvent controllerEvent = GetEvent<TEvent>();

            if (controllerEvent == null)
            {
                controllerEvent = new TEvent();
                controllerEvents.Add(typeof(TEvent), controllerEvent);
            }

            return controllerEvent;
        }

        private TControllerEvent GetEvent<TControllerEvent>()
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