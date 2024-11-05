using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private InputActions _inputActions;

    private Vector2 _moveDirection;
    private Vector2 _rotationDirection;

    public event EventHandler IntercationKeyPressed;

    private void Awake()
    {
        ActivateCursor();

        _inputActions = new InputActions();
        _inputActions.Enable();

        _inputActions.Player.Move.performed += ctx => _moveDirection = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => _moveDirection = Vector2.zero;

        _inputActions.Player.Look.performed += ctx => _rotationDirection = ctx.ReadValue<Vector2>();
        _inputActions.Player.Look.canceled += ctx => _rotationDirection = Vector2.zero;

        _inputActions.Player.Interact.performed += ctx => IntercationKeyPressed?.Invoke(this, EventArgs.Empty);

        _inputActions.UI.Escape.performed += ctx => ActivateCursor();
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= ctx => _moveDirection = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled -= ctx => _moveDirection = Vector2.zero;

        _inputActions.Player.Look.performed -= ctx => _rotationDirection = ctx.ReadValue<Vector2>();
        _inputActions.Player.Look.canceled -= ctx => _rotationDirection = Vector2.zero;

        _inputActions.Player.Interact.performed -= ctx => IntercationKeyPressed?.Invoke(this, EventArgs.Empty);

        _inputActions.UI.Escape.performed -= ctx => ActivateCursor();

        _inputActions.Disable();
    }

    private void ActivateCursor()
    {
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = !Cursor.visible;
    }

    public Vector2 GetMoveDirection()
    {
        return _moveDirection;
    }

    public Vector2 GetRotationDirection()
    {
        return _rotationDirection;
    }
}
