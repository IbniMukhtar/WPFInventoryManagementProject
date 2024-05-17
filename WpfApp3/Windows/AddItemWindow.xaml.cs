using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.Pages;

namespace WpfApp3.Windows
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        private readonly AppDbContext _dbContext;
        private readonly Items _itemsPage;
        public AddItemWindow(Items itemsPage)
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            _itemsPage = itemsPage;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if any of the required fields are empty
            if (string.IsNullOrEmpty(txtItemName.Text) ||
                string.IsNullOrEmpty(txtUnit.Text) ||
                string.IsNullOrEmpty(txtCode.Text) ||
                string.IsNullOrEmpty(txtArticleNo.Text))
            {
                MessageBox.Show("Please fill out all required fields.");
                return;
            }

            // Create a new item object with the data from the input fields
            MAITEM newItem = new MAITEM
            {
                ITEMNAME = txtItemName.Text,
                UNIT = txtUnit.Text,
                CODE = txtCode.Text,
                ARTICLENO = txtArticleNo.Text,
                STATUS = txtStatus.IsChecked ?? false,
                REMARKS = txtRemarks.Text,
                CDATE = DateTime.Now
            };

            try
            {
                _dbContext.MAITEMS.Add(newItem);
                _dbContext.SaveChanges();
                _itemsPage.RefreshItems();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the new item: " + ex.Message);
            }
            finally
            {
                _dbContext.Dispose();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
