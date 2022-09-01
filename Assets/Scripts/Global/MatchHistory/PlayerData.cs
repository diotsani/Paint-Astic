using UnityEditor;
using UnityEngine;

namespace PaintAstic.Global.MatchHistory
{
    [System.Serializable]
    public struct PlayerData
    {
        [SerializeField] public int winCount { get; set; }
        [SerializeField] public int availableColor { get; set; }
    }
}