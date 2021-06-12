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

        private VectorLine Ticks;

        private VectorLine circle;
        private VectorLine circleHalf;
        private VectorLine circleFull;
        private VectorLine SeanCircle;
        private VectorLine SeanCircle2;
        private VectorLine SeanTicks;


        private List<GameObject> TickLabelContainer = new List<GameObject>();
        private List<GameObject> TrackLabelContainer = new List<GameObject>();

        private GameObject GOContainer;
        private GameObject GOLabelContainer;
        private GameObject GOLabel;
        private GameObject GOTickLabel;
        private GameObject GOTicksAxis;
        private GameObject GOOptklContainer;
        private GameObject GOEnergyMap;
        private GameObject GOGreeks;

        public void DrawOptions(
            Dictionary<string, List<Vector3>> trackPositionData,
            Dictionary<string, List<Color32>> trackColorData,
            List<Label> tickLabelList,
            List<Label> trackLabelList,
            List<Vector3> tickPositionData,
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

                TickLabelContainer = new List<GameObject>();
                TrackLabelContainer = new List<GameObject>();

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
                Ticks = new VectorLine("Ticks", empty, dataParameters.TickWidth);
                foreach (Label tickLabel in tickLabelList)
                {
                    GameObject labelObject = (GameObject)Instantiate(
                        labelPrefab.transform.gameObject,
                        tickLabel.Location,
                        tickLabel.Rotation);
                    labelObject.name = "TickLabel_" + tickLabel.Text;
                    labelObject.transform.SetParent(GOTickLabel.gameObject.transform);
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = tickLabel.Text;
                    TickLabelContainer.Add(labelObject);
                }
                foreach (Label trackLabel in trackLabelList)
                {
                    GameObject labelObject = (GameObject)Instantiate(
                        labelPrefab.transform.gameObject,
                        trackLabel.Location,
                        trackLabel.Rotation);
                    labelObject.name = "TrackLabel_" + trackLabel.Text;
                    labelObject.transform.SetParent(GOTickLabel.gameObject.transform);
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackLabel.Text;
                    TrackLabelContainer.Add(labelObject);
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
                    line.points3 = trackPositionData[line.name];
                    line.SetColors(trackColorData[line.name]);
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
                    point.points3 = trackPositionData[point.name];
                    point.SetColors(trackColorData[point.name]);
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
                Ticks.active = true;
                Ticks.points3 = tickPositionData;
                Ticks.SetColor(colorControl.TickColor);
                Ticks.SetWidth(dataParameters.TickWidth, 0, Ticks.GetSegmentNumber());
                Ticks.Draw3D();
                Ticks.rectTransform.transform.SetParent(GOTicksAxis.transform);
            }
            else
            {
                Ticks.active = false;
            }

            if (dataParameters.ShowTickLabels)
            {
                for (int i = 0; i < TickLabelContainer.Count; i++)
                {
                    TickLabelContainer[i].gameObject.SetActive(true);
                    TextMesh t = (TextMesh)TickLabelContainer[i].GetComponent(typeof(TextMesh));
                    t.color = colorControl.LabelTickColor;
                    TickLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.TickLabelSize, dataParameters.TickLabelSize, 0f);
                    TickLabelContainer[i].gameObject.transform.localPosition = tickLabelList[i].Location;
                    TickLabelContainer[i].gameObject.transform.localRotation = tickLabelList[i].Rotation;
                }
            }
            else
            {
                foreach (GameObject tickLabel in TickLabelContainer)
                {
                    tickLabel.gameObject.SetActive(false);
                }
            }

            if (dataParameters.ShowLabels)
            {
                for (int i = 0; i < TrackLabelContainer.Count; i++)
                {
                    TrackLabelContainer[i].gameObject.SetActive(true);
                    TextMesh t = (TextMesh)TrackLabelContainer[i].GetComponent(typeof(TextMesh));
                    t.color = colorControl.LabelColor;
                    TrackLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.LabelSize, dataParameters.LabelSize, 0f);
                    TrackLabelContainer[i].gameObject.transform.localPosition = trackLabelList[i].Location;
                    TrackLabelContainer[i].gameObject.transform.localRotation = trackLabelList[i].Rotation;
                }
            }
            else
            {
                foreach (GameObject trackLabelGO in TrackLabelContainer)
                {
                    trackLabelGO.gameObject.SetActive(false);
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
