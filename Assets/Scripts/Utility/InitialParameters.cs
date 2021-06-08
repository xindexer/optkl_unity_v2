
using System.Collections.Generic;

namespace Optkl.Utilities {

    public class InitialParameters
    {
        public readonly Dictionary<string, OptionDataParameters> parameterPosition = new Dictionary<string, OptionDataParameters>(36) {
            { "stkPx", new OptionDataParameters(0, "stkPx", 0) },
            { "expireDate", new OptionDataParameters(1, "expireDate", 0) },
            { "yte", new OptionDataParameters(2, "yte", 9) },
            { "strike", new OptionDataParameters(3, "strike", 0) },
            { "cVolu", new OptionDataParameters(4, "Volu", 1) },
            { "cOi", new OptionDataParameters(5, "Oi", 2) },
            { "pVolu", new OptionDataParameters(6, "Volu", 1) },
            { "pOi", new OptionDataParameters(7, "Oi", 2) },
            { "cBidPx", new OptionDataParameters(8, "BidPx", 3) },
            { "cValue", new OptionDataParameters(9, "Value", 4) },
            { "cAskPx", new OptionDataParameters(10, "AskPx", 5) },
            { "pBidPx", new OptionDataParameters(11, "BidPx", 3) },
            { "pValue", new OptionDataParameters(12, "Value", 4) },
            { "pAskPx", new OptionDataParameters(13, "AskPx", 5) },
            { "cBidIv", new OptionDataParameters(14, "BidIv", 6) },
            { "cMidIv", new OptionDataParameters(15, "MidIv", 7) },
            { "cAskIv", new OptionDataParameters(16, "AskIv", 8) },
            { "smoothSmvVol", new OptionDataParameters(17, "smoothSmvVol", 10) },
            { "pBidIv", new OptionDataParameters(18, "BidIv", 6) },
            { "pMidIv", new OptionDataParameters(19, "MidIv", 7) },
            { "pAskIv", new OptionDataParameters(20, "AksIv", 8) },
            { "iRate", new OptionDataParameters(21, "iRate", 11) },
            { "divRate", new OptionDataParameters(22, "divRate", 12) },
            { "residualRateData", new OptionDataParameters(23, "residualRateData", 13) },
            { "delta", new OptionDataParameters(24, "delta", 14) },
            { "gamma", new OptionDataParameters(25, "gamma", 15) },
            { "theta", new OptionDataParameters(26, "theta", 16) },
            { "vega", new OptionDataParameters(27, "vega", 17) },
            { "rho", new OptionDataParameters(28, "rho", 18) },
            { "phi", new OptionDataParameters(29, "phi", 19) },
            { "driftlessTheta", new OptionDataParameters(30, "driftlessTheta", 20) },
            { "extVol", new OptionDataParameters(31, "extVol", 21) },
            { "extCTheo", new OptionDataParameters(32, "extTheo", 22) },
            { "extPTheo", new OptionDataParameters(33, "extTheo", 22) },
            { "spot_px", new OptionDataParameters(34, "spot_px", 23) },
            { "trade_date", new OptionDataParameters(35, "trade_date", 0) }
        };
            
        public OptionDataParameters this[string key]
        {
            get
            {
                return parameterPosition[key];
            }
            
        }
    }
}

// 0 - id', 1 - 'ticker ', 2 - 'stkPx ', 3 - 'expirDate ', 4 - 'yte ', 5 - 'strike '
// 6 - 'cVolu', 7 - 'cOi', 8 - 'pVolu', 9 - 'pOi', 10 - 'cBidPx',
// 11 - 'cValue', 12 - 'cAskPx', 13 - 'pBidPx', 14 - 'pValue', 15 - 'pAskPx',
// 16 - 'cBidIv', 17 - 'cMidIv', 18 - 'cAskIv', 19 - 'smoothSmvVol', 20 - 'pBidIv',
// 21 - 'pMidIv', 22 - 'pAskIv', 23 - 'iRate', 24 - 'divRate', 25 - 'residualRateData',
// 26 - 'delta', 27 - 'gamma', 28 - 'theta', 29 - 'vega', 30 - 'rho',
// 31 - 'phi', 32 - 'driftlessTheta', 33 - 'extVol', 34 - 'extCTheo', 35 - 'extPTheo',
// 36 - 'spot_px', 37 - 'trade_date'
