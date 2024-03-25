using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    private Vector3 targetPosition;

    private void Update()
    {
        float stoppingDistance = .01f;
        if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            transform.position += moveDir * movementSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition());
        }
    }

    private void Move(Vector3 aTargetPosition)
    {
        this.targetPosition = aTargetPosition;
    }
}
