﻿using Mvc;
using System;
using UnityEngine;

namespace CubeApplication.Views
{
    public class CubeView : MonoBehaviour, IView
    {
        public event Action Clicked;

        [SerializeField]
        private MeshRenderer meshRenderer;

        public Color Color
        {
            set
            {
                meshRenderer.material.color = value;
            }
        }

        private void OnMouseDown()
        {
            Clicked?.Invoke();
        }
    }
}