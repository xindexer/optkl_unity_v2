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
        Dictionary<string, VectorLine> vectorLineData;
        
        public void DrawIRIS(
            Dictionary<string, List<Vector3>> trackPositionData,
            Dictionary<string, List<Color32>> trackColorData,
            List<Label> tickLabelList,
            List<Label> trackLabelList,
            List<Vector3> tickPositionData,
            ColorControl colorControl,
            DataParameters dataParameters,
            bool redraw,
            OptklManager optklManager)
        {
            if (!redraw)
            {
                GOContainer = new GameObject(){name = dataParameters.TradeName};
                GOLabelContainer = new GameObject() {name = "Labels"};
                GOLabelContainer.transform.SetParent(GOContainer.gameObject.transform);
                GOLabel = new GameObject() {name = "ExpireDateLabels"};
                GOLabel.transform.SetParent(GOLabelContainer.gameObject.transform);
                GOTickLabel = new GameObject() {name = "TickLabels"};
                GOTickLabel.transform.SetParent(GOLabelContainer.gameObject.transform);
                GOTicksAxis = new GameObject() {name = "Ticks and Axis"};
                GOTicksAxis.transform.SetParent(GOLabelContainer.gameObject.transform);
                GOOptklContainer = new GameObject() {name = "OptklData"};
                GOOptklContainer.transform.SetParent(GOContainer.gameObject.transform);
                GOEnergyMap = new GameObject() {name = "EnergyMap"};
                GOEnergyMap.transform.SetParent(GOOptklContainer.gameObject.transform);
                GOGreeks = new GameObject() {name = "Greeks"};
                GOGreeks.transform.SetParent(GOOptklContainer.gameObject.transform);
                vectorLineData = new Dictionary<string, VectorLine>();
                TickLabelContainer = new List<GameObject>();
                TrackLabelContainer = new List<GameObject>();
                List<Vector3> empty = new List<Vector3>();
                foreach (string key in dataParameters.TrackOrder.Keys)
                {
                    if (dataParameters.TrackOrder[key].Active)
                    {
                        vectorLineData[key] = new VectorLine(key, trackPositionData[vectorLineData[key].name], dataParameters.TrackLineWidth);
                    }
                }
                foreach (string key in dataParameters.ShowGreek.Keys)
                {
                    if (dataParameters.ShowGreek[key])
                    {
                        vectorLineData[key] = new VectorLine(key, empty, dataParameters.TrackLineWidth, LineType.Points);
                    }
                }

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
            
            foreach (string key in dataParameters.TrackOrder.Keys)
            {
                if (dataParameters.TrackOrder[key].Active)
                {
                    vectorLineData[key].active = true;
                    vectorLineData[key].points3 = trackPositionData[vectorLineData[key].name];
                    vectorLineData[key].SetColors(trackColorData[vectorLineData[key].name]);
                    vectorLineData[key].SetWidth(dataParameters.TrackLineWidth, 0, vectorLineData[key].GetSegmentNumber());
                    vectorLineData[key].Draw3D();
                    vectorLineData[key].rectTransform.transform.SetParent(GOEnergyMap.transform);
                }
                else
                {
                    try
                    {
                        vectorLineData[key].active = false;
                    }
                    catch
                    {
                        // Debug.Log(key);
                    }
                }
            }

            foreach (string key in dataParameters.ShowGreek.Keys)
            {
                if (dataParameters.ShowGreek[key])
                {
                    vectorLineData[key].active = true;
                    vectorLineData[key].points3 = trackPositionData[vectorLineData[key].name];
                    vectorLineData[key].SetColors(trackColorData[vectorLineData[key].name]);
                    vectorLineData[key].SetWidth(dataParameters.GreekSize, 0, vectorLineData[key].GetSegmentNumber());
                    vectorLineData[key].Draw3D();
                }
                else
                {
                    try
                    {
                        vectorLineData[key].active = false;
                    }
                    catch {}
                }

                try
                {
                    vectorLineData[key].rectTransform.transform.SetParent(GOGreeks.transform);
                }
                catch {}
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
