using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;

    private GameObject CurrentTower;
    void Start()
    {
        
    }

    void Update()
    {
        if(CurrentTower != null)
        {
            Ray camaray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(camaray, out RaycastHit hitInfo, 100f))
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
