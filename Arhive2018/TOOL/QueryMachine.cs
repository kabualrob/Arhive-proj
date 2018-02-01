using Arhive2018.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arhive2018.FORMS;

namespace Arhive2018.TOOL
{
    public static class QueryMachine
    {
        public static void CheckUser(string username, string password, out string name, out bool isAdmin, out bool blocked,out int id, out string description)
        {
            id = 0;
             name = ""; description = "";
            isAdmin = false; blocked = true; 
            using (var sqlConnection = new SqlConnection(Settings.Default.ConnectionString))
            {
               // var sql = string.Format(Settings.Default.CheckUser, username, password);

                using (var searchSqlCommand = new SqlCommand(Settings.Default.CheckUser, sqlConnection))
                {
                    sqlConnection.Open();
                    searchSqlCommand.Parameters.AddWithValue("@lgn",username);
                    searchSqlCommand.Parameters.AddWithValue("@psw", password);
                    using (var dataReader = searchSqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            if (!dataReader.IsDBNull(0))
                            {
                              //  return SafeGetString(dataReader, 0);
                              name= SafeGetString(dataReader, 0);
                                isAdmin = Convert.ToBoolean(Convert.ToInt32(dataReader.GetValue(1)));
                                blocked = Convert.ToBoolean(Convert.ToInt32(dataReader.GetValue(2)));
                                id = Convert.ToInt32(dataReader.GetValue(3));
                                description = SafeGetString(dataReader, 4);
                                //  isAdmin =dataReader.GetValue(1);
                                //  blocked = dataReader.GetBoolean(2);
                            }
                        }
                    }
                }
            }
        }

        internal static object SelectUser()
        {
            DataTable DT = new DataTable();
            using (var connection = new SqlConnection(Settings.Default.ConnectionString))
            {
                connection.Open();
                using (var SelectCommand = new SqlCommand(Settings.Default.SelectUser, connection))
                {
                    SqlDataAdapter SDA = new SqlDataAdapter();
                    SDA.SelectCommand = SelectCommand;
                    SDA.Fill(DT);
                    connection.Close();
                }
            }
            return DT;
        }

