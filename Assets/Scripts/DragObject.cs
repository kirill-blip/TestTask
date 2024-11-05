using System;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField] private float _pickUpRange = 2f;
    [SerializeField] private float _moveSmoothness = 10f;

    private GameObject _selectedObject;
    private Rigidbody _selectedRigidbody;

    private InputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = FindObjectOfType<InputHandler>();

        _inputHandler.IntercationKeyPressed += IntercationKeyPressedHandler;
    }

    private void OnDestroy()
    {
        _inputHandler.IntercationKeyPressed -= IntercationKeyPressedHandler;
    }

    private void Update()
    {
        if (_selectedObject)
        {
            MoveObject();
        }
    }

    private void IntercationKeyPressedHandler(object sender, EventArgs e)
    {
        if (_selectedObject)
        {
            DropObject();
            return;
        }

        TryPickUpObject();
    }

    private void TryPickUpObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _pickUpRange))
        {
            if (hit.transform.TryGetComponent(out Item item))
            {
                _selectedObject = hit.transform.gameObject;

                _selectedRigidbody = item.GetComponent<Rigidbody>();
                _selectedRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                _selectedRigidbody.useGravity = false;
            }
        }
    }

    private void DropObject()
    {
        _selectedRigidbody.constraints = RigidbodyConstraints.None;
        _selectedRigidbody.useGravity = true;
        _selectedRigidbody = null;

        _selectedObject = null;
    }

    private void MoveObject()
    {
        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;

        float divider = 1.5f;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, _pickUpRange / divider));
        Vector3 moveDirection = (targetPosition - _selectedObject.transform.position);

        _selectedRigidbody.velocity = moveDirection * _moveSmoothness;
    }
}
