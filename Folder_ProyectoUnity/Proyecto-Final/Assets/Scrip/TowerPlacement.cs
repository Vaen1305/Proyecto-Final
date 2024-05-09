using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControler : MonoBehaviour
{
    private Vector3 targetPosition;

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        worldMousePosition.z = transform.position.z;

        transform.position = worldMousePosition;
    }
}
