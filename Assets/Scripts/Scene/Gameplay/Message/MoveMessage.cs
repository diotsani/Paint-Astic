using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct MoveMessage
    {
        public Vector2Int move { get; }
        public int playerId { get; }

        public MoveMessage(Vector2Int _move, int _playerId)
        {
            move = _move;
            playerId = _playerId;
        }
    }
}