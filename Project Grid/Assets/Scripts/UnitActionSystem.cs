using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSeletedUnitChanged;

    [SerializeField] private Unit seletedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than on UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection())
            {
                return;
            }
            
            seletedUnit.GetMoveAction().Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out Unit aUnit))
            {
                SetSeletedUnit(aUnit);
                return true;
            }
        }
        
        return false;
    }

    private void SetSeletedUnit(Unit aUnit)
    {
        seletedUnit = aUnit;
        OnSeletedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSeletedUnit()
    {
        return seletedUnit;
    }
}
