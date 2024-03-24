using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float horizontal = 0, vertical = 0;
    private float headRotUpDown = 0, headRotLefRight = 0;
    private AnimationController animatorCntllr;
    [SerializeField] private float speed = 0;
    [SerializeField] private GameObject headCamera;

    private void Start()
    {
        animatorCntllr = GetComponent<AnimationController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) //Run
        {
            speed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) //walk
        {
            speed /= 2;
        }
    }

    private void FixedUpdate()
    {
        MoveAndRotateCamera();
    }

    private void MoveAndRotateCamera()
    {
        Move();

        UpdateCameraTransform();
        if (horizontal != 0 || vertical != 0)
        {
            animatorCntllr.IsWalking = true;
            animatorCntllr.AnimationControl();
        }
        else
        {
            animatorCntllr.IsWalking = false;
            animatorCntllr.AnimationControl();
        }
        animatorCntllr.AnimationControl();
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 forwardDirection = headCamera.transform.forward;
            forwardDirection.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(forwardDirection), 0.4f);
        }
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement = transform.TransformDirection(movement);
        movement.Normalize();
        transform.position += movement * Time.deltaTime * speed;
    }

    private void UpdateCameraTransform()
    {
        headCamera.transform.position = transform.position + (1.5f * Vector3.up);
        headRotUpDown += Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * -150;
        headRotLefRight += Input.GetAxis("Mouse X") * Time.fixedDeltaTime * 150;
        headRotUpDown = Mathf.Clamp(headRotUpDown, -20, 20);
        headCamera.transform.rotation = Quaternion.Euler(headRotUpDown, headRotLefRight, 0);
    }
}
