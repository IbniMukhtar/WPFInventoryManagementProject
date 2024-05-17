using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.Windows;
namespace WpfApp3.Pages
{
    public partial class Items : Page
    {
        private readonly AppDbContext _dbContext;
        public ObservableCollection<MAITEM> Item { get; set; }
        public Items()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            Item = new ObservableCollection<MAITEM>();
            Loaded += Items_Loaded;
        }
        private void Items_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateItems();
        }

        private void PopulateItems()
        {
            try
            {
                var items = _dbContext.MAITEMS.ToList();
                foreach (var item in items)
                {
                    Item.Add(item);
                }
                datagridItems.ItemsSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while accessing the database: " + ex.Message);
            }
        }


        public void RefreshItems()
        {
            Item.Clear();
            PopulateItems();
        }
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addNewPartyWindow = new AddItemWindow(this);
            addNewPartyWindow.Show();
        }

        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {
            MAITEM selectedItem = (MAITEM)datagridItems.SelectedItem;

            if (selectedItem != null)
            {
                EditItemWindow editItemWindow = new EditItemWindow(selectedItem, _dbContext, this);
                editItemWindow.Show();
            }
            else
            {
                // Show an error message if no item is selected
                MessageBox.Show("Please select an item to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
