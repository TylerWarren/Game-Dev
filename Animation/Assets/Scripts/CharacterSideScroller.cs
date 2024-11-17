using UnityEngine;
using UnityEngine.Events;

public class CharacterSideScroller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 4f;
    public float gravity = -9.81f;
    public int maxJumps = 2;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private int jumpsRemaining;
    private bool isMoving = false;

    public UnityEvent jumpEvent;
    public UnityEvent runEvent;
    public UnityEvent idleEvent;
    public UnityEvent jumpEndEvent;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        jumpsRemaining = maxJumps;

        jumpEvent.AddListener(() => animator.SetTrigger("Jump"));
        runEvent.AddListener(() => animator.SetBool("IsRunning", true));
        idleEvent.AddListener(() => animator.SetBool("IsRunning", false));
        jumpEndEvent.AddListener(() => animator.SetTrigger("Land")); // Transition to idle
    }

    private void Update()
    {
        HorizontalMovement();
        ApplyGravity();
        Jump();
        SetZPositionToZero();

        controller.Move(velocity * Time.deltaTime);
    }

    private void HorizontalMovement()
    {
        var moveInput = Input.GetAxis("Horizontal");
        velocity.x = moveInput * moveSpeed;

        if (Mathf.Abs(moveInput) > 0.01f)
        {
            if (!isMoving)
            {
                isMoving = true;
                runEvent.Invoke();
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                idleEvent.Invoke();
            }
        }
    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            animator.SetBool("IsGrounded", false);
        }
        else
        {
            velocity.y = 0;
            jumpsRemaining = maxJumps;
            animator.SetBool("IsGrounded", true);
        }
    }

    private void Jump()
    {
        if (!Input.GetButtonDown("Jump") || jumpsRemaining <= 0) return;

        velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        jumpsRemaining--;
        jumpEvent.Invoke();
    }

    private void SetZPositionToZero()
    {
        var position = transform.position;
        position.z = 0;
        transform.position = position;
    }
}