using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace JBACodeTest
{
    public class DbManager
    {

        internal string InsertData(string localDbPath, List<RainfallEntry> entries)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(MakeConnectionString(localDbPath)))
            {

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("InsertRainfall");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    var start = DateTime.Now;

                    command.CommandText = "DELETE FROM Precipitation"; // NB deleting all existing rows in the table - for this test example at least, stops stuff accumulating in the DB
                    command.ExecuteNonQuery();

                    int blockSize = 200;

                    int i = 0;
                    while (i < entries.Count)
                    {
                        // write in blocks of N - this seems optimal but have not rigorously tested the alternatives
                        int block = Math.Min(entries.Count - i, blockSize); 

                        command.CommandText = "INSERT INTO Precipitation([Xref], [Yref], [Date], [Value]) VALUES ";

                        for (int j = 0; j < block; ++j)
                        {
                            if (j > 0)
                                command.CommandText += ", ";

                            var e = entries[i + j];
                            command.CommandText += String.Format(" ('{0}', '{1}', '{2}', '{3}')", e._x, e._y, e._date.ToString("yyyy-MM-dd"), e._amount);
                        }
                        command.CommandText += ";";

                        command.ExecuteNonQuery();

                        i += block;
                    }

                    var end = DateTime.Now;

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    TimeSpan span = end - start;
                    int ms = (int)span.TotalMilliseconds;

                    result = String.Format("Success - took {0}s", (float)(ms)/1000.0f);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Commit Exception Type: {0}\nMessage: {1}", ex.GetType(), ex.Message));
                    result = "error committing";

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        MessageBox.Show(String.Format("Commit Exception Type: {0}\nMessage: {1}", ex2.GetType(), ex2.Message));
                        result = "error rolling back";
                    }
                }


                connection.Close();
            }
            return result;
        }

        private static string MakeConnectionString(string localDbPath)
        {
            return @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localDbPath + @"; Integrated Security = True";
        }

        internal string Report(string localDbPath, int x, int y)
        {

            string result = "Test:\r\n";

            using (SqlConnection connection = new SqlConnection(MakeConnectionString(localDbPath)))
            {

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;//?

                try
                {
                    connection.Open();
                    command.CommandText = "SELECT COUNT(*) FROM Precipitation; ";
                    Int32 count = (Int32)command.ExecuteScalar();

                    result += String.Format("Table contains {0} rows\r\n", count);
                    result += String.Format("First 24 rows at {0},{1}:\r\n",x,y);

                    command.CommandText = String.Format("SELECT TOP 24 * FROM Precipitation WHERE Xref = {0} AND Yref = {1}", x, y);// "SELECT * FROM Precipitation LIMIT 0,10;";

                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            result += String.Format("{0}, {1}, {2}, {3}\r\n", reader["Xref"], reader["Yref"], reader["Date"], reader["Value"]);
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("DB Exception Type: {0}\nMessage: {1}", ex.GetType(), ex.Message));
                }


                connection.Close();
            }

            return result;
        }
    }
}