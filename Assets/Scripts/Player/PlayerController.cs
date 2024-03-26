using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [HideInInspector] public Animator animator;
    public bool IsWalking { get; set; }
    public bool IsCarrying { get; set; }
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    private IState currentState;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        currentState = new IdleState();
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        MovePlayer();
        JumpPlayer();
    }
    public void ChangeState(IState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    private void MovePlayer()
    {
        Vector2 vector2 = inputManager.GetMovementInput();
        Vector3 move = new Vector3(vector2.x, 0, vector2.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;
        characterController.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            IsWalking = true;
        }
        else
        {
            IsWalking = false;
        }
    }
    private void JumpPlayer()
    {
        if (inputManager.GetJumpInput() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
