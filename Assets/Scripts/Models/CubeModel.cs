using System;
using UnityEngine;

namespace CubeApplication.Models
{
    public class CubeModel
    {
        public event Action<Color> ColorChanged;

        private Color color;

        public void ChangeColor()
        {
            color = UnityEngine.Random.ColorHSV();
            ColorChanged?.Invoke(color);
        }
    }
}
