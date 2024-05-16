using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private LayerMask LayerMask;
    private GameObject CurrentTower;
   
    void Update()
    {
        if(CurrentTower != null)
        {
            Ray camaray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(camaray, out RaycastHit hitInfo, 100f, LayerMask))
            {
                CurrentTower.transform.position = hitInfo.point;
            }

            if (Input.GetMouseButtonDown(0))
            {
                CurrentTower = null;
            }
        }
    }
    public void SetTowerToPlace(GameObject Tower)
    {
        CurrentTower = Instantiate(Tower, Vector3.zero, Quaternion.identity);
    }
}