        internal static string GetLogoPath()
        {
            string path = "";
            using (var sqlConnection = new SqlConnection(Settings.Default.ConnectionString))
            {
                using (var searchSqlCommand = new SqlCommand(Settings.Default.GetLogoPath, sqlConnection))
                {
                    sqlConnection.Open();
                    using (var dataReader = searchSqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            if (!dataReader.IsDBNull(0))
                            {
                                path = SafeGetString(dataReader, 0);
                            }
                        }
                    }
                }
            }
            return path;
        }

        internal static string GetArhivePath()
        {
            string path = "";
            using (var sqlConnection = new SqlConnection(Settings.Default.ConnectionString))
            {
               using (var searchSqlCommand = new SqlCommand(Settings.Default.GetArhivePath, sqlConnection))
                {
                    sqlConnection.Open();
                    using (var dataReader = searchSqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            if (!dataReader.IsDBNull(0))
                            {
                                path = SafeGetString(dataReader, 0);
                            }
                        }
                    }
                }
            }
            return path;
        }

        public static void UpdateRow(int id, string adress, string agreement, DateTime releaseDate, int managerId, int reportTypeId, string quantity, string comment)
        {
            using (var connection = new SqlConnection(Settings.Default.ConnectionString))
            {
                connection.Open();
              //  var updateSql = string.Format(Settings.Default.UpdateRow, adress,agreement,releaseDate,managerId,reportTypeId,quantity,comment,id);
                using (var updateSqlCommand = new SqlCommand(Settings.Default.UpdateRow, connection))
                {
                    updateSqlCommand.Parameters.AddWithValue("@obj", adress);
                    updateSqlCommand.Parameters.AddWithValue("@agr", agreement);
                    updateSqlCommand.Parameters.AddWithValue("@date", releaseDate);
                    updateSqlCommand.Parameters.AddWithValue("@usr", managerId);
                    updateSqlCommand.Parameters.AddWithValue("@rep", reportTypeId);
                    updateSqlCommand.Parameters.AddWithValue("@qty", quantity);
                    updateSqlCommand.Parameters.AddWithValue("@com", comment);
                    updateSqlCommand.Parameters.AddWithValue("@id", id);
                    updateSqlCommand.ExecuteNonQuery();
                }
            }

        }

        internal static bool CheckIfReportInUse(int reportId)
        {
            bool hasrows = false;
            using (var sqlConnection = new SqlConnection(Settings.Default.ConnectionString))
            {
                using (var searchSqlCommand = new SqlCommand(Settings.Default.CheckIfReportInUse, sqlConnection))
                {
                    sqlConnection.Open();
                    searchSqlCommand.Parameters.AddWithValue("@ID", reportId);
                    using (var dataReader = searchSqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            hasrows = true;
                        }
                    }
                }
            }
            return hasrows; 
        }

        internal static bool CheckIfUserInUse(int userId)
        {
            bool hasrows = false;
            using (var sqlConnection = new SqlConnection(Settings.Default.ConnectionString))
            {
                using (var searchSqlCommand = new SqlCommand(Settings.Default.CheckIfUserInUse, sqlConnection))
                {
                    sqlConnection.Open();
                    searchSqlCommand.Parameters.AddWithValue("@ID", userId);
                    using (var dataReader = searchSqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            hasrows = true;
                        }
                    }
                }
            }
            return hasrows;
        }

        public static void InsertRow(string adress, string agreement, DateTime releaseDate, int managerId, int reportTypeId, string quantity, string comment,out int newId,int createdBy, DateTime date)
        {
            using (var connection = new SqlConnection(Settings.Default.ConnectionString))
            {
                connection.Open();
                //  var updateSql = string.Format(Settings.Default.UpdateRow, adress,agreement,releaseDate,managerId,reportTypeId,quantity,comment,id);
                using (var updateSqlCommand = new SqlCommand(Settings.Default.InsertRow, connection))
                {
                    updateSqlCommand.Parameters.AddWithValue("@obj", adress);
                    updateSqlCommand.Parameters.AddWithValue("@agr", agreement);
                    updateSqlCommand.Parameters.AddWithValue("@date", releaseDate);
                    updateSqlCommand.Parameters.AddWithValue("@usr", managerId);
                    updateSqlCommand.Parameters.AddWithValue("@rep", reportTypeId);
                    updateSqlCommand.Parameters.AddWithValue("@qty", quantity);
                    updateSqlCommand.Parameters.AddWithValue("@com", comment);
                    updateSqlCommand.Parameters.AddWithValue("@createdBy", createdBy);
                    updateSqlCommand.Parameters.AddWithValue("@createdOn", date);
                    // updateSqlCommand.Parameters.AddWithValue("@id", id);
                    newId = (int)updateSqlCommand.ExecuteScalar();
                   // updateSqlCommand.ExecuteNonQuery();
                }
            }

        }
        public static void DeleteRow(int id)
        {
            using (var connection = new SqlConnection(Settings.Default.ConnectionString))
            {
                connection.Open();
                //  var updateSql = string.Format(Settings.Default.UpdateRow, adress,agreement,releaseDate,managerId,reportTypeId,quantity,comment,id);
                using (var updateSqlCommand = new SqlCommand(Settings.Default.DeleteRow, connection))
                {                    
                    updateSqlCommand.Parameters.AddWithValue("@id", id);
                    updateSqlCommand.ExecuteNonQuery();
                }
            }

        }
        public static DataTable SelectReport()
        {
            DataTable DT = new DataTable();            
            using (var connection = new SqlConnection(Settings.Default.ConnectionString))
            {
                connection.Open();
                using (var SelectCommand = new SqlCommand(Settings.Default.SelectReport, connection))
                {
                    SqlDataAdapter SDA = new SqlDataAdapter();
                    SDA.SelectCommand = SelectCommand;                   
                    SDA.Fill(DT);
                    connection.Close();
                }
            }
            return DT;
        }
        public static System.Windows.Forms.AutoCompleteStringCollection GetObjectInfo(string custName)
        {
            List<string> result = new List<string>();
            var source = new System.Windows.Forms.AutoCompleteStringCollection();
            string sql = "SELECT OBJECT FROM ARHIVE WHERE OBJECT LIKE @partName";
            using (var sqlConnection = new SqlConnection(Settings.Default.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
            {
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@partName", "%"+ custName + "%");
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        if (!r.IsDBNull(0))
                        {
                            result.Add(r.GetString(0));
                            source.Add(r.GetString(0));
                        }
                    }
                }
            }
            return source;
        }

        internal static void UpdatePathLogo(string fileName)
        {
            using (var connection = new SqlConnection(Settings.Default.ConnectionString))
            {
                connection.Open();
                using (var updateSqlCommand = new SqlCommand(Settings.Default.UpdatePathLogo, connection))
                {
                    updateSqlCommand.Parameters.AddWithValue("@PATH", fileName);
                    updateSqlCommand.ExecuteNonQuery();
                }
            }
        }

        private static string SafeGetString(IDataRecord dr, int pos)
        {
            return dr.IsDBNull(pos) ? string.Empty : dr.GetString(pos);
        }

        internal static void UpdatePath(string selectedPath)
        {
            using (var connection = new SqlConnection(Settings.Default.ConnectionString))
            {
                connection.Open();
                using (var updateSqlCommand = new SqlCommand(Settings.Default.UpdatePath, connection))
                {
                    updateSqlCommand.Parameters.AddWithValue("@PATH", selectedPath);
                    updateSqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
