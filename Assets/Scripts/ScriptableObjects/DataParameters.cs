using UnityEngine;

[CreateAssetMenu(menuName = "Optkl/Data Parameters")]
public class DataParameters : ScriptableObject
{
    [SerializeField]
    private string tradeDate;
    public string TradeDate
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

    [SerializeField]
    [Range(0, 250)]
    private int trackSpacer = 100;
    public int TrackSpacer
    {
        get
        {
            return trackSpacer;
        }
        set
        {
            trackSpacer = value;
        }
    }

    [SerializeField]
    [Range(50, 1000)]
    private int trackThickness = 50;
    public int TrackThickness
    {
        get
        {
            return trackThickness;
        }
        set
        {
            trackThickness = value;
        }
    }

    [SerializeField]
    [Range(2000, 6000)]
    private int trackRadius = 2000;
    public int TrackRadius
    {
        get
        {
            return trackRadius;
        }
        set
        {
            trackRadius = value;
        }
    }

    [SerializeField]
    [Range(0, 500)]
    private int tickHeight = 30;
    public int TickHeight
    {
        get
        {
            return tickHeight;
        }
        set
        {
            tickHeight = value;
        }
    }

    [SerializeField]
    [Range(0, 10)]
    private int tickWidth = 1;
    public int TickWidth
    {
        get
        {
            return tickWidth;
        }
        set
        {
            tickWidth = value;
        }
    }

    [SerializeField]
    [Range(6000, 14000)]
    private int tickRadius = 10000;
    public int TickRadius
    {
        get
        {
            return tickRadius;
        }
        set
        {
            tickRadius = value;
        }
    }

    [SerializeField]
    [Range(10000, 16000)]
    private int labelRadius = 11000;
    public int LabelRadius
    {
        get
        {
            return labelRadius;
        }
        set
        {
            labelRadius = value;
        }
    }

    [SerializeField]
    [Range(1000, 5000)]
    private int scatterRadius = 1000;
    public int ScatterRadius
    {
        get
        {
            return scatterRadius;
        }
        set
        {
            scatterRadius = value;
        }
    }

    [SerializeField]
    [Range(0f, 10f)]
    private float pieSpacer = 3f;
    public float PieSpacer
    {
        get
        {
            return pieSpacer;
        }
        set
        {
            pieSpacer = value;
        }
    }

    [SerializeField]
    [Range(0f, 20f)]
    private float powerWedge = 10f;
    public float PowerWedge
    {
        get
        {
            return powerWedge;
        }
        set
        {
            powerWedge = value;
        }
    }

}
