using System;

namespace Optkl.Data
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

    }
}
