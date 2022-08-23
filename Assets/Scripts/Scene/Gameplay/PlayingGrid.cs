using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingGrid : MonoBehaviour
{
    int _height = 8;
    int _width = 8;
    public float _gridSpace = 1f;
    [SerializeField] private GameObject gridPrefab;
    public Vector3 gridOrigin = Vector3.zero;
    private GameObject[,] gridSize;
    private List<GameObject> gridObject;
    void Start()
    {
        SpawnGrid();
        
    }
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        gridSize = new GameObject[_height, _width];
        if(gridPrefab == null)
        {
            Debug.Log("Empty");
            return;
        }
    }

    private void SpawnGrid()
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
        GameObject gridObjects = Instantiate(gridPrefab, positionToSpawn, rotationToSpawn,transform);
    }
}
