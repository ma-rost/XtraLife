using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public PlayerControls playerControls;
    private InputAction move;
    private InputAction jump;

    private Vector2 moveDirection;
    [SerializeField] private float moveSpeed = 5;

    [SerializeField] private float jumpForce = 5;

    

    private bool isFacingRight = true;

    private Rigidbody2D rb;
    

    [SerializeField] private Transform playerSprite;

    [SerializeField] private GroundCheck groundCheck;
    private bool isGrounded;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null) rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null) groundCheck = GetComponent<GroundCheck>();
        groundCheck.onGroundStateChange += GroundStateChanged;
    }

    private void GroundStateChanged(bool b) => isGrounded = b;

    private void OnEnable()
    {
        playerControls.Enable();

        move = playerControls.Player.Move;
        jump = playerControls.Player.Jump;
        move.Enable();
        jump.Enable();

        jump.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

    }

    private void Flip()
    {
        if (isFacingRight && moveDirection.x < 0f || !isFacingRight && moveDirection.x > 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = playerSprite.localScale;
            localScale.x *= -1f;

            playerSprite.localScale = localScale;
        }
    }
}
