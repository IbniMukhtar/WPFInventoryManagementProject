using Microsoft.VisualBasic.ApplicationServices;
using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.Pages;

namespace WpfApp3.Windows
{
    public partial class EditPartyWindow : Window
    {
        private readonly MAPARTY _selectedParty;
        private readonly AppDbContext _dbContext;
        private readonly PartyPage _party;
        public EditPartyWindow(MAPARTY selectedParty, AppDbContext dbContext, PartyPage party)
        {
            InitializeComponent();
            _selectedParty = selectedParty;
            _dbContext = dbContext;
            _party = party;
            // Populate the input fields with the data from the selected party object
            txtID.Text = _selectedParty.ID.ToString();
            txtDate.Text = _selectedParty.CDATE.ToString();
            txtPartyName.Text = _selectedParty.PNAME;
            txtAddress.Text = _selectedParty.ADDRESS;
            txtMobileNo.Text = _selectedParty.MOBILENO;
            txtContactPerson.Text = _selectedParty.CONPERSON;
            txtCity.Text = _selectedParty.CITY;
            txtState.Text = _selectedParty.STATE;
            chkStatus.IsChecked = _selectedParty.STATUS;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Update the selected party with the modified values
            _selectedParty.PNAME = txtPartyName.Text;
            _selectedParty.ADDRESS = txtAddress.Text;
            _selectedParty.MOBILENO = txtMobileNo.Text;
            _selectedParty.CONPERSON = txtContactPerson.Text;
            _selectedParty.CITY = txtCity.Text;
            _selectedParty.STATE = txtState.Text;
            _selectedParty.STATUS = chkStatus.IsChecked ?? false;

            // Update the party in the database
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to update this party?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _dbContext.SaveChanges();
                    _party.RefreshParties();
                    MessageBox.Show("Party updated successfully.");
                    Close();
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the party: " + ex.Message);
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Display a confirmation dialog
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this party?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            // Delete the selected party from the database
            try
            {
                _dbContext.MAPARTIES.Remove(_selectedParty);
                _dbContext.SaveChanges();
                _party.RefreshParties();
                Close(); // Close the window after deleting
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the party: " + ex.Message);
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving changes
            Close();
        }

    }
}





/*private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedParty.PNAME = txtPartyName.Text;
            _selectedParty.ADDRESS = txtAddress.Text;
            _selectedParty.MOBILENO = txtMobileNo.Text;
            _selectedParty.CONPERSON = txtContactPerson.Text;
            _selectedParty.CITY = txtCity.Text;
            _selectedParty.STATE = txtState.Text;
            _selectedParty.STATUS = chkStatus.IsChecked ?? false;

            try
            {
                bool isReferenced = _dbContext.TB_ORDER.Any(o => o.PartyId == _selectedParty.ID);

                if (isReferenced)
                {
                    MessageBox.Show("Cannot update this party as it is referenced in other records.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to update this party?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _dbContext.SaveChanges();
                    _party.RefreshParties();
                    MessageBox.Show("Party updated successfully.");
                    Close();
                }
                else
                {
                    Close();
                }
            }
            catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 547) // Foreign key constraint violation
            {
                MessageBox.Show("Cannot update this party as it is referenced in other records.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the party: " + ex.Message);
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this party?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool isReferenced = _dbContext.TB_ORDER.Any(o => o.PartyId == _selectedParty.ID);

                    if (isReferenced)
                    {
                        MessageBox.Show("Cannot delete this party as it is referenced in other records.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    _dbContext.MAPARTIES.Remove(_selectedParty);
                    _dbContext.SaveChanges();
                    _party.RefreshParties();
                    MessageBox.Show("Party deleted successfully.");
                    Close(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting the party: " + ex.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Close(); 
            }
        }*/