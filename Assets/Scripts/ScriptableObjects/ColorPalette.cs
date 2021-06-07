using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionaryPro;
using Optkl.Events;
using System;

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
        public ChooseColorStorageMainDict colorSet;

        [SerializeField]
        public ChooseGreekColorStorageMainDict greekColorSet;

        public enum EnumPalette
        {
            palette1,
            palette2,
            palette3,
            palette4,
            palette5
        }

        public enum EnumColor
        {
            GreekNeon1,
            GreekNeon2,
            GreekNeon3,
            GreekNeon4,
            GreekNeon5,
            GreekNeon6,
            GreekPastel1,
            GreekPastel2,
            GreekPastel3,
            GreekPastel4,
            GreekPastel5,
            GreekPastel6,
            GreekCustom1,
            GreekCustom2,
            GreekCustom3,
            GreekCustom4,
            GreekCustom5,
            GreekCustom6,
        }

        public enum EnumTrack
        {
            yte,
            Oi,
            Volu,
            BidPx,
            Value,
            AskPx,
            BidIv,
            MidIv,
            AskIv,
            smoothSmvVol,
            iRate,
            divRate,
            residualRateData,
            extVol,
            extTheo
        }

        public enum EnumGreek
        {
            delta,
            gamma,
            theta,
            vega,
            rho,
            phi,
            driftlessTheta,
        }

        [SerializeField]
        public Color backGroundColor = new Color(0, 0, 0);

        public List<Color32> GetColorPalette(EnumPalette input)
        {

            if (input == EnumPalette.palette1)
                return palette1;
            else if (input == EnumPalette.palette2)
                return palette2;
            else if (input == EnumPalette.palette3)
                return palette3;
            else if (input == EnumPalette.palette4)
                return palette4;
            else if (input == EnumPalette.palette5)
                return palette5;
            return palette1;
        }

        public Color GetGreekColor(EnumColor input)
        {
            if (input == EnumColor.GreekNeon1)
                return GreeksNeon1;
            else if(input == EnumColor.GreekNeon2)
                return GreeksNeon2;
            else if(input == EnumColor.GreekNeon3)
                return GreeksNeon3;
            else if (input == EnumColor.GreekNeon4)
                return GreeksNeon4;
            else if (input == EnumColor.GreekNeon5)
                return GreeksNeon5;
            else if (input == EnumColor.GreekNeon6)
                return GreeksNeon6;
            else if(input == EnumColor.GreekNeon1)
                return GreeksPastel1;
            else if (input == EnumColor.GreekPastel2)
                return GreeksPastel2;
            else if (input == EnumColor.GreekPastel3)
                return GreeksPastel3;
            else if (input == EnumColor.GreekPastel4)
                return GreeksPastel4;
            else if (input == EnumColor.GreekPastel5)
                return GreeksPastel5;
            else if (input == EnumColor.GreekPastel6)
                return GreeksPastel6;
            else if(input == EnumColor.GreekCustom1)
                return GreeksCustom1;
            else if (input == EnumColor.GreekCustom2)
                return GreeksCustom2;
            else if (input == EnumColor.GreekCustom3)
                return GreeksCustom3;
            else if (input == EnumColor.GreekCustom4)
                return GreeksCustom4;
            else if (input == EnumColor.GreekCustom5)
                return GreeksCustom5;
            else if (input == EnumColor.GreekCustom6)
                return GreeksCustom6;
            return GreeksNeon1;
        }

        [System.Serializable]
        public class ChooseColorStorageMainDict : SerializableDictionary<EnumTrack, EnumPalette> { };

        [System.Serializable]
        public class ChooseGreekColorStorageMainDict : SerializableDictionary<EnumGreek, EnumColor> { };


        [SerializeField]
        public List<Color32> palette1 = new List<Color32>() {
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

        [SerializeField]
        public List<Color32> palette2 = new List<Color32>() {
            { new Color(215f / 255f,  48f / 255f,  39f / 255f, .5f) },
            { new Color(244f / 255f, 109f / 255f,  67f / 255f) },
            { new Color(253f / 255f, 174f / 255f,  97f / 255f) },
            { new Color(254f / 255f, 224f / 255f, 144f / 255f) },
            { new Color(255f / 255f, 255f / 255f, 191f / 255f) },
            { new Color(224f / 255f, 243f / 255f, 248f / 255f) },
            { new Color(171f / 255f, 217f / 255f, 233f / 255f) },
            { new Color(116f / 255f, 173f / 255f, 209f / 255f) },
            { new Color( 69f / 255f, 117f / 255f, 180f / 255f) }
        };

        [SerializeField]
        public List<Color32> palette3 = new List<Color32>();

        [SerializeField]
        public List<Color32> palette4 = new List<Color32>();

        [SerializeField]
        public List<Color32> palette5 = new List<Color32>();

        [SerializeField]
        public Color32 GreeksNeon1 = new Color(1f, 0f, 0f);

        [SerializeField]
        public Color32 GreeksNeon2 = new Color(0f, 1f, 0f);

        [SerializeField]
        public Color32 GreeksNeon3 = new Color(0f, 0f, 1f);

        [SerializeField]
        public Color32 GreeksNeon4 = new Color(1f, 1f, 0f);

        [SerializeField]
        public Color32 GreeksNeon5 = new Color(1f, 0f, 1f);

        [SerializeField]
        public Color32 GreeksNeon6 = new Color(0f, 1f, 1f);

        [SerializeField]
        public Color32 GreeksPastel1 = new Color(204f / 255f, 153f / 255f, 210f / 255f);

        [SerializeField]
        public Color32 GreeksPastel2 = new Color(158f / 255f, 193f / 255f, 207f / 255f);

        [SerializeField]
        public Color32 GreeksPastel3 = new Color(158f / 255f, 224f / 255f, 158f / 255f);

        [SerializeField]
        public Color32 GreeksPastel4 = new Color(253f / 255f, 253f / 255f, 151f / 255f);

        [SerializeField]
        public Color32 GreeksPastel5 = new Color(254f / 255f, 177f / 255f, 68f / 255f);

        [SerializeField]
        public Color32 GreeksPastel6 = new Color(255f / 255f, 102f / 255f, 51f / 255f);

        [SerializeField]
        public Color32 GreeksCustom1;

        [SerializeField]
        public Color32 GreeksCustom2;

        [SerializeField]
        public Color32 GreeksCustom3;

        [SerializeField]
        public Color32 GreeksCustom4;

        [SerializeField]
        public Color32 GreeksCustom5;

        [SerializeField]
        public Color32 GreeksCustom6;
    }
}
