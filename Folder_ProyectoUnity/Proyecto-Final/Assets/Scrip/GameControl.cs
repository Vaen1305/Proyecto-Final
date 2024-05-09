using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject Capsula;
    public GameObject current;
    public TowerControler towerController;

    public void CreateCapsula()
    {
        if (current == null)
        {
            current = Instantiate(Capsula, transform.position, transform.rotation);
            if (towerController != null)
            {
                towerController.SetTargetPosition(transform.position);
            }
        }
    }
}
