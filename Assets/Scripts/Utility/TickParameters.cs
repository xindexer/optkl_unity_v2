using System;
using UnityEngine;
using System.Collections;
using Optkl.Data;
using System.Collections.Generic;

namespace Optkl.Utilities
{
    public class TickParameters
    {
        public void BuildTicks(
            Boolean call,
            float[][] optionData,
            TrackData trackData,
            DataParameters dataParameters,
            DataStrike dataStrike,
            DataMax dataMax,
            Settings settings,
            TrackLabels trackLabels,
            TrackTickLabels trackTickLabels)
        {
            float trackCircumference = settings.tradeDate[dataParameters.TradeName].settings["TrackCircumference"];
            float trackRadials = 2 * (float)Math.PI / trackCircumference;
            float pieSpace = settings.tradeDate[dataParameters.TradeName].settings["PieSpace"];
            Vector3 tickStart;
            float deltaTheta;
            int minorTick;
            int majorTick;
            LabelList labelsContainer = new LabelList();
            labelsContainer.labelList = new List<Label>();
            TickLabelList tickLabelContainer = new TickLabelList();
            tickLabelContainer.tickLabelList = new List<Label>();
            if (trackCircumference <= 500)
            {
                minorTick = 1;
                majorTick = 5;
            }
            if (trackCircumference <= 4000)
            {
                minorTick = 2;
                majorTick = 10;
            }
            else if (trackCircumference > 4000 && trackCircumference <= 10000)
            {
                minorTick = 10;
                majorTick = 50;
            }
            else
            {
                minorTick = 50;
                majorTick = 250;
            }
            if (call)
                deltaTheta = -pieSpace / 2 * trackRadials;
            else
                deltaTheta = pieSpace / 2 * trackRadials;
            float theta = (float)Math.PI / 2 + deltaTheta;
            ICollection<string> expiredDates = dataStrike.tradeDate[dataParameters.TradeName].expireDate.Keys;
            TrackDataList newData = new TrackDataList();
            newData.vectorList = new List<Vector3>();
            foreach (object expiredObject in expiredDates) 
            {
                float minStrike = dataStrike.tradeDate[dataParameters.TradeName].expireDate[(string)expiredObject].strikeMin;
                float maxStrike = dataStrike.tradeDate[dataParameters.TradeName].expireDate[(string)expiredObject].strikeMax;
                float labelCircumference = (maxStrike - minStrike) / 2;
                float deltaLabelTheta;
                if (call)
                    deltaLabelTheta = -(float)(labelCircumference * trackRadials);
                else
                    deltaLabelTheta = (float)(labelCircumference * trackRadials);
                float labelTheta = theta + deltaLabelTheta;
                Quaternion rotate;
                if (call)
                    rotate = Quaternion.Euler(0, 0, (float)(labelTheta * Mathf.Rad2Deg));
                else
                    rotate = Quaternion.Euler(0, 0, (float)(labelTheta * Mathf.Rad2Deg) + 180);
                Label expiredDateLabel = new Label(
                    (string)expiredObject,
                    new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.LabelDistanceMultiplier)) * Math.Cos(labelTheta)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.LabelDistanceMultiplier)) * Math.Sin(labelTheta)),
                        0f),
                    rotate
                );
                labelsContainer.labelList.Add(expiredDateLabel);

                
                tickStart = new Vector3((float)(dataParameters.TickRadius * Math.Cos(theta)), (float)(dataParameters.TickRadius * Math.Sin(theta)), 0f);
                int nextTick = (int)minStrike;
                if (nextTick == 0)
                    nextTick++;
                addTick(call,
                    nextTick,
                    theta,
                    tickStart,
                    minorTick,
                    majorTick,
                    dataParameters,
                    tickLabelContainer,
                    newData,
                    true
                );
                while (nextTick % minorTick != 0 && nextTick < maxStrike)
                    nextTick++;
                if ((nextTick - minStrike) != 0)
                {
                    if (call)
                        theta -= ((float)nextTick - minStrike) * trackRadials; 
                    else
                        theta += ((float)nextTick - minStrike) * trackRadials;
                    tickStart = new Vector3(
                        (float)(dataParameters.TickRadius * Math.Cos(theta)),
                        (float)(dataParameters.TickRadius * Math.Sin(theta)),
                        0f
                    );
                    addTick(call,
                        nextTick,
                        theta,
                        tickStart,
                        minorTick,
                        majorTick,
                        dataParameters,
                        tickLabelContainer,
                        newData,
                        false
                    );
                }
                while (nextTick < maxStrike)
                {
                    nextTick += minorTick;
                    if (nextTick > maxStrike)
                    {
                        float endArc = maxStrike - (float)nextTick + (float)minorTick;
                        if (call)
                            theta -= endArc * trackRadials;
                        else
                            theta += endArc * trackRadials;
                        tickStart = new Vector3(
                            (float)(dataParameters.TickRadius * Math.Cos(theta)),
                            (float)(dataParameters.TickRadius * Math.Sin(theta)),
                            0f
                        );
                        addTick(call,
                            (int)maxStrike,
                            theta,
                            tickStart,
                            minorTick,
                            majorTick,
                            dataParameters,
                            tickLabelContainer,
                            newData,
                            true
                        );
                    }
                    else
                    {
                        if (call)
                            theta -= minorTick * trackRadials;
                        else
                            theta += minorTick * trackRadials;
                        tickStart = new Vector3(
                            (float)(dataParameters.TickRadius * Math.Cos(theta)),
                            (float)(dataParameters.TickRadius * Math.Sin(theta)),
                            0f
                        );
                        addTick(call,
                            nextTick,
                            theta,
                            tickStart,
                            minorTick,
                            majorTick,
                            dataParameters,
                            tickLabelContainer,
                            newData,
                            false
                        );
                    }
                }
                
                if (nextTick == maxStrike)
                {
                    float endArc = (float)nextTick - maxStrike;
                    if (call)
                        theta -= endArc * trackRadials;
                    else
                        theta += endArc * trackRadials;
                    addTick(call,
                        nextTick,
                        theta,
                        tickStart,
                        minorTick,
                        majorTick,
                        dataParameters,
                        tickLabelContainer,
                        newData,
                        true
                    );
                }
                if (call)
                    theta -= pieSpace * trackRadials;
                else
                    theta += pieSpace * trackRadials;
            }
            if (call) {
                TrackDataContainer customTrack = new TrackDataContainer();
                customTrack.trackName = new TrackDataContainerNestedDict();
                customTrack.trackName.Add("CallTicks", newData);
                trackData.tradeDate.Add(dataParameters.TradeName, customTrack);
                TrackLabel customLabel = new TrackLabel();
                customLabel.side = new TrackLabelsNestedDict();
                customLabel.side.Add("CallLabels", labelsContainer);
                trackLabels.tradeDate.Add(dataParameters.TradeName, customLabel);
                TrackTickLabel customTickLabel = new TrackTickLabel();
                customTickLabel.side = new TrackTickLabelsNestedDict();
                customTickLabel.side.Add("CallTickLabels", tickLabelContainer);
                trackTickLabels.tradeDate.Add(dataParameters.TradeName, customTickLabel);
            }
            else
            {
                trackData.tradeDate[dataParameters.TradeName].trackName.Add("PutTicks", newData);
                trackLabels.tradeDate[dataParameters.TradeName].side.Add("PutLabels", labelsContainer);
                trackTickLabels.tradeDate[dataParameters.TradeName].side.Add("PutTickLabels", tickLabelContainer);
            }
        }

        private void addTick(
            Boolean call,
            int labelCheck,
            double theta,
            Vector3 arc,
            int minorTick,
            int majorTick,
            DataParameters dataParameters,
            TickLabelList tickLabelContainer,
            TrackDataList newData,
            Boolean drawFirstLast)
        {
            Vector3 tick = Vector3.zero;
            if (labelCheck % majorTick == 0)
            {
                tick = new Vector3(
                    (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.MajorTickMultiplier)) * Math.Cos(theta)),
                    (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.MajorTickMultiplier)) * Math.Sin(theta)),
                    0f
                );
                Quaternion rotate;
                Label tickLabel;
                if (call)
                    rotate = Quaternion.Euler(0, 0, (float)(theta * Mathf.Rad2Deg));
                else
                    rotate = Quaternion.Euler(0, 0, (float)(theta * Mathf.Rad2Deg) + 180);
                tickLabel = new Label(
                    labelCheck.ToString(),
                    new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.TickLabelDistanceMultiplier)) * Math.Cos(theta)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.TickLabelDistanceMultiplier)) * Math.Sin(theta)),
                        0f
                    ),
                    rotate
                );
                
                tickLabelContainer.tickLabelList.Add(tickLabel);
                newData.vectorList.Add(arc);
                newData.vectorList.Add(tick);
            }
            else if (labelCheck % minorTick == 0)
            {
                tick = new Vector3(
                    (float)((dataParameters.TickRadius + dataParameters.TickHeight) * Math.Cos(theta)),
                    (float)((dataParameters.TickRadius + dataParameters.TickHeight) * Math.Sin(theta)),
                    0f
                );
                newData.vectorList.Add(arc);
                newData.vectorList.Add(tick);
            } 
            else if (drawFirstLast)
            {
                tick = new Vector3(
                    (float)((dataParameters.TickRadius - dataParameters.TickHeight) * Math.Cos(theta)),
                    (float)((dataParameters.TickRadius - dataParameters.TickHeight) * Math.Sin(theta)),
                    0f
                );
                newData.vectorList.Add(arc);
                newData.vectorList.Add(tick);   
            }
        }
    }
}
