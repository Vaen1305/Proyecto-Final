using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menu;

    public void OpenMenu()
    {
        menu.SetActive(true); 
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}
