using System.Collections;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct AddPointMessage
    {
        public int indexPlayer { get; }
        public int amountPoint { get; }
        public bool isDoublePoint { get; }
        public AddPointMessage(int IndexPlayer, int AmountPoint, bool IsDoublePoint)
        {
            indexPlayer = IndexPlayer;
            amountPoint = AmountPoint;
            isDoublePoint = IsDoublePoint;
        }
    }
}