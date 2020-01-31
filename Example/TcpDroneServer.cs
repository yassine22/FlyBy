using System;
using System.Net;
using System.Net.Sockets;  
using System.Text;  
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlyBy
{
class TcpDroneServer {
    
    static void Main(string[] args)
    {
        MainAsync(args).Wait();
        //  StartServer.Wait();  
    }

    private static async Task MainAsync(string[] args) {
        //StartServer();
        IPHostEntry host = Dns.GetHostEntry("localhost");  
        IPAddress ipAddress = host.AddressList[0];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);    
        Drone drone = new ParrotDrone();
  
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
                    if(droneCommand.commandName.Equals("connect")) {
                        Console.WriteLine(droneCommand.commandName + " " + droneCommand.commandData);
                        await drone.Connect(droneCommand.commandData);
                    } 

                    if(droneCommand.commandName.Equals("takeoff")) {
                        Console.WriteLine(droneCommand.commandName);
                        await drone.Takeoff();
                    } 

                    if(droneCommand.commandName.Equals("land")) {
                        Console.WriteLine(droneCommand.commandName);
                        await drone.Land();
                    } 
                    data="";
                }
            }              
            Console.WriteLine("Connected " + handler.Connected);
            await Task.Delay(2000);
            drone.Disconnect();
            //byte[] msg = Encoding.ASCII.GetBytes(data);  
            //handler.Send(msg);  
            handler.Shutdown(SocketShutdown.Both);  
            handler.Close();  
        }  
        catch (Exception e)  
        {  
            Console.WriteLine(e.ToString());  
        } 
    }

    public static async Task StartServer() {
        IPHostEntry host = Dns.GetHostEntry("localhost");  
        IPAddress ipAddress = host.AddressList[0];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);    
        Drone drone = new ParrotDrone();
  
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
            await Task.Delay(2000);
            while (handler.Connected)  
            {  
                Console.WriteLine("TEST");
                bytes = new byte[1024];  
                int bytesRec = handler.Receive(bytes);  
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                int eofIndex = data.IndexOf("<EOF>");  
                if (eofIndex > -1)  
                {  
                    data = data.Substring(0,eofIndex);
                    DroneCommand droneCommand = JsonSerializer.Deserialize<DroneCommand>(data);
                    if(droneCommand.commandName.Equals("connect")) {
                        Console.WriteLine(droneCommand.commandName + " " + droneCommand.commandData);
                        await drone.Connect(droneCommand.commandData);
                    } 

                    if(droneCommand.commandName.Equals("takeoff")) {
                        Console.WriteLine(droneCommand.commandName);
                        await drone.Takeoff();
                    } 

                    if(droneCommand.commandName.Equals("land")) {
                        Console.WriteLine(droneCommand.commandName);
                        await drone.Land();
                    } 
                }
            }              

            //byte[] msg = Encoding.ASCII.GetBytes(data);  
            //handler.Send(msg);  
            handler.Shutdown(SocketShutdown.Both);  
            handler.Close();  
        }  
        catch (Exception e)  
        {  
            Console.WriteLine(e.ToString());  
        }  
  
    }

     public static async Task StartServer2() {
        IPHostEntry host = Dns.GetHostEntry("localhost");  
        IPAddress ipAddress = host.AddressList[0]; 
        TcpListener server = new TcpListener(ipAddress, 11000);  
        server.Start();

        while (true)   //we wait for a connection
        {
            TcpClient client = server.AcceptTcpClient();  
            NetworkStream ns = client.GetStream();


            while (client.Connected)  //while the client is connected, we look for incoming messages
            {
                byte[] msg = new byte[1024];     
                ns.Read(msg, 0, msg.Length);
                String data = Encoding.ASCII.GetString(msg, 0, msg.Length);
                DroneCommand droneCommand = JsonSerializer.Deserialize<DroneCommand>(data);
                if(droneCommand.commandName.Equals("connect")) {
                    Console.WriteLine(droneCommand.commandName + " " + droneCommand.commandData);
                    Drone drone = new ParrotDrone();
                    await drone.Connect(droneCommand.commandData);
                    //Task.Run(() => {
                    //    drone.Connect(droneCommand.commandData);
                    //}).Wait();
                }
            }
            

        }
    }
    
}
}       
