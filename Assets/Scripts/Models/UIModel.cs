using System;

namespace CubeApplication.Models
{
    public class UIModel
    {
        public event Action<string> ColorTextChanged;

        private string colorText;

        public string ColorText
        {
            set
            {
                colorText = value;
                ColorTextChanged?.Invoke(colorText);
            }
        }
    }
}