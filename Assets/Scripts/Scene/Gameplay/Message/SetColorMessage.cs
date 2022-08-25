using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct SetColorMessage
    {
        public int playerIndex { get; }
        public int playerXPos { get; }
        public int playerZPos { get; }

        public SetColorMessage(int PlayerIndex, int PlayerXPos, int PlayerZPos)
        {
            playerIndex = PlayerIndex;
            playerXPos = PlayerXPos;
            playerZPos = PlayerZPos;
        }
    }
}

