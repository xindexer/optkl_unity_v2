using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Optkl.Load;

namespace Optkl.Data {

    public class LoadData
    {
        public IEnumerator LoadSymbolData(InputOptionData data, DataStorage dataStorage, Boolean firstLoad, OptklManager optklManager)
        {
            string jsonURL;
            if (firstLoad)
            {
                jsonURL = "file:///Users/dangordon/unity/optkl/Assets/Scripts/Load/AAPL20210607.json";
            }
            else
            {
                jsonURL = "https://rghl12kkzd.execute-api.us-east-1.amazonaws.com/dev/option_data?ticker=" +
                                 data.Symbol + "&date=" + data.JsonTradeDate;
            }

            UnityWebRequest www = UnityWebRequest.Get(jsonURL);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                JsonData jsnData = JsonConvert.DeserializeObject<JsonData>(www.downloadHandler.text);
                JsonList jsnArray = JsonConvert.DeserializeObject<JsonList>("{\"symbolData\":" + jsnData.data + "}");

                if (jsnArray.symbolData.Length == 0)
                {
                    //trigger event?
                    //notFound.gameObject.SetActive(true);
                    //reset(true);
                }
                else
                {
                    StorageData customStorage = new StorageData();
                    customStorage.optionDataSet = jsnArray.symbolData;
                    dataStorage.tradeDate.Add(data.Symbol + "-" + data.FormatTradeDate, customStorage);

                    optklManager.BuildIRIS(false, data.Symbol);
                    //notFound.gameObject.SetActive(false);
                }
            }
        }
    }
}