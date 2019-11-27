using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MbientLab.Warble;

namespace FlyBy
{
    //namespace MbientLab.Warble {
        public class BLEConnector : Connector
        {
            private Gatt gatt;
            private const int numRetryConnect = 5;

            public BLEConnector() {

            }
            public override async Task Connect(String addr) {
                gatt = new Gatt(addr);

                for (int i=0; i < numRetryConnect && !gatt.IsConnected; i++) {
                    await gatt.ConnectAsync();
                    await Task.Delay(100);
                }
                Console.WriteLine("Am I Connected ? " + gatt.IsConnected);
            }

            public override void Disconnect() {
                gatt.Disconnect();
                Console.WriteLine("Am I Connected ? " + gatt.IsConnected);
            }

             public async Task WriteData(String uuid, byte[] data) {
                GattChar gc = gatt.FindCharacteristic(uuid);
                await gc.WriteAsync(data);
            }

            public async Task WriteWithoutAckData(String uuid, byte[] data) {
                Console.WriteLine("WRITE " + BitConverter.ToString(data));
                GattChar gc = gatt.FindCharacteristic(uuid);
                if (gc != null) {
                    await gc.WriteWithoutResponseAsync(data);
                }
                else {
                    Console.WriteLine("GC IS NULL");
                }
            }
            
            public async Task<byte[]> ReadData(String uuid) {
                GattChar gc = gatt.FindCharacteristic(uuid);
                byte[] data = (await gc.ReadAsync());
                return data;
            }

            public async Task SetNotification(String uuid, Action<byte[]> OnNotificationReceived) {
                Console.WriteLine("BLECONNECTOR SETNOTIF");
                GattChar gc = gatt.FindCharacteristic(uuid);
                gc.OnNotificationReceived = OnNotificationReceived;
                await gc.EnableNotificationsAsync();    
            }
        }
}
