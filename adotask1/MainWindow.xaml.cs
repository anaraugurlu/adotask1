using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace adotask1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        string cs = "";
        DataTable table;
        SqlDataReader reader;
       
        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

        }

        private void orderdetailbtn_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand update = new SqlCommand("AddOrderDetail", conn);
                update.CommandType = CommandType.StoredProcedure;
                var param1 = new SqlParameter("@OrdersQuantity", SqlDbType.NVarChar);
                param1.Value = Convert.ToInt32(txt1.Text);
                update.Parameters.Add(param1);

                var param2 = new SqlParameter("@OrderId", SqlDbType.Int);
                param2.Value = Convert.ToInt32(txt2.Text);
                update.Parameters.Add(param2);

                update.ExecuteNonQuery();
            }
        }

        private void orderbtn_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand add = new SqlCommand("AddOrder", conn);
                add.CommandType = CommandType.StoredProcedure;
                var sqlparam1 = new SqlParameter("@ProductId", SqlDbType.Int);
                sqlparam1.Value = Convert.ToInt32(txt1.Text);
                add.Parameters.Add(sqlparam1);

                var sqlparam2 = new SqlParameter("@CustomerId", SqlDbType.Int);
                sqlparam2.Value = Convert.ToInt32(txt2.Text);
                add.Parameters.Add(sqlparam2);

                add.ExecuteNonQuery();
            }
        }

        private void custbtn_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand add = new SqlCommand("AddCustomer", conn);
                add.CommandType = CommandType.StoredProcedure;
                var sqlparam1 = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                sqlparam1.Value = txt1.Text;
                add.Parameters.Add(sqlparam1);

                var sqlparam2 = new SqlParameter("@LastName", SqlDbType.NVarChar);
                sqlparam2.Value = txt2.Text;
                add.Parameters.Add(sqlparam2);

                add.ExecuteNonQuery();
            }
        }

        private void prodbtn_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();

                SqlCommand add = new SqlCommand("AddProduct", conn);
                add.CommandType = CommandType.StoredProcedure;

                var sqlparam1 = new SqlParameter("@ProductName", SqlDbType.NVarChar);
                sqlparam1.Value = txt1.Text;
                add.Parameters.Add(sqlparam1);

                var sqlparam2 = new SqlParameter("@Price", SqlDbType.Int);
                sqlparam2.Value = Convert.ToInt32(txt2.Text);
                add.Parameters.Add(sqlparam2);

                add.ExecuteNonQuery();
            }
        }

        private void grid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void grid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
       
        private void grid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            }

        private void grid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        DataSet set;
        SqlDataAdapter da;
        private void showbtn_Click(object sender, RoutedEventArgs e)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();
                set = new DataSet();

                da = new SqlDataAdapter("select* from Products;select * from Orders;select * from OrderDetails;select * from Customers", conn) ;
               
                da.Fill(set, "shop");
               grid1.ItemsSource = set.Tables[0].DefaultView;

                grid2.ItemsSource = set.Tables[1].DefaultView;
                grid3.ItemsSource = set.Tables[2].DefaultView;
                grid4.ItemsSource = set.Tables[3].DefaultView;

            }
        }
    }
}
