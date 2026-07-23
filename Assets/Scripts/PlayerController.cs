using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float sprintMultiplier = 1.8f;
    public float upForce = 200f;

    public Rigidbody2D rb;
    float horizontalMovement;
    bool isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed, rb.linearVelocity.y);
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        horizontalMovement = input.x;
    }

    void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
    }
}
