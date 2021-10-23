using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Lesson17Task1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnectionStringBuilder sqlConnectionStringBuilder;
        SqlConnection sqlConnection;
        OleDbConnectionStringBuilder oleDbConnectionStringBuilder;
        OleDbConnection oleDbConnection;

        ElementsModel textBindings; //контекст для текстовых информационных полей формы
      
        public MainWindow()

        {
            InitializeComponent();
            textBindings = new ElementsModel();
            DataContext = textBindings;
            sqlConnection = new SqlConnection();
            oleDbConnection = new OleDbConnection();

            sqlConnection.StateChange += (s, e) =>
            { textBindings.SqlConnectionState = (s as SqlConnection).State.ToString(); };

            oleDbConnection.StateChange += (s, e) =>
            { textBindings.OleConnectionState = (s as OleDbConnection).State.ToString(); };

            //------------------------------- убрать
            userId.Text = "user1";
            userPassword.Password = "1234";

        }

        private async void LoginButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                //AttachDBFilename = @"C:\USERS\KPOLS\CUSTOMERSDB.MDF",
                InitialCatalog = "CustomersDB",
                IntegratedSecurity = false
            };
            sqlConnectionStringBuilder["User Id"] = userId.Text;
            textBindings.SqlPartialString = sqlConnectionStringBuilder.ConnectionString; //строка без пароля - для отображения в форме
            sqlConnectionStringBuilder["Password"] = userPassword.Password;

            if (sqlConnection.State != System.Data.ConnectionState.Closed)
                sqlConnection.Close();
            sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
            try
            {
                await sqlConnection.OpenAsync();
            }
            catch (Exception ex)
            {

                MessageBox.Show("При подключении к базе SQL возникла ошибка: "+ex.Message);
            }
            //textBindings.SqlConnectionState = sqlConnection.State.ToString();

            oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder()
            {
                Provider = "Microsoft.Jet.OLEDB.4.0",
                DataSource = @"..\..\DB\OLE\Orders.mdb",
            };

            oleDbConnectionStringBuilder.Add("Jet OLEDB:System Database", @"..\..\DB\OLE\Security.mdw");
            oleDbConnectionStringBuilder.Add("User ID", userId.Text);
            textBindings.OlePartialString = oleDbConnectionStringBuilder.ConnectionString;
            oleDbConnectionStringBuilder.Add("Password", userPassword.Password);

            if (oleDbConnection.State != System.Data.ConnectionState.Closed)
                oleDbConnection.Close();
            oleDbConnection.ConnectionString = oleDbConnectionStringBuilder.ConnectionString;
            try
            {
                await oleDbConnection.OpenAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("При подключении к базе OleDb возникла ошибка: " + ex.Message);
            }
            //textBindings.OleConnectionState = oleDbConnection.State.ToString();
        }

        private void LogoutButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sqlConnection.State != System.Data.ConnectionState.Closed)
            {
                sqlConnection.Close();
                //textBindings.SqlConnectionState = sqlConnection.State.ToString();
            }

            if (oleDbConnection.State != System.Data.ConnectionState.Closed)
            {
                oleDbConnection.Close();
                //textBindings.OleConnectionState = oleDbConnection.State.ToString();
            }
        }
    }
    // Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\KPOLS\SOURCE\REPOS\LESSON17TASK1\LESSON17TASK1\DB\SQL\CUSTOMERSDB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
    // Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\kpols\source\repos\Lesson17Task1\Lesson17Task1\DB\OLE\Orders.mdb;
    // User ID=Kpols;Jet OLEDB:System database=C:\Users\kpols\source\repos\Lesson17Task1\Lesson17Task1\DB\OLE\Security.mdw

    //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kpols\source\repos\Lesson17Task1\Lesson17Task1\DB\SQL\CustomersDB.mdf;
    //Initial Catalog=C:\USERS\KPOLS\SOURCE\REPOS\LESSON17TASK1\LESSON17TASK1\DB\SQL\CUSTOMERSDB.MDF;
    //Integrated Security=True;User ID=user1;Password=***********;Connect Timeout=30
}
