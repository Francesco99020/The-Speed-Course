using System;

[Serializable]
public class InputEntry
{
    public string playerName;
    public string playerVehicle;
    public float playerTime;

    public InputEntry(string name, string vehicle, float Time)
    {
        playerName = name;
        playerVehicle = vehicle;
        playerTime = Time;
    } 

}