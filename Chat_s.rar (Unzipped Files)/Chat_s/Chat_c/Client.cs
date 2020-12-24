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

namespace Chat_c
{
    public partial class fClient : Form
    {
        public TcpClient client;
        public NetworkStream clientStream;
        public bool ascult;
        public fClient clientForm;
        public Thread t;

        public fClient()
        {
            InitializeComponent();
            clientForm = this;

        }

        private void Asculta_client()
        {
            StreamReader citire = new StreamReader(clientStream);
            String dateClient;
            while (ascult)
            {
                dateClient = citire.ReadLine();
                //MethodInvoker m = new MethodInvoker(() => clientForm.textBox1.Text += ("Server: " + dateClient + Environment.NewLine));
                //clientForm.textBox1.Invoke(m);
            }
        }
        /*
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                
                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                //scriere.WriteLine(tbDate.Text);
               // textBox1.Text += "Client: " + tbDate.Text + Environment.NewLine;
               // tbDate.Clear();
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
        */

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ascult = false;
            t.Abort();
            clientStream.Close();
            client.Close();
        }
        /*
        private void tbDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnSend_Click(sender, e);
            }
        }
        */
        private void btnConect_Click(object sender, EventArgs e)
        {
            if (btnConect.Text == "Connect")
            {
                //if (tbAddress.Text.Length > 0)
                // {
                client = new TcpClient("127.0.0.1", 3000);
                ascult = true;
                t = new Thread(new ThreadStart(Asculta_client));
                t.Start();
                clientStream = client.GetStream();

                // tbDate.Enabled = true;
                // btnSend.Enabled = true;
                // label1.Visible = false;
                // tbAddress.Visible = false;
                btnConect.Text = "Disconect";
                //}
                // else
                // {
                //     MessageBox.Show("Specificati adresa de IP");
                //  }
            }
            else
            {
                ascult = false;
                t.Abort();
                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine("#Gata");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine(" 1 ");
                // textBox1.Text += "Client: " + tbDate.Text + Environment.NewLine;
                // tbDate.Clear();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine(" 2 ");
                // textBox1.Text += "Client: " + tbDate.Text + Environment.NewLine;
                // tbDate.Clear();
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine(" 3 ");
                // textBox1.Text += "Client: " + tbDate.Text + Environment.NewLine;
                // tbDate.Clear();
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine(" 4 ");
                // textBox1.Text += "Client: " + tbDate.Text + Environment.NewLine;
                // tbDate.Clear();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine(" 5 ");
                // textBox1.Text += "Client: " + tbDate.Text + Environment.NewLine;
                // tbDate.Clear();
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine(" 6 ");
                // textBox1.Text += "Client: " + tbDate.Text + Environment.NewLine;
                // tbDate.Clear();
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

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter scriere = new StreamWriter(clientStream);
                scriere.AutoFlush = true; // enable automatic flushing
                scriere.WriteLine(" 7 ");
                // textBox1.Text += "Client: " + tbDate.Text + Environment.NewLine;
                // tbDate.Clear();
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
    }
}
