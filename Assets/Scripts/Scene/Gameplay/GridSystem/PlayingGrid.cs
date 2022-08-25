using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;
using System.Linq;
using PaintAstic.Module.Message;

public class PlayingGrid : MonoBehaviour
{
    public int _height { get; private set; } = 8;
    public int _width { get; private set; } = 8;
    private float _gridSpace = 1f;
    [SerializeField] private Tile gridPrefab;
    public Vector3 gridOrigin = Vector3.zero;

    //private List<Tile> gridList = new List<Tile>();
    public Tile[,] gridList { get; private set; }

    [SerializeField] private int _currentIndexTileX;
    [SerializeField] private int _currentIndexTileZ;

    private Queue<int> _indexQueueX;
    private Queue<int> _indexQueueZ;

    //Useless
    private int _amountColorOne;
    private int _amountColorTwo;
    private Color defaultColor = Color.gray;

    private void Awake()
    {
        CreateGrid();

        _indexQueueX = new Queue<int>();
    }
    private void OnEnable()
    {
        //EventManager.StartListening("SetIndexTile", GetIndexTile);
        EventManager.StartListening("SetColor", SetColorTile);
        
    }
    private void OnDisable()
    {
        //EventManager.StopListening("SetIndexTile", GetIndexTile);
        EventManager.StopListening("SetColor", SetColorTile);
    }
    public void GetIndexTile(object indexTile)
    {
        TileIndexMessage tileIndexMessage = (TileIndexMessage)indexTile;
        _currentIndexTileX = tileIndexMessage.tileIndexX;
        _currentIndexTileZ = tileIndexMessage.tileIndexZ;

        _indexQueueX.Enqueue(_currentIndexTileX);
        _indexQueueZ.Enqueue(_currentIndexTileZ);
    }
    public void SetColorTile(object indexPlayer)
    {
        SetColorMessage colorIndex = (SetColorMessage)indexPlayer;
        gridList[colorIndex.playerXPos, colorIndex.playerZPos].ChangeColors(colorIndex.playerIndex);
    }
    private void CreateGrid()
    {
        gridList = new Tile[_height, _width];
        for (int x = 0; x < _height; x++)
        {
            for (int z = 0; z < _width; z++)
            {
                Vector3 spawnPosition = new Vector3(x * _gridSpace, 0, z * _gridSpace) + gridOrigin;
                Tile gridObjects = Instantiate(gridPrefab, spawnPosition, Quaternion.identity, transform);
                gridList[x, z] = gridObjects;

                gridObjects.gameObject.name = "Tile( " + ("X:" + x + " ,Z:" + z + " )");
                gridObjects.DefaultColors();
                gridObjects.SetIndexTile(x,z);
            }
        }
    }


    public void OnHitPlayerOne(GameObject obj) // Need Event On Trigger in Script Player
    {
        if (obj.GetComponent<MeshRenderer>().material.color == defaultColor)
        {
            _amountColorOne += 1;
            obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        if (obj.GetComponent<MeshRenderer>().material.color == Color.red)
        {
            _amountColorOne += 1;
            _amountColorTwo -= 1;
            obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }
    public void OnHitPlayerTwo(GameObject obj) // Need Event On Trigger in Script Player
    {
        if (obj.GetComponent<MeshRenderer>().material.color == defaultColor)
        {
            _amountColorTwo += 1;
            obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        if (obj.GetComponent<MeshRenderer>().material.color == Color.yellow)
        {
            _amountColorOne -= 1;
            _amountColorTwo += 1;
            obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    public void OnHitPlayerOneRender(Renderer obj) // Need Event On Trigger in Script Player
    {
        if (obj.material.color == defaultColor)
        {
            _amountColorOne += 1;
            obj.material.color = Color.yellow;
        }

        if (obj.material.color == Color.red)
        {
            _amountColorOne += 1;
            _amountColorTwo -= 1;
            obj.material.color = Color.yellow;
        }
    }
    public void OnHitPlayerTwoRender(Renderer obj) // Need Event On Trigger in Script Player
    {
        if (obj.material.color == defaultColor)
        {
            _amountColorTwo += 1;
            obj.material.color = Color.red;
        }

        if (obj.material.color == Color.yellow)
        {
            _amountColorOne -= 1;
            _amountColorTwo += 1;
            obj.material.color = Color.red;
        }
    }
}
