using System.Collections.Generic;
using Optkl.Events;
using UnityEngine;

namespace Optkl.Data
{

    [CreateAssetMenu(menuName = "Optkl/Color Palette")]
    public class ColorPalette : ScriptableObject
    {

        public GameEvent changeParameters;

#if UNITY_EDITOR
        private void OnValidate() { UnityEditor.EditorApplication.delayCall += _OnValidate; }
        private void _OnValidate()
        {
            changeParameters.Raise();
        }
#endif

        [SerializeField]
        public List<Color32> palette = new List<Color32>() {
            { new Color(1f, 1f, 229f / 255f, .5f) },
            { new Color(1f, 247f / 255f, 188f / 255f) },
            { new Color(254f / 255f, 227f / 255f, 145f / 255f) },
            { new Color(254f / 255f, 196f / 255f,  79f / 255f) },
            { new Color(254f / 255f, 153f / 255f,  41f / 255f) },
            { new Color(236f / 255f, 112f / 255f,  20f / 255f) },
            { new Color(204f / 255f,  76f / 255f,   2f / 255f) },
            { new Color(153f / 255f,  52f / 255f,   4f / 255f) },
            { new Color(102f / 255f,  37f / 255f,   6f / 255f) }
        };
    }
}
