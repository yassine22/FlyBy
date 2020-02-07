using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommandName
{    
    Connect,
    Takeoff,
    Land
}

[System.Serializable]
public class Command
{
    public CommandName commandName;
    public string commandData;

    public Command(CommandName name, string data)
    {
        commandName = name;
        commandData = data;
    }

    public Command() 
    {

    }
}