using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;

public class Tile : MonoBehaviour
{
    private Color defaultColor = Color.gray;
    [SerializeField]private Color[] playerColor = { Color.red, Color.yellow };
    void Start()
    {
    }
    private void OnEnable()
    {
        EventManager.StartListening("SetColor", ChangeColors);
    }
    private void OnDisable()
    {
        EventManager.StopListening("SetColor", ChangeColors);
    }
    public void DefaultColors()
    {
        gameObject.GetComponent<Renderer>().material.color = defaultColor;
    }

    public void ChangeColors(object indexPlayer)
    {
        int colorIndex = (int)indexPlayer;
        gameObject.GetComponent<Renderer>().material.color = playerColor[colorIndex];
    }

}
