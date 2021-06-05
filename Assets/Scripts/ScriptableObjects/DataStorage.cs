using UnityEngine;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionaryPro;


namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Data Storage")]
    public class DataStorage : ScriptableObject
    {
        [SerializeField]
        public DataStorageMainDict tradeDate;
    }

    public class StoragelData
    {
        public object[][] optionDataSet;
    }


    [System.Serializable]
    public class DataStorageMainDict : SerializableDictionary<string, StoragelData> { }

}