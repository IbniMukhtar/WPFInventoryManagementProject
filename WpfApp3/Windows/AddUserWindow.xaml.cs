using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3.Windows
{
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            PopulateCurrentDate();
        }
        private void PopulateCurrentDate()
        {
            txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Password) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtMobileNo.Text))
                {
                    MessageBox.Show("Please fill out all the required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var dbContext = new AppDbContext())
                {
                    // Check if the username already exists
                    if (dbContext.MAUSERS.Any(u => u.USERNAME == txtUsername.Text))
                    {
                        MessageBox.Show("Username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Create a new MAUSER object and populate its properties
                    var newUser = new MAUSER
                    {
                        FullName = txtFullName.Text,
                        USERNAME = txtUsername.Text,
                        UPASSWD = txtPassword.Password,
                        CDATE = DateTime.Now,
                        STATUS = chkStatus.IsChecked ?? false,
                        EMAIL = txtEmail.Text,
                        MOBILENO = txtMobileNo.Text
                    };

                    // Add the new user to the database
                    dbContext.MAUSERS.Add(newUser);
                    dbContext.SaveChanges();

                    MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Clear the form fields
                    txtFullName.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Password = "";
                    txtEmail.Text = "";
                    txtMobileNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
