using System;
using UnityEngine;

namespace CubeApplication.View
{
    public class CubeView : MonoBehaviour
    {
        public event Action OnMouseDownEvent;

        [SerializeField]
        private MeshRenderer meshRenderer;

        public MeshRenderer MeshRenderer => meshRenderer;

        private void OnMouseDown()
        {
            OnMouseDownEvent?.Invoke();
        }
    }
}