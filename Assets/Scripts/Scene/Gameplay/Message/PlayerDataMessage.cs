using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct PlayerDataMessage
    {
        public int currentX { get; set; }
        public int currentZ { get; set; }
        public int lastX { get; set; }
        public int lastZ { get; set; }
        public int playerIndex { get; set; }
        public bool isDoublePoint { get; set; }

        public PlayerDataMessage(int CurrentX, int CurrentZ, int LastX, int LastZ, int PlayerIndex, bool IsDoublePoint)
        {
            playerIndex = PlayerIndex;
            currentX = CurrentX;
            currentZ = CurrentZ;
            lastX = LastX;
            lastZ = LastZ;
            isDoublePoint = IsDoublePoint;
        }
    }
}