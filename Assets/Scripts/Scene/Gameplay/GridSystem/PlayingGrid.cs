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

    private List<GameObject> tileList = new List<GameObject>();
    public List<GameObject> getScore = new List<GameObject>();
    public Tile[,] gridList { get; private set; }

    [SerializeField]private int _amountColorTile;
    [SerializeField] private int _currentIndexTileX;
    [SerializeField] private int _currentIndexTileZ;

    private int _amountColorTwo;
    private Color defaultColor = Color.gray;

    private void Awake()
    {
        CreateGrid();
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

                tileList.Add(gridObjects.gameObject);
                gridObjects.gameObject.name = "Tile( " + ("X:" + x + " ,Z:" + z + " )");
                gridObjects.DefaultColors();
                gridObjects.SetIndexTile(x,z);
            }
        }
    }

    public void GetScoreCollect(int indexPlayer)
    {
        for (int x = 0; x < _height; x++)
        {
            for (int z = 0; z < _width; z++)
            {
                if(gridList[x,z]._tileIndexColor == indexPlayer+1)
                {
                    _amountColorTile++;
                }
            }
        }
    }


    #region Trash
    public void GetS()
    {
        foreach (GameObject item in tileList)
        {
            if (item.GetComponent<Tile>()._tileIndexColor == 1)
            {
                GameObject tiles = item.gameObject;
                getScore.Add(tiles);
                _amountColorTile = getScore.Count;
                Debug.Log(getScore.Count);
            }
        }
    }

    public void RemoveS()
    {
        for (int i = getScore.Count -1; i >= 0; i--)
        {
            getScore.RemoveAt(i);
        }
    }

    public void OnHitPlayerOne(GameObject obj) // Need Event On Trigger in Script Player
    {
        if (obj.GetComponent<MeshRenderer>().material.color == defaultColor)
        {
            _amountColorTile += 1;
            obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        if (obj.GetComponent<MeshRenderer>().material.color == Color.red)
        {
            _amountColorTile += 1;
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
            _amountColorTile -= 1;
            _amountColorTwo += 1;
            obj.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    public void OnHitPlayerOneRender(Renderer obj) // Need Event On Trigger in Script Player
    {
        if (obj.material.color == defaultColor)
        {
            _amountColorTile += 1;
            obj.material.color = Color.yellow;
        }

        if (obj.material.color == Color.red)
        {
            _amountColorTile += 1;
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
            _amountColorTile -= 1;
            _amountColorTwo += 1;
            obj.material.color = Color.red;
        }
    }
    #endregion
}
