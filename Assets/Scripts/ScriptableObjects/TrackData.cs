using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;
using System.Collections.Generic;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Track Data")]
    public class TrackData : ScriptableObject
    {
        [SerializeField]
        public TrackDataMainDict tradeDate;
    }

    [System.Serializable]
    public class TrackDataContainer
    {
        public TrackDataContainerNestedDict trackName;
    }

    [System.Serializable]
    public class TrackDataList
    {
        public List<Vector3> vectorList;
    }

    [System.Serializable]
    public class TrackDataMainDict : SerializableDictionary<string, TrackDataContainer> { }

    [System.Serializable]
    public class TrackDataContainerNestedDict : SerializableDictionary<string, TrackDataList> { }
}
