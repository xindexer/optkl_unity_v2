using UnityEngine;
using UnityEngine.UI;
using Optkl;
using Optkl.Data;
using Optkl.Load;
using System.Collections;

public class Opening : MonoBehaviour
{

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
            data.Symbol = "GME";
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
        // optklManager.InitialLoad(data);
    }
}
