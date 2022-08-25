using PaintAstic.Global;
using PaintAstic.Module.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace PaintAstic.Module.Player
{
    public class PlayerController : MonoBehaviour
    {
        private float _smoothSpeed = 1;
        public int playerIndex { get; set; }
        private int _currentX;
        private int _currentZ;

        private Vector3 _movement;
        private Vector3 _desiredPosition;
        private Vector3 _smoothPosition;

        public void Move(Vector3 move)
        {
            _movement = move;
            if(_movement == Vector3.left && _currentX == 0)
            {
                return;
            }
            if (_movement == Vector3.right && _currentX == 7)
            {
                return;
            }
            if (_movement == Vector3.forward && _currentZ == 7)
            {
                return;
            }
            if (_movement == Vector3.back && _currentZ == 0)
            {
                return;
            }

            _desiredPosition = transform.position + _movement;
            _smoothPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);
            transform.position = _smoothPosition;
        }
        private void OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.CompareTag("Tile"))
            {
                _currentX = collision.gameObject.GetComponent<Tile>().tileIndexX;
                _currentZ = collision.gameObject.GetComponent<Tile>().tileIndexZ;
                EventManager.TriggerEvent("SetColor", new SetColorMessage(playerIndex, _currentX, _currentZ));
            }
        }
    }
}



