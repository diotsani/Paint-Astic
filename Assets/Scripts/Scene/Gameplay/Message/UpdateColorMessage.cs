using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct UpdateColorMessage 
    {
        public int playerIndex { get; }
        public Color colorIndex { get;  }

        public UpdateColorMessage(int player, Color color)
        {
            playerIndex = player;
            colorIndex = color;
        }
    }
}