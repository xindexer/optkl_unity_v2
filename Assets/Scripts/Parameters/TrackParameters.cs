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
            TrackData trackData,
            TrackColors trackColors,
            ColorControl colorControl,
            DataParameters dataParameters,
            DataStrike dataStrike,
            DataMax dataMax,
            TrackReturn trackReturn)
        {
            float trackRadials = 2 * (float)Math.PI / trackReturn.TrackCircumference;
            float deltaTheta = trackReturn.PieSpace / 2 * trackRadials;
            float thetaCall = (float)Math.PI / 2 - deltaTheta;
            float thetaPut = (float)Math.PI / 2 + deltaTheta;
            DateTime pvDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string previousDateTime = pvDateTime.AddMilliseconds(optionData[0][1] * 1000 + 4.32e+7).ToString("yyyyMMMdd");
            float strikeDiff = dataStrike.tradeDate[dataParameters.TradeName].expireDate[previousDateTime].strikeMin;
            float greekRange = dataParameters.GreekOuterRadius - dataParameters.GreekInnerRadius;
            InitialParameters initialParameters = new InitialParameters();
            Dictionary<string, TrackDataVectorList> trackDataList = new Dictionary<string, TrackDataVectorList>();

            TrackDataList yte = new TrackDataList();
            yte.vectorList = new List<Vector3>();
            TrackColorsList yteColors = new TrackColorsList();
            yteColors.colorList = new List<Color32>();
            trackDataList.Add("yte", new TrackDataVectorList(yte, yteColors, "yte", "yte"));

            TrackDataList Oi = new TrackDataList();
            Oi.vectorList = new List<Vector3>();
            TrackColorsList OiColors = new TrackColorsList();
            OiColors.colorList = new List<Color32>();
            trackDataList.Add("Oi", new TrackDataVectorList(Oi, OiColors, "cOi", "pOi"));

            TrackDataList Volu = new TrackDataList();
            Volu.vectorList = new List<Vector3>();
            TrackColorsList VoluColors = new TrackColorsList();
            VoluColors.colorList = new List<Color32>();
            trackDataList.Add("Volu", new TrackDataVectorList(Volu, VoluColors, "cVolu", "pVolu"));

            TrackDataList BidPx = new TrackDataList();
            BidPx.vectorList = new List<Vector3>();
            TrackColorsList BidPxColors = new TrackColorsList();
            BidPxColors.colorList = new List<Color32>();
            trackDataList.Add("BidPx", new TrackDataVectorList(BidPx, BidPxColors, "cBidPx", "pBidPx"));

            TrackDataList Value = new TrackDataList();
            Value.vectorList = new List<Vector3>();
            TrackColorsList ValueColors = new TrackColorsList();
            ValueColors.colorList = new List<Color32>();
            trackDataList.Add("Value", new TrackDataVectorList(Value, ValueColors, "cValue", "pValue"));

            TrackDataList AskPx = new TrackDataList();
            AskPx.vectorList = new List<Vector3>();
            TrackColorsList AskPxColors = new TrackColorsList();
            AskPxColors.colorList = new List<Color32>();
            trackDataList.Add("AskPx", new TrackDataVectorList(AskPx, AskPxColors, "cAskPx", "pAskPx"));

            TrackDataList BidIv = new TrackDataList();
            BidIv.vectorList = new List<Vector3>();
            TrackColorsList BidIvColors = new TrackColorsList();
            BidIvColors.colorList = new List<Color32>();
            trackDataList.Add("BidIv", new TrackDataVectorList(BidIv, BidIvColors, "cBidIv", "pBidIv"));

            TrackDataList MidIv = new TrackDataList();
            MidIv.vectorList = new List<Vector3>();
            TrackColorsList MidIvColors = new TrackColorsList();
            MidIvColors.colorList = new List<Color32>();
            trackDataList.Add("MidIv", new TrackDataVectorList(MidIv, MidIvColors, "cMidIv", "pMidIv"));

            TrackDataList AskIv = new TrackDataList();
            AskIv.vectorList = new List<Vector3>();
            TrackColorsList AskIvColors = new TrackColorsList();
            AskIvColors.colorList = new List<Color32>();
            trackDataList.Add("AskIv", new TrackDataVectorList(AskIv, AskIvColors, "cAskIv", "pAskIv"));

            TrackDataList smoothSmvVol = new TrackDataList();
            smoothSmvVol.vectorList = new List<Vector3>();
            TrackColorsList smoothSmvVolColors = new TrackColorsList();
            smoothSmvVolColors.colorList = new List<Color32>();
            trackDataList.Add("smoothSmvVol", new TrackDataVectorList(smoothSmvVol, smoothSmvVolColors, "smoothSmvVol", "smoothSmvVol"));

            TrackDataList iRate = new TrackDataList();
            iRate.vectorList = new List<Vector3>();
            TrackColorsList iRateColors = new TrackColorsList();
            iRateColors.colorList = new List<Color32>();
            trackDataList.Add("iRate", new TrackDataVectorList(iRate, iRateColors, "iRate", "iRate"));

            TrackDataList divRate = new TrackDataList();
            divRate.vectorList = new List<Vector3>();
            TrackColorsList divRateColors = new TrackColorsList();
            divRateColors.colorList = new List<Color32>();
            trackDataList.Add("divRate", new TrackDataVectorList(divRate, divRateColors, "divRate", "divRate"));

            TrackDataList residualRateData = new TrackDataList();
            residualRateData.vectorList = new List<Vector3>();
            TrackColorsList residualRateDataColors = new TrackColorsList();
            residualRateDataColors.colorList = new List<Color32>();
            trackDataList.Add("residualRateData", new TrackDataVectorList(residualRateData, residualRateDataColors, "residualRateData", "residualRateData"));

            TrackDataList extVol = new TrackDataList();
            extVol.vectorList = new List<Vector3>();
            TrackColorsList extVolColors = new TrackColorsList();
            extVolColors.colorList = new List<Color32>();
            trackDataList.Add("extVol", new TrackDataVectorList(extVol, extVolColors, "extVol", "extVol"));

            TrackDataList extTheo = new TrackDataList();
            extTheo.vectorList = new List<Vector3>();
            TrackColorsList extTheoColors = new TrackColorsList();
            extTheoColors.colorList = new List<Color32>();
            trackDataList.Add("extTheo", new TrackDataVectorList(extTheo, extTheoColors, "extCTheo", "extPTheo"));

            TrackDataList delta = new TrackDataList();
            delta.vectorList = new List<Vector3>();
            TrackColorsList deltaColors = new TrackColorsList();
            deltaColors.colorList = new List<Color32>();
            trackDataList.Add("delta", new TrackDataVectorList(delta, deltaColors, "delta", "delta"));

            TrackDataList gamma = new TrackDataList();
            gamma.vectorList = new List<Vector3>();
            TrackColorsList gammaColors = new TrackColorsList();
            gammaColors.colorList = new List<Color32>();
            trackDataList.Add("gamma", new TrackDataVectorList(gamma, gammaColors, "gamma", "gamma"));

            TrackDataList theta = new TrackDataList();
            theta.vectorList = new List<Vector3>();
            TrackColorsList thetaColors = new TrackColorsList();
            thetaColors.colorList = new List<Color32>();
            trackDataList.Add("theta", new TrackDataVectorList(theta, thetaColors, "theta", "theta"));

            TrackDataList vega = new TrackDataList();
            vega.vectorList = new List<Vector3>();
            TrackColorsList vegaColors = new TrackColorsList();
            vegaColors.colorList = new List<Color32>();
            trackDataList.Add("vega", new TrackDataVectorList(vega, vegaColors, "vega", "vega"));

            TrackDataList rho = new TrackDataList();
            rho.vectorList = new List<Vector3>();
            TrackColorsList rhoColors = new TrackColorsList();
            rhoColors.colorList = new List<Color32>();
            trackDataList.Add("rho", new TrackDataVectorList(rho, rhoColors, "rho", "rho"));

            TrackDataList phi = new TrackDataList();
            phi.vectorList = new List<Vector3>();
            TrackColorsList phiColors = new TrackColorsList();
            phiColors.colorList = new List<Color32>();
            trackDataList.Add("phi", new TrackDataVectorList(phi, phiColors, "phi", "phi"));

            TrackDataList inTheMoney = new TrackDataList();
            inTheMoney.vectorList = new List<Vector3>();
            TrackColorsList inTheMoneyColors = new TrackColorsList();
            inTheMoneyColors.colorList = new List<Color32>();
            trackDataList.Add("inTheMoney", new TrackDataVectorList(inTheMoney, inTheMoneyColors, null, null));


            float innerRadius = dataParameters.TrackRadius;
            for (int i = 0; i < optionData.Length; i++)
            {
                DateTime expDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                string expireDateTime = expDateTime.AddMilliseconds(optionData[i][1] * 1000 + 4.32e+7).ToString("yyyyMMMdd");
                if (expireDateTime != previousDateTime)
                {
                    thetaCall -= trackReturn.PieSpace * trackRadials;
                    thetaPut += trackReturn.PieSpace * trackRadials;
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
                        trackDataList[key].TrackDataList.vectorList.Add(new Vector3((float)(innerRadius * Math.Cos(thetaCall)), (float)(innerRadius * Math.Sin(thetaCall)), 0f));
                        trackDataList[key].TrackDataList.vectorList.Add(new Vector3((float)((innerRadius + trackThickness) * Math.Cos(thetaCall)), (float)((innerRadius + trackThickness) * Math.Sin(thetaCall)), 0f));
                        if(key == "inTheMoney")
                        {
                            if (optionData[i][3] <= optionData[i][0])
                            {
                                trackDataList[key].TrackColorsList.colorList.Add(colorControl.trackColorSet[key].palette[0]);
                            }
                            else
                            {
                                trackDataList[key].TrackColorsList.colorList.Add(colorControl.trackColorSet[key].palette[1]);
                            }
                        }
                        else
                        {
                            
                            if (dataMax.tradeDate[dataParameters.TradeName].maxValues[key] != 0)
                            {
                                colorIndex = (int) Math.Floor(
                                    optionData[i][
                                        initialParameters.parameterPosition[trackDataList[key].CallName].index] /
                                    (dataMax.tradeDate[dataParameters.TradeName].maxValues[key] /
                                     (colorControl.trackColorSet[key].palette.Count - 0.1)));
                            }
                            trackDataList[key].TrackColorsList.colorList.Add(colorControl.trackColorSet[key].palette[colorIndex]);
                        }
                        trackDataList[key].TrackDataList.vectorList.Add(new Vector3((float)(innerRadius * Math.Cos(thetaPut)), (float)(innerRadius * Math.Sin(thetaPut)), 0f));
                        trackDataList[key].TrackDataList.vectorList.Add(new Vector3((float)((innerRadius + trackThickness) * Math.Cos(thetaPut)), (float)((innerRadius + trackThickness) * Math.Sin(thetaPut)), 0f));
                        if (key == "inTheMoney")
                        {
                            if (optionData[i][3] <= optionData[i][0])
                            {
                                trackDataList[key].TrackColorsList.colorList.Add(colorControl.trackColorSet[key].palette[0]);
                            }
                            else
                            {
                                trackDataList[key].TrackColorsList.colorList.Add(colorControl.trackColorSet[key].palette[1]);
                            }
                        }
                        else
                        {
                            if (dataMax.tradeDate[dataParameters.TradeName].maxValues[key] != 0)
                            {
                                colorIndex = (int) Math.Floor(
                                    optionData[i][
                                        initialParameters.parameterPosition[trackDataList[key].CallName].index] /
                                    (dataMax.tradeDate[dataParameters.TradeName].maxValues[key] /
                                     (colorControl.trackColorSet[key].palette.Count - 0.1)));
                            }
                            trackDataList[key].TrackColorsList.colorList.Add(colorControl.trackColorSet[key].palette[colorIndex]);
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
                        trackDataList[key].TrackDataList.vectorList.Add(new Vector3(
                            (float)(greekPosition * Math.Cos(thetaCall)),
                            (float)(greekPosition * Math.Sin(thetaCall)),
                            0f));
                        Color greekColor = colorControl.greekColorSet["greeks"].palette[greekColorPalettePosition];
                        greekColor.a = dataParameters.GreekOpacity + alphaMultiplier *
                            optionData[i][initialParameters.parameterPosition[key].index] /
                            dataMax.tradeDate[dataParameters.TradeName].maxValues[key];
                        trackDataList[key].TrackColorsList.colorList.Add(greekColor);
                        trackDataList[key].TrackDataList.vectorList.Add(new Vector3(
                            (float)(greekPosition * Math.Cos(thetaPut)),
                            (float)(greekPosition * Math.Sin(thetaPut)),
                            0f));
                        trackDataList[key].TrackColorsList.colorList.Add(greekColor);
                    }
                    greekColorPalettePosition++;
                }
                greekColorPalettePosition = 0;
            }

            Boolean firstTrack = true;

            foreach (string key in dataParameters.TrackOrder.Keys)
            {
                if (dataParameters.TrackOrder[key].Active)
                {
                    if (firstTrack)
                    {
                        trackData.tradeDate[dataParameters.TradeName].trackName.Add(key, trackDataList[key].TrackDataList);
                        TrackColorsContainer customColors = new TrackColorsContainer();
                        customColors.trackName = new TrackColorsContainerNestedDict();
                        customColors.trackName.Add(key, trackDataList[key].TrackColorsList);
                        trackColors.tradeDate.Add(dataParameters.TradeName, customColors);
                        firstTrack = false;
                    }
                    else
                    {
                        trackData.tradeDate[dataParameters.TradeName].trackName.Add(key, trackDataList[key].TrackDataList);
                        trackColors.tradeDate[dataParameters.TradeName].trackName.Add(key, trackDataList[key].TrackColorsList);
                    }
                }
            }

            foreach (string key in dataParameters.ShowGreek.Keys)
            {
                if (dataParameters.ShowGreek[key])
                {
                    trackData.tradeDate[dataParameters.TradeName].trackName.Add(key, trackDataList[key].TrackDataList);
                    trackColors.tradeDate[dataParameters.TradeName].trackName.Add(key, trackDataList[key].TrackColorsList);
                }
            }
        }
    }
}

