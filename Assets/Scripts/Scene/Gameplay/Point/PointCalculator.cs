using PaintAstic.Module.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;
using System.Linq;
using PaintAstic.Module.Message;
using PaintAstic.Global.Config;

namespace PaintAstic.Module.Point
{
    public class PointCalculator : MonoBehaviour
    {
        [SerializeField] private List<int> _playerPointList;
        [SerializeField] private PlayerSpawner playerSpawn;

        private int _doublePoint = 2;

        private void OnEnable()
        {
            EventManager.StartListening("AddPoint", AddPoint);
            EventManager.StartListening("OnGameOverMessage", OnGameOver);
        }
        private void OnDisable()
        {
            EventManager.StopListening("AddPoint", AddPoint);
            EventManager.StopListening("OnGameOverMessage", OnGameOver);
        }
        private void Awake()
        {
            _playerPointList = new List<int>();
        }

        private void Start()
        {
            for (int i = 0; i < playerSpawn.maxPlayer; i++)
            {
                _playerPointList.Add(0);
            }
        }
        void AddPoint(object pointData)
        {
            AddPointMessage message = (AddPointMessage)pointData;
            if (message.isDoublePoint)
            {
                _playerPointList[message.indexPlayer] += message.amountPoint * _doublePoint;
            }
            else
            {
                _playerPointList[message.indexPlayer] += message.amountPoint;
            }
            EventManager.TriggerEvent("UpdatePointMessage", new UpdatePointMessage(message.indexPlayer, _playerPointList[message.indexPlayer]));
        }

        void OnGameOver()
        {
            int highestPoint = _playerPointList[0];
            //int player1 = _playerPointList[0];
            int winnerIndex = 0;
            bool isDraw = true;
            List<int> playerPointList = new List<int>();

            for (int i = 0; i < ConfigData.configInstance.playerNumbers; i++)
            {
                playerPointList.Add(_playerPointList[i]);
            }
            
            for (int i = 0; i < _playerPointList.Count; i++)
            {
                if (highestPoint < _playerPointList[i])
                {
                    highestPoint = _playerPointList[i];
                    winnerIndex = i;
                    isDraw = false;
                }
            }
            for (int i = 1; i < _playerPointList.Count; i++)
            {
                if (highestPoint > _playerPointList[i])
                {
                    isDraw = false;
                }
            }
            
            EventManager.TriggerEvent("WinnerMessage", new WinnerMessage(winnerIndex, highestPoint, isDraw));
        }
    }
}

