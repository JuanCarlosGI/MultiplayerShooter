using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class PauseGame : MonoBehaviour
    {
        public static PauseGame Instance { get; private set; }
        public bool IsPaused { get; private set; }

        public Text PauseText;

        public void Pause()
        {
            Time.timeScale = 0;
            PauseText.text = "Press P to resume\nPress Q to quit\nPress M for main menu";
        }

        public void Resume()
        {
            Time.timeScale = 1;
            PauseText.text = "";
        }

        private void Start()
        {
            Instance = this;
            IsPaused = false;
            PauseText.text = "";
            Resume();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                IsPaused = !IsPaused;
                if (IsPaused)
                    Pause();
                else
                    Resume();
            }
            if (IsPaused)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    Application.Quit();
                if (Input.GetKeyDown(KeyCode.M))
                    SceneManager.LoadScene("start");
            }
        }
    }
}
