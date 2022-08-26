using System.Collections;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct AddPointMessage
    {
        public int indexPlayer { get; }
        public int amountPoint { get; }
        public AddPointMessage(int IndexPlayer, int AmountPoint)
        {
            indexPlayer = IndexPlayer;
            amountPoint = AmountPoint;
        }
    }
}