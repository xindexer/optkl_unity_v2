
public class JsonData
{
    public string data;
}

public class JsonList
{
    public object[][] symbolData;
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