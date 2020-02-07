using System.Collections;
using System.Text; 
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    string droneName;
    DroneType droneType;


    public Drone(string name, DroneType type) 
    {
        droneName = name;
        droneType = type;
    }

    public void CreateDrone() {
        if(DroneClient.isConnected()) {
            UnityEngine.Debug.Log(droneName + " " + droneType.ToString());
            DroneCommand droneCommand = new DroneCommand(droneName,droneType);
            string msg = JsonUtility.ToJson(droneCommand);
            DroneClient.SendMessage(msg);
        }
    }

    public void Connect(string addr) {
        if(DroneClient.isConnected()) {   
            Command command = new Command(CommandName.Connect,addr);
            //Command command = new Command(){ commandName=CommandName.Connect, commandData=addr};
            DroneCommand droneCommand = new DroneCommand(droneName,command);
            string msg = JsonUtility.ToJson(droneCommand);
            DroneClient.SendMessage(msg);
        }
    }

    public void Takeoff() {
        if(DroneClient.isConnected()) {   
            Command command = new Command(CommandName.Takeoff,"");
            DroneCommand droneCommand = new DroneCommand(droneName,command);
            string msg = JsonUtility.ToJson(droneCommand);
            DroneClient.SendMessage(msg);
            // JsonUtility.ToJson(droneCommand)
        }
    }

        public void Land() {
        if(DroneClient.isConnected()) {   
            Command command = new Command(CommandName.Land,"");
            DroneCommand droneCommand = new DroneCommand(droneName,command);
            string msg = JsonUtility.ToJson(droneCommand);
            DroneClient.SendMessage(msg);
            // JsonUtility.ToJson(droneCommand)
        }
    }
}
