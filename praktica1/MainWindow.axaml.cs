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
            if (user != null)
            {
                var Products = new Products();
                Products.Show();
                this.Close();
            }

        }


    }
}