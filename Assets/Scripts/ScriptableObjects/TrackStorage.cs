using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;
using System.Collections.Generic;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Track Storage")]
    public class TrackStorage : ScriptableObject
    {
        [SerializeField]
        public TrackStorageMainDict tradeDate;
    }

    [System.Serializable]
    public class TrackData
    {
        public TrackStorageNestedDict trackName;
    }

    [System.Serializable]
    public class TrackList
    {
        public List<Vector3> vectorList;
    }

    [System.Serializable]
    public class TrackStorageMainDict : SerializableDictionary<string, TrackData> { }

    [System.Serializable]
    public class TrackStorageNestedDict : SerializableDictionary<string, TrackList> { }
}
