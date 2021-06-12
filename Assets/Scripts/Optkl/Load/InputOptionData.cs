using System;

namespace Optkl.Load
{
    public class InputOptionData
    {

        private string symbol;
        public string Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;
            }
        }

        private int lookback;
        public int Lookback
        {
            get
            {
                return lookback;
            }
            set
            {
                lookback = value;
            }
        }

        private DateTime tradeDate;
        public DateTime TradeDate
        {
            get
            {
                return tradeDate;
            }
            set
            {
                tradeDate = value;
            }
        }

        public DateTime GetTradeDate () {
            return Convert.ToDateTime($"{month} {day}, {year} 12:00:00");
        }

        public string FormatTradeDate
        {
            get
            {
                return tradeDate.ToString("yyyyMMMdd");
            }
        }

        public string JsonTradeDate
        {
            get
            {
                return tradeDate.ToString("yyyyMMdd");
            }
        }

        private string month;
        public string Month {
            get {
                return month;
            }
            set {
                month = value;
            }
        }


        private string day;
        public string Day {
            get {
                return day;
            }
            set {
                day = value;
            }
        }


        private string year;
        public string Year {
            get {
                return year;
            }
            set {
                year = value;
            }
        }


    }
}
