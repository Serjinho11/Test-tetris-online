using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_s
{
    public partial class fServer : Form
    {
        public TcpListener server;
        public String dateServer;
     
        private static fServer serverForm;
        Thread t;
        bool workThread;
        NetworkStream streamServer;

        public fServer()
        {
            InitializeComponent();
            server = new TcpListener(System.Net.IPAddress.Any,3000);
            server.Start();
            t = new Thread(new ThreadStart(Asculta_Server));
            workThread = true;
           
            t.Start();
            serverForm = this;
        }
        public void Asculta_Server()
        {

            while (workThread)
            {
                Socket socketServer = server.AcceptSocket();
                try
                {
                    streamServer = new NetworkStream(socketServer);
                    StreamReader citireServer = new StreamReader(streamServer);
                   
                    while (workThread)
                    {

                        string dateServer= citireServer.ReadLine();
                        //char temp;
                        //    do {
                        //    temp = (char)citireServer.Read();
                        //    dateServer += temp;
                        //} while (!citireServer.EndOfStream);


                        if (dateServer == null) break;//primesc nimic - clientul a plecat
                        if (dateServer == "#Gata") //ca sa pot sa inchid serverul
                            workThread = false;
                        MethodInvoker m = new MethodInvoker(() => serverForm.textBox1.Text += (socketServer.LocalEndPoint + ": " + dateServer + Environment.NewLine));
                        serverForm.textBox1.Invoke(m);
                    }
                    streamServer.Close();
                }
                catch (Exception e)
                {
#if LOG
                    Console.WriteLine(e.Message);
#endif
                }
                socketServer.Close();
            }

        }
       

       
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            workThread = false;
            streamServer.Close();

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter scriere = new StreamWriter(streamServer);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine(tbServerDate.Text);
                textBox1.Text += "Server: " + tbServerDate.Text + Environment.NewLine;
                tbServerDate.Clear();
                // s_text.Close();
            }
            finally
            {
                // code in finally block is guranteed 
                // to execute irrespective of 
                // whether any exception occurs or does 
                // not occur in the try block
                //  client.Close();
            } 
        }

        private void tbServerDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnSend_Click(sender, e);
            }
        }

       
    }
}
