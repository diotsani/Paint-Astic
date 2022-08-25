using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct MoveMessage
    {
        public Vector3 _move { get; }
        public int _playerId { get; }

        public MoveMessage(Vector3 move, int playerId)
        {
            _move = move;
            _playerId = playerId;
        }
    }
}