using System;
using UnityEngine;

namespace TopDownShooter
{
    [Serializable]
    public class CharacterData
    {
        public Vector3 Position = Vector3.zero;
        public Quaternion Rotation = Quaternion.identity;
        public string WeaponName = "";
        public int CurrentWeaponAmmo = 0;
        public int CurrentHealth = 1;
        public bool IsDead = false;
    }

    [Serializable]
    public class GameData
    {
        public SeriazableDictionary<string, CharacterData> Characters;
        public SeriazableDictionary<string, bool> Usables;

        public GameData()
        {
            Characters = new SeriazableDictionary<string, CharacterData>();
            Usables = new SeriazableDictionary<string, bool>();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}