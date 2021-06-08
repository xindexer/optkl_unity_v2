using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;
using Optkl.Data;

namespace Optkl
{
    public class DrawManager : MonoBehaviour
    {
        VectorLine yteLines;
        VectorLine OiLines;
        VectorLine VoluLines;
        VectorLine BidPxLines;
        VectorLine ValueLines;
        VectorLine AskPxLines;
        VectorLine BidIvLines;
        VectorLine MidIvLines;
        VectorLine AskIvLines;
        VectorLine smoothSmvVolLines;
        VectorLine iRateLines;
        //VectorLine divRateLines;
        //VectorLine residualRateDataLines;
        VectorLine extVolLines;
        VectorLine extTheoLines;
        VectorLine inTheMoneyLines;

        VectorLine deltaPoints;
        VectorLine gammaPoints;
        VectorLine thetaPoints;
        VectorLine vegaPoints;
        VectorLine rhoPoints;
        VectorLine phiPoints;
        VectorLine driftlessThetaPoints;

        VectorLine CallTicks;
        VectorLine PutTicks;

        public void DrawOptions(
            TrackData trackData,
            TrackColors trackColors,
            TexturesAndMaterials texturesAndMaterials,
            DataParameters dataParameters,
            Boolean redraw,
            OptklManager optklManager)
        {
            if (!redraw) {
                List<Vector3> empty = new List<Vector3>();
                yteLines = new VectorLine("yte", empty, dataParameters.TrackLineWidth);
                OiLines = new VectorLine("Oi", empty, dataParameters.TrackLineWidth);
                VoluLines = new VectorLine("Volu", empty, dataParameters.TrackLineWidth);
                BidPxLines = new VectorLine("BidPx", empty, dataParameters.TrackLineWidth);
                ValueLines = new VectorLine("Value", empty, dataParameters.TrackLineWidth);
                AskPxLines = new VectorLine("AskPx", empty, dataParameters.TrackLineWidth);
                BidIvLines = new VectorLine("BidIv", empty, dataParameters.TrackLineWidth);
                MidIvLines = new VectorLine("MidIv", empty, dataParameters.TrackLineWidth);
                AskIvLines = new VectorLine("AskIv", empty, dataParameters.TrackLineWidth);
                smoothSmvVolLines = new VectorLine("smoothSmvVol", empty, dataParameters.TrackLineWidth);
                iRateLines = new VectorLine("iRate", empty, dataParameters.TrackLineWidth);
                //divRateLines = new VectorLine("divRate", empty, dataParameters.TrackLineWidth);
                //residualRateDataLines = new VectorLine("residualRateData", empty, dataParameters.TrackLineWidth);
                extVolLines = new VectorLine("extVol", empty, dataParameters.TrackLineWidth);
                extTheoLines = new VectorLine("extTheo", empty, dataParameters.TrackLineWidth);
                inTheMoneyLines = new VectorLine("inTheMoney", empty, dataParameters.TrackLineWidth);

                deltaPoints = new VectorLine("delta", empty, dataParameters.GreekSize, LineType.Points);
                gammaPoints = new VectorLine("gamma", empty, dataParameters.GreekSize, LineType.Points);
                thetaPoints = new VectorLine("theta", empty, dataParameters.GreekSize, LineType.Points);
                vegaPoints = new VectorLine("vega", empty, dataParameters.GreekSize, LineType.Points);
                rhoPoints = new VectorLine("rho", empty, dataParameters.GreekSize, LineType.Points);
                phiPoints = new VectorLine("phi", empty, dataParameters.GreekSize, LineType.Points);
                driftlessThetaPoints = new VectorLine("driftlessTheta", empty, dataParameters.GreekSize, LineType.Points);
                CallTicks = new VectorLine("CallTicks", empty, dataParameters.TickWidth);
                PutTicks = new VectorLine("PutTicks", empty, dataParameters.TickWidth);
            }

            List<VectorLine> trackList = new List<VectorLine>()
            {
                yteLines,
                OiLines,
                VoluLines,
                BidPxLines,
                ValueLines,
                AskPxLines,
                BidIvLines,
                MidIvLines,
                AskIvLines,
                smoothSmvVolLines,
                iRateLines,
                //divRateLines,
                //residualRateDataLines,
                extVolLines,
                extTheoLines,
                inTheMoneyLines
            };

            List<VectorLine> greekList = new List<VectorLine>()
            {
                deltaPoints,
                gammaPoints,
                thetaPoints,
                vegaPoints,
                rhoPoints,
                phiPoints,
                driftlessThetaPoints
        };

            foreach (VectorLine line in trackList)
            {
                if (dataParameters.TrackOrder[line.name].Active)
                {
                    line.active = true;
                    line.points3 = trackData.tradeDate[dataParameters.TradeDate].trackName[line.name].vectorList;
                    line.SetColors(trackColors.tradeDate[dataParameters.TradeDate].trackName[line.name].colorList);
                    line.SetWidth(dataParameters.TrackLineWidth, 0, line.GetSegmentNumber());
                    //if (texturesAndMaterials.OptklTextures.ContainsKey(line.name))
                    //    line.texture = texturesAndMaterials.OptklTextures[line.name].Texture;
                    //if (texturesAndMaterials.OptklTextures.ContainsKey(line.name))
                    //    line.material = texturesAndMaterials.OptklTextures[line.name].Material;
                    line.Draw3D();
                }
                else
                {
                    line.active = false;
                }
            }

            foreach (VectorLine point in greekList)
            {
                if (dataParameters.ShowGreek[point.name])
                {
                    point.active = true;
                    point.points3 = trackData.tradeDate[dataParameters.TradeDate].trackName[point.name].vectorList;
                    point.SetColors(trackColors.tradeDate[dataParameters.TradeDate].trackName[point.name].colorList);
                    point.SetWidth(dataParameters.GreekSize, 0, point.GetSegmentNumber());
                    if (texturesAndMaterials.OptklTextures.ContainsKey(point.name))
                        point.texture = texturesAndMaterials.OptklTextures[point.name].Texture;
                    if (texturesAndMaterials.OptklTextures.ContainsKey(point.name))
                        point.material = texturesAndMaterials.OptklTextures[point.name].Material;
                    point.Draw3D();
                }
                else
                {
                    point.active = false;
                }
            }

            if(dataParameters.ShowTicks)
            {
                CallTicks.active = true;
                CallTicks.points3 = trackData.tradeDate[dataParameters.TradeDate].trackName[CallTicks.name].vectorList;
                CallTicks.SetWidth(dataParameters.TrackLineWidth, 0, CallTicks.GetSegmentNumber());
                CallTicks.Draw3D();
                PutTicks.active = true;
                PutTicks.points3 = trackData.tradeDate[dataParameters.TradeDate].trackName[PutTicks.name].vectorList;
                PutTicks.SetWidth(dataParameters.TrackLineWidth, 0, PutTicks.GetSegmentNumber());
                PutTicks.Draw3D();
            }
            else
            {
                CallTicks.active = false;
                PutTicks.active = false;
            }
            optklManager.ShowIRIS();
        }
    }
}
