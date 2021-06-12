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
        private Camera cam;
        
        [SerializeField]
        private DrawManager drawManager;

        [SerializeField]
        private DataStrike dataStrike;

        [SerializeField]
        private DataStorage dataStorage;

        [SerializeField]
        private ColorControl colorControl;
        
        [SerializeField]
        private DataParameters dataParameters;

        [SerializeField]
        private DataMax dataMax;

        [SerializeField]
        private Logger logger;
        
        private LoadData _loadData = new LoadData();

        private int _blockCounter = 0;
        
        private void Awake()
        {
            ClearCalculatedVariables();
            InputOptionData data = new InputOptionData()
            {
                Symbol = "AAPL",
                TradeDate = Convert.ToDateTime("Jun 7, 2021"),
                Lookback = 50
            };
            InitialLoad(data);
        }
        
        public void ClearCalculatedVariables()
        {
            dataStrike.tradeDate.Clear();
            dataMax.tradeDate.Clear();
        }

        public void InitialLoad(InputOptionData data)
        {
            logger.Log($"Loading {data.Symbol} for {data.JsonTradeDate}");
            logger.StartTimer();
            dataParameters.TradeDate = data.FormatTradeDate;
            StartCoroutine(_loadData.LoadSymbolData(data, dataStorage, true, this));
        }
        
        public void Load(InputOptionData data)
        {
            ClearCalculatedVariables();
            logger.Log($"Loading {data.Symbol} for {data.JsonTradeDate}");
            logger.StartTimer();
            dataParameters.TradeDate = data.FormatTradeDate;
            StartCoroutine(_loadData.LoadSymbolData(data, dataStorage, false, this));
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
            if (!dataStrike.tradeDate.TryGetValue(dataParameters.TradeName, out StrikeData value ))
            {
                StrikeParameters strikeParameters = new StrikeParameters();
                strikeParameters.BuildStrikeData(
                    storageData.optionDataSet,
                    dataParameters,
                    dataStrike,
                    dataMax);
            }
            CircumferenceParameters circumferenceParameters = new CircumferenceParameters();
            CircumferenceParameterData circumferenceParameterData = circumferenceParameters.CalculateCircumference(
                    dataParameters,
                    dataStrike);
            TickParameters tickParameters = new TickParameters();
            List<Label> tickLabelList = new List<Label>();
            List<Label> trackLabelList = new List<Label>();
            List<Vector3> tickPositionData = new List<Vector3>();
            tickParameters.BuildTicks(
                true,
                dataParameters,
                dataStrike,
                circumferenceParameterData,
                ref tickLabelList,
                ref trackLabelList,
                ref tickPositionData);
            tickParameters.BuildTicks(
                false,
                dataParameters,
                dataStrike,
                circumferenceParameterData,
                ref tickLabelList,
                ref trackLabelList,
                ref tickPositionData
            );
            TrackParameters trackParameters = new TrackParameters();
            trackParameters.BuildTracks(
                storageData.optionDataSet,
                colorControl,
                dataParameters,
                dataStrike,
                dataMax,
                circumferenceParameterData,
                out Dictionary<string, List<Vector3>> trackPositionData,
                out Dictionary<string, List<Color32>> trackColorData);
            drawManager.DrawIRIS(
                trackPositionData,
                trackColorData,
                tickLabelList,
                trackLabelList,
                tickPositionData,
                colorControl,
                dataParameters,
                redraw,
                this);
        }

        public void ShowIRIS()
        {
            logger.EndTimer("Draw IRIS");
        }

        public void RespondToEvent()
        {
            if (_blockCounter < 6)
            {
                _blockCounter++;
            }
            else 
            {
                Camera.main.backgroundColor = colorControl.backGroundColor;
                BuildIRIS(true);
            }
        }
    }
}
