using System;
using System.Threading.Tasks;
namespace FlyBy
{
    class Program
    {
        static void Main(string[] args)
        {
             MainAsync(args).Wait();
            
        }

        private static async Task MainAsync(string[] args) {

            Drone drone = new ParrotDrone();
            await drone.Connect("D0:3A:97:3D:E6:23");
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                if (keyinfo.Key == ConsoleKey.T) { 
                    await drone.Takeoff();
                }

                if (keyinfo.Key == ConsoleKey.L) { 
                    await drone.Land();
                }

                if (keyinfo.Key == ConsoleKey.W) { 
                    await drone.Up(20);
                }

                if (keyinfo.Key == ConsoleKey.S) { 
                    await drone.Down(20);
                }

                if (keyinfo.Key == ConsoleKey.A) { 
                    await drone.Yaw(20);
                }

                if (keyinfo.Key == ConsoleKey.D) { 
                    await drone.Yaw(0xFF-20);
                }

                if (keyinfo.Key == ConsoleKey.UpArrow) { 
                    await drone.Pitch(20);
                }

                if (keyinfo.Key == ConsoleKey.DownArrow) { 
                    await drone.Pitch(0xFF-20);
                }

                if (keyinfo.Key == ConsoleKey.LeftArrow) { 
                    await drone.Roll(20);
                }

                if (keyinfo.Key == ConsoleKey.RightArrow) { 
                    await drone.Roll(0xFF-20);
                }
            }
            while (keyinfo.Key != ConsoleKey.X);
            drone.Disconnect();

        }
    }
}
