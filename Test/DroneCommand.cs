using System;
public class DroneCommand {

    public DroneCommand(string name, string data)
    {
        commandName = name;
        commandData = data;
    }
    public string commandName { get; set; }
    public string commandData { get; set; }
}