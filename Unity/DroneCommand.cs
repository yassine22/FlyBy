using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DroneCommand
{
    public string commandName;
    public string commandData;

    public DroneCommand(string name, string data) 
    {
        commandName = name;
        commandData = data;
    }
}
