using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public AudioClip ButtonPressed;

    private AudioSource _asButtonPressed;

    public void ChangeScene(string sceneName)
    {
        _asButtonPressed = gameObject.AddComponent<AudioSource>();
        _asButtonPressed.clip = ButtonPressed;
        _asButtonPressed.Play();

        if (sceneName == "Quit")
            Application.Quit();
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
