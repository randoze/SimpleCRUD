using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLManager _mgr;
        DataTable dt;
        MySqlCommand cmd;
        MySqlDataAdapter da;
        public MainWindow()
        {
            InitializeComponent();
            CreateDatabase();
            CreateEmployeeTable();
            dgvEmployee.ItemsSource = LoadEmployee().DefaultView;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddEmployee(txtEmpname.Text, int.Parse(txtAge.Text), decimal.Parse(txtSalary.Text), dp_join.SelectedDate.GetValueOrDefault(), txtPhone.Text);
            }
            catch
            {
                MessageBox.Show("All fields are required.");
            }
        
        }
        private void RefreshInput()
        {
            txtEmpname.Clear();
            txtAge.Clear();
            txtSalary.Clear();
            txtPhone.Clear();
        }
        private void AddEmployee(string name,int age, decimal salary, DateTime join_date, string phone)
        {
            _mgr = new SQLManager();
            _mgr.connectToDatabase();
            _mgr.conn.Open();
            
            string query = "INSERT INTO tblemployee(emp_name,emp_age,emp_salary,join_date,phone) VALUES (@emp_name, @emp_age, @emp_salary,@join_date,@phone)";

            cmd = new MySqlCommand(query, _mgr.conn);
            cmd.Parameters.AddWithValue("@emp_name", name);
            cmd.Parameters.AddWithValue("@emp_age", age);
            cmd.Parameters.AddWithValue("@emp_salary", salary);
            cmd.Parameters.AddWithValue("@join_date", join_date);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.ExecuteNonQuery();
            _mgr.conn.Close();
            dgvEmployee.ItemsSource = LoadEmployee().DefaultView;
            RefreshInput();
        }
        private void IsEmployeeValid()
        {
            if (string.IsNullOrEmpty(txtAge.Text))
            {
                
            }
        }
        private DataTable LoadEmployee()
        {
            _mgr = new SQLManager();
            _mgr.connectToDatabase();
            _mgr.conn.Open();

            dt = new DataTable();
         
            String query = "SELECT * FROM tblemployee";

            cmd = new MySqlCommand(query, _mgr.conn);
           
            // cmd.ExecuteNonQuery();
            da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void CreateDatabase()
        {
            _mgr = new SQLManager();
            _mgr.connectToDatabase();
            _mgr.conn.Open();
            string query = @"CREATE DATABASE IF NOT EXISTS `employeewpf` ";
            MySqlCommand cmd = new MySqlCommand(query, _mgr.conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            _mgr.conn.Close();
            _mgr.conn.Dispose();

        }
        private void CreateEmployeeTable()
        {
            _mgr = new SQLManager();
            _mgr.connectToDatabase();
            _mgr.conn.Open();
          

             string query = @"CREATE TABLE IF NOT EXISTS  `tblemployee` (
              `ID` INT NOT NULL AUTO_INCREMENT,
              `emp_name` VARCHAR(100) NOT NULL,
              `emp_age` INT NOT NULL,
              `emp_salary` DECIMAL(10, 2) NOT NULL,
               `join_date` DATETIME NOT NULL,
              `phone` VARCHAR(20) NOT NULL,
              PRIMARY KEY(`ID`))
            ";

            cmd = new MySqlCommand(query, _mgr.conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            _mgr.conn.Close();
            _mgr.conn.Dispose();

        }

        private void txtAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]").IsMatch(e.Text);
        }

        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9-]").IsMatch(e.Text);
        }

        private void txtSalary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9,]").IsMatch(e.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RefreshInput();
        }
    }
}
