using PaintAstic.Global;
using PaintAstic.Module.Message;
using PaintAstic.Module.Player;
using UnityEngine;

namespace PaintAstic.Module.Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputConfig[] _inputConfigs;
        private bool isPlayerMove;
        private void OnEnable()
        {
            EventManager.StartListening("GameStartMessage", PlayerCanMove);
        }
        private void OnDisable()
        {
            EventManager.StopListening("GameStartMessage", PlayerCanMove);
        }
        private void Update()
        {
            if(isPlayerMove)
            {
                for (int i = 0; i < _inputConfigs.Length; i++)
                {
                    var inputConfig = _inputConfigs[i];

                    if (Input.GetKey(inputConfig.moveUp))
                    {
                        EventManager.TriggerEvent("Move", new MoveMessage(Vector2Int.up, i));
                    }
                    if (Input.GetKey(inputConfig.moveDown))
                    {
                        EventManager.TriggerEvent("Move", new MoveMessage(Vector2Int.down, i));
                    }
                    if (Input.GetKey(inputConfig.moveLeft))
                    {
                        EventManager.TriggerEvent("Move", new MoveMessage(Vector2Int.left, i));
                    }
                    if (Input.GetKey(inputConfig.moveRight))
                    {
                        EventManager.TriggerEvent("Move", new MoveMessage(Vector2Int.right, i));
                    }
                }
            } 
        }
        void PlayerCanMove()
        {
            isPlayerMove = true;
        }
    }
}