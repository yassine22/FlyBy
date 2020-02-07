using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    Drone drone;

    void Start()
    {
        drone = new Drone("test",DroneType.Parrot);
        //StartServer();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            UnityEngine.Debug.Log("CONNECT");
            drone.CreateDrone();
            drone.Connect("D0:3A:97:3D:E6:23");
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            UnityEngine.Debug.Log("TAKEOFF");
            drone.Takeoff();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.Debug.Log("LAND");
            drone.Land();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            UnityEngine.Debug.Log("STOP");
            DroneClient.StopClient();
        }
  
    }
}
