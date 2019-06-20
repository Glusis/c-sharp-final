using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WeatherApp
{
    class DBconnect
    {
        public DBconnect(string query)
        {
            try
            {
                //Data Source=(localdb)\ProjectsV13;Initial Catalog=Weather;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
                string connectionString =
                @"Data Source = (localdb)\ProjectsV13;" +
                @"AttachDbFilename =" +
                @"C:\Users\Glusis\AppData\Local\Microsoft\VisualStudio\SSDT\Weather.mdf;" +
                @"Integrated Security = True;" +
                @"Connect Timeout = 30;";

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

