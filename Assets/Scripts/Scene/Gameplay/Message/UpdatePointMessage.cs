using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct UpdatePointMessage
    {
        public int playerIndex { get; }
        public int point { get; }

        public UpdatePointMessage(int PlayerIndex, int Point)
        {
            playerIndex = PlayerIndex;
            point = Point;
        }
    }
}

