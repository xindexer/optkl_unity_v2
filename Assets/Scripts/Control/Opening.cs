using UnityEngine;
using UnityEngine.UI;
using Paroxe.SuperCalendar;
using Optkl;
using Optkl.Data;
using System.Collections;

public class Opening : MonoBehaviour
{

    [SerializeField]
    private Calendar calendar;

    [SerializeField]
    private InputField symbol;

    [SerializeField]
    private InputField lookback;

    [SerializeField]
    private Canvas opening;

    [SerializeField]
    private Canvas loading;

    [SerializeField]
    public OptklManager optklManager;

    public InputOptionData data = new InputOptionData();

    public void OnCalendarChange()
    {
        data.TradeDate = calendar.DateTime;
    }

    private void Start()
    {
        symbol.Select();
        symbol.ActivateInputField();
    }

    [SerializeField]
    public void OnSubmit()
    {
        if (symbol.text == "")
        {
            data.Symbol = "AAPL";
        }
        else
        {
            data.Symbol = symbol.text;
        }
        if (lookback.text == "")
        {
            data.Lookback = 30;
        }
        else
        {
            data.Lookback = int.Parse(lookback.text); //verify int only
        }
        optklManager.InitialLoad(data);
    }
}
