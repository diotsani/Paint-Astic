using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct CollectPointMessage
    {
        public int indexPlayer { get; }
        public bool isDoublePoint { get; }

        public CollectPointMessage(int IndexPlayer, bool IsDoublePoint)
        {
            indexPlayer = IndexPlayer;
            isDoublePoint = IsDoublePoint;
        }
    }

}

