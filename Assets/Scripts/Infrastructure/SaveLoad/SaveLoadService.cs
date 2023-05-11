using System.Collections.Generic;
using UnityEngine;
using Utility.Extensions;

namespace Infrastructure.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private List<ISavable> _savables = new List<ISavable>();

        public bool TryLoad<T>(out T loadedObject) where T : class, ISavable
        {
            string json = PlayerPrefs.GetString(typeof(T).ToString());
            loadedObject = JsonUtility.FromJson<T>(json);
            return loadedObject != null;
        }

        public void RegisterSavable(ISavable savable) =>
            _savables.Add(savable);

        public void SaveAll()
        {
            _savables.Map(savable =>
            {
                PlayerPrefs.SetString(savable.GetType().ToString(), JsonUtility.ToJson(savable));
            });
        }
    }
}