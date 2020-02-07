using System;
using System.Net;
using System.Net.Sockets;  
using System.Text;  
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FlyBy
{
class DroneServer {

    public static Dictionary<string, Drone> droneList;
    
    static void Main(string[] args)
    {
        droneList = new Dictionary<string, Drone>();
        MainAsync(args).Wait();
        //  StartServer.Wait();  
    }

    private static async Task MainAsync(string[] args) {
        //StartServer();
        IPHostEntry host = Dns.GetHostEntry("localhost");  
        IPAddress ipAddress = host.AddressList[0];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);    
        
        try {   
  
            // Create a Socket that will use Tcp protocol      
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  
            listener.Bind(localEndPoint);  
            listener.Listen(10);  
  
            Console.WriteLine("Waiting for a connection...");  
            Socket handler = listener.Accept();  
  
             // Incoming data from the client.    
            string data = null;  
            byte[] bytes = null;  
            Console.WriteLine("Connected " + handler.Connected);

            while (handler.Connected)  
            {  
                bytes = new byte[1024];  
                int bytesRec = handler.Receive(bytes);  
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                int eofIndex = data.IndexOf("<EOF>");  
                if (eofIndex > -1)  
                {  
                    data = data.Substring(0,eofIndex);
                    Console.WriteLine(data);
                    DroneCommand droneCommand = JsonSerializer.Deserialize<DroneCommand>(data);
                    String droneName = droneCommand.droneName;
                    Command command = droneCommand.command;
                    if(droneList.ContainsKey(droneName) && !(command is System.DBNull)) {
                        Drone drone =  droneList[droneName];
                        ExecuteCommand(drone,command);
                    }
                    else {
                        Drone drone = null;
                        switch(droneCommand.droneType) {
                            case DroneType.Parrot:
                                drone = new ParrotDrone();
                                break;
                        }
                        if(droneList.ContainsKey(droneName)) droneList.Remove(droneName);
                        droneList.Add(droneName,drone);
                    }
                    
                     
                    data="";
                }
            }              
            Console.WriteLine("Disconnect");
            handler.Shutdown(SocketShutdown.Both);  
            handler.Close();  
        }  
        catch (Exception e)  
        {  
            Console.WriteLine(e.ToString());  
        } 
    }

    public static async void ExecuteCommand(Drone drone,Command command) {
        switch(command.commandName) {
            case CommandName.Connect:
                await drone.Connect(command.commandData);
                break;
            case CommandName.Takeoff:
                await drone.Takeoff();
                break;
            case CommandName.Land:
                await drone.Land();
                break;
        }
    }
    
}
}       
