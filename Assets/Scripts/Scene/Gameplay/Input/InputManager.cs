using PaintAstic.Global;
using PaintAstic.Module.Player;
using UnityEngine;

namespace PaintAstic.Module.Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputConfig[] inputConfigs;
        private MoveMessage moveMessage;

        void Update()
        {
            for (int i = 0; i < inputConfigs.Length; i++)
            {
                var inputConfig = inputConfigs[i];

                if (Input.GetKey(inputConfig.moveUp))
                {
                    moveMessage = new MoveMessage(Vector3.forward, i);
                    
                    EventManager.TriggerEvent("Move", moveMessage);
                }
                if (Input.GetKey(inputConfig.moveDown))
                {
                    EventManager.TriggerEvent("Move", Vector3.back);
                }
                if (Input.GetKey(inputConfig.moveLeft))
                {
                    EventManager.TriggerEvent("Move", Vector3.left);
                }
                if (Input.GetKey(inputConfig.moveRight))
                {
                    EventManager.TriggerEvent("Move", Vector3.right);
                }
            }
            
        }
    }
}