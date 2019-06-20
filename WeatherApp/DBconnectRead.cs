using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WeatherApp
{
    class DBconnectRead
    {
        public DBconnectRead() {
        }

        public static double[] getTemps(string query) {
            string connectionString =
                     @"Data Source = (localdb)\ProjectsV13;" +
                     @"AttachDbFilename =" +
                     @"C:\Users\Glusis\AppData\Local\Microsoft\VisualStudio\SSDT\Weather.mdf;" +
                     @"Integrated Security = True;" +
                     @"Connect Timeout = 30;";

            double[] temps = new double[5];
            string[] dates = new string[5];

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                for (int i = 0; i < 5; i++)
                {
                    reader.Read();
                    var myDouble = 0.0;
                    var myString = "";
                    try { myDouble = Convert.ToDouble(reader[0]); } catch { }
                    try { myString = reader[1].ToString(); } catch { }
                    temps.SetValue(myDouble, i);
                    dates.SetValue(myString, i);
                    Console.WriteLine(myDouble);
                    Console.WriteLine(myString);
                }
            }
            
            reader.Close();
            conn.Close();
            return temps;
        }
        public static string[] getDates(string query)
        {
            string connectionString =
                     @"Data Source = (localdb)\ProjectsV13;" +
                     @"AttachDbFilename =" +
                     @"C:\Users\Glusis\AppData\Local\Microsoft\VisualStudio\SSDT\Weather.mdf;" +
                     @"Integrated Security = True;" +
                     @"Connect Timeout = 30;";

            double[] temps = new double[5];
            string[] dates = new string[5];

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                for (int i = 0; i < 5; i++)
                {
                    reader.Read();
                    var myDouble = 0.0;
                    var myString = "";
                    try { myDouble = Convert.ToDouble(reader[0]); } catch { }
                    try { myString = reader[1].ToString(); } catch { }
                    temps.SetValue(myDouble, i);
                    dates.SetValue(myString, i);
                    Console.WriteLine(myDouble);
                    Console.WriteLine(myString);
                }
            }

            reader.Close();
            conn.Close();
            return dates;
        }
    }
}
