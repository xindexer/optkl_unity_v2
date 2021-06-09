using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;
using System.Collections.Generic;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Track Tick Labels")]
    public class TrackTickLabels : ScriptableObject
    {
        [SerializeField]
        public TrackTickLabelsMainDict tradeDate;
    }

    [System.Serializable]
    public class TrackTickLabel
    {
        public TrackTickLabelsNestedDict side;
    }

    [System.Serializable]
    public class TickLabelList
    {
        public List<Label> tickLabelList;
    }

    [System.Serializable]
    public class TrackTickLabelsMainDict : SerializableDictionary<string, TrackTickLabel> { }

    [System.Serializable]
    public class TrackTickLabelsNestedDict : SerializableDictionary<string, TickLabelList> { }
}
