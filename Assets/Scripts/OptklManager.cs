using UnityEngine;
using Optkl.Utilities;
using Optkl.Data;
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
        private ColorPalette colorPalette;

        [SerializeField]
        private TexturesAndMaterials texturesAndMaterials;

        [SerializeField]
        private TrackLabels trackLabels;

        [SerializeField]
        private DataParameters dataParameters;

        [SerializeField]
        private DataMax dataMax;

        [SerializeField]
        private Settings settings;

        [SerializeField]
        private Logger logger;

        [SerializeField]
        private Canvas openingCanvas;

        [SerializeField]
        private Canvas loadingCanvas;

        [SerializeField]
        private Canvas runningCanvas;

        private LoadData loadData = new LoadData();

        private int blockOnValidate = 0;

        private void Awake()
        {
            ClearCalculatedVariables();
            dataStorage.tradeDate.Clear();
            StartCanvas();
        }

        private void ClearCalculatedVariables()
        {
            trackData.tradeDate.Clear();
            trackColors.tradeDate.Clear();
            trackLabels.tradeDate.Clear();
            dataStrike.tradeDate.Clear();
            dataMax.tradeDate.Clear();
            settings.tradeDate.Clear();
        }

        private void StartCanvas()
        {
            openingCanvas.gameObject.SetActive(true);
            loadingCanvas.gameObject.SetActive(false);
            runningCanvas.gameObject.SetActive(false);
        }

        public void InitialLoad(InputOptionData data)
        {
            openingCanvas.gameObject.SetActive(false);
            loadingCanvas.gameObject.SetActive(true);
            logger.Log($"Loading {data.Symbol} for {data.JsonTradeDate}");
            logger.StartTimer();
            dataParameters.TradeDate = data.FormatTradeDate;
            StartCoroutine(loadData.LoadFirstData(data, dataStorage, this));
        }

        public void BuildIRIS(string tradeDate, Boolean redraw = false)
        {

            if (!redraw)
                logger.EndTimer("Load Data");
            StoragelData storageData = dataStorage.tradeDate[tradeDate];
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
                trackLabels);
            tickParameters.BuildTicks(
                false,
                storageData.optionDataSet,
                trackData,
                dataParameters,
                dataStrike,
                dataMax,
                settings,
                trackLabels);
            //logger.EndTimer("Build Ticks");
            //logger.StartTimer();
            TrackParameters trackParameters = new TrackParameters();
            trackParameters.BuildTracks(
                storageData.optionDataSet,
                trackData,
                trackColors,
                colorPalette,
                dataParameters,
                dataStrike,
                dataMax,
                settings);
            //logger.EndTimer("Build Tracks");
            //logger.StartTimer();
            drawManager.DrawOptions(
                trackData,
                trackColors,
                colorPalette,
                texturesAndMaterials,
                dataParameters,
                redraw);
            loadingCanvas.gameObject.SetActive(false);
            runningCanvas.gameObject.SetActive(true);
            logger.EndTimer("Draw IRIS");
        }

        public void RespondToEvent()
        {
            if (blockOnValidate < 2)
            {
                blockOnValidate++;
            }
            else
            {
                Debug.Log(dataParameters.ShowGreek["delta"]);
                ClearCalculatedVariables();
                Camera.main.backgroundColor = colorPalette.backGroundColor;
                BuildIRIS(dataParameters.TradeDate, true);
            }
        }
    }
}
