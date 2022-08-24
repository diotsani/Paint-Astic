using PaintAstic.Global;
using UnityEngine;
using UnityEngine.Events;

namespace PaintAstic.Module.Player
{
    public class PlayerController : MonoBehaviour
    {
        private float _smoothSpeed = 1;

        private Vector3 _movement;
        private Vector3 _desiredPosition;
        private Vector3 _smoothPosition;

        int playerIndex = 1;

        public void Move(Vector3 move)
        {
            _movement = move;

            _desiredPosition = transform.position + _movement;
            _smoothPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);
            transform.position = _smoothPosition;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Tile"))
            {
                Tile tiles = collision.gameObject.GetComponent<Tile>();
                tiles.ChangeColors(playerIndex);
            }
        }
    }
}



