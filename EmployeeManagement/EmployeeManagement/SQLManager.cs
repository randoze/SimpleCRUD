using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    class SQLManager
    {
        public MySqlConnection conn;
        private string server = "";
        private String username = "";
        private String password = "";
        String db_name = "";
        String connString = "";


        public void connectToDatabase()
        {
            try
            {
                server = "localhost";
                username = "root";
                password = "";
                db_name = "employeewpf";
                connString = "server=" + server + ";user=" + username + ";password=" + password + ";database=" + db_name + ";";
                conn = new MySqlConnection(connString);
            }
            catch (MySqlException ex)
            {

            }
        }
    }
}
