using PaintAstic.Global;
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

        private List<PlayerController> _pooledPlayers;
        [SerializeField] private List<Vector3> _spawnPos;

        [SerializeField] private int _maxPlayer;


        private void OnEnable()
        {
            EventManager.StartListening("Move", MovePlayer);
        }

        private void OnDisable()
        {
            EventManager.StopListening("Move", MovePlayer);
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
            _spawnPos.Add(_playingGrid.gridList[_playingGrid._height - 1, _playingGrid._width -1].transform.position);
            _spawnPos.Add(_playingGrid.gridList[_playingGrid._height - 1, 0].transform.position);
            _spawnPos.Add(_playingGrid.gridList[0, _playingGrid._width - 1].transform.position);

            SpawnPlayer();
        }

        void MovePlayer(object data)
        {
            MoveMessage moveMessage = (MoveMessage)data;

            _pooledPlayers[moveMessage._playerId].Move(moveMessage._move);
        }

        void SpawnPlayer()
        {
            for (int i = 0; i < _maxPlayer; i++)
            {
                var spawnPos = new Vector3(_spawnPos[i].x , 5 , _spawnPos[i].z);
                
                PlayerController player = Instantiate(_player, spawnPos, Quaternion.identity, transform);
                player.playerIndex = i;

                _pooledPlayers.Add(player);
            }
        }
    }
}