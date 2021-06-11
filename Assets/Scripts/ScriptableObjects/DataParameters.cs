using System;
using UnityEngine;
using Optkl.Events;
using RotaryHeart.Lib.SerializableDictionaryPro;

namespace Optkl.Data
{
    [CreateAssetMenu(menuName = "Optkl/Data Parameters")]
    public class DataParameters : ScriptableObject
    {
        public GameEvent changeParameters;

#if UNITY_EDITOR
        private void OnValidate() { UnityEditor.EditorApplication.delayCall += _OnValidate; }
        private void _OnValidate()
        {
            changeParameters.Raise();
        }
#endif

        private string tradeDate;
        public string TradeDate
        {
            get
            {
                return tradeDate;
            }
            set
            {
                tradeDate = value;
            }
        }

        private string tradeSymbol;
        public string TradeSymbol
        {
            get
            {
                return tradeSymbol;
            }
            set
            {
                tradeSymbol = value;
            }
        }

        public string TradeName {
            get { return tradeSymbol + "-" + tradeDate; }
        }

        [SerializeField]
        [Range(5000, 8000)]
        private int trackRadius = 6000;
        public int TrackRadius
        {
            get
            {
                return trackRadius;
            }
            set
            {
                trackRadius = value;
            }
        }

        [SerializeField]
        [Range(0, 250)]
        private int trackSpacer = 100;
        public int TrackSpacer
        {
            get
            {
                return trackSpacer;
            }
            set
            {
                trackSpacer = value;
            }
        }

        [SerializeField]
        [Range(0f, 10f)]
        private float pieSpacer = 3f;
        public float PieSpacer
        {
            get
            {
                return pieSpacer;
            }
            set
            {
                pieSpacer = value;
            }
        }

        [SerializeField]
        [Range(0f, 20f)]
        private float powerWedge = 10f;
        public float PowerWedge
        {
            get
            {
                return powerWedge;
            }
            set
            {
                powerWedge = value;
            }
        }

        [SerializeField]
        [Range(50, 1000)]
        private int trackThickness = 150;
        public int TrackThickness
        {
            get
            {
                return trackThickness;
            }
            set
            {
                trackThickness = value;
            }
        }

        [SerializeField]
        [Range(0.05f, 5f)]
        private float trackLineWidth = 1f;
        public float TrackLineWidth
        {
            get
            {
                return trackLineWidth;
            }
            set
            {
                trackLineWidth = value;
            }
        }

        [SerializeField]
        [Range(1000, 5000)]
        private int greekInnerRadius = 1000;
        public int GreekInnerRadius
        {
            get
            {
                return greekInnerRadius;
            }
            set
            {
                greekInnerRadius = value;
            }
        }

        [SerializeField]
        [Range(3000, 10000)]
        private int greekOuterRadius = 1000;
        public int GreekOuterRadius
        {
            get
            {
                return greekOuterRadius;
            }
            set
            {
                greekOuterRadius = value;
            }
        }

        [SerializeField]
        [Range(0.05f, 50f)]
        private float greekSize = 1f;
        public float GreekSize
        {
            get
            {
                return greekSize;
            }
            set
            {
                greekSize = value;
            }
        }

        [SerializeField]
        [Range(0f, .5f)]
        private float greekOpacity = 0.3f;
        public float GreekOpacity
        {
            get
            {
                return greekOpacity;
            }
            set
            {
                greekOpacity = value;
            }
        }

        [SerializeField]
        private Boolean negativeGreeks = false;
        public Boolean NegativeGreeks
        {
            get
            {
                return negativeGreeks;
            }
            set
            {
                negativeGreeks = value;
            }
        }

        [SerializeField]
        [Range(6000, 20000)]
        private int tickRadius = 10000;
        public int TickRadius
        {
            get
            {
                return tickRadius;
            }
            set
            {
                tickRadius = value;
            }
        }

        [SerializeField]
        [Range(0, 500)]
        private int tickHeight = 30;
        public int TickHeight
        {
            get
            {
                return tickHeight;
            }
            set
            {
                tickHeight = value;
            }
        }

        [SerializeField]
        [Range(1, 5)]
        private int majorTickMultiplier = 1;
        public int MajorTickMultiplier
        {
            get
            {
                return majorTickMultiplier;
            }
            set
            {
                majorTickMultiplier = value;
            }
        }

        [SerializeField]
        [Range(0.1f, 10f)]
        private int tickWidth = 1;
        public int TickWidth
        {
            get
            {
                return tickWidth;
            }
            set
            {
                tickWidth = value;
            }
        }

        [SerializeField]
        private Boolean showTicks = true;
        public Boolean ShowTicks
        {
            get
            {
                return showTicks;
            }
            set
            {
                showTicks = value;
            }
        }

