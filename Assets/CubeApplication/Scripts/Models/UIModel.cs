using System;
using UnityEngine;

namespace CubeApplication.Models
{
    public class UIModel
    {
        public event Action<string> ColorChanged;

        private Color color;

        public Color Color
        {
            set
            {
                color = value;

                var colorText = GetColorText(color);
                ColorChanged?.Invoke(colorText);
            }
        }

        private string GetColorText(Color color)
        {
            return $"Current cube's color is {color}.";
        }
    }
}