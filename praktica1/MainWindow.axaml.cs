using Avalonia.Controls;
using praktica1.Models;
using System.Linq;

namespace praktica1
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string login = log.Text;
            string password = pass.Text;

            using var context = new ShchetinkinContext();

            var user = context.Users
                 .FirstOrDefault(u => u.UserLogin == login && u.UserPassword == password);

            var Products = new Products(user.UserId, user.UserLogin, user.RoleId);
            Products.Show();
            this.Close();

           


        }


    }
}