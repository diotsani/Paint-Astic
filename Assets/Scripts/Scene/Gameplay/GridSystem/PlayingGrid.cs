using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;
using System.Linq;
using PaintAstic.Module.Message;
using PaintAstic.Module.Tiles;

namespace PaintAstic.Module.GridSystem
{
    public class PlayingGrid : MonoBehaviour
    {
        public int row { get; private set; } = 8;
        public int column { get; private set; } = 8;
        private float _gridSpace = 1f;
        [SerializeField] private Tile gridPrefab;
        public Vector3 gridOrigin = Vector3.zero;

        private List<GameObject> tileList = new List<GameObject>();
        public List<GameObject> getScore = new List<GameObject>();
        public Tile[,] gridList { get; private set; }

        [SerializeField] private int _amountColorTile;
        [SerializeField] private int _currentIndexTileX;
        [SerializeField] private int _currentIndexTileZ;

        private void Awake()
        {
            CreateGrid();
        }
        private void OnEnable()
        {
            EventManager.StartListening("SetColor", SetColorTile);
            EventManager.StartListening("CollectPointMessage", GetScoreCollect);
            EventManager.StartListening("RevertTilesMessage", RevertTiles);

        }
        private void OnDisable()
        {
            EventManager.StopListening("SetColor", SetColorTile);
            EventManager.StopListening("CollectPointMessage", GetScoreCollect);
            EventManager.StopListening("RevertTilesMessage", RevertTiles);
        }
        public void GetIndexTile(object indexTile)
        {
            TileIndexMessage tileIndexMessage = (TileIndexMessage)indexTile;
            _currentIndexTileX = tileIndexMessage.tileIndexX;
            _currentIndexTileZ = tileIndexMessage.tileIndexZ;
        }
        public void SetColorTile(object indexPlayer)
        {
            SetColorMessage colorIndex = (SetColorMessage)indexPlayer;
            gridList[colorIndex.playerXPos, colorIndex.playerZPos].ChangeColors(colorIndex.playerIndex);
        }
        private void CreateGrid()
        {
            gridList = new Tile[row, column];
            for (int x = 0; x < row; x++)
            {
                for (int z = 0; z < column; z++)
                {
                    Vector3 spawnPosition = new Vector3(x * _gridSpace, 0, z * _gridSpace) + gridOrigin;
                    Tile gridObjects = Instantiate(gridPrefab, spawnPosition, Quaternion.identity, transform);

                    gridList[x, z] = gridObjects;

                    tileList.Add(gridObjects.gameObject);
                    gridObjects.gameObject.name = "Tile( " + ("X:" + x + " ,Z:" + z + " )");
                    gridObjects.DefaultColors();
                    gridObjects.SetIndexTile(x, z);
                }
            }
        }

        public void GetScoreCollect(object indexPlayer)
        {
            CollectPointMessage message = (CollectPointMessage)indexPlayer;
            for (int x = 0; x < row; x++)
            {
                for (int z = 0; z < column; z++)
                {
                    if (gridList[x, z]._tileIndexColor == message.indexPlayer + 1)
                    {
                        _amountColorTile++;
                        gridList[x, z].DefaultColors();
                    }
                }
            }
            EventManager.TriggerEvent("AddPoint", new AddPointMessage(message.indexPlayer, _amountColorTile, message.isDoublePoint));
            _amountColorTile = 0;
        }

        public void RevertTiles(object indexPlayer)
        {
            int index = (int)indexPlayer;
            for (int x = 0; x < row; x++)
            {
                for (int z = 0; z < column; z++)
                {
                    if (gridList[x, z]._tileIndexColor == index + 1)
                    {
                        gridList[x, z].DefaultColors();
                    }
                }
            }
        }
    }
}

