using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum DroneType
{    
    Parrot
}
[System.Serializable]
public class DroneCommand
{
    public string droneName;
    public DroneType droneType;
    
    public Command command; 
    public DroneCommand(string name, DroneType type) 
    {
        droneName = name;
        droneType = type;
    }

    public DroneCommand(string name, Command cmd) 
    {
        droneName = name;
        command = cmd;
    }

    public DroneCommand()
    {
        
    }
}
