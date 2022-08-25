using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;
using PaintAstic.Module.Message;

public class Tile : MonoBehaviour
{
    [SerializeField]public int tileIndexX { get; private set; }
    [SerializeField]public int tileIndexZ { get; private set; }
    private Color defaultColor = Color.gray;
    [SerializeField]private Color[] playerColor = { Color.red, Color.yellow };
    void Start()
    {
    }
    
    public void SetIndexTile(int tileIndexX, int tileIndexZ)
    {
        this.tileIndexX = tileIndexX;
        this.tileIndexZ = tileIndexZ;
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
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Player"))
    //    {
    //        EventManager.TriggerEvent("SetIndexTile", new TileIndexMessage(tileIndexX, tileIndexZ));
    //    }
    //}
}
