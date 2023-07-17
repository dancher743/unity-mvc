using MvcPattern;

namespace CubeApplication.Event
{
    public class CubeColorEvent : IControllerEventWithArgs<string>
    {
        public string Color { get; private set; }

        public CubeColorEvent(string color)
        {
            Color = color;
        }

        public void Update(string color)
        {
            Color = color;
        }
    }
}