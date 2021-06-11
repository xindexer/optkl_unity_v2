using UnityEngine;
using Optkl.Parameters;
using Optkl.Data;
using Optkl.Load;
using System;
using System.Collections.Generic;

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
        private Logger logger;
        
        private LoadData loadData = new LoadData();

        private int blockCounter = 0;
        
        private void Awake()
        {
            dataStorage.tradeDate.Clear();
            ClearCalculatedVariables();
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
                dataParameters.TradeSymbol = symbol;
            }
            StorageData storageData = dataStorage.tradeDate[dataParameters.TradeName];
            logger.StartTimer();
            StrikeParameters strikeParameters = new StrikeParameters();
            TrackReturn trackReturn = strikeParameters.BuildStrikeData(
                storageData.optionDataSet, 
                dataParameters, 
                dataStrike, 
                dataMax);
            TickParameters tickParameters = new TickParameters();
            tickParameters.BuildTicks(
                true,
                trackData,
                dataParameters,
                dataStrike,
                trackLabels,
                trackTickLabels,
                trackReturn);
            tickParameters.BuildTicks(
                false,
                trackData,
                dataParameters,
                dataStrike,
                trackLabels,
                trackTickLabels,
                trackReturn
            );
            TrackParameters trackParameters = new TrackParameters();
            trackParameters.BuildTracks(
                storageData.optionDataSet,
                trackData,
                trackColors,
                colorControl,
                dataParameters,
                dataStrike,
                dataMax,
                trackReturn);
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
            ClearCalculatedVariables();
            logger.EndTimer("Draw IRIS");
        }

        public void RespondToEvent()
        {
            if (blockCounter < 5)
            {
                blockCounter++;
            }
            else 
            {
                ClearCalculatedVariables();
                Camera.main.backgroundColor = colorControl.backGroundColor;
                BuildIRIS(true);
            }
        }
    }
}
