using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "NewFruitConfig", menuName = "Configs/FruitConfig")]
public class FruitConfig : ScriptableObject
{
    [Header("Movement Settings")]
    public float MoveSpeed = 10.0f;
    public float SprintSpeed = 15.0f;
    public float Gravity = -9.81f;
    public float TerminalVelocity = -50f;
    public float SprintCooldown = 4.0f;
    public float SprintDuration = 2.0f;
    public float KnockbackDecay = 20f; // how fast knockback slows down

    [Header("Game Settings")]
    public float DeathThreshold = -20.0f;

    [Header("Effects")]
    public VisualEffectAsset CollisionVfx;
    public AudioClip CollisionAudio;
}
