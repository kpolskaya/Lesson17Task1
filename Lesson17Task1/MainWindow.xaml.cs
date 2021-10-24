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

        ElementsModel textBindings; //контекст данных для текстовых информационных полей формы
      
        public MainWindow()

        {
            InitializeComponent();
            textBindings = new ElementsModel();
            DataContext = textBindings;
            PrepareToConnect();
        }

        private void PrepareToConnect()
        {
            // -------------- раскомментировать для автоматического ввода логина/пароля
            //userId.Text = "user1";
            //userPassword.Password = "1234";
            
            sqlConnection = new SqlConnection();
            oleDbConnection = new OleDbConnection();

            sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "CustomersDB",
                IntegratedSecurity = false
            };

            oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder()
            {
                Provider = "Microsoft.Jet.OLEDB.4.0",
                DataSource = @"..\..\DB\OLE\Orders.mdb",
            };
            oleDbConnectionStringBuilder.Add("Jet OLEDB:System Database", @"..\..\DB\OLE\Security.mdw");

            sqlConnection.StateChange += (s, e) =>
            { textBindings.SqlConnectionState = (s as SqlConnection).State.ToString(); };

            oleDbConnection.StateChange += (s, e) =>
            { textBindings.OleConnectionState = (s as OleDbConnection).State.ToString(); };

        }

        private async Task ConnectToSqlDb()
        {
            sqlConnectionStringBuilder["User Id"] = userId.Text;
            sqlConnectionStringBuilder["Password"] = "****";
            textBindings.SqlPublicString = sqlConnectionStringBuilder.ConnectionString; //строка без пароля - для отображения в форме
            sqlConnectionStringBuilder["Password"] = userPassword.Password;

            if (sqlConnection.State != System.Data.ConnectionState.Closed)
                sqlConnection.Close();
            sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
            await sqlConnection.OpenAsync();
        }

        private async Task ConnectToOleDb()
        {
            oleDbConnectionStringBuilder.Add("User ID", userId.Text);
            oleDbConnectionStringBuilder.Add("Password", "****");
            textBindings.OlePublicString = oleDbConnectionStringBuilder.ConnectionString;
            oleDbConnectionStringBuilder.Add("Password", userPassword.Password);

            if (oleDbConnection.State != System.Data.ConnectionState.Closed)
                oleDbConnection.Close();
            oleDbConnection.ConnectionString = oleDbConnectionStringBuilder.ConnectionString;
            await oleDbConnection.OpenAsync();
        }

        private async void LoginButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                await ConnectToSqlDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("При подключении к базе SQL возникла ошибка: " + ex.Message);
            }

            try
            {
                await ConnectToOleDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("При подключении к базе OleDb возникла ошибка: " + ex.Message);
            }
        }

        private void LogoutButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sqlConnection.State != System.Data.ConnectionState.Closed)
            {
                sqlConnection.Close();
            }

            if (oleDbConnection.State != System.Data.ConnectionState.Closed)
            {
                oleDbConnection.Close();
            }
        }
    }
    
}
