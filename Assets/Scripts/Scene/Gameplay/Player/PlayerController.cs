using PaintAstic.Global;
using PaintAstic.Module.GridSystem;
using PaintAstic.Module.Message;
using UnityEngine;

namespace PaintAstic.Module.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayingGrid _gridManager;
        [SerializeField] private MeshRenderer _mesh;
        [SerializeField] private SkinnedMeshRenderer mat;

        private float _smoothSpeed = 1;
        private float _intervalMove = 0.3f;
        private float _movingTimer = 0f;
        private float _movingTime = 0.2f;
        private float _timerIsMovable = 0;
        private float _timerLastCollectPoint = 0;
        private float _intervalLastCollectPoint = 7f;
        private bool _isMovable;
        private bool _isMoving;
        private int _maxX;
        private int _maxZ;
        private Vector3 _desiredPos;

        public bool isDoublePoint { get; private set; } = true;
        public int playerIndex { get; set; }
        public int currentX { get; private set; }
        public int currentZ { get; private set; }
        public int lastX { get; private set; }
        public int lastZ { get; private set; }

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

            if (_timerIsMovable > _intervalMove)
            {
                _isMovable = true;
            }

            if (_timerLastCollectPoint > _intervalLastCollectPoint)
            {
                isDoublePoint = false;
            }

            if (_isMoving)
            {
                if (_movingTimer < _movingTime)
                {
                    transform.position = Vector3.Lerp(transform.position, _desiredPos, _movingTimer / _movingTime);
                    _movingTimer += Time.deltaTime;
                }
                else
                {
                    _movingTimer = 0;
                    currentX = (int)_desiredPos.x;
                    currentZ = (int)_desiredPos.z;
                    EventManager.TriggerEvent("SendPlayerData", new PlayerDataMessage(currentX, currentZ, lastX, lastZ, playerIndex, isDoublePoint));
                    _isMoving = false;
                }

            }
        }

        public void Move(Vector2Int move)
        {
            if (_isMovable)
            {
                var nextMove = new Vector2Int(currentX + move.x, currentZ + move.y);
                if (nextMove.x < 0 || nextMove.x > _maxX || nextMove.y < 0
                    || nextMove.y > _maxZ || _gridManager.gridList[nextMove.x, nextMove.y].isStepped)
                {
                    return;
                }
                lastX = currentX;
                lastZ = currentZ;
                _desiredPos = _gridManager.gridList[nextMove.x, nextMove.y].transform.position;
                _isMoving = true;
                _isMovable = false;
                _timerIsMovable = 0;
                EventManager.TriggerEvent("PlayMoveMessage");
            }

        }

        public void SetDependencies(PlayingGrid playingGrid)
        {
            _gridManager = playingGrid;
        }

        public void ChangePlayerColor(Color color)
        {
            _mesh.material.color = color;
            for (int i = 0; i < 2; i++)
            {
                //mat.materials[i].color = color;
            }
            
        }

        public void ResetLastCollectPoint()
        {
            _timerLastCollectPoint = 0;
            isDoublePoint = true;
        }
    }
}



