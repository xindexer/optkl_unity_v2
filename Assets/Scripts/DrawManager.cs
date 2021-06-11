using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;
using Optkl.Data;

namespace Optkl
{
    public class DrawManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject labelPrefab;

        private VectorLine yteLines;
        private VectorLine OiLines;
        private VectorLine VoluLines;
        private VectorLine BidPxLines;
        private VectorLine ValueLines;
        private VectorLine AskPxLines;
        private VectorLine BidIvLines;
        private VectorLine MidIvLines;
        private VectorLine AskIvLines;
        private VectorLine smoothSmvVolLines;
        private VectorLine iRateLines;
        // private VectorLine divRateLines;
        // private VectorLine residualRateDataLines;
        private VectorLine extVolLines;
        private VectorLine extTheoLines;
        private VectorLine inTheMoneyLines;

        private VectorLine deltaPoints;
        private VectorLine gammaPoints;
        private VectorLine thetaPoints;
        private VectorLine vegaPoints;
        private VectorLine rhoPoints;
        private VectorLine phiPoints;

        private VectorLine CallTicks;
        private VectorLine PutTicks;

        private VectorLine circle;
        private VectorLine circleHalf;
        private VectorLine circleFull;
        private VectorLine SeanCircle;
        private VectorLine SeanCircle2;
        private VectorLine SeanTicks;


        private List<GameObject> CallTickLabelContainer = new List<GameObject>();
        private List<GameObject> PutTickLabelContainer = new List<GameObject>();
        private List<GameObject> CallLabelContainer = new List<GameObject>();
        private List<GameObject> PutLabelContainer = new List<GameObject>();

        private GameObject GOContainer;
        private GameObject GOLabelContainer;
        private GameObject GOLabel;
        private GameObject GOTickLabel;
        private GameObject GOTicksAxis;
        private GameObject GOOptklContainer;
        private GameObject GOEnergyMap;
        private GameObject GOGreeks;

