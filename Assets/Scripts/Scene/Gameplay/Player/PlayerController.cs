using PaintAstic.Global;
using PaintAstic.Module.Message;
using UnityEngine;

namespace PaintAstic.Module.Player
{
    public class PlayerController : MonoBehaviour
    {
        private float _smoothSpeed = 1;
        private float _timer = 0;
        public int playerIndex { get; set; }
        public int currentX { get; private set; }
        public int currentZ { get; private set; }
        public Vector3 lerpPos { get; set; }

        private Vector3 _movement;
        private Vector3 _desiredPosition;
        private Vector3 _smoothPosition;

        public void Move(Vector3 move)
        {
            _movement = move;
            if (_movement == Vector3.left && currentX == 0)
            {
                return;
            }
            if (_movement == Vector3.right && currentX == 7)
            {
                return;
            }
            if (_movement == Vector3.forward && currentZ == 7)
            {
                return;
            }
            if (_movement == Vector3.back && currentZ == 0)
            {
                return;
            }
            _desiredPosition = transform.position + _movement;

            _smoothPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);
            _timer += Time.deltaTime;
            transform.position = _smoothPosition;

            if (_timer >= 0.4f)
            {
                _timer = 0;
            }
            
        }
        private void OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.CompareTag("Tile"))
            {
                currentX = collision.gameObject.GetComponent<Tile>().tileIndexX;
                currentZ = collision.gameObject.GetComponent<Tile>().tileIndexZ;
                EventManager.TriggerEvent("SetColor", new SetColorMessage(playerIndex, currentX, currentZ));
            }
        }
    }
}



