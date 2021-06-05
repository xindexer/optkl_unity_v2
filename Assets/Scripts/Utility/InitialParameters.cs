
using System.Collections.Generic;

namespace Optkl.Utilities {

    public class InitialParameters
    {
        public readonly Dictionary<string, OptionDataParameters> parameterPosition = new Dictionary<string, OptionDataParameters>(38) {
            { "id", new OptionDataParameters(0, "id", 0) },
            { "ticker", new OptionDataParameters(1, "ticker", 0) },
            { "stkPx", new OptionDataParameters(2, "stkPx", 0) },
            { "expireDate", new OptionDataParameters(3, "expireDate", 0) },
            { "yte", new OptionDataParameters(4, "yte", 9) },
            { "strike", new OptionDataParameters(5, "strike", 0) },
            { "cVolu", new OptionDataParameters(6, "Volu", 1) },
            { "cOi", new OptionDataParameters(7, "Oi", 2) },
            { "pVolu", new OptionDataParameters(8, "Volu", 1) },
            { "pOi", new OptionDataParameters(9, "Oi", 2) },
            { "cBidPx", new OptionDataParameters(10, "BidPx", 3) },
            { "cValue", new OptionDataParameters(11, "Value", 4) },
            { "cAskPx", new OptionDataParameters(12, "AskPx", 5) },
            { "pBidPx", new OptionDataParameters(13, "BidPx", 3) },
            { "pValue", new OptionDataParameters(14, "Value", 4) },
            { "pAskPx", new OptionDataParameters(15, "AskPx", 5) },
            { "cBidIv", new OptionDataParameters(16, "BidIv", 6) },
            { "cMidIv", new OptionDataParameters(17, "MidIv", 7) },
            { "cAskIv", new OptionDataParameters(18, "AskIv", 8) },
            { "smoothSmvVol", new OptionDataParameters(19, "smoothSmvVol", 10) },
            { "pBidIv", new OptionDataParameters(20, "BidIv", 6) },
            { "pMidIv", new OptionDataParameters(21, "MidIv", 7) },
            { "pAskIv", new OptionDataParameters(22, "AksIv", 8) },
            { "iRate", new OptionDataParameters(23, "iRate", 11) },
            { "divRate", new OptionDataParameters(24, "divRate", 12) },
            { "residualRateData", new OptionDataParameters(25, "residualRateData", 13) },
            { "delta", new OptionDataParameters(26, "delta", 14) },
            { "gamma", new OptionDataParameters(27, "gamma", 15) },
            { "theta", new OptionDataParameters(28, "theta", 16) },
            { "vega", new OptionDataParameters(29, "vega", 17) },
            { "rho", new OptionDataParameters(30, "rho", 18) },
            { "phi", new OptionDataParameters(31, "phi", 19) },
            { "driftlessTheta", new OptionDataParameters(32, "driftlessTheta", 20) },
            { "extVol", new OptionDataParameters(33, "extVol", 21) },
            { "extCTheo", new OptionDataParameters(34, "extCTheo", 22) },
            { "extPtheo", new OptionDataParameters(35, "extPtheo", 23) },
            { "spot_px", new OptionDataParameters(36, "spot_px", 24) },
            { "trade_date", new OptionDataParameters(37, "trade_date", 0) }
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
