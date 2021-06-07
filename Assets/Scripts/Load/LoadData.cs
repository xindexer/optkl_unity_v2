using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Optkl.Data {

    public class LoadData
    {
        public IEnumerator LoadFirstData(InputOptionData data, DataStorage dataStorage, OptklManager optklManager)
        {
            string jsonURL = "https://rghl12kkzd.execute-api.us-east-1.amazonaws.com/dev/option_data?ticker=" + data.Symbol + "&date=" + data.JsonTradeDate;
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
                    StoragelData customStorage = new StoragelData();
                    customStorage.optionDataSet = jsnArray.symbolData;
                    dataStorage.tradeDate.Add(data.FormatTradeDate, customStorage);

                    optklManager.BuildIRIS(data.FormatTradeDate);
                    //notFound.gameObject.SetActive(false);
                }
            }
        }
    }
}