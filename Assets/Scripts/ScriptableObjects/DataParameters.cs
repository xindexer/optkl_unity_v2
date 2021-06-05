using UnityEngine;

[CreateAssetMenu(menuName = "Optkl/Data Parameters")]
public class DataParameters : ScriptableObject
{
    [SerializeField]
    private int trackCircumference;
    public int TrackCircumference
    {
        get
        {
            return trackCircumference;
        }
        set
        {
            trackCircumference = value;
        }
    }

    [SerializeField]
    [Range(0, 250)]
    private int trackSpacer;
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
    [Range(0f, 10f)]
    private int pieSpacer;
    public int PieSpacer
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
    private int powerWedge;
    public int PowerWedge
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
}
