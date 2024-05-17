using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.Pages;

namespace WpfApp3.Windows
{
    public partial class AddNewPartyWindow : Window
    {
        private readonly AppDbContext _dbContext;
        private readonly PartyPage _party;

        public AddNewPartyWindow(PartyPage party)
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            _party = party;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if any of the required fields are empty
            if (string.IsNullOrEmpty(txtPartyName.Text) ||
                string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtMobileNo.Text) ||
                string.IsNullOrEmpty(txtContactPerson.Text) ||
                string.IsNullOrEmpty(txtCity.Text) ||
                string.IsNullOrEmpty(txtState.Text))
            {
                MessageBox.Show("Please fill out all required fields.");
                return;
            }
            if (IsNumeric(txtMobileNo.Text) != true)
            {
                MessageBox.Show("Please enter valid Mobile Number.");
                return;
            }

            // Create a new party object with the data from the input fields
            MAPARTY newParty = new MAPARTY
            {
                PNAME = txtPartyName.Text,
                ADDRESS = txtAddress.Text,
                MOBILENO = txtMobileNo.Text,
                CONPERSON = txtContactPerson.Text,
                CITY = txtCity.Text,
                STATE = txtState.Text,
                STATUS = chkStatus.IsChecked ?? false,
                CDATE = DateTime.Now
            };


            try
            {
                _dbContext.MAPARTIES.Add(newParty);
                _dbContext.SaveChanges();
                _party.RefreshParties();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the new party: " + ex.Message);
            }
            finally
            {
                _dbContext.Dispose();
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsNumeric(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

    }
}

