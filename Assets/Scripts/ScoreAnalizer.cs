using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ScoreAnalizer
    {
        private readonly IDataManager<TimeSpan> _dm = new DictionaryManager();

        public void SaveScore(TimeSpan timeSpan)
        {
            _dm.SetData("HighScore", timeSpan);
        }

        public TimeSpan GetHighScore()
        {
            return _dm.GetData("HighScore");
        }
    }

    internal interface IDataManager<T>
    {
        void SetData(string key, T value);
        T GetData(string key);
    }

    internal class DictionaryManager : IDataManager<TimeSpan>
    {
        private static Dictionary<string, TimeSpan> _dict = new Dictionary<string, TimeSpan>();

        public void SetData(string key, TimeSpan value)
        {
            _dict[key] = value;
        }

        public TimeSpan GetData(string key)
        {
            return _dict.ContainsKey(key) ? _dict[key] : new TimeSpan();
        }
    }
}
