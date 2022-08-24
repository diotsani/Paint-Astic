using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;

public class PlayingGrid : MonoBehaviour
{
    private int _height = 8;
    private int _width = 8;
    private float _gridSpace = 1f;
    public Vector3 gridOrigin = Vector3.zero;

    [SerializeField] private Tile gridPrefab;
    private List<Tile> gridList = new List<Tile>();
    Tile[,] tiles;

    //Useless
    private int _amountColorOne;
    private int _amountColorTwo;
    private Color defaultColor = Color.gray;

    private void Awake()
    {
        CreateGrid();
    }
    private void CreateGrid()
    {
        tiles = new Tile[_height,_width];
        for (int x = 0; x < _height; x++)
        {
            for (int z = 0; z < _width; z++)
            {
                Vector3 spawnPosition = new Vector3(x * _gridSpace, 0, z * _gridSpace) + gridOrigin;
                Tile gridObjects = Instantiate(gridPrefab, spawnPosition, Quaternion.identity, transform);
                tiles[x, z] = gridObjects;
                gridObjects.DefaultColors();
                gridList.Add(gridObjects);
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
