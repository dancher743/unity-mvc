using UnityEngine;
using UnityEngine.UI;

namespace CubeApplication.Views
{
    public class UIView : MonoBehaviour
    {
        [SerializeField]
        private Text colorText;

        public Text ColorText => colorText;
    }
}