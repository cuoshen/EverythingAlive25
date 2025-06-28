using UnityEngine;

[CreateAssetMenu(fileName = "NewFruitConfig", menuName = "Configs/FruitConfig")]
public class FruitConfig : ScriptableObject
{
    [Header("Movement Settings")]
    public float MoveSpeed = 10.0f;
    public float SprintSpeed = 15.0f;
    public float Gravity = -9.81f;
    public float TerminalVelocity = -50f;
}
