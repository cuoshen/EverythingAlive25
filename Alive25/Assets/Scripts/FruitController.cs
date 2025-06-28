using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class FruitController : MonoBehaviour
{
    [SerializeField]
    private FruitConfig config;

    [SerializeField]
    private Transform fruitTransform;

    [SerializeField]
    private AudioSource fruitAudioSource;

    private PlayerInput playerInput;
    private CharacterController controller;
    private Vector2 moveInput;
    private bool isSprinting;

    private float verticalVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        InputAction moveAction = playerInput.actions["Move"];
        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += _ => moveInput = Vector2.zero;

        InputAction sprintAction = playerInput.actions["Sprint"];
        sprintAction.performed += _ => isSprinting = true;
        sprintAction.canceled += _ => isSprinting = false;
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3();
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
        {
            fruitTransform.rotation = Quaternion.LookRotation(moveDirection);
        }

        float moveSpeed = isSprinting ? config.SprintSpeed : config.MoveSpeed;
        Vector3 velocity = moveDirection * moveSpeed;

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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<FruitController>() != null)
        {
            Debug.Log("Run into another player");
            PlayVFXAtCollision(hit.point);
        }
    }

    void PlayVFXAtCollision(Vector3 position)
    {
        VisualEffectAsset collisionVfx = config.CollisionVfx;
        if (collisionVfx != null)
        {

        }

        AudioClip collisionSfx = config.CollisionAudio;
        if (collisionSfx != null)
        {
            fruitAudioSource.clip = collisionSfx;
            fruitAudioSource.Play();
        }
    }
}
