using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float sprintMultiplier = 1.8f;
    public float upForce = 200f;

    public Rigidbody2D rb;
    public Animator animator; // 1. Ссылка на компонент Animator

    float horizontalMovement;
    bool isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 2. Автоматически находим Animator, если забыли перетащить в инспекторе
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        rb.linearVelocity = new Vector2(horizontalMovement * currentSpeed, rb.linearVelocity.y);

        // 3. Передаем абсолютную скорость движения в аниматор (без учета знака "-" при движении влево)
        float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", horizontalSpeed);

        // 4. Поворот спрайта в сторону движения (Flip)
        if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
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