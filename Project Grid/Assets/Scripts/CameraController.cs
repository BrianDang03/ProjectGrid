using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFEST = 12f;

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float zoomAmount = 1f;
    [SerializeField] private float zoomSpeed = 1f;

    CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffest;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffest = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * movementSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffest.y -= zoomAmount;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffest.y += zoomAmount;
        }

        targetFollowOffest.y = Mathf.Clamp(targetFollowOffest.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFEST);

        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffest, Time.deltaTime * zoomSpeed);
    }
}
