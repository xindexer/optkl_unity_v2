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

        [SerializeField]
        private GameObject deltaPrefab;

        [SerializeField]
        private GameObject gammaPrefab;

        [SerializeField]
        private GameObject thetaPrefab;

        [SerializeField]
        private GameObject vegaPrefab;

        [SerializeField]
        private GameObject rhoPrefab;

        [SerializeField]
        private GameObject phiPrefab;

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
        //private VectorLine divRateLines;
        //private VectorLine residualRateDataLines;
        private VectorLine extVolLines;
        private VectorLine extTheoLines;
        private VectorLine inTheMoneyLines;

        private VectorLine deltaPoints;
        private VectorLine gammaPoints;
        private VectorLine thetaPoints;
        private VectorLine vegaPoints;
        private VectorLine rhoPoints;
        private VectorLine phiPoints;
        private VectorLine driftlessThetaPoints;

        private VectorLine CallTicks;
        private VectorLine PutTicks;

        private List<GameObject> CallTickLabelContainer = new List<GameObject>();
        private List<GameObject> PutTickLabelContainer = new List<GameObject>();
        private List<GameObject> CallLabelContainer = new List<GameObject>();
        private List<GameObject> PutLabelContainer = new List<GameObject>();

        private GameObject GOContainer;
        private GameObject GOLabelContainer;
        private GameObject GOOptklContainer;

        public void DrawOptions(
            TrackData trackData,
            TrackColors trackColors,
            TrackLabels trackLabels,
            TrackTickLabels trackTickLabels,
            TexturesAndMaterials texturesAndMaterials,
            DataParameters dataParameters,
            Boolean redraw,
            OptklManager optklManager)
        {
            if (!redraw) {
                GOContainer = new GameObject();
                GOContainer.name = dataParameters.TradeDate;
                GOLabelContainer = new GameObject();
                GOLabelContainer.name = "Labels";
                GOLabelContainer.transform.parent = GOContainer.gameObject.transform;
                GOOptklContainer = new GameObject();
                GOOptklContainer.name = "OptklData";
                GOOptklContainer.transform.parent = GOContainer.gameObject.transform;
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
                for (int i = 0; i < trackTickLabels.tradeDate[dataParameters.TradeDate].side["CallTickLabels"].tickLabelList.Count; i++)
                {
                    GameObject labelObject = (GameObject)Instantiate(
                        labelPrefab.transform.gameObject,
                        trackTickLabels.tradeDate[dataParameters.TradeDate].side["CallTickLabels"].tickLabelList[i].Location,
                        trackTickLabels.tradeDate[dataParameters.TradeDate].side["CallTickLabels"].tickLabelList[i].Rotation);
                    labelObject.name = "CallTickLabel_" + trackLabels.tradeDate[dataParameters.TradeDate].side["CallTickLabels"].labelList[i].Text;
                    labelObject.transform.parent = GOLabelContainer.gameObject.transform;
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackTickLabels.tradeDate[dataParameters.TradeDate].side["CallTickLabels"].tickLabelList[i].Text;
                    CallTickLabelContainer.Add(labelObject);
                }
                for (int i = 0; i < trackTickLabels.tradeDate[dataParameters.TradeDate].side["PutTickLabels"].tickLabelList.Count; i++)
                {
                    GameObject labelObject = (GameObject)Instantiate(labelPrefab.transform.gameObject,
                        trackTickLabels.tradeDate[dataParameters.TradeDate].side["PutTickLabels"].tickLabelList[i].Location,
                        trackTickLabels.tradeDate[dataParameters.TradeDate].side["PutTickLabels"].tickLabelList[i].Rotation);
                    labelObject.name = "PutTickLabel_" + trackLabels.tradeDate[dataParameters.TradeDate].side["PutTickLabels"].labelList[i].Text;
                    labelObject.transform.parent = GOLabelContainer.gameObject.transform;
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackTickLabels.tradeDate[dataParameters.TradeDate].side["PutTickLabels"].tickLabelList[i].Text;
                    PutTickLabelContainer.Add(labelObject);
                }
                for (int i = 0; i < trackLabels.tradeDate[dataParameters.TradeDate].side["CallLabels"].labelList.Count; i++)
                {
                    GameObject labelObject = (GameObject)Instantiate(
                        labelPrefab.transform.gameObject,
                        trackLabels.tradeDate[dataParameters.TradeDate].side["CallLabels"].labelList[i].Location,
                        trackLabels.tradeDate[dataParameters.TradeDate].side["CallLabels"].labelList[i].Rotation);
                    labelObject.name = "CallLabel_" + trackLabels.tradeDate[dataParameters.TradeDate].side["CallLabels"].labelList[i].Text;
                    labelObject.transform.parent = GOLabelContainer.gameObject.transform;
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackLabels.tradeDate[dataParameters.TradeDate].side["CallLabels"].labelList[i].Text;
                    CallLabelContainer.Add(labelObject);
                }
                for (int i = 0; i < trackLabels.tradeDate[dataParameters.TradeDate].side["PutLabels"].labelList.Count; i++)
                {
                    GameObject labelObject = (GameObject)Instantiate(
                        labelPrefab.transform.gameObject,
                        trackLabels.tradeDate[dataParameters.TradeDate].side["PutLabels"].labelList[i].Location,
                        trackLabels.tradeDate[dataParameters.TradeDate].side["PutLabels"].labelList[i].Rotation);
                    labelObject.name = "PutLabel_" + trackLabels.tradeDate[dataParameters.TradeDate].side["PutLabels"].labelList[i].Text;
                    labelObject.transform.parent = GOLabelContainer.gameObject.transform;
                    TextMesh t = (TextMesh)labelObject.GetComponent(typeof(TextMesh));
                    t.text = trackLabels.tradeDate[dataParameters.TradeDate].side["PutLabels"].labelList[i].Text;
                    PutLabelContainer.Add(labelObject);
                }
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

            //for (int i = 0; i < trackData.tradeDate[dataParameters.TradeDate].trackName["delta"].vectorList.Count; i++)
            //{
            //    Instantiate(deltaPrefab.transform.gameObject,
            //            trackData.tradeDate[dataParameters.TradeDate].trackName["delta"].vectorList[i],
            //            Quaternion.identity);
            //    Instantiate(gammaPrefab.transform.gameObject,
            //            trackData.tradeDate[dataParameters.TradeDate].trackName["gamma"].vectorList[i],
            //            Quaternion.identity);
            //    Instantiate(thetaPrefab.transform.gameObject,
            //            trackData.tradeDate[dataParameters.TradeDate].trackName["theta"].vectorList[i],
            //            Quaternion.identity);
            //    Instantiate(vegaPrefab.transform.gameObject,
            //            trackData.tradeDate[dataParameters.TradeDate].trackName["vega"].vectorList[i],
            //            Quaternion.identity);
            //    Instantiate(rhoPrefab.transform.gameObject,
            //            trackData.tradeDate[dataParameters.TradeDate].trackName["rho"].vectorList[i],
            //            Quaternion.identity);
            //    Instantiate(phiPrefab.transform.gameObject,
            //            trackData.tradeDate[dataParameters.TradeDate].trackName["phi"].vectorList[i],
            //            Quaternion.identity);
            //}


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

            if (dataParameters.ShowTicks)
            {
                CallTicks.active = true;
                CallTicks.points3 = trackData.tradeDate[dataParameters.TradeDate].trackName[CallTicks.name].vectorList;
                CallTicks.SetColor(Color.white);
                CallTicks.SetWidth(dataParameters.TickWidth, 0, CallTicks.GetSegmentNumber());
                CallTicks.Draw3D();
                PutTicks.active = true;
                PutTicks.points3 = trackData.tradeDate[dataParameters.TradeDate].trackName[PutTicks.name].vectorList;
                PutTicks.SetColor(Color.white);
                PutTicks.SetWidth(dataParameters.TickWidth, 0, PutTicks.GetSegmentNumber());
                PutTicks.Draw3D();
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
                    CallTickLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.TickLabelSize, dataParameters.TickLabelSize, 0f);
                    CallTickLabelContainer[i].gameObject.transform.localPosition =
                        trackTickLabels.tradeDate[dataParameters.TradeDate].side["CallTickLabels"].tickLabelList[i].Location;
                    CallTickLabelContainer[i].gameObject.transform.localRotation =
                        trackTickLabels.tradeDate[dataParameters.TradeDate].side["CallTickLabels"].tickLabelList[i].Rotation;
                }

                for (int i = 0; i < PutTickLabelContainer.Count; i++)
                {
                    PutTickLabelContainer[i].gameObject.SetActive(true);
                    PutTickLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.TickLabelSize, dataParameters.TickLabelSize, 0f);
                    PutTickLabelContainer[i].gameObject.transform.localPosition =
                        trackTickLabels.tradeDate[dataParameters.TradeDate].side["PutTickLabels"].tickLabelList[i].Location;
                    PutTickLabelContainer[i].gameObject.transform.localRotation =
                        trackTickLabels.tradeDate[dataParameters.TradeDate].side["PutTickLabels"].tickLabelList[i].Rotation;
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
                    CallLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.LabelSize, dataParameters.LabelSize, 0f);
                    CallLabelContainer[i].gameObject.transform.localPosition =
                        trackLabels.tradeDate[dataParameters.TradeDate].side["CallLabels"].labelList[i].Location;
                    CallLabelContainer[i].gameObject.transform.localRotation =
                        trackLabels.tradeDate[dataParameters.TradeDate].side["CallLabels"].labelList[i].Rotation;
                }

                for (int i = 0; i < PutLabelContainer.Count; i++)
                {
                    PutLabelContainer[i].gameObject.SetActive(true);
                    PutLabelContainer[i].gameObject.transform.localScale = new Vector3(dataParameters.LabelSize, dataParameters.LabelSize, 0f);
                    PutLabelContainer[i].gameObject.transform.localPosition =
                        trackLabels.tradeDate[dataParameters.TradeDate].side["PutLabels"].labelList[i].Location;
                    PutLabelContainer[i].gameObject.transform.localRotation =
                        trackLabels.tradeDate[dataParameters.TradeDate].side["PutLabels"].labelList[i].Rotation;
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
            optklManager.ShowIRIS();
        }
    }
}
