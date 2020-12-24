using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Game : Form
    {
        Launcher lnc;
        Piesa piesa;
        Tabel tabel;

        public int scor = 0;

        public int currentScore = 0;

        Random rd = new Random();
        int nrRand;

        public int[,] matrice = new int[12, 22]; // toate elementele sunt initializate cu 0


        //pt server:

        public TcpListener server;
        public String dateServer;

        private static Game serverForm;
        Thread t;
        bool workThread;
        NetworkStream streamServer;


        //gata server
        public Game(Launcher launcher)
        {

            InitializeComponent();
            lnc = launcher;
            tabel = new Tabel();
            tabel.DrawTableBorder(this);


            //pt server :

            server = new TcpListener(System.Net.IPAddress.Any, 3000);
            server.Start();
            t = new Thread(new ThreadStart(Asculta_Server));
            workThread = true;

            t.Start();
            serverForm = this;
            //gata server
        }



        private void timer1_Tick(object sender, EventArgs e)
        {

            piesa.MutaJos(this);

            if (piesa.stateOfPiece == false)
            {
                tabel.StergeLinieDacaECompleta(this, piesa);
            }

            if (tabel.GameOver(this))
            {
                currentScore = scor;

                if (currentScore >= lnc.HighScore)
                    lnc.HighScore = currentScore;

                lnc.lblHighScore.Text = lnc.HighScore.ToString();
                timer1.Enabled = false;
                MessageBox.Show("GAME OVER \n Scorul dvs. este: " + scor);
                
                this.Hide();
                lnc.Show();

            }

            DifficultyChanger();

        }




        public void GenerarePiesaRandom()
        {
            /*---------------------------------------------------------------------------
                 DESCRIPTION: - va genera la intamplare o piesa din cele 5.
            ---------------------------------------------------------------------------*/

            nrRand = rd.Next(1, 8);//genereaza un nr random intre 1 si 7

            if (nrRand == 1)
            {
                piesa = new Patrat();
                piesa.PozInit(this);
            }
            else if (nrRand == 2)
            {
                piesa = new Linie();
                piesa.PozInit(this);
            }
            else if (nrRand == 3)
            {
                piesa = new T();
                piesa.PozInit(this);
            }
            else if (nrRand == 4)
            {
                piesa = new L();
                piesa.PozInit(this);
            }
            else if (nrRand == 5)
            {
                piesa = new Patru();
                piesa.PozInit(this);
            }
            else if (nrRand == 6)
            {
                piesa = new J();
                piesa.PozInit(this);
            }
            else
            {
                piesa = new PatruIntors();
                piesa.PozInit(this);
            }
        }

        public void DifficultyChanger()
        {

            if (scor > 100)
            {
                timer1.Interval = 275;
                lblLevel.Text = "Basic";
            }
            if (scor > 200)
            {
                timer1.Interval = 250;
                lblLevel.Text = "Intermediate";

            }
            if (scor > 300)
            {
                timer1.Interval = 200;
                lblLevel.Text = "Advanced";

            }
            if (scor > 400)
            {
                timer1.Interval = 175;
                lblLevel.Text = "Expert";
            }
            if (scor > 500)
            {
                timer1.Interval = 150;
                lblLevel.Text = "No Pain, No Gain";
            }
            if (scor > 600)
            {
                timer1.Interval = 135;
                lblLevel.Text = "Damn I'm Good";
            }
            if (scor > 800)
            {
                timer1.Interval = 95;
                lblLevel.Text = "Hardcore";
            }
            if (scor > 1000)
            {
                timer1.Interval = 50;
                lblLevel.Text = "Nightmare!";

            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            GenerarePiesaRandom();

            timer1.Start();

            btnPlay.Enabled = false;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Right)
            {
                piesa.MutaDreapta(this);
            }
            if (e.KeyCode == Keys.Left)
            {
                piesa.MutaStanga(this);
            }
            if (e.KeyCode == Keys.Up)
            {
                piesa.RotirePiesa(this);
            }
            if (e.KeyCode == Keys.Down)
            {
                piesa.MutaJos(this);
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
                lnc.Show();
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            lnc.Show();
        }







        //pentru server:
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

                        string dateServer = citireServer.ReadLine();

                        if (dateServer == " 1 ")
                        {
                            piesa = new Linie();
                            piesa.PozInit(this);
                        }

                        if (dateServer == " 2 ")
                        {
                            piesa = new Patrat();
                            piesa.PozInit(this);
                        }

                        if (dateServer == " 3 ")
                        {
                            piesa = new Patru();
                            piesa.PozInit(this);
                        }
                        if (dateServer == " 4 ")
                        {
                            piesa = new PatruIntors();
                            piesa.PozInit(this);
                        }

                        if (dateServer == " 5 ")
                        {
                            piesa = new T();
                            piesa.PozInit(this);
                        }

                        if (dateServer == " 6 ")
                        {
                            piesa = new J();
                            piesa.PozInit(this);
                        }
                        if (dateServer == " 7 ")
                        {
                            piesa = new L();
                            piesa.PozInit(this);
                        }
                        //char temp;
                        //    do {
                        //    temp = (char)citireServer.Read();
                        //    dateServer += temp;
                        //} while (!citireServer.EndOfStream);


                        if (dateServer == null) break;//primesc nimic - clientul a plecat
                        if (dateServer == "#Gata") //ca sa pot sa inchid serverul
                            workThread = false;
                       // MethodInvoker m = new MethodInvoker(() => serverForm.textBox1.Text += (socketServer.LocalEndPoint + ": " + dateServer + Environment.NewLine));
                        //serverForm.textBox1.Invoke(m);
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


        /*
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
        */


        //gata server

    }
}
