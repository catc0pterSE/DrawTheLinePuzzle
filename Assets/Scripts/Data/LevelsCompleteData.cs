using System;
using Infrastructure.SaveLoad;
using UnityEngine;
using Utility.Static;

namespace Data
{
    [Serializable]
    public class LevelsCompleteData : ISavable
    {
        [SerializeField] private bool[] _data = new bool[NumericConstants.TotalLevelsNumber];

        public bool Get(int levelNumber) =>
            _data[levelNumber - 1];

        public void Set(int levelNumber, bool value)
            => _data[levelNumber - 1] = value;

        public void Update(int newLevelCount)
        {
            if (newLevelCount == _data.Length)
                return;

            bool[] newArray = new bool[newLevelCount];
            int levelsToCopyCount = newLevelCount > _data.Length ? _data.Length : newLevelCount;

            for (int i = 0; i < levelsToCopyCount; i++)
            {
                newArray[i] = _data[i];
            }

            _data = newArray;
        }
    }
}