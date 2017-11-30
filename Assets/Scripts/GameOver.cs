using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class GameOver : MonoBehaviour
    {
        public static TimeSpan LatestScore;
        public static int LatestTargetsDestroyed;
        public Text GameOverText;
        private readonly ScoreAnalizer _sa = new ScoreAnalizer();

        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            var high = _sa.GetHighScore();
            GameOverText.text = "HighScore: " + high + "\n\n" +
                           "Your score: " + LatestTargetsDestroyed + "\n" +
                           "Time survived:\n" + TimeToString(LatestScore);
        }

        private string TimeToString(TimeSpan timeSpan)
        {
            return timeSpan.Minutes + ":" + (timeSpan.Seconds < 10 ? "0" : "") + timeSpan.Seconds + ":" +
                   (timeSpan.Milliseconds < 100 ? "0" : "") + timeSpan.Milliseconds / 10;
        }
    }
}
