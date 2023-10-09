using UnityEngine;

namespace CubeApplication.Events
{
    public readonly struct CubeColorData
    {
        public Color Color { get; }

        public CubeColorData(Color color)
        {
            Color = color;
        }
    }
}