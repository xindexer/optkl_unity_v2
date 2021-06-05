using UnityEngine;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionaryPro;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Data Labels")]
    public class DataLabels : ScriptableObject
    {
        [SerializeField]
        public DataLabelMainDict tradeDate;
    }

    [System.Serializable]
    public class LabelData
    {
        public List<string> order;
    }

    [System.Serializable]
    public class DataLabelMainDict : SerializableDictionary<string, LabelData> { }

}