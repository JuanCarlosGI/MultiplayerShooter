using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ScoreAnalizer
    {
        private readonly IDataManager<int> _dm = new DictionaryManager();

        public void SaveScore(int score)
        {
            _dm.SetData("HighScore", score);
        }

        public int GetHighScore()
        {
            return _dm.GetData("HighScore");
        }
    }

    internal interface IDataManager<T>
    {
        void SetData(string key, T value);
        T GetData(string key);
    }

    internal class DictionaryManager : IDataManager<int>
    {
        private static Dictionary<string, int> _dict = new Dictionary<string, int>();

        public void SetData(string key, int value)
        {
            _dict[key] = value;
        }

        public int GetData(string key)
        {
            return _dict.ContainsKey(key) ? _dict[key] : 0;
        }
    }
}
