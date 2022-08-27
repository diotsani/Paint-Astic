using PaintAstic.Global;
using PaintAstic.Module.GridSystem;
using PaintAstic.Module.Message;
using PaintAstic.Scene.Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.ItemSpawner
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private ItemCollectPoint _collectPointPrefab;
        [SerializeField] private ItemBomb _bombPrefab;

        [SerializeField] private PlayingGrid _gridManager;
        [SerializeField] private float _spawnDelay = 2f;

        private bool _isRunning;
        private float _spawnDelayTimer;
        private int _prevX;
        private int _prevZ;

        private List<ItemCollectPoint> _collectPointPool = new List<ItemCollectPoint>();
        private List<ItemBomb> _bombPool = new List<ItemBomb>();

        private void OnEnable()
        {
            EventManager.StartListening("OnGamePauseMessage", OnGamePause);
            EventManager.StartListening("OnGameContinueMessage", OnGameContinue);
            EventManager.StartListening("SendPlayerData", OnPlayerTouch);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnGamePauseMessage", OnGamePause);
            EventManager.StopListening("OnGameContinueMessage", OnGameContinue);
            EventManager.StopListening("SendPlayerData", OnPlayerTouch);
        }

        private void Start()
        {
            _prevX = -1;
            _prevZ = -1;
        }

        private void Update()
        {
            _spawnDelayTimer += Time.deltaTime;
            if (_spawnDelayTimer > _spawnDelay)
            {
                SpawnRandomItem();
                _spawnDelayTimer = 0;
            }
        }

        private void SpawnRandomItem()
        {
            int random = Random.Range(0, 2);
            switch (random)
            {
                case 0: SpawnCollectPoint(); break;
                case 1: SpawnBombItem(); break;
            }
        }

        private void SpawnCollectPoint()
        {
            ItemCollectPoint collectPoint = _collectPointPool.Find(i => !i.gameObject.activeSelf);
            if (collectPoint == null)
            {
                collectPoint = InstantiateItem(_collectPointPrefab, _collectPointPool);
            }

            ConfigSpawnedItem(collectPoint);
        }

        private void SpawnBombItem()
        {
            ItemBomb bomb = _bombPool.Find(i => !i.gameObject.activeSelf);
            if (bomb == null)
            {
                bomb = InstantiateItem(_bombPrefab, _bombPool);
            }

            ConfigSpawnedItem(bomb);
        }

        private T InstantiateItem<T>(T prefab, List<T> pool) where T : BaseItem
        {
            T baseItem = Instantiate(prefab, transform);
            pool.Add(baseItem);

            return baseItem;
        }

        private void ConfigSpawnedItem(BaseItem baseItem)
        {
            baseItem.transform.position = new Vector3(Random.Range(0, _gridManager.row), 1, Random.Range(0, _gridManager.column));
            while (((baseItem.transform.position.x == _prevX) && (baseItem.transform.position.z == _prevZ)) || 
                _gridManager.gridList[(int)baseItem.transform.position.x, (int)baseItem.transform.position.z].isStepped)
            {
                baseItem.transform.position = new Vector3(Random.Range(0, _gridManager.row), 1, Random.Range(0, _gridManager.column));
            }
            _prevX = (int)baseItem.transform.position.x;
            _prevZ = (int)baseItem.transform.position.z;
            baseItem.itemIndexX = (int)baseItem.transform.position.x;
            baseItem.itemIndexZ = (int)baseItem.transform.position.z;
            baseItem.maxX = _gridManager.row;
            baseItem.maxZ = _gridManager.column;
            baseItem.gameObject.SetActive(true);
        }

        private void OnGamePause()
        {
            Time.timeScale = 0f;
        }

        private void OnGameContinue()
        {
            Time.timeScale = 1f;
        }

        private void OnPlayerTouch(object data)
        {
            PlayerDataMessage playerData = (PlayerDataMessage)data;

            for (int i = 0; i < _collectPointPool.Count; i++)
            {
                if (_collectPointPool[i].itemIndexX == playerData.currentX && _collectPointPool[i].itemIndexZ == playerData.currentZ)
                {
                    _collectPointPool[i].OnCollided(playerData.playerIndex, playerData.isDoublePoint);
                    _collectPointPool[i].ResetIndex();
                }
            }
            for (int i = 0; i < _bombPool.Count; i++)
            {
                if (_bombPool[i].itemIndexX == playerData.currentX && _bombPool[i].itemIndexZ == playerData.currentZ)
                {
                    _bombPool[i].OnCollided(playerData.playerIndex);
                    _bombPool[i].ResetIndex();
                }
            }
        }
    }
}

