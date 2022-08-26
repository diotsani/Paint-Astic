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

        private Color defaultColor = Color.gray;
        [SerializeField] private Color[] playerColor = { Color.red, Color.yellow, Color.green, Color.blue };

        public int _tileIndexColor { get; private set; }
        public void SetIndexTile(int tileIndexX, int tileIndexZ)
        {
            this.tileIndexX = tileIndexX;
            this.tileIndexZ = tileIndexZ;
        }
        public void DefaultColors()
        {
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

