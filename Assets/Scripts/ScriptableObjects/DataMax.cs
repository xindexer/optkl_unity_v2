using System;
using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Data Max")]
    public class DataMax : ScriptableObject
    {
        [SerializeField]
        public DataMaxMainDict tradeDate;
    }

    [System.Serializable]
    public class MaxData
    {
        public DataMaxNestedDict maxValues;
    }

    [System.Serializable]
    public class DataMaxMainDict : SerializableDictionary<string, MaxData> { }

    [System.Serializable]
    public class DataMaxNestedDict : SerializableDictionary<string, double> { }
}