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
        private DataParameters dataParameters;

        [SerializeField]
        private DataLabels dataLabels;

        [SerializeField]
        private DataMax dataMax;

        [SerializeField]
        private Logger logger;

        [SerializeField]
        private Canvas openingCanvas;

        [SerializeField]
        private Canvas loadingCanvas;

        [SerializeField]
        private Canvas runningCanvas;

        private LoadData loadData = new LoadData();

        private float buildTime = 0;

        private void Awake()
        {
            buildTime = Time.realtimeSinceStartup;
            dataStorage.tradeDate.Clear();
            dataStrike.tradeDate.Clear();
            dataLabels.tradeDate.Clear();
            dataMax.tradeDate.Clear();
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
            StartCoroutine(loadData.LoadFirstData(data, dataStorage, this));
        }

        public void InitialLoadComplete(string tradeDate)
        {

            logger.EndTimer("Load Data");
            StoragelData storageData = dataStorage.tradeDate[tradeDate];
            logger.StartTimer();
            LabelParameters labelParameters = new LabelParameters();
            labelParameters.BuildLabels(storageData.optionDataSet, dataParameters, dataLabels, dataStrike, dataMax);
            logger.EndTimer("Build Labels");
        }
    }
}
