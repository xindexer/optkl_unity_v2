using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;


namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Date Strike")]
    public class DataStrike : ScriptableObject
    {
        [SerializeField]
        public DataStrikeMainDict tradeDate;
    }

    [System.Serializable]
    public class StrikeData
    {
        public DataStrikeNestedDict expireDate;
    }

    [System.Serializable]
    public class StrikeMinMax
    {
        public float strikeMax;
        public float strikeMin;
    }

    [System.Serializable]
    public class DataStrikeMainDict : SerializableDictionary<string, StrikeData> { }

    [System.Serializable]
    public class DataStrikeNestedDict : SerializableDictionary<string, StrikeMinMax> { }
}
