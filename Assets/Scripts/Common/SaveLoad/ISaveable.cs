using System;
using UnityEngine;
using UnityEngine.Events;

namespace TopDownShooter
{
    public interface ISaveable
    {
       public void Save(ref GameData gameData);
       public void Load(GameData gameData);
    }
}