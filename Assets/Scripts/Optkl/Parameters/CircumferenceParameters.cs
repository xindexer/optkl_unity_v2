using Optkl.Data;

namespace Optkl.Parameters
{
    public class CircumferenceParameters
    {
        public CircumferenceParameterData CalculateCircumference(
            DataParameters dataParameters,
            DataStrike dataStrike)
        {
            float trackCircumference = 0f;
            int numberPies = 0;
            foreach (string key in dataStrike.tradeDate[dataParameters.TradeName].expireDate.Keys)
            {
                StrikeMinMax minMax = dataStrike.tradeDate[dataParameters.TradeName].expireDate[key];
                trackCircumference += (minMax.strikeMax - minMax.strikeMin);
                numberPies++;
            }
            trackCircumference *= 2;
            float pieSpace = dataParameters.PieSpacer / 100 * trackCircumference;
            float powerWedgeSpace = dataParameters.PowerWedge / 100 * trackCircumference;
            trackCircumference += powerWedgeSpace + (numberPies * 2 - 1) * pieSpace;
            return new CircumferenceParameterData(trackCircumference, pieSpace);
        }
    }
}