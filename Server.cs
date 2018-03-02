using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;
using System.Windows.Forms;
using System.Threading;

namespace Data_Server_A01
{
    public partial class Server : Form
    {
        Network net;
        Packing pack = new Packing();
        Random rnd = new Random();
        BackgroundWorker bw = new BackgroundWorker();
        public Server()
        {
            InitializeComponent();
            bw.WorkerSupportsCancellation = true;
            net = new Network("127.0.0.1",8001,pack);
            net.sendData(pack.getData(true));
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
            // if Display enable
            
            
        }
        public bool en_Display = false;

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Visible = false;
            while (true)
            {
                
                int r = rnd.Next(2);
                if (r == 1) { 
                    net.sendData(pack.getData(true));
                    net.Want_Data_once = true;
                 }
                else
                {
                     net.sendData(pack.getData(false));
                }

                //if display 
                // display();
            }
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
       { 
            bw.CancelAsync();
            if (bw.CancellationPending)
            {
                bw.Dispose();
                return;
            }
            Thread.Sleep(55);
        }
        string var;

       
        //private void display()
        //{
        //    if (richTextBox1.InvokeRequired)
        //    {
        //        MethodInvoker up = delegate
        //        {
        //            //label1.Text = pack.dataonce[3];
        //            for (int i = 0; i < net.Send_Prot.Count; i++)
        //            {
        //                var += net.Send_Prot[i] + "\n";
        //            }
        //            richTextBox1.Text = var;
        //        };
        //        this.Invoke(up);

        //    }
        //    else
        //    {
        //        //label1.Text = pack.dataonce[3];
        //        for (int i = 0; i < net.Send_Prot.Count; i++)
        //        {
        //            var += net.Send_Prot[i] + "\n";
        //        }

        //        richTextBox1.Text = var;
        //    }

        //}


    }
    public struct qanten
    {
        public string TSelect;
        public string Class;
        public string ValType;
    }
}
