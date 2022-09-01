using UnityEditor;
using UnityEngine;

namespace PaintAstic.Global.MatchHistory
{
    [System.Serializable]
    public struct PlayerData
    {
        [SerializeField] private int _winCount;
        [SerializeField] private int _availableColor;

        public int winCount => _winCount;
        public int availableColor => _availableColor;
    }
}