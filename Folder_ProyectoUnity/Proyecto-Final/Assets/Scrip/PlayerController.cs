using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 Velocity;
    private Vector2 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    [SerializeField] private int Health;
    [SerializeField] private int Money;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        PlayerMovementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (Mouse.current.rightButton.isPressed)
        {
            PlayerMouseInput = context.ReadValue<Vector2>();
        }
        else
        {
            PlayerMouseInput = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(new Vector3(PlayerMovementInput.x, 0, PlayerMovementInput.y));

        if (Keyboard.current.spaceKey.isPressed)
        {
            Velocity.y = 1f;
        }
        else if (Keyboard.current.leftShiftKey.isPressed)
        {
            Velocity.y = -1f;
        }

        rb.MovePosition(rb.position + moveVector * Speed * Time.fixedDeltaTime);
        rb.velocity = new Vector3(rb.velocity.x, Velocity.y * Speed, rb.velocity.z);

        Velocity.y = 0f;
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;
        transform.Rotate(Vector3.up, PlayerMouseInput.x * Sensitivity);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
