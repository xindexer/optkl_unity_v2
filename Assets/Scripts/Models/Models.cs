using UnityEngine;

public class JsonData
{
    public string data;
}

public class JsonList
{
    public float[][] symbolData;
}

public class OptionDataParameters
{

    public OptionDataParameters() { }

    public OptionDataParameters(int ind, string nm, int pr)
    {
        index = ind;
        name = nm;
        pair = pr;
    }


    public int index { get; set; }
    public string name { get; set; }
    public int pair { get; set; }
}

public class Label
{
    public string LabelType;
    public string Text;
    public Vector3 Location;
    public Quaternion Rotation;

    public Label(string labelType, string text, Vector3 location, Quaternion rotation)
    {
        LabelType = labelType;
        Text = text;
        Location = location;
        Rotation = rotation;
    }
}