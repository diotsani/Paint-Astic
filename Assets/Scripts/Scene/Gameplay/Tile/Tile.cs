using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintAstic.Global;
using PaintAstic.Module.Message;

namespace PaintAstic.Module.Tiles
{
    public class Tile : MonoBehaviour
    {
        public int tileIndexX { get; private set; }
        public int tileIndexZ { get; private set; }

        private Color defaultColor;
        [SerializeField] private List <Color> playerColor = new List<Color>();

        public int _tileIndexColor { get; private set; }
        public bool isStepped = false;
        public void SetIndexTile(int tileIndexX, int tileIndexZ)
        {
            this.tileIndexX = tileIndexX;
            this.tileIndexZ = tileIndexZ;
        }

        public void SetColorTile(Color color)
        {
            playerColor.Add(color);
        }
        public void DefaultColors()
        {
            ColorUtility.TryParseHtmlString("C0C0C0",out defaultColor);
            gameObject.GetComponent<Renderer>().material.color = defaultColor;
            _tileIndexColor = 0;
        }
        public void ChangeColors(int indexPlayer)
        {
            gameObject.GetComponent<Renderer>().material.color = playerColor[indexPlayer];
            _tileIndexColor = indexPlayer + 1;
        }
    }
}

