using System;
using System.Threading.Tasks;
namespace FlyBy
{
    class Program
    {
        /*
        static void Main(string[] args)
        {
             MainAsync(args).Wait();
            
        }

        private static async Task MainAsync(string[] args) {

            //TcpDroneServer tcpDroneServer = new TcpDroneServer();
            //tcpDroneServer.StartServer();
            Drone drone = new ParrotDrone();
            
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                
                if(keyinfo.Key == ConsoleKey.C) {
                    Console.WriteLine("CONNECT");
                    await drone.Connect("D0:3A:97:3D:E6:23");        
                }
                
                if (keyinfo.Key == ConsoleKey.T) { 
                    Console.WriteLine("TAKEOFF");
                    await drone.Takeoff();
                }

                if (keyinfo.Key == ConsoleKey.L) { 
                    Console.WriteLine("LAND");
                    await drone.Land();
                }

                if (keyinfo.Key == ConsoleKey.W) { 
                    Console.WriteLine("UP");
                    await drone.Up(20);
                }

                if (keyinfo.Key == ConsoleKey.S) { 
                    Console.WriteLine("DOWN");
                    await drone.Down(20);
                }

                if (keyinfo.Key == ConsoleKey.A) { 
                    Console.WriteLine("YAW LEFT");
                    await drone.Yaw(20);
                }

                if (keyinfo.Key == ConsoleKey.D) {
                    Console.WriteLine("YAW RIGHT"); 
                    await drone.Yaw(0xFF-20);
                }

                if (keyinfo.Key == ConsoleKey.UpArrow) { 
                    Console.WriteLine("PITCH FORWARDS");
                    await drone.Pitch(20);
                }

                if (keyinfo.Key == ConsoleKey.DownArrow) { 
                    Console.WriteLine("PITCH BACKWARDS");
                    await drone.Pitch(0xFF-20);
                }

                if (keyinfo.Key == ConsoleKey.LeftArrow) {
                    Console.WriteLine("ROLL LEFT"); 
                    await drone.Roll(20);
                }

                if (keyinfo.Key == ConsoleKey.RightArrow) { 
                    Console.WriteLine("ROLL RIGHT");
                    await drone.Roll(0xFF-20);
                }
            }
            while (keyinfo.Key != ConsoleKey.X);
            drone.Disconnect();

        }
    */

    } 
}
