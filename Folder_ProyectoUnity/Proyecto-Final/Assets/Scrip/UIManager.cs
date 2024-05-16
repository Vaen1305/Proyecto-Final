using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    private string sceneName = "SampleScene";
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
