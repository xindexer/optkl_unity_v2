using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;
using Optkl.Data;

namespace Optkl
{
    public class DrawManager : MonoBehaviour
    {
        VectorLine OiLines;
        VectorLine VoluLines;
        VectorLine BidPxLines;
        VectorLine ValueLines;

        VectorLine deltaPoints;

        public void DrawOptions(
            TrackData trackData,
            TrackColors trackColors,
            ColorPalette colorPalette,
            TexturesAndMaterials texturesAndMaterials,
            DataParameters dataParameters,
            Boolean redraw)
        {
            if (!redraw) {
                List<Vector3> empty = new List<Vector3>();
                OiLines = new VectorLine("Oi", empty, dataParameters.LineSize);
                VoluLines = new VectorLine("Volu", empty, dataParameters.LineSize);
                BidPxLines = new VectorLine("BidPx", empty, dataParameters.LineSize);
                ValueLines = new VectorLine("Value", empty, dataParameters.LineSize);
                deltaPoints = new VectorLine("delta", empty, dataParameters.GreekSize, LineType.Points);
            }

            List<VectorLine> trackList = new List<VectorLine>()
            {
                OiLines,
                VoluLines,
                BidPxLines,
                ValueLines
            };

            List<VectorLine> greekList = new List<VectorLine>()
            {
                deltaPoints
            };

            foreach (VectorLine line in trackList)
            {
                if (dataParameters.TrackOrder[line.name])
                {
                    line.active = true;
                    line.points3 = trackData.tradeDate[dataParameters.TradeDate].trackName[line.name].vectorList;
                    line.SetColors(trackColors.tradeDate[dataParameters.TradeDate].trackName[line.name].colorList);
                    line.SetWidth(dataParameters.LineSize, 0, line.GetSegmentNumber());
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
        }
    }
}
