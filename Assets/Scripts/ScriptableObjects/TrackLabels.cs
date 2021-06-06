using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;
using System.Collections.Generic;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Track Labels")]
    public class TrackLabels : ScriptableObject
    {
        [SerializeField]
        public TrackLabelsMainDict tradeDate;
    }

    [System.Serializable]
    public class TrackLabel
    {
        public TrackLabelsNestedDict side;
    }

    [System.Serializable]
    public class LabelList
    {
        public List<Label> labelList;
    }

    [System.Serializable]
    public class TrackLabelsMainDict : SerializableDictionary<string, TrackLabel> { }

    [System.Serializable]
    public class TrackLabelsNestedDict : SerializableDictionary<string, LabelList> { }
}
