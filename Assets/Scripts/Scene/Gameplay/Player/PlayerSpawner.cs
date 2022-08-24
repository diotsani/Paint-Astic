using PaintAstic.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private int _maxPlayer;
        private List<PlayerController> _pooledPlayers;

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
                PlayerController player = Instantiate(_player, transform);
                
                _pooledPlayers.Add(player);
            }
        }
    }
}