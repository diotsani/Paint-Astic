using PaintAstic.Global;
using UnityEngine;
using UnityEngine.Events;

namespace PaintAstic.Module.Player
{
    public class PlayerController : MonoBehaviour
    {
        private float _smoothSpeed = 1;
        public int _playerIndex { get; set; }

        private Vector3 _movement;
        private Vector3 _desiredPosition;
        private Vector3 _smoothPosition;

        public void Move(Vector3 move)
        {
            _movement = move;

            _desiredPosition = transform.position + _movement;
            _smoothPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);
            transform.position = _smoothPosition;
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag("Tile"))
            {
                Debug.Log(other.gameObject);
                EventManager.TriggerEvent("SetColor", _playerIndex);
            }
        }
    }
}



