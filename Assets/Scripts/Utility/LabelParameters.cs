using System;
using UnityEngine;
using Optkl.Data;

namespace Optkl.Utilities
{
    public class LabelParameters
    {

        public void BuildLabels(
            object[][] optionData,
            DataParameters dataParameters,
            DataLabels dataLabels,
            DataStrike dataStrike,
            DataMax dataMax)
        {
            DateTime pvDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); 
            string prevDateTime = pvDateTime.AddMilliseconds(Convert.ToUInt64(optionData[0][3])).ToString("yyyyMMMdd");
            DateTime trDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string tradeDate = trDateTime.AddMilliseconds(Convert.ToUInt64(optionData[0][37])).ToString("yyyyMMMdd");
            Boolean isLast = false;
            Boolean isFirst = true;
            float minStrike = 100000000f;
            float maxStrike = 0f;
            float trackLength = 0f;
            InitialParameters initialParameters = new InitialParameters();
            MaxData customMax = new MaxData();
            initializeDataMax(initialParameters, tradeDate, dataMax, customMax);
            for (int i = 0; i < optionData.Length; i++) 
            {
                if (i == optionData.Length - 1)
                    isLast = true;
                DateTime crDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                string currDateTime = crDateTime.AddMilliseconds(Convert.ToUInt64(optionData[i][3])).ToString("yyyyMMMdd");
                if (isLast)
                {
                    if(Convert.ToUInt64(optionData[i][5]) > maxStrike)
                    {
                        maxStrike = Convert.ToUInt64(optionData[i][5]);
                    }
                    if (Convert.ToUInt64(optionData[i][5]) < minStrike)
                    {
                        minStrike = Convert.ToUInt64(optionData[i][5]);
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
                        dataStrike.tradeDate.Add(tradeDate, customStrike);
                        isFirst = false;
                    }
                    else
                    {
                        dataStrike.tradeDate[tradeDate].expireDate.Add(prevDateTime, newData);
                    }


                    //labelArray.Add(prevDateTime); // ordered list of labels 
                    trackLength += (maxStrike - minStrike);
                    //numberPies++;// increase the track length (will have to remove 1 pie spaces due to the loop
                    minStrike = 100000000f; 
                    maxStrike = 0f;
                    prevDateTime = currDateTime; 
                }
                if (Convert.ToUInt64(optionData[i][5]) > maxStrike)
                {
                    maxStrike = Convert.ToUInt64(optionData[i][5]);
                }
                if (Convert.ToUInt64(optionData[i][5]) < minStrike)
                {
                    minStrike = Convert.ToUInt64(optionData[i][5]);
                }
                FindMaxValues(optionData[i], initialParameters, dataMax, customMax, tradeDate);
                //findSingleMaxValues(optionData[i]);
                //if (Convert.ToInt64(optionData[i][3]) != previousExpireDate)
                //{
                //    tempArray.Reverse();
                //    optklArray = Utilities.Combine(optklArray, tempArray).ToList();
                //    tempArray.Clear();
                //    previousExpireDate = Convert.ToInt64(optionData[i][3]);
                //}
                //tempArray.Add(optionData[i]);
            }
            //labelArray.Reverse();
            //tempArray.Reverse();
            //optklArray = Utilities.Combine(optklArray, tempArray).ToList();
            //optklArray.Reverse();
            //trackLength *= 2;
            //pieSpace = percentPieSpace * trackLength / 100;
            //if (showLabel)
            //    labelSpace = percentLabelSpace * trackLength / 100;
            //else
            //    labelSpace = pieSpace;
            //trackLength += (float)labelSpace + (numberPies * 2 - 1) * pieSpace; //remove last pie space
        }

        private void initializeDataMax(InitialParameters initialParameters, string tradeDate, DataMax dataMax, MaxData customMax)
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

        private void FindMaxValues(object[] optionList, InitialParameters initialParameters, DataMax dataMax, MaxData customMax, string tradeDate)
        {
            foreach(string key in initialParameters.parameterPosition.Keys)
            { 
                if (initialParameters.parameterPosition[key].pair != 0)
                {
                    double valueNumber = Convert.ToDouble(optionList[initialParameters.parameterPosition[key].index]);
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
