using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;  
using System.Net.Sockets;  
using System.Text; 
using System.Text.Json;
using System.Text.Json.Serialization; 

namespace Test
{
    class Program
    {
        static Socket sender;
        static void Main(string[] args)
        {
             MainAsync(args).Wait();
            
        }

        private static async Task MainAsync(string[] args) 
        {
            StartClient();         
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                
                if(keyinfo.Key == ConsoleKey.C) {
                    DroneCommand droneCommand = new DroneCommand("connect","D0:3A:97:3D:E6:23");
                    String s = JsonSerializer.Serialize<DroneCommand>(droneCommand);
                    Console.WriteLine(s);
                    SendMessage(s);
                    Console.WriteLine("c was pressed");         
                }
                
                if (keyinfo.Key == ConsoleKey.T) { 
                    DroneCommand droneCommand = new DroneCommand("takeoff","");
                    String s = JsonSerializer.Serialize<DroneCommand>(droneCommand);
                    Console.WriteLine(s);
                    SendMessage(s);
                    Console.WriteLine("space key was pressed");
                }

                if (keyinfo.Key == ConsoleKey.L) { 
                    DroneCommand droneCommand = new DroneCommand("land","");
                    String s = JsonSerializer.Serialize<DroneCommand>(droneCommand);
                    Console.WriteLine(s);
                    SendMessage(s);
                    Console.WriteLine("space key was pressed");
                }
            }
            while (keyinfo.Key != ConsoleKey.X);
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
                    Console.WriteLine("CONNECTED: " + sender.RemoteEndPoint.ToString());                  
                }  
                catch (ArgumentNullException ane)  
                {  
                    Console.WriteLine("ArgumentNullException : " + ane.ToString());  
                }  
                catch (SocketException se)  
                {  
                    Console.WriteLine("SocketException : " + se.ToString());  
                }  
                catch (Exception e)  
                {  
                    Console.WriteLine("Unexpected exception : " + e.ToString());  
                }  
    
            }  
            catch (Exception e)  
            {  
                Console.WriteLine(e.ToString());  
            }  
        }  
    
        static void SendMessage(String msg) {
            Console.WriteLine("SEND: " + msg);
            // Encode the data string into a byte array.   
            byte[] data = Encoding.ASCII.GetBytes(msg+"<EOF>");  
            // Send the data through the socket.    
            Console.WriteLine(msg);
            try {
                int bytesSent = sender.Send(data);  
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message + " Error code:" + e.ErrorCode);
            }   
            
        }
        static async Task ReceiveMessage() {
            byte[] bytes = new byte[1024];
            int numOfBytes = 0;
            try {
                numOfBytes = sender.Receive(bytes); 
            } 
            catch (SocketException e)
            {
                Console.WriteLine(e.Message + " Error code:" + e.ErrorCode);
            }   
            
            
            Console.WriteLine("RECEIVED: " + Encoding.ASCII.GetString(bytes, 0, numOfBytes));
            //return Encoding.ASCII.GetString(bytes,0,numOfBytes);
        }

        static void StopClient() 
        {
            sender.Shutdown(SocketShutdown.Both);  
            sender.Close();  
        }

    }
}
