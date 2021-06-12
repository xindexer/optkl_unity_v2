using System;
using UnityEngine;
using System.Collections.Generic;
using Optkl.Data;


namespace Optkl.Parameters
{
    public class TrackParameters
    {
        public void BuildTracks(
            float[][] optionData,
            ColorControl colorControl,
            DataParameters dataParameters,
            DataStrike dataStrike,
            DataMax dataMax,
            CircumferenceParameterData circumferenceParameterData,
            out Dictionary<string, List<Vector3>> trackPositionData,
            out Dictionary<string, List<Color32>> trackColorData
            )
        {
            trackPositionData = new Dictionary<string, List<Vector3>>();
            trackColorData = new Dictionary<string, List<Color32>>();
            float trackRadials = 2 * (float)Math.PI / circumferenceParameterData.TrackCircumference;
            float deltaTheta = circumferenceParameterData.PieSpace / 2 * trackRadials;
            float thetaCall = (float)Math.PI / 2 - deltaTheta;
            float thetaPut = (float)Math.PI / 2 + deltaTheta;
            DateTime pvDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string previousDateTime = pvDateTime.AddMilliseconds(optionData[0][1] * 1000 + 4.32e+7).ToString("yyyyMMMdd");
            float strikeDiff = dataStrike.tradeDate[dataParameters.TradeName].expireDate[previousDateTime].strikeMin;
            float greekRange = dataParameters.GreekOuterRadius - dataParameters.GreekInnerRadius;
            InitialParameters initialParameters = new InitialParameters();

            foreach(string key in dataParameters.TrackOrder.Keys)
            {
                if (dataParameters.TrackOrder[key].Active)
                {
                    trackPositionData[key] = new List<Vector3>();
                    trackColorData[key] = new List<Color32>();
                }
            }
            
            foreach(string key in dataParameters.ShowGreek.Keys)
            {
                if (dataParameters.ShowGreek[key])
                {
                    trackPositionData[key] = new List<Vector3>();
                    trackColorData[key] = new List<Color32>();
                }
            }
            
            float innerRadius = dataParameters.TrackRadius;
            for (int i = 0; i < optionData.Length; i++)
            {
                DateTime expDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                string expireDateTime = expDateTime.AddMilliseconds(optionData[i][1] * 1000 + 4.32e+7).ToString("yyyyMMMdd");
                if (expireDateTime != previousDateTime)
                {
                    thetaCall -= circumferenceParameterData.PieSpace * trackRadials;
                    thetaPut += circumferenceParameterData.PieSpace * trackRadials;
                    previousDateTime = expireDateTime;
                    strikeDiff = dataStrike.tradeDate[dataParameters.TradeName].expireDate[previousDateTime].strikeMin;
                }
                thetaCall -= (optionData[i][3] - strikeDiff) * trackRadials;
                thetaPut += (optionData[i][3] - strikeDiff) * trackRadials;
                strikeDiff = optionData[i][3];
                foreach (string key in dataParameters.TrackOrder.Keys)
                {
                    int colorIndex = 0;
                    if (dataParameters.TrackOrder[key].Active)
                    {
                        float trackThickness = dataParameters.TrackThickness * dataParameters.TrackOrder[key].HeightMultiplier;
                        trackPositionData[key].Add(new Vector3((float)(innerRadius * Math.Cos(thetaCall)), (float)(innerRadius * Math.Sin(thetaCall)), 0f));
                        trackPositionData[key].Add(new Vector3((float)((innerRadius + trackThickness) * Math.Cos(thetaCall)), (float)((innerRadius + trackThickness) * Math.Sin(thetaCall)), 0f));
                        if(key == "inTheMoney")
                        {
                            if (optionData[i][3] <= optionData[i][0])
                            {
                                trackColorData[key].Add(colorControl.trackColorSet[key].palette[0]);
                            }
                            else
                            {
                                trackColorData[key].Add(colorControl.trackColorSet[key].palette[1]);
                            }
                        }
                        else
                        {
                            if (dataMax.tradeDate[dataParameters.TradeName].maxValues[key] != 0)
                            {
                                colorIndex = (int) Math.Floor(
                                    optionData[i][
                                        initialParameters.parameterPosition[initialParameters.CallPutMatch[key].CallName].index] /
                                    (dataMax.tradeDate[dataParameters.TradeName].maxValues[key] /
                                     (colorControl.trackColorSet[key].palette.Count - 0.1)));
                            }
                            trackColorData[key].Add(colorControl.trackColorSet[key].palette[colorIndex]);
                        }
                        trackPositionData[key].Add(new Vector3((float)(innerRadius * Math.Cos(thetaPut)), (float)(innerRadius * Math.Sin(thetaPut)), 0f));
                        trackPositionData[key].Add(new Vector3((float)((innerRadius + trackThickness) * Math.Cos(thetaPut)), (float)((innerRadius + trackThickness) * Math.Sin(thetaPut)), 0f));
                        if (key == "inTheMoney")
                        {
                            if (optionData[i][3] <= optionData[i][0])
                            {
                                trackColorData[key].Add(colorControl.trackColorSet[key].palette[0]);
                            }
                            else
                            {
                                trackColorData[key].Add(colorControl.trackColorSet[key].palette[1]);
                            }
                        }
                        else
                        {
                            if (dataMax.tradeDate[dataParameters.TradeName].maxValues[key] != 0)
                            {
                                colorIndex = (int) Math.Floor(
                                    optionData[i][
                                        initialParameters.parameterPosition[initialParameters.CallPutMatch[key].PutName].index] /
                                    (dataMax.tradeDate[dataParameters.TradeName].maxValues[key] /
                                     (colorControl.trackColorSet[key].palette.Count - 0.1)));
                            }
                            trackColorData[key].Add(colorControl.trackColorSet[key].palette[colorIndex]);
                        }
                        innerRadius += dataParameters.TrackSpacer + trackThickness;
                    }
                }
                innerRadius = dataParameters.TrackRadius;
                int greekColorPalettePosition = 0;
                float alphaMultiplier;
                foreach (string key in dataParameters.ShowGreek.Keys)
                {
                    if (dataParameters.ShowGreek[key])
                    {
                        float greekOffset = greekRange * optionData[i][initialParameters.parameterPosition[key].index] /
                            dataMax.tradeDate[dataParameters.TradeName].maxValues[key];
                        float greekPosition = dataParameters.GreekInnerRadius + greekOffset;
                        if (key == "phi" || key == "driftlessTheta" || key == "theta")
                        {
                            alphaMultiplier = dataParameters.GreekOpacity - 1;
                            if (dataParameters.NegativeGreeks)
                            {
                                greekPosition = dataParameters.GreekOuterRadius + greekOffset;
                            }
                            else
                            {
                                greekPosition = dataParameters.GreekInnerRadius + greekOffset;
                            }
                        }
                        else
                        {
                            alphaMultiplier = 1 - dataParameters.GreekOpacity;
                        }
                        trackPositionData[key].Add(new Vector3(
                            (float)(greekPosition * Math.Cos(thetaCall)),
                            (float)(greekPosition * Math.Sin(thetaCall)),
                            0f));
                        Color greekColor = colorControl.greekColorSet["greeks"].palette[greekColorPalettePosition];
                        greekColor.a = dataParameters.GreekOpacity + alphaMultiplier *
                            optionData[i][initialParameters.parameterPosition[key].index] /
                            dataMax.tradeDate[dataParameters.TradeName].maxValues[key];
                        trackColorData[key].Add(greekColor);
                        trackPositionData[key].Add(new Vector3(
                            (float)(greekPosition * Math.Cos(thetaPut)),
                            (float)(greekPosition * Math.Sin(thetaPut)),
                            0f));
                        trackColorData[key].Add(greekColor);
                    }
                    greekColorPalettePosition++;
                }
            }
        }
    }
}

