using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerType {
    Player1,
    Player2
}

public class PlayerMovement : MonoBehaviour{

    [HideInInspector] public bool isFacingRight = true;

    [SerializeField] PlayerType playerType = PlayerType.Player1;
    [Header("Player Stats")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float counterJump = 100f;

    [Header("Caching attributes")]
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;

    class InputString {
        public string Horizontal = "Horizontal";
        public string Jump = "Jump";
        public string Down = "Down";
        
        public InputString(PlayerType playerType) {
            if (playerType == PlayerType.Player1) {
                Horizontal += "1";
                Jump += "1";
                Down += "1";
            } else {
                Horizontal += "2";
                Jump += "2";
                Down += "2";
            }
        }
    }
    private float horizontalInput;
    private bool isDownButtonPressed;
    private InputString inputString;
    private float groundDetectRadius = 0.2f;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferTimeCounter;

    private void Awake() {
        inputString = new InputString(this.playerType);
    }
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        groundDetectRadius = 0.2f;
        coyoteTime = 0.2f;
        jumpBufferTime = 0.2f;
    }
    private void Update() {
        CheckInput();
        FlipPlayer();
        Jump();
    }
    private void FixedUpdate() {
        MovePlayer();
        CounterJump();
    }


    private void CheckInput() {
        horizontalInput = Input.GetAxisRaw(inputString.Horizontal);
        isDownButtonPressed = Input.GetButton(inputString.Down);
    }
    private void MovePlayer() {
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
    }
    private void FlipPlayer() {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f)) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void Jump() {
        if (IsOnGround()) {
            //can jump while off the ground of 0.2 seconds
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown(inputString.Jump)) {
            //can jump if pressed jump button a bit soon before hitting the gound
            jumpBufferTimeCounter = jumpBufferTime;
        } else {
            jumpBufferTimeCounter -= Time.deltaTime;
        }
        if (jumpBufferTimeCounter > 0f && coyoteTimeCounter > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0;
            jumpBufferTimeCounter = 0;
        }
    }
    private void CounterJump() {
        if (isDownButtonPressed && IsOnGround() == false) {
            Vector2 downwardVector = new(0, -counterJump);
            rb.AddForce(downwardVector);
        }
    }
    private bool IsOnGround() {
        return Physics2D.OverlapCircle(groundCheckPoint.position, groundDetectRadius, groundLayer);
    }
}
