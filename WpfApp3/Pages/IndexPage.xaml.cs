using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3.Pages
{
    public partial class IndexPage : Page
    {
        private readonly AppDbContext _dbContext;
        public ObservableCollection<OrderViewModel> Orders { get; set; }

        public IndexPage()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            Orders = new ObservableCollection<OrderViewModel>();
            Loaded += IndexPage_Loaded;
        }

        private void IndexPage_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateOrders();
        }

        private void PopulateOrders()
        {
            try
            {
                var orders = (from order in _dbContext.TB_ORDER
                              join party in _dbContext.MAPARTIES on order.PartyId equals party.ID
                              orderby order.OrderDate descending
                              select new
                              {
                                  OrderNumber = order.OrderNumber,
                                  PartyName = party.PNAME,
                                  OrderDate = order.OrderDate,
                                  Remarks = order.Remarks
                              }).ToList()
               .Select(o => new OrderViewModel
               {
                   OrderNumber = o.OrderNumber,
                   PartyName = o.PartyName,
                   OrderDate = o.OrderDate.ToString("dd-MM-yyyy"),
                   Remarks = o.Remarks
               }).ToList();


                Orders.Clear();
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
                datagridOrders.ItemsSource = Orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while accessing the database: " + ex.Message);
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is OrderViewModel selectedOrder)
            {
                MessageBox.Show($"Viewing order: {selectedOrder.OrderNumber}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is OrderViewModel selectedOrder)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this order?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var order = _dbContext.TB_ORDER.FirstOrDefault(o => o.OrderNumber == selectedOrder.OrderNumber);
                        if (order != null)
                        {
                            var orderDetails = _dbContext.TB_ORDERDTL.Where(od => od.MId == order.Id).ToList();
                            if (orderDetails.Count > 0)
                            {
                                MessageBoxResult deleteResult = MessageBox.Show("This order has related details. Deleting it will also delete the associated details. Are you sure you want to delete this order?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                if (deleteResult == MessageBoxResult.Yes)
                                {
                                    _dbContext.TB_ORDERDTL.RemoveRange(orderDetails);
                                }
                            }

                            _dbContext.TB_ORDER.Remove(order);
                            _dbContext.SaveChanges();
                            Orders.Remove(selectedOrder);
                            MessageBox.Show("Order deleted successfully.");
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 547)
                    {
                        MessageBox.Show("Cannot delete this order as it is referenced in other records.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while deleting the order: " + ex.Message);
                    }
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("fContainer") is Frame fContainer)
            {
                fContainer.Navigate(new OrderPage());
            }
            else
            {
                Console.WriteLine("Navigation failed. fContainer not found.");
            }
        }
    }
}
