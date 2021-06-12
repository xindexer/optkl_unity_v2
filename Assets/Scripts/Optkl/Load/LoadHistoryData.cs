using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Optkl.Data;
namespace Optkl.Load
{
    public class LoadHistoryData
    {
        public IEnumerator LoadSymbolDataHistory(InputOptionData data, DataStorage dataStorage, int lookback, OptklManager optklManager)
        {
            string jsonURL = "file://" + Directory.GetCurrentDirectory() + "/Assets/Scripts/Optkl/Load/AAPL20210607Plus5.json";
            
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