        public void DrawOptions(
            TrackData trackData,
            TrackColors trackColors,
            TrackLabels trackLabels,
            TrackTickLabels trackTickLabels,
            TexturesAndMaterials texturesAndMaterials,
            ColorControl colorControl,
            DataParameters dataParameters,
            Boolean redraw,
            OptklManager optklManager)
        {
            if (!redraw) {
                GOContainer = new GameObject();
                GOContainer.name = dataParameters.TradeName;
                GOLabelContainer = new GameObject();
                GOLabelContainer.name = "Labels";
                GOLabelContainer.transform.SetParent(GOContainer.gameObject.transform);
                GOLabel = new GameObject();
                GOLabel.name = "ExpireDateLabels";
                GOLabel.transform.SetParent(GOLabelContainer.gameObject.transform);
                GOTickLabel = new GameObject();
                GOTickLabel.name = "TickLabels";
                GOTickLabel.transform.SetParent(GOLabelContainer.gameObject.transform);
                GOTicksAxis = new GameObject();
                GOTicksAxis.name = "Ticks and Axis";
                GOTicksAxis.transform.SetParent(GOLabelContainer.gameObject.transform);

                GOOptklContainer = new GameObject();
                GOOptklContainer.name = "OptklData";
                GOOptklContainer.transform.SetParent(GOContainer.gameObject.transform);
                GOEnergyMap = new GameObject();
                GOEnergyMap.name = "EnergyMap";
                GOEnergyMap.transform.SetParent(GOOptklContainer.gameObject.transform);
                GOGreeks = new GameObject();
                GOGreeks.name = "Greeks";
                GOGreeks.transform.SetParent(GOOptklContainer.gameObject.transform);

                CallTickLabelContainer = new List<GameObject>();
                PutTickLabelContainer = new List<GameObject>();
                CallLabelContainer = new List<GameObject>();
                PutLabelContainer = new List<GameObject>();

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
                // divRateLines = new VectorLine("divRate", empty, dataParameters.TrackLineWidth);
                // residualRateDataLines = new VectorLine("residualRateData", empty, dataParameters.TrackLineWidth);
                extVolLines = new VectorLine("extVol", empty, dataParameters.TrackLineWidth);
                extTheoLines = new VectorLine("extTheo", empty, dataParameters.TrackLineWidth);
                inTheMoneyLines = new VectorLine("inTheMoney", empty, dataParameters.TrackLineWidth);

                deltaPoints = new VectorLine("delta", empty, dataParameters.GreekSize, LineType.Points);
                gammaPoints = new VectorLine("gamma", empty, dataParameters.GreekSize, LineType.Points);
                thetaPoints = new VectorLine("theta", empty, dataParameters.GreekSize, LineType.Points);
                vegaPoints = new VectorLine("vega", empty, dataParameters.GreekSize, LineType.Points);
                rhoPoints = new VectorLine("rho", empty, dataParameters.GreekSize, LineType.Points);
                phiPoints = new VectorLine("phi", empty, dataParameters.GreekSize, LineType.Points);
                CallTicks = new VectorLine("CallTicks", empty, dataParameters.TickWidth);
                PutTicks = new VectorLine("PutTicks", empty, dataParameters.TickWidth);
                for (int i = 0; i < trackTickLabels.tradeDate[dataParameters.TradeName].side["CallTickLabels"].tickLabelList.Count; i++)
                {
                    GameObject labelObject = (GameObject)Instantiate(
                        labelPrefab.transform.gameObject,
                        trackTickLabels.tradeDate[dataParameters.TradeName].side["CallTickLabels"].tickLabelList[i].Location,
                        trackTickLabels.tradeDate[dataParameters.TradeName].side["CallTickLabels"].tickLabelList[i].Rotation);
                    labelObject.name = "CallTickLabel_" + trackTickLabels.tradeDate[dataParameters.TradeName].side["CallTickLabels"].tickLabelList[i].Text;
                    labelObject.transform.SetParent(GOTickLabel.gameObject.transform);
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackTickLabels.tradeDate[dataParameters.TradeName].side["CallTickLabels"].tickLabelList[i].Text;
                    CallTickLabelContainer.Add(labelObject);
                }
                for (int i = 0; i < trackTickLabels.tradeDate[dataParameters.TradeName].side["PutTickLabels"].tickLabelList.Count; i++)
                {
                    GameObject labelObject = (GameObject)Instantiate(labelPrefab.transform.gameObject,
                        trackTickLabels.tradeDate[dataParameters.TradeName].side["PutTickLabels"].tickLabelList[i].Location,
                        trackTickLabels.tradeDate[dataParameters.TradeName].side["PutTickLabels"].tickLabelList[i].Rotation);
                    labelObject.name = "PutTickLabel_" + trackTickLabels.tradeDate[dataParameters.TradeName].side["CallTickLabels"].tickLabelList[i].Text;
                    labelObject.transform.SetParent(GOTickLabel.gameObject.transform);
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackTickLabels.tradeDate[dataParameters.TradeName].side["PutTickLabels"].tickLabelList[i].Text;
                    PutTickLabelContainer.Add(labelObject);
                }
                for (int i = 0; i < trackLabels.tradeDate[dataParameters.TradeName].side["CallLabels"].labelList.Count; i++)
                {
                    GameObject labelObject = (GameObject)Instantiate(
                        labelPrefab.transform.gameObject,
                        trackLabels.tradeDate[dataParameters.TradeName].side["CallLabels"].labelList[i].Location,
                        trackLabels.tradeDate[dataParameters.TradeName].side["CallLabels"].labelList[i].Rotation);
                    labelObject.name = "CallLabel_" + trackLabels.tradeDate[dataParameters.TradeName].side["CallLabels"].labelList[i].Text;
                    labelObject.transform.SetParent(GOLabel.gameObject.transform);
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackLabels.tradeDate[dataParameters.TradeName].side["CallLabels"].labelList[i].Text;
                    CallLabelContainer.Add(labelObject);
                }
                for (int i = 0; i < trackLabels.tradeDate[dataParameters.TradeName].side["PutLabels"].labelList.Count; i++)
                {
                    GameObject labelObject = (GameObject)Instantiate(
                        labelPrefab.transform.gameObject,
                        trackLabels.tradeDate[dataParameters.TradeName].side["PutLabels"].labelList[i].Location,
                        trackLabels.tradeDate[dataParameters.TradeName].side["PutLabels"].labelList[i].Rotation);
                    labelObject.name = "PutLabel_" + trackLabels.tradeDate[dataParameters.TradeName].side["PutLabels"].labelList[i].Text;
                    labelObject.transform.SetParent(GOLabel.gameObject.transform);
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackLabels.tradeDate[dataParameters.TradeName].side["PutLabels"].labelList[i].Text;
                    PutLabelContainer.Add(labelObject);
                }

                // axis

                circle = new VectorLine("InnerAxis", new List<Vector3>(500), 1f, LineType.Continuous);
                circleHalf = new VectorLine("MiddleAxis", new List<Vector3>(500), 1f, LineType.Continuous);
                circleFull = new VectorLine("OuterAxis", new List<Vector3>(500), 1f, LineType.Continuous);

                SeanTicks = new VectorLine("SeanTicks", empty, 1f, LineType.Discrete);
                SeanCircle = new VectorLine("SeanCircle", new List<Vector3>(500), 1f, LineType.Discrete);
                SeanCircle2 = new VectorLine("SeanCircle2", new List<Vector3>(500), 1f, LineType.Discrete);
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
                // divRateLines,
                // residualRateDataLines,
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
            };

            foreach (VectorLine line in trackList)
            {
                if (dataParameters.TrackOrder[line.name].Active)
                {
                    line.active = true;
                    line.points3 = trackData.tradeDate[dataParameters.TradeName].trackName[line.name].vectorList;
                    line.SetColors(trackColors.tradeDate[dataParameters.TradeName].trackName[line.name].colorList);
                    line.SetWidth(dataParameters.TrackLineWidth, 0, line.GetSegmentNumber());
                    //if (texturesAndMaterials.OptklTextures.ContainsKey(line.name))
                    //    line.texture = texturesAndMaterials.OptklTextures[line.name].Texture;
                    //if (texturesAndMaterials.OptklTextures.ContainsKey(line.name))
                    //    line.material = texturesAndMaterials.OptklTextures[line.name].Material;
                    line.Draw3D();
                    line.rectTransform.transform.SetParent(GOEnergyMap.transform);
                }
                else
                {
                    line.active = false;
                }
                line.rectTransform.transform.SetParent(GOEnergyMap.transform);
            }

            foreach (VectorLine point in greekList)
            {
                if (dataParameters.ShowGreek[point.name])
                {
                    point.active = true;
                    point.points3 = trackData.tradeDate[dataParameters.TradeName].trackName[point.name].vectorList;
                    point.SetColors(trackColors.tradeDate[dataParameters.TradeName].trackName[point.name].colorList);
                    point.SetWidth(dataParameters.GreekSize, 0, point.GetSegmentNumber());
                    // if (texturesAndMaterials.OptklTextures.ContainsKey(point.name))
                    //     point.texture = texturesAndMaterials.OptklTextures[point.name].Texture;
                    // if (texturesAndMaterials.OptklTextures.ContainsKey(point.name))
                    //     point.material = texturesAndMaterials.OptklTextures[point.name].Material;
                    point.Draw3D();
                }
                else
                {
                    point.active = false;
                }
                point.rectTransform.transform.SetParent(GOGreeks.transform);
            }

            if (dataParameters.ShowTicks)
            {
                CallTicks.active = true;
                CallTicks.points3 = trackData.tradeDate[dataParameters.TradeName].trackName[CallTicks.name].vectorList;
                CallTicks.SetColor(colorControl.TickColor);
                CallTicks.SetWidth(dataParameters.TickWidth, 0, CallTicks.GetSegmentNumber());
                CallTicks.Draw3D();
                CallTicks.rectTransform.transform.SetParent(GOTicksAxis.transform);
                PutTicks.active = true;
                PutTicks.points3 = trackData.tradeDate[dataParameters.TradeName].trackName[PutTicks.name].vectorList;
                PutTicks.SetColor(colorControl.TickColor);
                PutTicks.SetWidth(dataParameters.TickWidth, 0, PutTicks.GetSegmentNumber());
                PutTicks.Draw3D();
                PutTicks.rectTransform.transform.SetParent(GOTicksAxis.transform);
            }
            else
            {
                CallTicks.active = false;
                PutTicks.active = false;
            }

            if (dataParameters.ShowTickLabels)
            {
                for (int i = 0; i < CallTickLabelContainer.Count; i++)
                {
                    CallTickLabelContainer[i].gameObject.SetActive(true);
                    TextMesh t = (TextMesh)CallTickLabelContainer[i].GetComponent(typeof(TextMesh));
                    t.color = colorControl.LabelTickColor;
                    CallTickLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.TickLabelSize, dataParameters.TickLabelSize, 0f);
                    CallTickLabelContainer[i].gameObject.transform.localPosition =
                        trackTickLabels.tradeDate[dataParameters.TradeName].side["CallTickLabels"].tickLabelList[i].Location;
                    CallTickLabelContainer[i].gameObject.transform.localRotation =
                        trackTickLabels.tradeDate[dataParameters.TradeName].side["CallTickLabels"].tickLabelList[i].Rotation;
                }

                for (int i = 0; i < PutTickLabelContainer.Count; i++)
                {
                    PutTickLabelContainer[i].gameObject.SetActive(true);
                    TextMesh t = (TextMesh)PutTickLabelContainer[i].GetComponent(typeof(TextMesh));
                    t.color = colorControl.LabelTickColor;
                    PutTickLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.TickLabelSize, dataParameters.TickLabelSize, 0f);
                    PutTickLabelContainer[i].gameObject.transform.localPosition =
                        trackTickLabels.tradeDate[dataParameters.TradeName].side["PutTickLabels"].tickLabelList[i].Location;
                    PutTickLabelContainer[i].gameObject.transform.localRotation =
                        trackTickLabels.tradeDate[dataParameters.TradeName].side["PutTickLabels"].tickLabelList[i].Rotation;
                }
            }
            else
            {
                for (int i = 0; i < CallTickLabelContainer.Count; i++)
                {
                    CallTickLabelContainer[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < PutTickLabelContainer.Count; i++)
                {
                    PutTickLabelContainer[i].gameObject.SetActive(false);
                }
            }

            if (dataParameters.ShowLabels)
            {
                for (int i = 0; i < CallLabelContainer.Count; i++)
                {
                    CallLabelContainer[i].gameObject.SetActive(true);
                    TextMesh t = (TextMesh)CallLabelContainer[i].GetComponent(typeof(TextMesh));
                    t.color = colorControl.LabelColor;
                    CallLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.LabelSize, dataParameters.LabelSize, 0f);
                    CallLabelContainer[i].gameObject.transform.localPosition =
                        trackLabels.tradeDate[dataParameters.TradeName].side["CallLabels"].labelList[i].Location;
                    CallLabelContainer[i].gameObject.transform.localRotation =
                        trackLabels.tradeDate[dataParameters.TradeName].side["CallLabels"].labelList[i].Rotation;
                }

                for (int i = 0; i < PutLabelContainer.Count; i++)
                {
                    PutLabelContainer[i].gameObject.SetActive(true);
                    TextMesh t = (TextMesh)PutLabelContainer[i].GetComponent(typeof(TextMesh));
                    t.color = colorControl.LabelColor;
                    PutLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.LabelSize, dataParameters.LabelSize, 0f);
                    PutLabelContainer[i].gameObject.transform.localPosition =
                        trackLabels.tradeDate[dataParameters.TradeName].side["PutLabels"].labelList[i].Location;
                    PutLabelContainer[i].gameObject.transform.localRotation =
                        trackLabels.tradeDate[dataParameters.TradeName].side["PutLabels"].labelList[i].Rotation;
                }
            }
            else
            {
                for (int i = 0; i < CallLabelContainer.Count; i++)
                {
                    CallLabelContainer[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < PutLabelContainer.Count; i++)
                {
                    PutLabelContainer[i].gameObject.SetActive(false);
                }
            }

            //axis

            if (dataParameters.ShowAxis)
            {
                circle.active = true;
                circle.SetColor(colorControl.AxisColor);
                circle.SetWidth(dataParameters.AxisWidth);
                circle.MakeCircle(Vector3.zero, dataParameters.GreekInnerRadius);
                circle.Draw3D();
                circle.rectTransform.transform.SetParent(GOTicksAxis.transform);

                circleHalf.active = true;
                circleHalf.SetColor(colorControl.AxisColor);
                circleHalf.SetWidth(dataParameters.AxisWidth);
                circleHalf.MakeCircle(Vector3.zero, dataParameters.GreekInnerRadius + (dataParameters.GreekOuterRadius - dataParameters.GreekInnerRadius) / 2);
                circleHalf.Draw3D();
                circleHalf.rectTransform.transform.SetParent(GOTicksAxis.transform);

                circleFull.active = true;
                circleFull.SetColor(colorControl.AxisColor);
                circleFull.SetWidth(dataParameters.AxisWidth);
                circleFull.MakeCircle(Vector3.zero, dataParameters.GreekOuterRadius);
                circleFull.Draw3D();
                circleFull.rectTransform.transform.SetParent(GOTicksAxis.transform);
            }
            else
            {
                circle.active = false;
                circleHalf.active = false;
                circleFull.active = false;
            }

            if (dataParameters.ShowSeanCicrle) {

                Vector3 LeftHighPoint = new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier)) * Math.Cos((-90 + dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier)) * Math.Sin((-90 + dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        0f);
                Vector3 LeftLowPoint = new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick) * Math.Cos((-90 + dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick) * Math.Sin((-90 + dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        0f);
                Vector3 RightHighPoint = new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier)) * Math.Cos((90 - dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier)) * Math.Sin((90 - dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        0f);
                Vector3 RightLowPoint = new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick) * Math.Cos((90 - dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick) * Math.Sin((90 - dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        0f);
                Vector3 LeftHighPoint2 = new Vector3(
                       (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier)) * Math.Cos((-90 - dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                       (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier)) * Math.Sin((-90 - dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                       0f);
                Vector3 LeftLowPoint2 = new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick) * Math.Cos((-90 - dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick) * Math.Sin((-90 - dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        0f);
                Vector3 RightHighPoint2 = new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier)) * Math.Cos((90 + dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier)) * Math.Sin((90 + dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        0f);
                Vector3 RightLowPoint2 = new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick) * Math.Cos((90 + dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick) * Math.Sin((90 + dataParameters.SeanCircleSeparator) * Mathf.Deg2Rad)),
                        0f);
                List<Vector3> SeanTickList = new List<Vector3>()
                {
                    { LeftHighPoint },
                    { LeftLowPoint },
                    { RightHighPoint },
                    { RightLowPoint },
                    { LeftHighPoint2 },
                    { LeftLowPoint2 },
                    { RightHighPoint2 },
                    { RightLowPoint2 }
                };
                SeanTicks.active = true;
                SeanTicks.points3 = SeanTickList;
                SeanTicks.SetWidth(dataParameters.SeanCicrleWidth);
                SeanTicks.SetColor(colorControl.SeanCircleColor);
                SeanTicks.Draw3D();
                SeanTicks.rectTransform.transform.SetParent(GOTicksAxis.transform);

                SeanCircle.active = true;
                SeanCircle.SetWidth(dataParameters.SeanCicrleWidth);
                SeanCircle.SetColor(colorControl.SeanCircleColor);
                SeanCircle.MakeArc(
                    Vector3.zero,
                    dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick,
                    dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick,
                    dataParameters.SeanCircleSeparator, 180 - dataParameters.SeanCircleSeparator);
                SeanCircle.Draw3D();
                SeanCircle.rectTransform.transform.SetParent(GOTicksAxis.transform);

                SeanCircle2.active = true;
                SeanCircle2.SetWidth(dataParameters.SeanCicrleWidth);
                SeanCircle2.SetColor(colorControl.SeanCircleColor);
                SeanCircle2.MakeArc(
                    Vector3.zero,
                    dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick,
                    dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.SeanCircleMultiplier) + dataParameters.SeanCircleTick,
                    180 + dataParameters.SeanCircleSeparator, 360 - dataParameters.SeanCircleSeparator);
                SeanCircle2.Draw3D();
                SeanCircle2.rectTransform.transform.SetParent(GOTicksAxis.transform);


            }
            else
            {
                SeanCircle.active = false;
                SeanCircle2.active = false;
                SeanTicks.active = false;
            }

            optklManager.ShowIRIS();
        }
    }
}
