using UnityEngine;
using RotaryHeart.Lib.SerializableDictionaryPro;


namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Data Storage")]
    public class DataStorage : ScriptableObject
    {
        [SerializeField]
        public DataStorageMainDict tradeDate;
    }

    public class StorageData
    {
        public float[][] optionDataSet;
    }

    [System.Serializable]
    public class DataStorageMainDict : SerializableDictionary<string, StorageData> { }
}