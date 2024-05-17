using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp3.Data;
using WpfApp3.Pages;

namespace WpfApp3
{
    public partial class LoginWindow : Window
    {
        private readonly AppDbContext _dbContext;
        public LoginWindow()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            bool isAuthenticated = AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                MainWindow mainWindow = new MainWindow();
                if (mainWindow.FindName("fContainer") is Frame fContainer)
                {
                    IndexPage indexPage = new IndexPage();
                    fContainer.Content = indexPage;
                }
                else
                {
                    MessageBox.Show("Frame fContainer not found in MainWindow.");
                    return;
                }

                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }


        private bool AuthenticateUser(string username, string password)
        {
            var user = _dbContext.MAUSERS.FirstOrDefault(u => u.USERNAME == username && u.UPASSWD == password);
            return user != null;
        }





        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize




        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Enter Username")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter Username";
                textBox.Foreground = Brushes.LightGray;
            }
        }


    }
}
