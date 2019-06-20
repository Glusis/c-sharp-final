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

        private void Button2_Click(object sender, EventArgs e)//this literally does everything all the time.
        {
            string city = textBox1.Text;
            ClientWeb = new WebClient();
            var result = ClientWeb.DownloadString(@"https://api.openweathermap.org/data/2.5/weather?q="+ city +"&units=metric&APPID=585ee2cd4cfe209b0e2fdc7a06863fa9");
            //Console.WriteLine(result);
            string temp = result.Split(new String[] { ",\"main\":{\"temp\":" }, StringSplitOptions.None)[1];
            temp = temp.Remove(temp.IndexOf(','));
            float tempInt = float.Parse(temp);
            label2.Text = (tempInt).ToString();
            label1.Text = "Last Updated:  " + DateTime.Now.ToString();
            addWeather(city, tempInt);
            fillChart();
        }

        private void addWeather(string city, float temp)
        {
            string query = "INSERT INTO WeatherData (city, temp, time) VALUES ('"+ city + "', '" + temp + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            DBconnect dbcon = new DBconnect(query);
        }
        private void getWeather()
        {
            string theDate = dateTimePicker1.Value.ToString("dd/MM/yyyy");

            string query = "SELECT city, AVG(temp) FROM WeatherData WHERE date = \"" + theDate + "\"";
            DBconnect dbcon = new DBconnect(query);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button2_Click(sender, e);
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
            Environment.Exit(0);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Button2_Click(sender, e);

        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        
        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //this.contextMenuStrip1.Opening() += ;
                //this.FormClosing += this.Form1_FormClosing;
            }
            else//left or middle click
            {
                //do something here
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Button2_Click(sender, e);
            timer1.Start();
        }

        private void fillChart()
        {
            chart1.Series["history"].Points.Clear();
            //string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            /*
            for (int i = 0; i < 5; i++)
            {
                var date = DateTime.Now.AddDays(i - 4);
                string dateString = date.ToString("yyyy-MM-dd");
            }
            */

            //DateTime theDate = dateTimePicker1.Value;
            //string query = "SELECT temp FROM WeatherData WHERE time = '" + theDate + "' AND city = '" + textBox1.Text + "'";
            string query = "SELECT  AVG(temp), time FROM WeatherData WHERE city = '" + textBox1.Text + "' GROUP BY time ORDER BY time DESC";

            double[] temps = DBconnectRead.getTemps(query);
            string[] dates = DBconnectRead.getDates(query);
            
            for (int i = 0; i < 5; i++)
            {
                
                chart1.Series["history"].Points.AddXY(dates[i], temps[i]);
                Console.WriteLine(temps[i]);
            }
            //chart1.Series["history"].Points.AddXY(city, temp);
            //chart title  
            //chart1.Titles.Add("Weather History");
        }


        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(numericUpDown1.Value) * 60000;
        }
    }
}
