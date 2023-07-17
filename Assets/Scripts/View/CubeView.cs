using System;
using UnityEngine;

namespace CubeApplication.View
{
    public class CubeView : MonoBehaviour
    {
        public event Action OnMouseDownEvent;

        [SerializeField]
        private MeshRenderer meshRenderer;

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;
        }

        private void OnMouseDown()
        {
            OnMouseDownEvent?.Invoke();
        }
    }
}