using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 120.0f;
    [SerializeField] private float stopDistance = 2.5f;
    [SerializeField] private Vector3 destination;
    private bool reachedDestination;

    public bool ReachedDestination { get => reachedDestination; set => reachedDestination = value; }

    void Update()
    {
        if (transform.position!= destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;
            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
        }
    }
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}
