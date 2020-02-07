using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;  
using System.Net.Sockets;  
using System.Text;  



public class DroneClient : MonoBehaviour
{
    //static Process process;
    static Socket sender;
    
     void Awake()
    {
        StartServer();
    }

    public static void StartServer() 
    {
        /*
        process = new Process();
        process.StartInfo.FileName = "C:\\Users\\Yassine\\Desktop\\C#\\FlyBy\\Example\\bin\\Debug\\netcoreapp3.0\\Example.exe";
        process.Start();
        DroneCommand droneCommand = new DroneCommand("connect","D0:3A:97:3D:E6:23");
        SendMessage(JsonUtility.ToJson(droneCommand));
        ReceiveMessage();
        */
        
        StartClient();
    }

    public static void StartClient()  
    {  
        byte[] bytes = new byte[1024];  
  
        try  
        {  
            IPHostEntry host = Dns.GetHostEntry("localhost");  
            IPAddress ipAddress = host.AddressList[0];  
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);  
  
            // Create a TCP/IP  socket.    
            sender = new Socket(ipAddress.AddressFamily,  
                SocketType.Stream, ProtocolType.Tcp);  
  
            // Connect the socket to the remote endpoint. Catch any errors.    
            try  
            {  
                // Connect to Remote EndPoint  
                sender.Connect(remoteEP);  
                UnityEngine.Debug.Log("CONNECTED: " + sender.RemoteEndPoint.ToString());                  
            }  
            catch (ArgumentNullException ane)  
            {  
                UnityEngine.Debug.Log("ArgumentNullException : " + ane.ToString());  
            }  
            catch (SocketException se)  
            {  
                UnityEngine.Debug.Log("SocketException : " + se.ToString());  
            }  
            catch (Exception e)  
            {  
                UnityEngine.Debug.Log("Unexpected exception : " + e.ToString());  
            }  
  
        }  
        catch (Exception e)  
        {  
            UnityEngine.Debug.Log(e.ToString());  
        }  
    }  
    
    public static void SendMessage(String msg) {
        UnityEngine.Debug.Log("SEND: " + msg);
        // Encode the data string into a byte array.   
        byte[] data = Encoding.ASCII.GetBytes(msg+"<EOF>");  
        // Send the data through the socket.    
        try {
            int bytesSent = sender.Send(data);  
        }
        catch (SocketException e)
        {
            UnityEngine.Debug.Log(e.Message + " Error code:" + e.ErrorCode);
        }   
          
    }
    public static void ReceiveMessage() {
        byte[] bytes = new byte[1024];
        int numOfBytes = 0;
        try {
            numOfBytes = sender.Receive(bytes); 
        } 
        catch (SocketException e)
        {
            UnityEngine.Debug.Log(e.Message + " Error code:" + e.ErrorCode);
        }   
        
        UnityEngine.Debug.Log("RECEIVED: " + Encoding.ASCII.GetString(bytes, 0, numOfBytes));
        //return Encoding.ASCII.GetString(bytes,0,numOfBytes);
    }

    public static bool isConnected()
    {
        return sender.Connected;
    }
    public static void StopClient() 
    {
        sender.Shutdown(SocketShutdown.Both);  
        sender.Close();  
    }

    void OnApplicationQuit() {
        StopClient();
    }

}
