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

    [SerializeField]
    private int armyID;
    public int ArmyID => armyID;

    [SerializeField]
    private PlayerInput playerInput;

    private FruitGameMaster fruitGameMaster = null;

    private CharacterController controller;
    private Vector2 moveInput;
    private bool isSprintingTriggered;

    private float verticalVelocity;

    private Vector3 currentVelocity = new Vector3();
    public Vector3 CurrentVelocity => currentVelocity;
    private Vector3 lastPosition = new Vector3();
    private Vector3 knockbackVelocity;

    private bool isAlive = true;
    private bool isPlayerControlled = false;
    public bool IsPlayerControlled => isPlayerControlled;
    private double lastSprintTime = 0;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void InjectGameMaster(FruitGameMaster gameMaster)
    {
        fruitGameMaster = gameMaster;
    }

    public void EnablePlayerControl()
    {
        InputAction moveAction = playerInput.actions["Move"];
        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += _ => moveInput = Vector2.zero;

        InputAction sprintAction = playerInput.actions["Sprint"];
        sprintAction.performed += _ => isSprintingTriggered = true;
        sprintAction.canceled += _ => isSprintingTriggered = false;

        isPlayerControlled = true;
    }

    void Update()
    {
        if (transform.position.y < config.DeathThreshold && isAlive)
        {
            fruitGameMaster.ReportFruitDeath(this);
            isPlayerControlled = false;
            isAlive = false;
        }

        Vector3 moveDirection = new Vector3();
        if (isPlayerControlled)
        {
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            moveDirection.Normalize();
        }

        if (moveDirection != Vector3.zero)
        {
            fruitTransform.rotation = Quaternion.LookRotation(moveDirection);
        }

        bool isSprinting = false;
        double timeSinceLastSprintStart = Time.timeAsDouble - lastSprintTime;
        if (timeSinceLastSprintStart < config.SprintDuration)
        {
            isSprinting = true;
        }

        if (isSprintingTriggered && timeSinceLastSprintStart > config.SprintCooldown)
        {
            lastSprintTime = Time.timeAsDouble; // Start a new sprint
            isSprinting = true;
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

        // Apply additional knockback
        if (knockbackVelocity.sqrMagnitude > 0.01f)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, config.KnockbackDecay * Time.deltaTime);
        }

        // Update player velocity based on last frame
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        FruitController otherPlayer = hit.collider.GetComponent<FruitController>();
        if (otherPlayer != null)
        {
            PlayEffectsAtPlayerCollision(hit.point);

            // Knock back
            Vector3 relativeVelocity = currentVelocity - otherPlayer.currentVelocity;
            Vector3 knockbackDir = (transform.position - otherPlayer.transform.position).normalized;

            // Apply knockback in both directions
            knockbackVelocity += knockbackDir * relativeVelocity.magnitude;
            otherPlayer.knockbackVelocity -= knockbackDir * relativeVelocity.magnitude;
        }
        else
        {
            PlayEffectsWhenCollideWithWall(hit.point);
        }
    }

    private void PlayEffectsAtPlayerCollision(Vector3 position)
    {
        ParticleSystem collisionVfx = config.CollisionVfx;
        if (collisionVfx != null)
        {
            ParticleSystem vfxInstance = GameObject.Instantiate(collisionVfx, position, Quaternion.identity);
            vfxInstance.Play();
            Destroy(vfxInstance.gameObject, vfxInstance.main.duration + vfxInstance.main.startLifetime.constantMax);
        }

        AudioClip collisionSfx = config.CollisionAudio;
        if (collisionSfx != null)
        {
            fruitAudioSource.PlayOneShot(collisionSfx);
        }
    }

    private void PlayEffectsWhenCollideWithWall(Vector3 position)
    {

    }
}
