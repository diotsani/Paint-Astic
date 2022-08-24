using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // This is if you want a little delay and that your sphere doesn't go super fast
    bool movementOnGoing = true;

    // If you want to see them teleport instead of moving to the other case, make this smaller (It's the time that it takes to move the Player)
    public float movementUnit = 1;
    float smoothSpeed = 1;
    float negativeMovementUnit;

    Vector3 Movement;
    Vector3 desiredPosition;
    Vector3 smoothPosition;

    void Awake()
    {
        negativeMovementUnit = (-1 * movementUnit);
    }

    void Update()
    {
        if (movementOnGoing)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Movement = new Vector3(negativeMovementUnit, 0.0f, 0.0f);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Movement = new Vector3(0.0f, 0.0f, negativeMovementUnit);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Movement = new Vector3(movementUnit, 0.0f, 0.0f);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Movement = new Vector3(0.0f, 0.0f, movementUnit);
            }
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;

        if (transform.position == desiredPosition) { Movement = new Vector3(0.0f, 0.0f, 0.0f); movementOnGoing = true; }
    }
}
