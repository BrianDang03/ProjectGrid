using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    const string IS_WALKING = "IsWalking";

    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        float stoppingDistance = .01f;
        if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
            unitAnimator.SetBool(IS_WALKING, true);
            transform.position += moveDir * movementSpeed * Time.deltaTime;
        }
        else
        {
            unitAnimator.SetBool(IS_WALKING, false);
        }
    }

    public void Move(Vector3 aTargetPosition)
    {
        targetPosition = aTargetPosition;
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);

                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
