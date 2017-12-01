using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class PauseGame : NetworkBehaviour
    {
        public static PauseGame Instance { get; private set; }
        public bool IsPaused { get; private set; }

        [SyncVar(hook = "Pause")]
        private bool _syncPaused;

        public Text PauseText;

        private void Pause(bool pause)
        {
            IsPaused = pause;
            if (!pause)
            {
                Resume();
                return;
            }

            Time.timeScale = 0;
            PauseText.text = "Presiona P para Continuar\nPresiona Q para Salir\nPresiona M para Ir al Menú Principal";
        }

        private void Resume()
        {
            Time.timeScale = 1;
            PauseText.text = "";
        }

        private void Start()
        {
            Instance = this;
            IsPaused = false;
            PauseText = GameObject.Find("PauseText").GetComponent<Text>();
            PauseText.text = "";
            Resume();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!IsPaused)
                    CmdPause();
                else
                    CmdResume();
            }
            if (IsPaused)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    Application.Quit();
                if (Input.GetKeyDown(KeyCode.M))
                    SceneManager.LoadScene("start");
            }
        }

        [Command]
        void CmdPause()
        {
            _syncPaused = true;
        }

        [Command]
        void CmdResume()
        {
            _syncPaused = false;
        }
    }
}
