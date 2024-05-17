using System.Windows;
using System.Windows.Automation.Text;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.Pages;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WpfApp3.Windows
{
    public partial class EditItemWindow : Window
    {
        private readonly MAITEM _selectedItem;
        private readonly AppDbContext _dbContext;
        private readonly Items _itemsPage;

        public EditItemWindow(MAITEM selectedItem, AppDbContext dbContext, Items itemsPage)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            _dbContext = dbContext;
            _itemsPage = itemsPage;

            txtID.Text = _selectedItem.ID.ToString();
            txtDate.Text = _selectedItem.CDATE.ToString();
            txtItemName.Text = _selectedItem.ITEMNAME;
            txtUnit.Text = _selectedItem.UNIT;
            txtCode.Text = _selectedItem.CODE;
            txtArticleNo.Text = _selectedItem.ARTICLENO;
            txtStatus.IsChecked = _selectedItem.STATUS;
            txtRemarks.Text = _selectedItem.REMARKS;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedItem.ITEMNAME = txtItemName.Text;
            _selectedItem.UNIT = txtUnit.Text;
            _selectedItem.CODE = txtCode.Text;
            _selectedItem.ARTICLENO = txtArticleNo.Text;
            _selectedItem.STATUS = txtStatus.IsChecked ?? false;
            _selectedItem.REMARKS = txtRemarks.Text;

            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to update this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _dbContext.SaveChanges();
                    _itemsPage.RefreshItems();
                    MessageBox.Show("Item updated successfully.");
                    Close();
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the item: " + ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    _dbContext.MAITEMS.Remove(_selectedItem);
                    _dbContext.SaveChanges();
                    _itemsPage.RefreshItems();
                    MessageBox.Show("Item Deleted successfully.");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the item: " + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}


/*private void UpdateButton_Click(object sender, RoutedEventArgs e)
{
    _selectedItem.ITEMNAME = txtItemName.Text;
    _selectedItem.UNIT = txtUnit.Text;
    _selectedItem.CODE = txtCode.Text;
    _selectedItem.ARTICLENO = txtArticleNo.Text;
    _selectedItem.STATUS = txtStatus.IsChecked ?? false;
    _selectedItem.REMARKS = txtRemarks.Text;

    try
    {
        bool isReferenced = _dbContext.TB_ORDERDTL.Any(od => od.ItemID == _selectedItem.ID);

        if (isReferenced)
        {
            MessageBox.Show("Cannot update this item as it is referenced in other records.", "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        MessageBoxResult result = MessageBox.Show("Are you sure you want to update this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _dbContext.SaveChanges();
            _itemsPage.RefreshItems();
            MessageBox.Show("Item updated successfully.");
            Close();
        }
        else
        {
            Close();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("An error occurred while updating the item: " + ex.Message);
    }
}

private void DeleteButton_Click(object sender, RoutedEventArgs e)
{
    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
    try
    {
        if (result == MessageBoxResult.Yes)
        {
            bool isReferenced = _dbContext.TB_ORDERDTL.Any(od => od.ItemID == _selectedItem.ID);

            if (isReferenced)
            {
                MessageBox.Show("Cannot delete this item as it is referenced in other records.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _dbContext.MAITEMS.Remove(_selectedItem);
            _dbContext.SaveChanges();
            _itemsPage.RefreshItems();
            MessageBox.Show("Item deleted successfully.");
            Close();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("An error occurred while deleting the item: " + ex.Message);
    }
}*/