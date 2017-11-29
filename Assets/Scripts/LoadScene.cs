using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public void ChangeScene(string sceneName)
    {
        if (sceneName == "Quit")
            Application.Quit();
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
