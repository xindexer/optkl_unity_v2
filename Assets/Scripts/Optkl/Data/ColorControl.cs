using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionaryPro;
using Optkl.Events;
using System;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Color Control")]
    public class ColorControl : ScriptableObject
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
        public ChooseColorStorageMainDict trackColorSet = new ChooseColorStorageMainDict()
        {
            {"yte", null},
            {"Oi", null},
            {"Volu", null},
            {"BidPx", null},
            {"Value", null},
            {"AskPx", null},
            {"BidIv", null},
            {"MidIv", null},
            {"AskIv", null},
            {"smoothSmvVol", null},
            {"iRate", null},
            {"divRate", null},
            {"residualRateData", null},
            {"extVol", null},
            {"extTheo", null},
            {"inTheMoney", null}
        };

        [SerializeField]
        public ChooseGreekColorStorageMainDict greekColorSet;

        [SerializeField]
        public InTheMoneyColorStorageMainDict inTheMoneyColorSet;

        [SerializeField]
        public Color backGroundColor = new Color(0, 0, 0);

        [SerializeField]
        public Color TickColor = new Color(1, 1, 1);

        [SerializeField]
        public Color LabelTickColor = new Color(1, 1, 1);

        [SerializeField]
        public Color LabelColor = new Color(1, 1, 1);

        [SerializeField]
        public Color AxisColor = new Color(1, 1, 1);

        [SerializeField]
        public Color SeanCircleColor = new Color(1, 1, 1);

        [System.Serializable]
        public class ChooseColorStorageMainDict : SerializableDictionary<string, ColorPalette> { };

        [System.Serializable]
        public class ChooseGreekColorStorageMainDict : SerializableDictionary<string, ColorPalette> { };

        [System.Serializable]
        public class InTheMoneyColorStorageMainDict : SerializableDictionary<string, ColorPalette> { };

    }
}
