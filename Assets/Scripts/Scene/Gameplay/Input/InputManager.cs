using PaintAstic.Global;
using UnityEngine;

namespace PaintAstic.Module.Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private KeyCode _moveUp;
        [SerializeField] private KeyCode _moveDown;
        [SerializeField] private KeyCode _moveLeft;
        [SerializeField] private KeyCode _moveRight;

        void Update()
        {
            if (Input.GetKeyDown(_moveUp))
            {
                EventManager.TriggerEvent("Move", Vector3.forward);
            }
            if (Input.GetKeyDown(_moveDown))
            {
                EventManager.TriggerEvent("Move", Vector3.back);
            }
            if (Input.GetKeyDown(_moveLeft))
            {
                EventManager.TriggerEvent("Move", Vector3.left);
            }
            if (Input.GetKeyDown(_moveRight))
            {
                EventManager.TriggerEvent("Move", Vector3.right);
            }
        }
    }
}