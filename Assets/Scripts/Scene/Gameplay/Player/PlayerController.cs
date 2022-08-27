using PaintAstic.Global;
using PaintAstic.Module.Message;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using PaintAstic.Module.Tiles;
using PaintAstic.Module.GridSystem;
using System.Collections.Generic;

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
        private int _maxX;
        private int _maxZ;

        public bool isDoublePoint { get; private set; } = true;
    public int playerIndex { get; set; }
        public int currentX { get; private set; }
        public int currentZ { get; private set; }
        public int lastX { get; private set; }
        public int lastZ { get; private set; }


        private bool isMovable;

        private void Start()
        {
            _maxX = _gridManager.row - 1;
            _maxZ = _gridManager.column - 1;

            currentX = (int)transform.position.x;
            currentZ = (int)transform.position.z;
        }

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

        public void Move(Vector2Int move)
        {
            if (isMovable)
            {
                var nextMove = new Vector2Int(currentX + move.x, currentZ + move.y);
                if (nextMove.x < 0 || nextMove.x > _maxX || nextMove.y < 0
                    || nextMove.y > _maxZ || _gridManager.gridList[nextMove.x, nextMove.y].isStepped)
                {
                    return;
                }
                lastX = currentX;
                lastZ = currentZ;

                Vector3 desiredPos = _gridManager.gridList[nextMove.x, nextMove.y].transform.position;

                Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, _smoothSpeed);


                transform.position = smoothPos;
                currentX = (int)smoothPos.x;
                currentZ = (int)smoothPos.z;
                EventManager.TriggerEvent("SendPlayerData", new PlayerDataMessage(currentX, currentZ, lastX, lastZ, playerIndex, isDoublePoint));

                isMovable = false;
                _timerIsMovable = 0;
                EventManager.TriggerEvent("PlayMoveMessage");
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



