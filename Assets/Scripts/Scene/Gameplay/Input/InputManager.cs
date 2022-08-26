using PaintAstic.Global;
using PaintAstic.Module.Message;
using PaintAstic.Module.Player;
using UnityEngine;

namespace PaintAstic.Module.Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputConfig[] _inputConfigs;

        private void Update()
        {
            for (int i = 0; i < _inputConfigs.Length; i++)
            {
                var inputConfig = _inputConfigs[i];

                if (Input.GetKey(inputConfig.moveUp))
                {
                    EventManager.TriggerEvent("Move", new MoveMessage(Vector3.forward, i));
                }
                if (Input.GetKey(inputConfig.moveDown))
                {
                    EventManager.TriggerEvent("Move", new MoveMessage(Vector3.back, i));
                }
                if (Input.GetKey(inputConfig.moveLeft))
                {
                    EventManager.TriggerEvent("Move", new MoveMessage(Vector3.left, i));
                }
                if (Input.GetKey(inputConfig.moveRight))
                {
                    EventManager.TriggerEvent("Move", new MoveMessage(Vector3.right, i));
                }
            }
        }
    }
}