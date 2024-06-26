using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private LayerMask LayerMask;
    private Tower CurrentTower;
    public Transform[] towerPlacementPoints;
    private bool[] isPlacementPointOccupied;
    private PlayerController playerController;

    void Start()
    {
        isPlacementPointOccupied = new bool[towerPlacementPoints.Length];
        playerController = FindObjectOfType<PlayerController>();

        if (playerController == null)
        {

        }
    }

    void Update()
    {
        if (CurrentTower != null)
        {
            Ray camaray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camaray, out RaycastHit hitInfo, 1000f, LayerMask))
            {
                Transform closestPoint = GetClosestPlacementPoint(hitInfo.point);
                Vector3 newPosition = hitInfo.point;

                newPosition.y = closestPoint.position.y;
                CurrentTower.transform.position = newPosition;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Transform closestPoint = GetClosestPlacementPoint(CurrentTower.transform.position);
                int closestPointIndex = System.Array.IndexOf(towerPlacementPoints, closestPoint);
                if (Vector3.Distance(CurrentTower.transform.position, closestPoint.position) < 1f && !isPlacementPointOccupied[closestPointIndex])
                {
                    CurrentTower.transform.position = closestPoint.position;
                    isPlacementPointOccupied[closestPointIndex] = true;
                    CurrentTower.isPlaced = true;
                    CurrentTower = null;
                }
            }
        }
    }

    Transform GetClosestPlacementPoint(Vector3 position)
    {
        Transform closestPoint = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < towerPlacementPoints.Length; ++i)
        {
            float distance = Vector3.Distance(position, towerPlacementPoints[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = towerPlacementPoints[i];
            }
        }

        return closestPoint;
    }

    public void SetTowerToPlace(Tower towerPrefab)
    {
        if (CurrentTower != null)
        {
            return;
        }

        if (playerController != null)
        {
            if (playerController.CanAfford(towerPrefab.config.price))
            {
                playerController.SpendMoney(towerPrefab.config.price);
                CurrentTower = Instantiate(towerPrefab);

                Ray camaray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(camaray, out RaycastHit hitInfo, 100f, LayerMask))
                {
                    Transform closestPoint = GetClosestPlacementPoint(hitInfo.point);
                    Vector3 newPosition = hitInfo.point;
                    newPosition.y = closestPoint.position.y;
                    CurrentTower.transform.position = newPosition;
                }
            }
        }
    }
}
