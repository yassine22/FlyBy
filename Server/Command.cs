using System;

public enum CommandName
{    
    Connect,
    Takeoff,
    Land
}
public class Command
{
    public CommandName commandName { get; set; }
    public string commandData { get; set; }
    public Command(CommandName name, string data)
    {
        commandName=name;
        commandData=data;
    }
    public Command()
    {

    }
    
}