using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingGrid : MonoBehaviour
{
    int _height = 8;
    int _width = 8;
    private float _gridSpace = 1f;
    [SerializeField] private GameObject gridPrefab;
    public Vector3 gridOrigin = Vector3.zero;
    [SerializeField] private List<GameObject> gridList = new List<GameObject>();
    public int _amountColorOne;
    private void Awake()
    {
        CreateGrid();
    }
    void Start()
    {
        //OnHitPlayerOne();
        
    }
    void Update()
    {
        CalculateGridColor();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < _height; x++)
        {
            for (int z = 0; z < _width; z++)
            {
                Vector3 spawnPosition = new Vector3(x * _gridSpace, 0, z * _gridSpace) + gridOrigin;
                Spawn(spawnPosition, Quaternion.identity);
            }
        }
    }
    void Spawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject gridObjects = Instantiate(gridPrefab, positionToSpawn, rotationToSpawn, transform);
        gridList.Add(gridObjects);
    }
    void OnHitPlayerOne() // Need Event On Trigger in Script Player
    {
        foreach (GameObject grid_1 in gridList)
        {
            grid_1.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    void CalculateGridColor() // Need Event On Trigger with Item collect Point
    {
        foreach (GameObject gridColors in gridList)
        {
            for (int i = 0; i < gridList.Count; i++)
            {
                if (gridColors.GetComponent<MeshRenderer>().material.color == Color.red)
                {
                    _amountColorOne = gridList.Count;
                    Debug.Log(gridList.Count);
                }
            }
            
        }
    }
}
