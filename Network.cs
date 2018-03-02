using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
namespace Data_Server_A01 
{
    class Network
    {
        public bool Want_Data_once
        {
            get; set;
        }
        public Network(string ip, int port, Packing p)
        {
            //server.ClientConnected += Server_ClientConnected;
            Want_Data_once = true;
            pack = p;
            System.Net.IPAddress.TryParse(ip,out S_IP);
            server = new BKRServer.AsyncTcpServer(S_IP, port);
            server.Start();
            
        }

        BKRServer.AsyncTcpServer server;
        System.Net.IPAddress S_IP;
        BackgroundWorker bw = new BackgroundWorker();
        Packing pack;
        public string[] olddata = new string[200];
       


        private void Server_ClientConnected(object sender, BKRServer.ClientConnectedEventArgs e)
        {
        
            MessageBox.Show("Conected");
        }

        string[] Class = { };
        //if display public List<string> Send_Prot = new List<string> { };
        string send;
        byte[] T_Send = { };

        public void sendData(string[] Data)
        {
            //Static Data Send if Want_Data_once
            if (Want_Data_once)
            {
                for (int i = 0; i < pack.ValofStarneed1.Count; i++)
                {
                    Class = pack.ValofStarneed1[i].Class.Split('_');
                    //Net Data Style 
                    if(Data[i] != null)
                        send = "#1;" + pack.ValofStarneed1[i].TSelect.ToString() + ";" + Data[i].ToString() +
                        ";" + Class[1].ToString() + ";" + pack.ValofStarneed1[i].ValType.ToString() + ";O#";
                    // convert 2 Byte []
                    T_Send = Encoding.UTF8.GetBytes(send);
                    server.SendPacket(T_Send);
                    //if Display
                    //Send_Prot.Add(send);
                }
                Want_Data_once = false;
            }
            //Temp Data send if Data != data from last run 
            else if(Data != null)
            {
                for (int i = 0; i < pack.ValofStar.Count; i++)
                {
                    if (Data[i] != olddata[i])
                    {
                        Class = pack.ValofStar[i].Class.Split('_');
                        //Net Data Style 
                        if (Data[i] != null)
                            send = "#1;" + pack.ValofStar[i].TSelect.ToString() + ";" + Data[i].ToString() +
                            ";" + Class[1].ToString() + ";" + pack.ValofStar[i].ValType.ToString() + ";T#";
                        // convert 2 Byte []
                        T_Send = Encoding.UTF8.GetBytes(send);
                        server.SendPacket(T_Send);
                        //if display
                        //Send_Prot.Add(send);
                        
                        olddata[i] = Data[i];
                    }
                }
            }
            
        }
    }
}
