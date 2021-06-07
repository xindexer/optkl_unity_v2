using System;
using System.Collections.Generic;
using Optkl.Data;
using UnityEngine;

namespace Optkl.Utilities
{
    public class LabelParameters
    {

        public void BuildLabels(
            float[][] optionData,
            DataParameters dataParameters,
            DataStrike dataStrike,
            DataMax dataMax,
            Settings settings)
        {
            DateTime pvDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); 
            string prevDateTime = pvDateTime.AddMilliseconds(optionData[0][1] * 1000 + 4.32e+7).ToString("yyyyMMMdd");
            Boolean isLast = false;
            Boolean isFirst = true;
            float minStrike = 100000000f;
            float maxStrike = 0f;
            float trackCircumference = 0f;
            int numberPies = 0;
            InitialParameters initialParameters = new InitialParameters();
            MaxData customMax = new MaxData();
            initializeDataMax(initialParameters, dataMax, customMax, dataParameters.TradeDate);
            for (int i = 0; i < optionData.Length; i++) 
            {
                if (i == optionData.Length - 1)
                    isLast = true;
                DateTime crDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                string currDateTime = crDateTime.AddMilliseconds(optionData[i][1]* 1000 + 4.32e+7).ToString("yyyyMMMdd");
                if (isLast)
                {
                    if(optionData[i][3] > maxStrike)
                    {
                        maxStrike = optionData[i][3];
                    }
                    if (optionData[i][3] < minStrike)
                    {
                        minStrike = optionData[i][3];
                    }
                }
                if (currDateTime != prevDateTime || isLast)
                {
                    StrikeData customStrike = new StrikeData();
                    customStrike.expireDate = new DataStrikeNestedDict();
                    StrikeMinMax newData = new StrikeMinMax()
                    {
                        strikeMin = minStrike,
                        strikeMax = maxStrike
                    };
                    if (isFirst)
                    {
                        customStrike.expireDate.Add(prevDateTime, newData);
                        dataStrike.tradeDate.Add(dataParameters.TradeDate, customStrike);
                        isFirst = false;
                    }
                    else
                    {
                        dataStrike.tradeDate[dataParameters.TradeDate].expireDate.Add(prevDateTime, newData);
                    }
                    trackCircumference += (maxStrike - minStrike);
                    numberPies++;
                    minStrike = 100000000f; 
                    maxStrike = 0f;
                    prevDateTime = currDateTime; 
                }
                if (optionData[i][3] > maxStrike)
                {
                    maxStrike = optionData[i][3];
                }
                if (optionData[i][3] < minStrike)
                {
                    minStrike = optionData[i][3];
                }
                FindMaxValues(optionData[i], initialParameters, dataMax, customMax, dataParameters.TradeDate);
            }
            trackCircumference *= 2;
            float pieSpace = dataParameters.PieSpacer / 100 * trackCircumference;
            float powerWedgeSpace = dataParameters.PowerWedge / 100 * trackCircumference;
            trackCircumference += powerWedgeSpace + (numberPies * 2 - 1) * pieSpace;

            SettingsData customSettings = new SettingsData();
            customSettings.settings = new SettingsNestedDict();
            customSettings.settings.Add("TrackCircumference", trackCircumference);
            customSettings.settings.Add("PieSpace", pieSpace);
            customSettings.settings.Add("PowerWedgeSpace", powerWedgeSpace);
            settings.tradeDate.Add(dataParameters.TradeDate, customSettings);
        }

        private void initializeDataMax(InitialParameters initialParameters, DataMax dataMax, MaxData customMax, string tradeDate)
        {
            
            customMax.maxValues = new DataMaxNestedDict();
            foreach (string key in initialParameters.parameterPosition.Keys)
            {
                if (initialParameters.parameterPosition[key].pair != 0)
                {
                    if (!customMax.maxValues.ContainsKey(initialParameters.parameterPosition[key].name))
                    {
                        customMax.maxValues[initialParameters.parameterPosition[key].name] = 0;
                    }
                }
            }
            dataMax.tradeDate.Add(tradeDate, customMax);
        }

        private void FindMaxValues(float[] optionList, InitialParameters initialParameters, DataMax dataMax, MaxData customMax, string tradeDate)
        {
            foreach(string key in initialParameters.parameterPosition.Keys)
            { 
                if (initialParameters.parameterPosition[key].pair != 0)
                {
                    float valueNumber = optionList[initialParameters.parameterPosition[key].index];
                    if (valueNumber != 0)
                    {
                        if (Math.Abs(valueNumber) > Math.Abs(dataMax.tradeDate[tradeDate].maxValues[initialParameters.parameterPosition[key].name])) {
                            dataMax.tradeDate[tradeDate].maxValues[initialParameters.parameterPosition[key].name] = Math.Abs(valueNumber);
                        }
                    }
                }
            }
        }
    }
}
