using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct WinnerMessage
    {
        public int playerIndex { get; }
        public int point { get; }
        public bool isDraw { get; }

        public WinnerMessage(int PlayerIndex, int Point, bool IsDraw)
        {
            playerIndex = PlayerIndex;
            point = Point;
            isDraw = IsDraw;
        }
    }
}
