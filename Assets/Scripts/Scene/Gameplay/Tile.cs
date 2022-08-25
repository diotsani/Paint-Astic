using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;

public class Tile : MonoBehaviour
{
    [SerializeField]private int _tileIndex;
    private Color defaultColor = Color.gray;
    [SerializeField]private Color[] playerColor = { Color.red, Color.yellow };
    void Start()
    {
    }
    
    public void SetIndexTile(int tileIndex)
    {
        _tileIndex = tileIndex;
    }
    public void DefaultColors()
    {
        gameObject.GetComponent<Renderer>().material.color = defaultColor;
    }

    public void ChangeColors(int indexPlayer)
    {
        //int colorIndex = (int)indexPlayer;
        gameObject.GetComponent<Renderer>().material.color = playerColor[indexPlayer];
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            EventManager.TriggerEvent("SetIndexTile", _tileIndex);
        }
    }
}
