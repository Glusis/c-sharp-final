using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        WebClient ClientWeb;
        string city = "London";

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += this.Form1_FormClosing;
            //this.MouseDoubleClick += this.notifyIcon1_MouseDoubleClick;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            aboutForm.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            city = label1.Text;
            ClientWeb = new WebClient();
            var result = ClientWeb.DownloadString(@"https://api.openweathermap.org/data/2.5/weather?q="+ city +"&units=metric&APPID=585ee2cd4cfe209b0e2fdc7a06863fa9");
            //Console.WriteLine(result);
            string temp = result.Split(new String[] { ",\"main\":{\"temp\":" }, StringSplitOptions.None)[1];
            temp = temp.Remove(temp.IndexOf(','));
            float tempInt = float.Parse(temp);
            label2.Text = (tempInt).ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            notifyIcon1.Visible = true;
        }
        /*private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        } */

        private void NotifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }
    }
}
