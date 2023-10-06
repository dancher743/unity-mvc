using MvcPattern;

namespace CubeApplication.Events
{
    public struct CubeColorEvent
    {
        public string Color { get; private set; }

        public CubeColorEvent(string color)
        {
            Color = color;
        }
    }
}