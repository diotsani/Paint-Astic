using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Inputs
{
    [System.Serializable]
    public struct InputConfig
    {
        [SerializeField] private KeyCode _moveUp;
        [SerializeField] private KeyCode _moveDown;
        [SerializeField] private KeyCode _moveLeft;
        [SerializeField] private KeyCode _moveRight;

        public KeyCode moveUp => _moveUp;
        public KeyCode moveDown => _moveDown;
        public KeyCode moveLeft => _moveLeft;

        public KeyCode moveRight => _moveRight;
    }
}
