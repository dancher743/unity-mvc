using UnityEngine;

namespace CubeApplication.Messages
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