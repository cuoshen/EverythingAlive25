using UnityEngine;

[CreateAssetMenu(fileName = "NewFruitConfig", menuName = "Configs/FruitConfig")]
public class FruitConfig : ScriptableObject
{
    [Header("Movement Settings")]
    public float MoveSpeed = 15f;
    [SerializeField]
    public float Gravity = -9.81f;
    [SerializeField]
    public float TerminalVelocity = -50f;
}
