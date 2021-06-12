using UnityEngine;
using RotaryHeart.Lib.SerializableDictionaryPro;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Textures and Materials")]
    public class TexturesAndMaterials : ScriptableObject
    {
        [SerializeField]
        public TextureDict OptklTextures = new TextureDict()
        {
           { "yte", new OptklTexturesAndMaterials(null, null) },
           { "Oi", new OptklTexturesAndMaterials(null, null) },
           { "Volu", new OptklTexturesAndMaterials(null, null) },
           { "BidPx", new OptklTexturesAndMaterials(null, null) },
           { "Value", new OptklTexturesAndMaterials(null, null) },
           { "AskP", new OptklTexturesAndMaterials(null, null) },
           { "BidIv", new OptklTexturesAndMaterials(null, null) },
           { "MidIv", new OptklTexturesAndMaterials(null, null) },
           { "AskIv", new OptklTexturesAndMaterials(null, null) },
           { "smoothSmvVol", new OptklTexturesAndMaterials(null, null) },
           { "iRate", new OptklTexturesAndMaterials(null, null) },
           { "divRate", new OptklTexturesAndMaterials(null, null) },
           { "residualqateData", new OptklTexturesAndMaterials(null, null) },
           { "extVol", new OptklTexturesAndMaterials(null, null) },
           { "extTheo", new OptklTexturesAndMaterials(null, null) },
           { "delta", new OptklTexturesAndMaterials(null, null) },
           { "gamma", new OptklTexturesAndMaterials(null, null) },
           { "theta", new OptklTexturesAndMaterials(null, null) },
           { "vega", new OptklTexturesAndMaterials(null, null) },
           { "rho", new OptklTexturesAndMaterials(null, null) },
           { "phi", new OptklTexturesAndMaterials(null, null) },
           { "driftlessTheta", new OptklTexturesAndMaterials(null, null) },
        };

        [System.Serializable]
        public class TextureDict : SerializableDictionary<string, OptklTexturesAndMaterials> { };

    }
}