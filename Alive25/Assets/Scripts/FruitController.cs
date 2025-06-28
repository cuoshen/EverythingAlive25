using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection.Normalize(); // Normalize to avoid faster diagonal movement

        // World space movement
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
