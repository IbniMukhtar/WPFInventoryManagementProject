using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.Windows;

namespace WpfApp3.Pages
{
    public partial class UserView : Page
    {
        private AddUserWindow? _addUserWindow;
        private EditUserWindow? _editUserWindow;

        public ObservableCollection<MAUSER> Users { get; } = new ObservableCollection<MAUSER>();

        public UserView()
        {
            InitializeComponent();
            Loaded += UsersView_Loaded;
        }

        private void UsersView_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateUsers();
        }

        public void PopulateUsers()
        {
            using (var dbContext = new AppDbContext())
            {
                try
                {
                    var users = dbContext.MAUSERS.ToList();
                    Users.Clear();
                    foreach (var user in users)
                    {
                        Users.Add(user);
                    }
                    datagridUsers.ItemsSource = Users;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while accessing the database: " + ex.Message);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (datagridUsers.SelectedItem is MAUSER selectedUser)
            {
                _editUserWindow = new EditUserWindow(selectedUser, this);
                _editUserWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a user to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            _addUserWindow = new AddUserWindow();
            _addUserWindow.Show();
        }
    }
}
