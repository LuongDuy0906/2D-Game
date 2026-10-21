using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    private static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    private static readonly int IsJumpingHash = Animator.StringToHash("isJumping");
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference jumpInput;

    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    private float _moveX;
    private bool _jumpRequest;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        moveInput.action.Enable();
        jumpInput.action.Enable();
    }

    void OnDisable()
    {
        moveInput.action.Disable();
        jumpInput.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        HandleInput();
    }

    private void CheckInput()
    {
        if(GameManager.Instance.IsGameOver() || GameManager.Instance.IsGameWin()) {
            _moveX = 0;
            _jumpRequest = false;
            return;
        }

        _moveX = moveInput.action.ReadValue<Vector2>().x;

        if (jumpInput.action.WasPressedThisFrame())
            _jumpRequest = true;

        if (_moveX != 0)
            transform.localScale = new Vector3(Mathf.Sign(_moveX), 1, 1);
    }

    private void HandleInput()
    {
        isGrounded = Physics2D.OverlapCircle(
        groundCheck.position, 0.2f, groundLayer);

        rb.linearVelocity = new Vector2(_moveX * moveSpeed, rb.linearVelocity.y);

        if (_jumpRequest && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            AudioManager.Instance.PlayJumpSound();
        }

        _jumpRequest = false;
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;

        animator.SetBool(IsRunningHash, isRunning);
        animator.SetBool(IsJumpingHash, isJumping);
    }
}
