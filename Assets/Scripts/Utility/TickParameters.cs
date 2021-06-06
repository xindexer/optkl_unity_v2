﻿using System;
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
            TrackStorage trackStorage,
            DataParameters dataParameters,
            DataStrike dataStrike,
            DataMax dataMax,
            Settings settings,
            TrackLabels trackLabels)
        {
            float trackRadials = 2 * (float)Math.PI / settings.tradeDate[dataParameters.TradeDate].settings["TrackCircumference"];
            Vector3 tickStart;
            float deltaTheta;
            int minorTick;
            int majorTick;
            LabelList labelsContainer = new LabelList();
            if (settings.tradeDate[dataParameters.TradeDate].settings["TrackCircumference"] <= 4000)
            {
                minorTick = 2;
                majorTick = 10;
            }
            else if (settings.tradeDate[dataParameters.TradeDate].settings["TrackCircumference"] > 4000 &&
                settings.tradeDate[dataParameters.TradeDate].settings["TrackCircumference"] <= 8000)
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
                deltaTheta = -(settings.tradeDate[dataParameters.TradeDate].settings["PieSpace"] / 2 * trackRadials);
            else
                deltaTheta = settings.tradeDate[dataParameters.TradeDate].settings["PieSpace"] / 2 * trackRadials;
            float theta = (float)Math.PI / 2 + deltaTheta;
            ICollection<string> expiredDates = dataStrike.tradeDate[dataParameters.TradeDate].expireDate.Keys;
            TrackList newData = new TrackList();
            newData.vectorList = new List<Vector3>();
            foreach (object expiredObject in expiredDates) 
            {
                float minStrike = dataStrike.tradeDate[dataParameters.TradeDate].expireDate[(string)expiredObject].strikeMin;
                float maxStrike = dataStrike.tradeDate[dataParameters.TradeDate].expireDate[(string)expiredObject].strikeMax;
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
                    "expire",
                    (string)expiredObject,
                    new Vector3((float)(labelCircumference * Math.Cos(labelTheta)), (float)(labelCircumference * Math.Sin(labelTheta)), 0f),
                    rotate
                );
                labelsContainer.labelList.Add(expiredDateLabel);

                
                tickStart = new Vector3((float)(dataParameters.TickRadius * Math.Cos(theta)), (float)(dataParameters.TickRadius * Math.Sin(theta)), 0f);
                addTick(call,
                    (int)minStrike,
                    theta,
                    tickStart,
                    minorTick,
                    majorTick,
                    dataParameters,
                    labelsContainer,
                    newData
                );
                int nextTick = (int)minStrike;
                while (nextTick % minorTick != 0)
                    nextTick++;
                if ((nextTick - minStrike) != 0)
                {
                    if (call)
                        theta -= (nextTick - minStrike) * trackRadials; 
                    else
                        theta += (nextTick - minStrike) * trackRadials;
                    tickStart = new Vector3(
                        (float)(dataParameters.TickRadius * Math.Cos(theta)),
                        (float)(dataParameters.TickRadius * Math.Sin(theta)),
                        0f
                    );
                    addTick(call,
                        (int)minStrike,
                        theta,
                        tickStart,
                        minorTick,
                        majorTick,
                        dataParameters,
                        labelsContainer,
                        newData
                    );
                }
                while (nextTick < maxStrike)
                {
                    nextTick += minorTick;
                    if (nextTick > maxStrike)
                    {
                        float endArc = nextTick - maxStrike;
                        if (call)
                            theta -= endArc * trackRadials;
                        else
                            theta += endArc * trackRadials;
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
                            (int)minStrike,
                            theta,
                            tickStart,
                            minorTick,
                            majorTick,
                            dataParameters,
                            labelsContainer,
                            newData
                        );
                    }
                }
                if (call)
                    theta -= settings.tradeDate[dataParameters.TradeDate].settings["PieSpace"] * trackRadials;
                else
                    theta += settings.tradeDate[dataParameters.TradeDate].settings["PieSpace"] * trackRadials;
            }
            if (call) {
                TrackData customTrack = new TrackData();
                customTrack.trackName = new TrackStorageNestedDict();
                customTrack.trackName.Add("CallTicks", newData);
                trackStorage.tradeDate.Add(dataParameters.TradeDate, customTrack);
                TrackLabel customLabel = new TrackLabel();
                customLabel.side = new TrackLabelsNestedDict();
                customLabel.side.Add("CallLabels", labelsContainer);
                trackStorage.tradeDate.Add(dataParameters.TradeDate, customTrack);
            }
            else
            {
                trackStorage.tradeDate[dataParameters.TradeDate].trackName.Add("PutTicks", newData);
                trackLabels.tradeDate[dataParameters.TradeDate].side.Add("PutLabels", labelsContainer);
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
            LabelList labelsContainer,
            TrackList newData)
        {
            Vector3 tick = Vector3.zero;
            if (labelCheck % majorTick == 0)
            {
                tick = new Vector3(
                    (float)((dataParameters.TickRadius + (dataParameters.TickHeight * 2)) * Math.Cos(theta)),
                    (float)((dataParameters.TickRadius + (dataParameters.TickHeight * 2)) * Math.Sin(theta)),
                    0f
                );
                Quaternion rotate;
                Label tickLabel;
                if (call)
                    rotate = Quaternion.Euler(0, 0, (float)(theta * Mathf.Rad2Deg));
                else
                    rotate = Quaternion.Euler(0, 0, (float)(theta * Mathf.Rad2Deg) + 180);
                tickLabel = new Label(
                    "tick",
                    labelCheck.ToString(),
                    new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * 5)) * Math.Cos(theta)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * 5)) * Math.Sin(theta)),
                        0f
                    ),
                    rotate
                );
                labelsContainer.labelList.Add(tickLabel);
            }
            else if (labelCheck % minorTick == 0)
            {
                tick = new Vector3(
                    (float)((dataParameters.TickRadius + dataParameters.TickHeight) * Math.Cos(theta)),
                    (float)((dataParameters.TickRadius + dataParameters.TickHeight) * Math.Sin(theta)),
                    0f
                );
            }
            newData.vectorList.Add(arc);
            newData.vectorList.Add(tick);
        }
    }
}