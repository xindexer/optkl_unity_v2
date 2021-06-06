using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Optkl.Utilities;
using Optkl.Data;

namespace Optkl
{
    [System.Serializable]
    public class OptklManager : MonoBehaviour
    {
        [SerializeField]
        private DataStrike dataStrike;

        [SerializeField]
        private DataStorage dataStorage;

        [SerializeField]
        private TrackStorage trackStorage;

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

        public GameObject threeDTextPrefab;


        private float buildTime;

        private void Awake()
        {
            buildTime = Time.realtimeSinceStartup;
            trackStorage.tradeDate.Clear();
            dataStorage.tradeDate.Clear();
            dataStrike.tradeDate.Clear();
            dataMax.tradeDate.Clear();
            settings.tradeDate.Clear();
            StartCanvas();
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

        public void InitialLoadComplete(string tradeDate)
        {

            logger.EndTimer("Load Data");
            StoragelData storageData = dataStorage.tradeDate[tradeDate];
            logger.StartTimer();
            LabelParameters labelParameters = new LabelParameters();
            labelParameters.BuildLabels(storageData.optionDataSet, dataParameters, dataStrike, dataMax, settings);
            logger.EndTimer("Build Labels");
            logger.StartTimer();
            TickParameters tickParameters = new TickParameters();
            tickParameters.BuildTicks(
                true,
                storageData.optionDataSet,
                trackStorage,
                dataParameters,
                dataStrike,
                dataMax,
                settings,
                trackLabels);
            tickParameters.BuildTicks(
                false,
                storageData.optionDataSet,
                trackStorage,
                dataParameters,
                dataStrike,
                dataMax,
                settings,
                trackLabels);
            logger.EndTimer("Build Ticks");
            //logger.StartTimer();
            //TrackParameters trackParameters = new TrackParameters();
            //trackParameters.BuildTracks(storageData.optionDataSet, dataParameters, dataStrike, dataMax, settings);
            //logger.EndTimer("Build Tracks");
        }
    }
}
