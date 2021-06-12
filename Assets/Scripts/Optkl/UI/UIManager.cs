using UnityEngine;
using UnityEngine.UI;
using Optkl;
using Optkl.Data;
using Optkl.Load;
using System.Collections;
using System;


namespace Optkl.UI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] private InputField symbol;

        [SerializeField] private Dropdown month;

        [SerializeField] private Dropdown day;

        [SerializeField] private Dropdown year;

        [SerializeField] private InputField lookback;

        [SerializeField] public OptklManager optklManager;

        public InputOptionData data = new InputOptionData();

        private void Awake() 
        {
            symbol.text = "AAPL";
            month.value = 5;
            day.value = 7;
            year.value = 0;
        }

        [SerializeField]
        public void OnSubmit()
        {
            data.Month =  month.options[month.value].text;
            data.Day = day.options[day.value].text;
            data.Year = year.options[year.value].text;
            data.TradeDate = data.GetTradeDate();

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
            optklManager.Load(data);
        }
    }
}