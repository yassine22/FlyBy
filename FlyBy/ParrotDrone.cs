using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FlyBy
{
    public class ParrotDrone : Drone
    {
        private byte cmdIndex;
        private byte ackIndex;
        
        
        public ParrotDrone() {
            cmdIndex = 0;
            ackIndex = 0;
            connector = new BLEConnector();
        }

        private async Task EnableNotification() {
            Console.WriteLine("ENABLE NOTIF");
            await ((BLEConnector) connector).SetNotification("9A66FB0E-0800-9191-11E4-012D1540CB8E",OnNotficationRecCmdAck);
            await ((BLEConnector) connector).SetNotification("9A66FB0F-0800-9191-11E4-012D1540CB8E",OnNotficationRecCmd);
            await ((BLEConnector) connector).SetNotification("9A66FB1B-0800-9191-11E4-012D1540CB8E",OnNotficationAck1);
            await ((BLEConnector) connector).SetNotification("9A66FB1C-0800-9191-11E4-012D1540CB8E",OnNotficationAck2);
        }

        private async Task sendAck(byte packetId) {
            ackIndex++;
            await ((BLEConnector) connector).WriteWithoutAckData("9A66FA1E-0800-9191-11E4-012D1540CB8E",new byte[] {1,ackIndex,packetId});
        }

        private void OnNotficationRecCmdAck(byte[] data) {
            //sConsole.WriteLine("RECEIVED COMMANDS WITH ACK");
            Console.WriteLine("READ  " + BitConverter.ToString(data));
            byte packetId = data[1];
            Task.Run(async () => {await sendAck(packetId);});
        }

        private void OnNotficationRecCmd(byte[] data) {
            Console.WriteLine("RECEIVED COMMANDS WITHOUT ACK");
            Console.WriteLine("DATA:  " + BitConverter.ToString(data));
        }

        private void OnNotficationAck1(byte[] data) {
            String ackIdString = BitConverter.ToString(data);
            Console.WriteLine("NOTIFICATION - ACK ID:  " + ackIdString );
            
        }
        private void OnNotficationAck2(byte[] data) {
            Console.WriteLine("1C");
        }

        public override async Task Connect(String addr) {
            await ((BLEConnector) connector).Connect(addr);
            await EnableNotification();
        }
        public override void Disconnect() {
            ((BLEConnector) connector).Disconnect();
        }

        private async Task WriteWithoutAckData(byte[] data) {
            await ((BLEConnector) connector).WriteWithoutAckData("9A66FA0B-0800-9191-11E4-012D1540CB8E",data);
            await Task.Delay(500);
            cmdIndex++;
        }
        public override async Task Takeoff() {
            Console.WriteLine("Takeoff "  + cmdIndex);
            await this.WriteWithoutAckData(new byte[]{04,cmdIndex,02,00,01,00});
        }
        public override async Task Land() {
            Console.WriteLine("Land " + cmdIndex.ToString());
            await this.WriteWithoutAckData(new byte[]{04,cmdIndex,02,00,03,00});
        }

        private async Task PCMD(byte roll, byte pitch, byte yaw, byte gaz) {
            byte[] data = new byte[]{02,cmdIndex,02,00,02,00,01,roll,pitch,yaw,gaz,00,00,00,00};
            await this.WriteWithoutAckData(data);
        }
        public override async Task Up(int value) {
            Console.WriteLine("Up " + value.ToString() + " " + cmdIndex.ToString());
            await PCMD(0,0,0,Convert.ToByte(value & 0xff));
        }

        public override async Task Down(int value) {
            Console.WriteLine("Up " + value.ToString() + " " + cmdIndex.ToString());
            await PCMD(0,0,0,Convert.ToByte(0xff-(value & 0xff)));
        }

        public override async Task Yaw(int value) {
            Console.WriteLine("Up " + value.ToString() + " " + cmdIndex.ToString());
            await PCMD(0,0,Convert.ToByte(value & 0xff),0);
        }

        public override async Task Pitch(int value) {
            Console.WriteLine("Up " + value.ToString() + " " + cmdIndex.ToString());
            await PCMD(0,Convert.ToByte(value & 0xff),0,0);
        }

        public override async Task Roll(int value) {
            Console.WriteLine("Up " + value.ToString() + " " + cmdIndex.ToString());
            await PCMD(Convert.ToByte(value & 0xff),0,0,0);
        }


    }
}
