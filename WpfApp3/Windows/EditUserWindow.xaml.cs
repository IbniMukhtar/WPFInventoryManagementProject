using System.Linq;
using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.Pages;

namespace WpfApp3.Windows
{
    /// <summary>

    public partial class EditUserWindow : Window
    {
        private readonly AppDbContext _dbContext;
        private readonly UserView _usersView;
        private readonly MAUSER? _user; 
        public EditUserWindow(MAUSER? selectedUser, UserView usersView)
        {
            InitializeComponent();
            _usersView = usersView ?? throw new ArgumentNullException(nameof(usersView));
            _dbContext = new AppDbContext();

            if (selectedUser == null)
            {
                MessageBox.Show("No user selected.");
                Close();
                return;
            }

            _user = _dbContext.MAUSERS.FirstOrDefault(u => u.ID == selectedUser.ID);
            if (_user == null)
            {
                MessageBox.Show("User not found in the database.");
                Close();
                return;
            }

            PopulateUserData();
        }

        private void PopulateUserData()
        {
            if (_user == null)
            {
                MessageBox.Show("User data is not available.");
                Close();
                return;
            }

            txtID.Text = _user.ID.ToString();
            txtUsername.Text = _user.USERNAME;
            txtPassword.Password = _user.UPASSWD;
            txtFullName.Text = _user.FullName;
            txtMobileNo.Text = _user.MOBILENO;
            txtEmail.Text = _user.EMAIL;
            chkStatus.IsChecked = _user.STATUS;
            txtDate.Text = _user.CDATE?.ToString("dd-MM-yyyy");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("User data is not available.");
                Close();
                return;
            }

            _user.USERNAME = txtUsername.Text;
            _user.UPASSWD = txtPassword.Password;
            _user.FullName = txtFullName.Text;
            _user.MOBILENO = txtMobileNo.Text;
            _user.EMAIL = txtEmail.Text;
            _user.STATUS = chkStatus.IsChecked ?? false;

            _dbContext.SaveChanges();

            _usersView.PopulateUsers();
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                MessageBox.Show("User data is not available.");
                Close();
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _dbContext.MAUSERS.Remove(_user);
                _dbContext.SaveChanges();

                _usersView.PopulateUsers();
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
