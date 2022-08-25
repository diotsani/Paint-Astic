using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;

public class PlayingGrid : MonoBehaviour
{
    private int _height = 8;
    private int _width = 8;
    private float _gridSpace = 1f;
    [SerializeField] private Tile gridPrefab;
    public Vector3 gridOrigin = Vector3.zero;

    [SerializeField] private Tile gridPrefab;
    private List<Tile> gridList = new List<Tile>();

    [SerializeField] private int _currentIndexTile;
    //Useless
    private int _amountColorOne;
    private int _amountColorTwo;
    private Color defaultColor = Color.gray;

    private void Awake()
    {
        CreateGrid();
    }
    private void OnEnable()
    {
        EventManager.StartListening("SetIndexTile", GetIndexTile);
        EventManager.StartListening("SetColor", SetColorTile);
        
    }
    private void OnDisable()
    {
        EventManager.StopListening("SetIndexTile", GetIndexTile);
        EventManager.StopListening("SetColor", SetColorTile);
    }
    
    public void GetIndexTile(object indexTile)
    {
        _currentIndexTile = (int)indexTile;
        Debug.Log(_currentIndexTile);
        
    }
    public void SetColorTile(object indexPlayer)
    {
        int colorIndex = (int)indexPlayer;
        gridList[_currentIndexTile].ChangeColors(colorIndex);
    }
    private void CreateGrid()
    {
        for (int x = 0; x < _height; x++)
        {
            for (int z = 0; z < _width; z++)
            {
                Vector3 spawnPosition = new Vector3(x * _gridSpace, 0, z * _gridSpace) + gridOrigin;
                Tile gridObjects = Instantiate(gridPrefab, spawnPosition, Quaternion.identity, transform);

                gridObjects.gameObject.name = "Tile( " + ("X:" + x + " ,Z:" + z + " )");
                gridObjects.DefaultColors();
                gridList.Add(gridObjects);
                gridObjects.SetIndexTile(gridList.Count - 1);
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
