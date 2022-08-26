using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Module.Message
{
    public struct SpawnPosMessage
    {
        public Vector3 spawnFirst { get; }
        public Vector3 spawnLast { get; }

        public SpawnPosMessage(Vector3 SpawnFirst, Vector3 SpawnLast)
        {
            spawnFirst = SpawnFirst;
            spawnLast = SpawnLast;
        }
    }
}