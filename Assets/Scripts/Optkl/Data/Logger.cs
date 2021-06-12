using UnityEngine;

[CreateAssetMenu]
public class Logger : ScriptableObject
{
    private float startTime;
    private float timerTime;

    public void StartTimer()
    {
        startTime = Time.realtimeSinceStartup;
    }

    public void EndTimer(string name)
    {
        timerTime = Time.realtimeSinceStartup - startTime;
        Debug.Log($"{name}: {timerTime}");
    }

    public void Log(string log)
    {
        Debug.Log(log);
    }
}
