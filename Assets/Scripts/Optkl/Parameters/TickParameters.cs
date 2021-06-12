using System;
using UnityEngine;
using Optkl.Data;
using System.Collections.Generic;

namespace Optkl.Parameters
{
    public class TickParameters
    {
        public void BuildTicks(
            bool call,
            DataParameters dataParameters,
            DataStrike dataStrike,
            StrikeParameterData strikeParameterData,
            ref List<Label> tickLabelList,
            ref List<Label> trackLabelList,
            ref List<Vector3> tickPositionData)
        {
            float trackRadials = 2 * (float)Math.PI / strikeParameterData.TrackCircumference;
            float deltaTheta;
            int minorTick;
            int majorTick;
            if (strikeParameterData.TrackCircumference <= 500)
            {
                minorTick = 1;
                majorTick = 5;
            }
            if (strikeParameterData.TrackCircumference <= 4000)
            {
                minorTick = 2;
                majorTick = 10;
            }
            else if (strikeParameterData.TrackCircumference > 4000 && strikeParameterData.TrackCircumference <= 10000)
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
                deltaTheta = -strikeParameterData.PieSpace / 2 * trackRadials;
            else
                deltaTheta = strikeParameterData.PieSpace / 2 * trackRadials;
            float theta = (float)Math.PI / 2 + deltaTheta;
            ICollection<string> expiredDates = dataStrike.tradeDate[dataParameters.TradeName].expireDate.Keys;
            foreach (string expiredObject in expiredDates) 
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
                trackLabelList.Add(expiredDateLabel);

                
                Vector3 tickStart = new Vector3((float)(dataParameters.TickRadius * Math.Cos(theta)), (float)(dataParameters.TickRadius * Math.Sin(theta)), 0f);
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
                    tickLabelList,
                    tickPositionData,
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
                        tickLabelList,
                        tickPositionData,
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
                            tickLabelList,
                            tickPositionData,
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
                            tickLabelList,
                            tickPositionData,
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
                        tickLabelList,
                        tickPositionData,
                        true
                    );
                }
                if (call)
                    theta -= strikeParameterData.PieSpace * trackRadials;
                else
                    theta += strikeParameterData.PieSpace * trackRadials;
            }
        }

        private void addTick(
            bool call,
            int labelCheck,
            double theta,
            Vector3 arc,
            int minorTick,
            int majorTick,
            DataParameters dataParameters,
            List<Label> tickLabelContainer,
            List<Vector3> trackPositionData,
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
                if (call)
                    rotate = Quaternion.Euler(0, 0, (float)(theta * Mathf.Rad2Deg));
                else
                    rotate = Quaternion.Euler(0, 0, (float)(theta * Mathf.Rad2Deg) + 180);
                Label tickLabel = new Label(
                    labelCheck.ToString(),
                    new Vector3(
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.TickLabelDistanceMultiplier)) * Math.Cos(theta)),
                        (float)((dataParameters.TickRadius + (dataParameters.TickHeight * dataParameters.TickLabelDistanceMultiplier)) * Math.Sin(theta)),
                        0f
                    ),
                    rotate
                );
                
                tickLabelContainer.Add(tickLabel);
                trackPositionData.Add(arc);
                trackPositionData.Add(tick);
            }
            else if (labelCheck % minorTick == 0)
            {
                tick = new Vector3(
                    (float)((dataParameters.TickRadius + dataParameters.TickHeight) * Math.Cos(theta)),
                    (float)((dataParameters.TickRadius + dataParameters.TickHeight) * Math.Sin(theta)),
                    0f
                );
                trackPositionData.Add(arc);
                trackPositionData.Add(tick);
            } 
            else if (drawFirstLast)
            {
                tick = new Vector3(
                    (float)((dataParameters.TickRadius - dataParameters.TickHeight) * Math.Cos(theta)),
                    (float)((dataParameters.TickRadius - dataParameters.TickHeight) * Math.Sin(theta)),
                    0f
                );
                trackPositionData.Add(arc);
                trackPositionData.Add(tick);   
            }
        }
    }
}