        [SerializeField]
        [Range(5, 20)]
        private float tickLabelSize = 5f;
        public float TickLabelSize
        {
            get
            {
                return tickLabelSize;
            }
            set
            {
                tickLabelSize = value;
            }
        }

        [SerializeField]
        [Range(1, 20)]
        private float tickLabelDistanceMultiplier = 5f;
        public float TickLabelDistanceMultiplier
        {
            get
            {
                return tickLabelDistanceMultiplier;
            }
            set
            {
                tickLabelDistanceMultiplier = value;
            }
        }

        [SerializeField]
        private Boolean showTickLabels = true;
        public Boolean ShowTickLabels
        {
            get
            {
                return showTickLabels;
            }
            set
            {
                showTickLabels = value;
            }
        }

        [SerializeField]
        [Range(10, 40)]
        private float labelSize = 10f;
        public float LabelSize
        {
            get
            {
                return labelSize;
            }
            set
            {
                labelSize = value;
            }
        }

        [SerializeField]
        [Range(10, 40)]
        private float labelDistanceMultiplier = 5f;
        public float LabelDistanceMultiplier
        {
            get
            {
                return labelDistanceMultiplier;
            }
            set
            {
                labelDistanceMultiplier = value;
            }
        }

        [SerializeField]
        private Boolean showLabels = true;
        public Boolean ShowLabels
        {
            get
            {
                return showLabels;
            }
            set
            {
                showLabels = value;
            }
        }


        [SerializeField]
        [Range(0.5f, 10)]
        private float axisWidth = 1f;
        public float AxisWidth
        {
            get
            {
                return axisWidth;
            }
            set
            {
                axisWidth = value;
            }
        }

        [SerializeField]
        private Boolean showAxis = true;
        public Boolean ShowAxis
        {
            get
            {
                return showAxis;
            }
            set
            {
                showAxis = value;
            }
        }

        [SerializeField]
        [Range(0.5f, 10)]
        private float seanCircleWidth = 1f;
        public float SeanCicrleWidth
        {
            get
            {
                return seanCircleWidth;
            }
            set
            {
                seanCircleWidth = value;
            }
        }

        [SerializeField]
        [Range(10, 1000)]
        private float seanCircleTick = 200f;
        public float SeanCircleTick
        {
            get
            {
                return seanCircleTick;
            }
            set
            {
                seanCircleTick = value;
            }
        }

        [SerializeField]
        [Range(0, 3)]
        private float seanCircleSeparator = .7f;
        public float SeanCircleSeparator
        {
            get
            {
                return seanCircleSeparator;
            }
            set
            {
                seanCircleSeparator = value;
            }
        }

        [SerializeField]
        [Range(0.5f, 100)]
        private float seanCircleMultiplier = 30f;
        public float SeanCircleMultiplier
        {
            get
            {
                return seanCircleMultiplier;
            }
            set
            {
                seanCircleMultiplier = value;
            }
        }

        [SerializeField]
        private Boolean showSeanCircle = true;
        public Boolean ShowSeanCicrle
        {
            get
            {
                return showSeanCircle;
            }
            set
            {
                showSeanCircle = value;
            }
        }

        [SerializeField]
        public TrackOrderDict TrackOrder = new TrackOrderDict()
        {
           { "Oi", new TrackControl(true, 1) },
           { "Volu", new TrackControl(true, 1) },
           { "BidPx", new TrackControl(true, 1) },
           { "Value", new TrackControl(true, 1) },
           { "AskPx", new TrackControl(true, 1) },
           { "BidIv", new TrackControl(true, 1) },
           { "MidIv", new TrackControl(true, 1) },
           { "AskIv", new TrackControl(true, 1) },
           { "smoothSmvVol", new TrackControl(true, 1) },
           { "iRate", new TrackControl(true, 1) },
           { "divRate", new TrackControl(true, 1) },
           { "residualRateData", new TrackControl(true, 1) },
           { "extVol", new TrackControl(true, 1) },
           { "extTheo", new TrackControl(true, 1) },
           { "yte", new TrackControl(true, 0.5f) },
           { "inTheMoney", new TrackControl(true, 0.5f) }
        };

        [System.Serializable]
        public class TrackOrderDict : SerializableDictionary<string, TrackControl> { };

        [SerializeField]
        public ShowGreekDict ShowGreek = new ShowGreekDict()
        {
           { "delta", true },
           { "gamma", true },
           { "theta", true },
           { "vega", true },
           { "rho", true },
           { "phi", true },
        };

        [System.Serializable]
        public class ShowGreekDict : SerializableDictionary<string, Boolean> { };
    }
}
