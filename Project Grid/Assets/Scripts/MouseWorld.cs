using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private LayerMask mousePlaneLayerMask;

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
