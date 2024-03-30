using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem gridSystem;
    private void Start()
    {
        gridSystem = new GridSystem(10, 10, 2f);
    }

    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}
