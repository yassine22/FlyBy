using System;
public enum DroneType
{    
    Parrot
}
public class DroneCommand {
    public string droneName { get; set; }
    public DroneType droneType { get; set; }
    public Command command { get; set; }

    public DroneCommand(string name, DroneType type) 
    {
        droneName=name;
        droneType=type;
    }

    public DroneCommand(string name, Command cmd) 
    {
        droneName=name;
        command=cmd;
    }

    public DroneCommand()
    {

    }
    
}
