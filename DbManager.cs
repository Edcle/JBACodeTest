using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace JBACodeTest
{
    public class DbManager
    {
        internal void Connect(string localDbPath)
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localDbPath + @"; Integrated Security = True";

            SqlConnection conn;

            conn = new SqlConnection(connectionString);
            conn.Open();
            MessageBox.Show("Connection Open  !");
            conn.Close();
        }
    }
}