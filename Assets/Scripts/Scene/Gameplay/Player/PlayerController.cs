using PaintAstic.Global;
using PaintAstic.Module.Message;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using PaintAstic.Module.Tiles;
using PaintAstic.Module.GridSystem;

namespace PaintAstic.Module.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayingGrid _gridManager;

        private float _smoothSpeed = 1;
        private float _intervalMove = 0.2f;
        private float _timerIsMovable = 0;
        private float _timerLastCollectPoint = 0;
        private float _intervalLastCollectPoint = 7f;

        public bool isDoublePoint = true;
        public int playerIndex { get; set; }
        public int currentX { get; private set; }
        public int currentZ { get; private set; }
        public Vector3 lerpPos { get; set; }

        private Vector3 _movement;
        private Vector3 _desiredPosition;
        private Vector3 _smoothPosition;

        private bool isMovable;

        private void Update()
        {
            _timerIsMovable += Time.deltaTime;
            _timerLastCollectPoint += Time.deltaTime;

            if(_timerIsMovable > _intervalMove)
            {
                isMovable = true;
            }

            if (_timerLastCollectPoint > _intervalLastCollectPoint)
            {
                isDoublePoint = false;
            }
        }

        public void Move(Vector3 move)
        {
            if (isMovable)
            {
                _movement = move;
                if (_movement == Vector3.left && currentX == 0)
                {
                    return;
                }
                if (_movement == Vector3.right && currentX == _gridManager.row - 1)
                {
                    return;
                }
                if (_movement == Vector3.forward && currentZ == _gridManager.column - 1)
                {
                    return;
                }
                if (_movement == Vector3.back && currentZ == 0)
                {
                    return;
                }
                _desiredPosition = transform.position + _movement;

                _smoothPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);

                transform.position = _smoothPosition;
                isMovable = false;
                _timerIsMovable = 0;
                EventManager.TriggerEvent("PlayMoveMessage");
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

        public void SetDependencies(PlayingGrid playingGrid)
        {
            _gridManager = playingGrid;
        }

        public void ResetLastCollectPoint()
        {
            _timerLastCollectPoint = 0;
            isDoublePoint = true;
        }
    }
}



