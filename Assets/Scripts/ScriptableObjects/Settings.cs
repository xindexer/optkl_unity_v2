using UnityEngine;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionaryPro;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Settings")]
    public class Settings : ScriptableObject
    {
        [SerializeField]
        public SettingsMainDict tradeDate;
    }

    [System.Serializable]
    public class SettingsData
    {
        public SettingsNestedDict settings;
    }

    [System.Serializable]
    public class SettingsMainDict : SerializableDictionary<string, SettingsData> { }

    [System.Serializable]
    public class SettingsNestedDict : SerializableDictionary<string, float> { }
}