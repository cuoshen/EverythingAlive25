using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FruitController : MonoBehaviour
{

    [SerializeField]
    private FruitConfig config;

    private PlayerInput playerInput;
    private CharacterController controller;
    private Vector2 moveInput;

    private float verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        var moveAction = playerInput.actions["Move"];
        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += _ => moveInput = Vector2.zero;
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        moveDirection.Normalize();

        Vector3 velocity = moveDirection * config.MoveSpeed;

        // Apply gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -1f; // stick to ground
        }
        else
        {
            verticalVelocity += config.Gravity * Time.deltaTime;
            verticalVelocity = Mathf.Max(verticalVelocity, config.TerminalVelocity);
        }

        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
}
