using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace TopDownShooter
{
    public class SaveLoadManager : Singleton<SaveLoadManager>
    {
        [SerializeField]
        private string FileName;

        private List<ISaveable> _saveables = new List<ISaveable>();
        private List<Character> _characters = new List<Character>();
        private GameData _gameData;

        private void Start()
        {
            _gameData = new GameData();
            _characters = new List<Character>(FindObjectsByType<Character>(FindObjectsSortMode.None));
            _saveables = FindAllSaveables();
            SubscribeToCharacterDeath();
        }

        public void Save()
        {
            var fullPath = Path.Combine(Application.persistentDataPath, FileName);            
            foreach (var saveable in  _saveables)
            {
                saveable.Save(ref _gameData);
            }
            try
            {                               
                using (FileStream fs = File.OpenWrite(fullPath))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(_gameData.ToString());
                    }
                }
                Debug.Log($"Saved to {fullPath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"File Write Error: {fullPath} can't be written: {e}");
            }
        }

        public void Load()
        {
            string jsonData;
            var fullPath = Path.Combine(Application.persistentDataPath, FileName);
            if (File.Exists(fullPath))
            {
                try
                {
                    using (FileStream fs = File.OpenRead(fullPath))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            jsonData = sr.ReadToEnd();
                        }
                    }
                    _gameData = JsonUtility.FromJson<GameData>(jsonData);
                    foreach(var saveable in _saveables)
                    {
                        saveable.Load(_gameData);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"File Read Error: {fullPath} can't be read: {e}");
                }
            }
        }

        private void SubscribeToCharacterDeath()
        {
            foreach (var character in _characters)
            {
                character.OnDeath += CharacterDeath;
            }
        }

        private void CharacterDeath(string id)
        {
            _gameData.Characters[id].IsDead = true;
        }

        private List<ISaveable> FindAllSaveables()
        {
            IEnumerable<ISaveable> dataSaveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>();
            return new List<ISaveable>(dataSaveables);
        }
    }
}