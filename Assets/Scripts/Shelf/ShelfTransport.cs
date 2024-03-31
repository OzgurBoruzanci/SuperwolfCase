using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShelfTransport : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private bool canDrag;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        if (InputManager.Instance.GetGBtnPressed())
        {
            canDrag = true;
        }
    }
    private void OnMouseDown()
    {
        if (canDrag)
        {
            offset = transform.position - GetMouseWorldPosition();
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            transform.position = newPosition;
        }
        if (InputManager.Instance.GetEBtnPressed())
        {
            transform.Rotate(Vector3.up, 90f);
        }
        if (InputManager.Instance.GetQBtnPressed())
        {
            transform.Rotate(Vector3.up, -90f);
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = inputManager.GetMousePosition();
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            canDrag = false;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
