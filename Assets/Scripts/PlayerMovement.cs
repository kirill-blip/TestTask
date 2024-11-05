using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _velocity = 5f;

    private Rigidbody _rigidbody;
    private InputHandler _inputHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputHandler = FindObjectOfType<InputHandler>();
    }

    private void FixedUpdate()
    {
        float moveDirectionX = _inputHandler.GetMoveDirection().x;
        float moveDirectionY = _inputHandler.GetMoveDirection().y;

        Vector3 velocity = transform.TransformDirection(moveDirectionX, 0, moveDirectionY) * _velocity;
        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }
}
