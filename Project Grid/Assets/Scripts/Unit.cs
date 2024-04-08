using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const string IS_WALKING = "IsWalking";

    [SerializeField] private Animator unitAnimator;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    private Vector3 targetPosition;
    private GridPosition gridPosition;
    

    private void Awake()
    {
        targetPosition = this.transform.position;
    }

    private void Start()
    {
        LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
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

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public void Move(Vector3 aTargetPosition)
    {
        this.targetPosition = aTargetPosition;
    }
}
