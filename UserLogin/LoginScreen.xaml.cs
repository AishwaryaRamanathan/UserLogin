using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
using static System.Net.Mime.MediaTypeNames;

namespace UserLogin
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
       
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string Username = txtUserName.Text;
            string password = txtPassword.Password;
            if(Username.Length == 0 )
            {
                MessageBox.Show("Username cannot be empty!");
            }
            bool result = 
                password.Any(c => char.IsLower(c)) &&
                password.Any(c => char.IsUpper(c)) &&
                password.Any(c => !char.IsLetterOrDigit(c));
            if(password.Length < 8)
            {
                MessageBox.Show("Password is invalid.");
            }
            else if(result == false)
            {
                MessageBox.Show("Password is Invalid.");
            }

            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-DE967HI; Initial Catalog=UsersDB; Integrated Security=True;");
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                string query = "SELECT COUNT(1) FROM User_Details WHERE username=@Username AND password_= @password";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if(count == 1)
                {
                    string updateQuery = "UPDATE User_Details SET hasLoggedIn = @bool WHERE username = @Username AND password_ = @password";
                    SqlCommand cmd2 = new SqlCommand(updateQuery, conn);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@bool", "True");
                    cmd2.Parameters.AddWithValue("@Username", Username);
                    cmd2.Parameters.AddWithValue("@password", password);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Login Was Successful!");

                    dahsboard db = new dahsboard();
                    this.Visibility = Visibility.Hidden;
                    db.Show();
                   
                }
                else
                {
                    MessageBox.Show("UserName or password is inalid.");
                }



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}
