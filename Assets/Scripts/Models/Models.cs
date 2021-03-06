using System;
using System.Collections.Generic;
using UnityEngine;
using Optkl.Data;


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
    public string Text;
    public Vector3 Location;
    public Quaternion Rotation;

    public Label(string text, Vector3 location, Quaternion rotation)
    {
        Text = text;
        Location = location;
        Rotation = rotation;
    }
}

[Serializable]
public class TrackOrder
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public Boolean Active;

    public TrackOrder(string name, Boolean active)
    {
        Name = name;
        Active = active;
    }
}

[Serializable]
public class TrackControl
{
    [SerializeField]
    public Boolean Active;

    [SerializeField]
    [Range(0f, 5f)]
    public float HeightMultiplier;

    public TrackControl(Boolean active, float heightMultiplier)
    {
        Active = active;
        HeightMultiplier = heightMultiplier;
    }
}

[Serializable]
public class OptklTexturesAndMaterials
{
    [SerializeField]
    public Texture Texture;

    [SerializeField]
    public Material Material;

    public OptklTexturesAndMaterials(Texture texture, Material material)
    {
        Texture = texture;
        Material = material;
    }
}

public class TrackDataVectorList
{
    public List<Vector3> TrackDataList;
    public List<Color32> TrackColorsList;
    public string CallName;
    public string PutName;

    public TrackDataVectorList(List<Vector3> trackDataList, List<Color32> trackColorsList, string callName, string putName)
    {
        TrackDataList = trackDataList;
        TrackColorsList = trackColorsList;
        CallName = callName;
        PutName = putName;
    }
}

public class CircumferenceParameterData 
{
    public float TrackCircumference;
    public float PieSpace;
    public CircumferenceParameterData(float trackCirc, float pspace) {
        TrackCircumference = trackCirc;
        PieSpace = pspace;
    }
}

public class CallPutPair
{
    public string CallName;
    public string PutName;

    public CallPutPair(string inCallName, string inPutName)
    {
        CallName = inCallName;
        PutName = inPutName;
    }
}
