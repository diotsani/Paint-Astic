using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct MoveMessage
    {
        public Vector3 move { get; }
        public int playerId { get; }

        public MoveMessage(Vector3 _move, int _playerId)
        {
            move = _move;
            playerId = _playerId;
        }
    }
}