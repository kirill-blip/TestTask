using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    [SerializeField] private RotationAxes _rotationAxes;
    
    [SerializeField] private float _sensitivity = 5f;
    [SerializeField] private float _rotationAngle = 42f;

    private float _rotationX;
    private InputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = FindObjectOfType<InputHandler>();
    }

    private void Update()
    {
        switch (_rotationAxes)
        {
            case RotationAxes.MouseX:
                transform.Rotate(0, _inputHandler.GetRotationDirection().x * _sensitivity * Time.deltaTime, 0);
                break;

            case RotationAxes.MouseY:
                _rotationX -= _inputHandler.GetRotationDirection().y * _sensitivity * Time.deltaTime;

                _rotationX = Mathf.Clamp(_rotationX, -_rotationAngle, _rotationAngle);

                float rotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY);
                break;
        }
    }
}
