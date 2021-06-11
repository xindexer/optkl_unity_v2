using UnityEngine;
using Optkl.Utilities;
using Optkl.Data;
using Optkl.Load;
using System;

namespace Optkl
{
    [System.Serializable]
    public class OptklManager : MonoBehaviour
    {
        [SerializeField]
        private DrawManager drawManager;

        [SerializeField]
        private DataStrike dataStrike;

        [SerializeField]
        private DataStorage dataStorage;

        [SerializeField]
        private TrackData trackData;

        [SerializeField]
        private TrackColors trackColors;

        [SerializeField]
        private ColorControl colorControl;

        [SerializeField]
        private TexturesAndMaterials texturesAndMaterials;

        [SerializeField]
        private TrackLabels trackLabels;

        [SerializeField]
        private TrackTickLabels trackTickLabels;

        [SerializeField]
        private DataParameters dataParameters;

        [SerializeField]
        private DataMax dataMax;

        [SerializeField]
        private Settings settings;

        [SerializeField]
        private Logger logger;
        
        private LoadData loadData = new LoadData();

        private float blockTimer;
        
        private void Awake()
        {
            ClearCalculatedVariables();
            dataStorage.tradeDate.Clear();
            InputOptionData data = new InputOptionData();
            data.Symbol = "AAPL";
            data.TradeDate = Convert.ToDateTime("Jun 7, 2021");
            data.Lookback = 50;
            InitialLoad(data);
        }

        private void ClearCalculatedVariables()
        {
            trackData.tradeDate.Clear();
            trackColors.tradeDate.Clear();
            trackLabels.tradeDate.Clear();
            trackTickLabels.tradeDate.Clear();
            dataStrike.tradeDate.Clear();
            dataMax.tradeDate.Clear();
            settings.tradeDate.Clear();
        }

        public void InitialLoad(InputOptionData data)
        {
            logger.Log($"Loading {data.Symbol} for {data.JsonTradeDate}");
            logger.StartTimer();
            dataParameters.TradeDate = data.FormatTradeDate;
            StartCoroutine(loadData.LoadSymbolData(data, dataStorage, true, this));
        }
        
        public void Load(InputOptionData data)
        {
            logger.Log($"Loading {data.Symbol} for {data.JsonTradeDate}");
            logger.StartTimer();
            dataParameters.TradeDate = data.FormatTradeDate;
            StartCoroutine(loadData.LoadSymbolData(data, dataStorage, false, this));
        }

        public void BuildIRIS(Boolean redraw = false, string symbol = "")
        {

            if (!redraw)
            {
                logger.EndTimer("Load Data");
                blockTimer = Time.realtimeSinceStartup;
                dataParameters.TradeSymbol = symbol;
            }
            StoragelData storageData = dataStorage.tradeDate[dataParameters.TradeName];
            logger.StartTimer();
            LabelParameters labelParameters = new LabelParameters();
            labelParameters.BuildLabels(storageData.optionDataSet, dataParameters, dataStrike, dataMax, settings);
            //logger.EndTimer("Build Labels");
            //logger.StartTimer();
            TickParameters tickParameters = new TickParameters();
            tickParameters.BuildTicks(
                true,
                storageData.optionDataSet,
                trackData,
                dataParameters,
                dataStrike,
                dataMax,
                settings,
                trackLabels,
                trackTickLabels);
            tickParameters.BuildTicks(
                false,
                storageData.optionDataSet,
                trackData,
                dataParameters,
                dataStrike,
                dataMax,
                settings,
                trackLabels,
                trackTickLabels);
            //logger.EndTimer("Build Ticks");
            //logger.StartTimer();
            TrackParameters trackParameters = new TrackParameters();
            trackParameters.BuildTracks(
                storageData.optionDataSet,
                trackData,
                trackColors,
                colorControl,
                dataParameters,
                dataStrike,
                dataMax,
                settings);
            //logger.EndTimer("Build Tracks");
            //logger.StartTimer();
            drawManager.DrawOptions(
                trackData,
                trackColors,
                trackLabels,
                trackTickLabels,
                texturesAndMaterials,
                colorControl,
                dataParameters,
                redraw,
                this);
        }

        public void ShowIRIS()
        {
            // ClearCalculatedVariables();            
            logger.EndTimer("Draw IRIS");
        }

        public void RespondToEvent()
        {
            // if (Time.realtimeSinceStartup - blockTimer > 2)
            // {
            //     ClearCalculatedVariables();
            //     Camera.main.backgroundColor = colorControl.backGroundColor;
            //     BuildIRIS(true);
            // }
        }
    }
}
