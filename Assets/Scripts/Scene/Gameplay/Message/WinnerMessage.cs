using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct WinnerMessage
    {
        public int playerIndex { get; }
        public int point { get; }

        public WinnerMessage(int PlayerIndex, int Point)
        {
            playerIndex = PlayerIndex;
            point = Point;
        }
    }
}
