using PaintAstic.Global;
using UnityEngine;
using UnityEngine.Events;

namespace PaintAstic.Module.Player
{
    public class PlayerController : MonoBehaviour
    {
        public float movementUnit = 1;
        float smoothSpeed = 1;

        Vector3 Movement;
        Vector3 desiredPosition;
        Vector3 smoothPosition;
        private void OnEnable()
        {
            EventManager.StartListening("Move", Move);
        }

        private void OnDisable()
        {
            EventManager.StopListening("Move", Move);
        }

        void Move(object data)
        {
            Movement = (Vector3)data;

            desiredPosition = transform.position + Movement;
            smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }
    }
}



