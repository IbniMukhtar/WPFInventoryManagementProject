using System.Windows;
using System.Windows.Controls;
using WpfApp3.ViewModels;
namespace WpfApp3.Pages
{
    public partial class OrderPage : Page
    {
        private OrderViewModel viewModel;
        public OrderPage()
        {
            InitializeComponent();
            viewModel = new OrderViewModel();
            DataContext = viewModel;
        }
        private void txtPartyId_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void btnAddNewOrder_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
            MessageBox.Show("Order details saved successfully!");
            int orderId = viewModel.GetLastOrderId();
            PlaceOrder placeOrder = new PlaceOrder(orderId);
            this.NavigationService.Navigate(placeOrder);
        }
    }
}
