using Mvc;
using UnityEngine;
using UnityEngine.UI;

namespace CubeApplication.Views
{
    public class UIView : MonoBehaviour, IView
    {
        [SerializeField]
        private Text colorText;

        public string ColorText
        {
            set
            {
                colorText.text = value;
            }
        }
    }
}