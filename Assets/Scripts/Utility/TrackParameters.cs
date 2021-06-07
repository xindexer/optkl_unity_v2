using System;
using UnityEngine;
using System.Collections.Generic;
using Optkl.Data;


namespace Optkl.Utilities
{
    public class TrackParameters
    {
        public void BuildTracks(
            float[][] optionData,
            TrackData trackData,
            TrackColors trackColors,
            ColorPalette colorPalette,
            DataParameters dataParameters,
            DataStrike dataStrike,
            DataMax dataMax,
            Settings settings)
        {

            float trackCircumference = settings.tradeDate[dataParameters.TradeDate].settings["TrackCircumference"];
            float trackRadials = 2 * (float)Math.PI / trackCircumference;
            float pieSpace = settings.tradeDate[dataParameters.TradeDate].settings["PieSpace"];
            float deltaTheta = pieSpace / 2 * trackRadials;
            float thetaCall = (float)Math.PI / 2 - deltaTheta;
            float thetaPut = (float)Math.PI / 2 + deltaTheta;
            DateTime pvDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string previousDateTime = pvDateTime.AddMilliseconds(optionData[0][1] * 1000 + 4.32e+7).ToString("yyyyMMMdd");
            float strikeDiff = dataStrike.tradeDate[dataParameters.TradeDate].expireDate[previousDateTime].strikeMin;
            float greekDistance = dataParameters.GreetOuterRadius - dataParameters.GreekInnerRadius;
            InitialParameters initialParameters = new InitialParameters();

            TrackDataList yte = new TrackDataList();
            yte.vectorList = new List<Vector3>();
            TrackColorsList yteColors = new TrackColorsList();
            yteColors.colorList = new List<Color32>();

            TrackDataList Oi = new TrackDataList();
            Oi.vectorList = new List<Vector3>();
            TrackColorsList OiColors = new TrackColorsList();
            OiColors.colorList = new List<Color32>();

            TrackDataList Volu = new TrackDataList();
            Volu.vectorList = new List<Vector3>();
            TrackColorsList VoluColors = new TrackColorsList();
            VoluColors.colorList = new List<Color32>();
            
            TrackDataList BidPx = new TrackDataList();
            BidPx.vectorList = new List<Vector3>();
            TrackColorsList BidPxColors = new TrackColorsList();
            BidPxColors.colorList = new List<Color32>();

            TrackDataList Value = new TrackDataList();
            Value.vectorList = new List<Vector3>();
            TrackColorsList ValueColors = new TrackColorsList();
            ValueColors.colorList = new List<Color32>();

            TrackDataList AskPx = new TrackDataList();
            AskPx.vectorList = new List<Vector3>();
            TrackColorsList AskPxColors = new TrackColorsList();
            AskPxColors.colorList = new List<Color32>();

            TrackDataList BidIv = new TrackDataList();
            BidIv.vectorList = new List<Vector3>();
            TrackColorsList BidIvColors = new TrackColorsList();
            BidIvColors.colorList = new List<Color32>();

            TrackDataList MidIv = new TrackDataList();
            MidIv.vectorList = new List<Vector3>();
            TrackColorsList MidIvColors = new TrackColorsList();
            MidIvColors.colorList = new List<Color32>();

            TrackDataList AskIv = new TrackDataList();
            AskIv.vectorList = new List<Vector3>();
            TrackColorsList AskIvColors = new TrackColorsList();
            AskIvColors.colorList = new List<Color32>();

            TrackDataList SmoothSmvVol = new TrackDataList();
            SmoothSmvVol.vectorList = new List<Vector3>();
            TrackColorsList SmoothSmvVolColors = new TrackColorsList();
            SmoothSmvVolColors.colorList = new List<Color32>();

            TrackDataList iRate = new TrackDataList();
            iRate.vectorList = new List<Vector3>();
            TrackColorsList iRateColors = new TrackColorsList();
            iRateColors.colorList = new List<Color32>();

            TrackDataList divRate = new TrackDataList();
            divRate.vectorList = new List<Vector3>();
            TrackColorsList divRateColors = new TrackColorsList();
            divRateColors.colorList = new List<Color32>();

            TrackDataList residualRateData = new TrackDataList();
            residualRateData.vectorList = new List<Vector3>();
            TrackColorsList residualRateDataColors = new TrackColorsList();
            residualRateDataColors.colorList = new List<Color32>();

            TrackDataList extVol = new TrackDataList();
            extVol.vectorList = new List<Vector3>();
            TrackColorsList extVolColors = new TrackColorsList();
            extVolColors.colorList = new List<Color32>();

            TrackDataList extTheo = new TrackDataList();
            extTheo.vectorList = new List<Vector3>();
            TrackColorsList extTheoColors = new TrackColorsList();
            extTheoColors.colorList = new List<Color32>();

            TrackDataList delta = new TrackDataList();
            delta.vectorList = new List<Vector3>();
            TrackColorsList deltaColors = new TrackColorsList();
            deltaColors.colorList = new List<Color32>();

            TrackDataList gamma = new TrackDataList();
            gamma.vectorList = new List<Vector3>();
            TrackColorsList gammaColors = new TrackColorsList();
            gammaColors.colorList = new List<Color32>();

            TrackDataList theta = new TrackDataList();
            theta.vectorList = new List<Vector3>();
            TrackColorsList thetaColors = new TrackColorsList();
            thetaColors.colorList = new List<Color32>();

            TrackDataList vega = new TrackDataList();
            vega.vectorList = new List<Vector3>();
            TrackColorsList vegaColors = new TrackColorsList();
            vegaColors.colorList = new List<Color32>();

            TrackDataList rho = new TrackDataList();
            rho.vectorList = new List<Vector3>();
            TrackColorsList rhoColors = new TrackColorsList();
            rhoColors.colorList = new List<Color32>();

            TrackDataList phi = new TrackDataList();
            phi.vectorList = new List<Vector3>();
            TrackColorsList phiColors = new TrackColorsList();
            phiColors.colorList = new List<Color32>();

            TrackDataList driftlessTheta = new TrackDataList();
            driftlessTheta.vectorList = new List<Vector3>();
            TrackColorsList driftlessThetaColors = new TrackColorsList();
            driftlessThetaColors.colorList = new List<Color32>();
           
            for (int i = 0; i < optionData.Length; i++)
            {
                DateTime expDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                string expireDateTime = expDateTime.AddMilliseconds(optionData[i][1] * 1000 + 4.32e+7).ToString("yyyyMMMdd");
                if (expireDateTime != previousDateTime)
                {
                    thetaCall -= pieSpace * trackRadials;
                    thetaPut += pieSpace * trackRadials;
                    previousDateTime = expireDateTime;
                    strikeDiff = dataStrike.tradeDate[dataParameters.TradeDate].expireDate[previousDateTime].strikeMin;
                }
                thetaCall -= (optionData[i][3] - strikeDiff) * trackRadials;
                thetaPut += (optionData[i][3] - strikeDiff) * trackRadials;
                strikeDiff = optionData[i][3];

                float OiInner = GetTrackRadius("Oi", dataParameters);
                float OiOuter = GetTrackRadius("Oi", dataParameters) + dataParameters.TrackThickness;
                Oi.vectorList.Add(new Vector3((float)(OiInner * Math.Cos(thetaCall)), (float)(OiInner * Math.Sin(thetaCall)), 0f));
                Oi.vectorList.Add(new Vector3((float)(OiOuter * Math.Cos(thetaCall)), (float)(OiOuter * Math.Sin(thetaCall)), 0f));
                OiColors.colorList.Add(
                    colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Oi])
                    [(int)Math.Floor(optionData[i][initialParameters.parameterPosition["cOi"].index] /
                    (dataMax.tradeDate[dataParameters.TradeDate].maxValues["Oi"] /
                    (colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Oi]).Count - 0.1)))]);
                Oi.vectorList.Add(new Vector3((float)(OiInner * Math.Cos(thetaPut)), (float)(OiInner * Math.Sin(thetaPut)), 0f));
                Oi.vectorList.Add(new Vector3((float)(OiOuter * Math.Cos(thetaPut)), (float)(OiOuter * Math.Sin(thetaPut)), 0f));
                OiColors.colorList.Add(
                    colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Oi])
                    [(int)Math.Floor(optionData[i][initialParameters.parameterPosition["pOi"].index] /
                    (dataMax.tradeDate[dataParameters.TradeDate].maxValues["Oi"] /
                    (colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Oi]).Count - 0.1)))]);

                float VoluInner = GetTrackRadius("Volu", dataParameters);
                float VoluOuter = GetTrackRadius("Volu", dataParameters) + dataParameters.TrackThickness;
                Volu.vectorList.Add(new Vector3((float)(VoluInner * Math.Cos(thetaCall)), (float)(VoluInner * Math.Sin(thetaCall)), 0f));
                Volu.vectorList.Add(new Vector3((float)(VoluOuter * Math.Cos(thetaCall)), (float)(VoluOuter * Math.Sin(thetaCall)), 0f));
                VoluColors.colorList.Add(
                    colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Volu])
                    [(int)Math.Floor(optionData[i][initialParameters.parameterPosition["cVolu"].index] /
                    (dataMax.tradeDate[dataParameters.TradeDate].maxValues["Volu"] /
                    (colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Volu]).Count - 0.1)))]);
                Volu.vectorList.Add(new Vector3((float)(VoluInner * Math.Cos(thetaPut)), (float)(VoluInner * Math.Sin(thetaPut)), 0f));
                Volu.vectorList.Add(new Vector3((float)(VoluOuter * Math.Cos(thetaPut)),(float)(VoluOuter * Math.Sin(thetaPut)), 0f));
                VoluColors.colorList.Add(
                    colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Volu])
                    [(int)Math.Floor(optionData[i][initialParameters.parameterPosition["pVolu"].index] /
                    (dataMax.tradeDate[dataParameters.TradeDate].maxValues["Volu"] /
                    (colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Volu]).Count - 0.1)))]);

                float bidPxInner = GetTrackRadius("BidPx", dataParameters);
                float bidPxOuter = GetTrackRadius("BidPx", dataParameters) + dataParameters.TrackThickness;
                BidPx.vectorList.Add(new Vector3((float)(bidPxInner * Math.Cos(thetaCall)), (float)(bidPxInner * Math.Sin(thetaCall)), 0f));
                BidPx.vectorList.Add(new Vector3((float)(bidPxOuter * Math.Cos(thetaCall)), (float)(bidPxOuter * Math.Sin(thetaCall)), 0f));
                BidPxColors.colorList.Add(
                    colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.BidPx])
                    [(int)Math.Floor(optionData[i][initialParameters.parameterPosition["cBidPx"].index] /
                    (dataMax.tradeDate[dataParameters.TradeDate].maxValues["BidPx"] /
                    (colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.BidPx]).Count - 0.1)))]);
                BidPx.vectorList.Add(new Vector3((float)(bidPxInner * Math.Cos(thetaPut)), (float)(bidPxInner * Math.Sin(thetaPut)), 0f));
                BidPx.vectorList.Add(new Vector3((float)(bidPxOuter * Math.Cos(thetaPut)), (float)(bidPxOuter * Math.Sin(thetaPut)), 0f));
                BidPxColors.colorList.Add(
                    colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.BidPx])
                    [(int)Math.Floor(optionData[i][initialParameters.parameterPosition["pBidPx"].index] /
                    (dataMax.tradeDate[dataParameters.TradeDate].maxValues["BidPx"] /
                    (colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.BidPx]).Count - 0.1)))]);

                float ValueInner = GetTrackRadius("Value", dataParameters);
                float ValueOuter = GetTrackRadius("Value", dataParameters) + dataParameters.TrackThickness;
                Value.vectorList.Add(new Vector3((float)(ValueInner * Math.Cos(thetaCall)), (float)(ValueInner * Math.Sin(thetaCall)), 0f));
                Value.vectorList.Add(new Vector3((float)(ValueOuter * Math.Cos(thetaCall)), (float)(ValueOuter * Math.Sin(thetaCall)), 0f));
                ValueColors.colorList.Add(
                    colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Value])
                    [(int)Math.Floor(optionData[i][initialParameters.parameterPosition["cValue"].index] /
                    (dataMax.tradeDate[dataParameters.TradeDate].maxValues["Value"] /
                    (colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Value]).Count - 0.1)))]);
                Value.vectorList.Add(new Vector3((float)(ValueInner * Math.Cos(thetaPut)), (float)(ValueInner * Math.Sin(thetaPut)), 0f));
                Value.vectorList.Add(new Vector3((float)(ValueOuter * Math.Cos(thetaPut)), (float)(ValueOuter * Math.Sin(thetaPut)), 0f));
                ValueColors.colorList.Add(
                    colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Value])
                    [(int)Math.Floor(optionData[i][initialParameters.parameterPosition["pValue"].index] /
                    (dataMax.tradeDate[dataParameters.TradeDate].maxValues["Value"] /
                    (colorPalette.GetColorPalette(colorPalette.colorSet[ColorPalette.EnumTrack.Value]).Count - 0.1)))]);




                float deltaPosition = dataParameters.GreekInnerRadius +
                greekDistance * optionData[i][initialParameters.parameterPosition["delta"].index] /
                dataMax.tradeDate[dataParameters.TradeDate].maxValues["delta"];
                delta.vectorList.Add(new Vector3((float)(deltaPosition * Math.Cos(thetaCall)), (float)(deltaPosition * Math.Sin(thetaCall)), 0f));
                Color deltaColor = colorPalette.GetGreekColor(colorPalette.greekColorSet[ColorPalette.EnumGreek.delta]);
                deltaColor.a = dataParameters.GreekOpacity + (1 - dataParameters.GreekOpacity) * optionData[i][initialParameters.parameterPosition["delta"].index] / dataMax.tradeDate[dataParameters.TradeDate].maxValues["delta"];
                deltaColors.colorList.Add(deltaColor);
                delta.vectorList.Add(new Vector3((float)(deltaPosition * Math.Cos(thetaPut)), (float)(deltaPosition * Math.Sin(thetaPut)), 0f));
                deltaColors.colorList.Add(deltaColor);
            }
            trackData.tradeDate[dataParameters.TradeDate].trackName.Add("Volu", Volu);
            TrackColorsContainer customColors = new TrackColorsContainer();
            customColors.trackName = new TrackColorsContainerNestedDict();
            customColors.trackName.Add("Volu", VoluColors);
            trackColors.tradeDate.Add(dataParameters.TradeDate, customColors);

            trackData.tradeDate[dataParameters.TradeDate].trackName.Add("Oi", Oi);
            trackColors.tradeDate[dataParameters.TradeDate].trackName.Add("Oi", OiColors);

            trackData.tradeDate[dataParameters.TradeDate].trackName.Add("BidPx", BidPx);
            trackColors.tradeDate[dataParameters.TradeDate].trackName.Add("BidPx", BidPxColors);

            trackData.tradeDate[dataParameters.TradeDate].trackName.Add("Value", Value);
            trackColors.tradeDate[dataParameters.TradeDate].trackName.Add("Value", ValueColors);

            trackData.tradeDate[dataParameters.TradeDate].trackName.Add("delta", delta);
            trackColors.tradeDate[dataParameters.TradeDate].trackName.Add("delta", deltaColors);

        }

        private float GetTrackRadius(
            string trackName,
            DataParameters dataParameters
            )
        {
            int trackPosition = 0;
            foreach(string key in dataParameters.TrackOrder.Keys)
            {
                if (key != trackName && dataParameters.TrackOrder[key])
                    trackPosition++;
                else if (key == trackName)
                    break;
            }
            return dataParameters.TrackRadius + trackPosition * dataParameters.TrackSpacer + trackPosition * dataParameters.TrackThickness;
        }
    }
}
