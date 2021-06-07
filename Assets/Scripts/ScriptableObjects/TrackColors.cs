using RotaryHeart.Lib.SerializableDictionaryPro;
using UnityEngine;
using System.Collections.Generic;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Track Colors")]
    public class TrackColors : ScriptableObject
    {
        [SerializeField]
        public TrackColorsMainDict tradeDate;
    }

    [System.Serializable]
    public class TrackColorsContainer
    {
        public TrackColorsContainerNestedDict trackName;
    }

    [System.Serializable]
    public class TrackColorsList
    {
        public List<Color32> colorList;
    }

    [System.Serializable]
    public class TrackColorsMainDict : SerializableDictionary<string, TrackColorsContainer> { }

    [System.Serializable]
    public class TrackColorsContainerNestedDict : SerializableDictionary<string, TrackColorsList> { }
}
