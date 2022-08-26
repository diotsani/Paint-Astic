using PaintAstic.Global;
using PaintAstic.Module.GridSystem;
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

        [SerializeField] PlayingGrid _gridManager;
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
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnGamePauseMessage", OnGamePause);
            EventManager.StopListening("OnGameContinueMessage", OnGameContinue);
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
            baseItem.transform.position = new Vector3(Random.Range(0, _gridManager.row), 2, Random.Range(0, _gridManager.column));
            while ((baseItem.transform.position.x == _prevX) && (baseItem.transform.position.z == _prevZ))
            {
                baseItem.transform.position = new Vector3(Random.Range(0, _gridManager.row), 2, Random.Range(0, _gridManager.column));
            }
            _prevX = (int)baseItem.transform.position.x;
            _prevZ = (int)baseItem.transform.position.z;
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
    }
}

