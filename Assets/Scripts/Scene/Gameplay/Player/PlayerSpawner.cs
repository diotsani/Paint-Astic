using PaintAstic.Global;
using PaintAstic.Module.GridSystem;
using PaintAstic.Module.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private PlayingGrid _playingGrid;

        [SerializeField]public int _maxPlayer { get;} = 2;

        private List<PlayerController> _pooledPlayers;
        private List<Vector3> _spawnPos;

        private void OnEnable()
        {
            EventManager.StartListening("Move", MovePlayer);
            EventManager.StartListening("ResetLastCollectPointMessage", ResetLastCollectPoint);
        }

        private void OnDisable()
        {
            EventManager.StopListening("Move", MovePlayer);
            EventManager.StopListening("ResetLastCollectPointMessage", ResetLastCollectPoint);

        }

        void Start()
        {
            _pooledPlayers = new List<PlayerController>();
            _spawnPos = new List<Vector3>();

            SetPosition();

        }
        void SetPosition()
        {
            _spawnPos.Add(_playingGrid.gridList[0, 0].transform.position);
            _spawnPos.Add(_playingGrid.gridList[_playingGrid.row - 1, _playingGrid.column -1].transform.position);
            _spawnPos.Add(_playingGrid.gridList[_playingGrid.row - 1, 0].transform.position);
            _spawnPos.Add(_playingGrid.gridList[0, _playingGrid.column - 1].transform.position);

            SpawnPlayer();
        }

        void MovePlayer(object data)
        {
            MoveMessage moveMessage = (MoveMessage)data;

            _pooledPlayers[moveMessage.playerId].Move(moveMessage.move);
        }

        void SpawnPlayer()
        {
            for (int i = 0; i < _maxPlayer; i++)
            {
                var spawnPos = new Vector3(_spawnPos[i].x , 0f , _spawnPos[i].z);
                
                PlayerController player = Instantiate(_player, spawnPos, Quaternion.identity, transform);
                player.playerIndex = i;
                player.SetDependencies(_playingGrid);

                var tile = _playingGrid.gridList[(int)_spawnPos[i].x, (int)_spawnPos[i].z];
                tile.isStepped = true;
                tile.ChangeColors(i);

                //config player data
                _pooledPlayers.Add(player);
            }
        }

        void ResetLastCollectPoint(object index)
        {
            int playerIndex = (int)index;
            _pooledPlayers[playerIndex].ResetLastCollectPoint();
        }
    }
}