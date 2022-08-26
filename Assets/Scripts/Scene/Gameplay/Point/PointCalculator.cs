using PaintAstic.Module.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;
using System.Linq;
using PaintAstic.Module.Message;
namespace PaintAstic.Module.Point
{
    public class PointCalculator : MonoBehaviour
    {
        [SerializeField] private List<int> _playerPointList;
        [SerializeField] private PlayerSpawner playerSpawn;

        private void OnEnable()
        {
            EventManager.StartListening("AddPoint", AddPoint);
        }
        private void OnDisable()
        {
            EventManager.StopListening("AddPoint", AddPoint);
        }
        private void Awake()
        {
            _playerPointList = new List<int>();
        }

        private void Start()
        {
            for (int i = 0; i < playerSpawn._maxPlayer; i++)
            {
                _playerPointList.Add(0);
            }
        }
        void AddPoint(object pointData)
        {
            AddPointMessage message = (AddPointMessage)pointData;
            _playerPointList[message.indexPlayer] += message.amountPoint;
        }
    }
}

