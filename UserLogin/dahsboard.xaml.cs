using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace UserLogin
{
    /// <summary>
    /// Interaction logic for dahsboard.xaml
    /// </summary>
    public partial class dahsboard : Window
    {
        public dahsboard()
        {
            InitializeComponent();
            binddatagrid();
        }

        private void binddatagrid()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-DE967HI; Initial Catalog=UsersDB; Integrated Security=True;");
            conn.Open();
            string query = "Select * from [User_Details]";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("User_Details");
            da.Fill(dt);

            g1.ItemsSource = dt.DefaultView;
            
            
        }



       
    }
}
