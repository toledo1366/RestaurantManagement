using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using Microsoft.VisualBasic;

namespace DesktopApp
{
    class DatabaseReader
    {
        public SqlConnection connection;
        private bool connected;

        public DatabaseReader()
        {
            connected = false;
        }

        public bool Connect()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "25.63.58.83",
                    UserID = "railsUser4",
                    Password = "sebastian1Q",
                    InitialCatalog = "Projekt3"
                };
                connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                connected = true;
                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }

        }

        public List<List<string>> Read(string sql)
        {
            SqlDataReader reader = null;
            List<List<string>> result = new List<List<string>>();
            if (connected)
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> reader2 = new List<string>();
                            reader2.Clear();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                reader2.Add(reader.GetValue(i).ToString());
                            }
                            result.Add(reader2);
                        }
                    }
                }
            }
            return result;
        }

        public void Close()
        {
            connection.Close();
            connected = false;
        }
    }
}
