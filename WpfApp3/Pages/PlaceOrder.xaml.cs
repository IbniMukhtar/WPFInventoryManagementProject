using System.Windows;
using System.Windows.Controls;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.ViewModels;

namespace WpfApp3.Pages
{
    public partial class PlaceOrder : Page
    {
        private int orderId;
        private PlaceOrderViewModel viewModel;

        public PlaceOrder(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
            viewModel = new PlaceOrderViewModel();
            DataContext = viewModel;
            PopulateOrderDetails();
            PopulateItemNames();
        }

        private void PopulateOrderDetails()
        {
            using (var context = new AppDbContext())
            {
                var order = context.TB_ORDER.FirstOrDefault(o => o.Id == orderId);
                if (order != null)
                {
                    viewModel.PartyName = context.MAPARTIES.FirstOrDefault(p => p.ID == order.PartyId)?.PNAME!;
                    viewModel.Remarks = order.Remarks!;
                    viewModel.OrderDate = order.OrderDate.Date;
                    viewModel.OrderNumber = order.OrderNumber!;
                }
            }
        }

        private void PopulateItemNames()
        {
            using (var context = new AppDbContext())
            {
                var itemNames = context.MAITEMS.Select(item => item.ITEMNAME).ToList();
                comboBox.ItemsSource = itemNames;
            }
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                // Get the selected ItemName
                string selectedItemName = comboBox.SelectedItem as string ?? string.Empty;

                // Find the corresponding ID from the database
                int itemId = context.MAITEMS
                                    .Where(item => item.ITEMNAME == selectedItemName)
                                    .Select(item => item.ID)
                                    .FirstOrDefault();

                if (itemId != default(int))
                {
                    int quantity = int.Parse(txtQuantity.Text);
                    string unit = (comboBoxUnit.SelectedItem as ComboBoxItem)!.Content.ToString()!;


                    TB_ORDERDTL newOrderDetail = new TB_ORDERDTL
                    {
                        MId = orderId,
                        ItemID = itemId,
                        Quantity = quantity,
                        Unit = unit
                    };

                    context.TB_ORDERDTL.Add(newOrderDetail);
                    context.SaveChanges();

                    // Update the DataGrid
                    var orderDetails = (from od in context.TB_ORDERDTL
                                        where od.MId == orderId
                                        join item in context.MAITEMS on od.ItemID equals item.ID
                                        select new
                                        {
                                            od.Id,
                                            od.MId,
                                            ItemName = item.ITEMNAME,
                                            od.Quantity,
                                            od.Unit
                                        }).ToList();

                    orderDetailsDataGrid.ItemsSource = orderDetails;

                    orderDetailsDataGrid.ItemsSource = orderDetails;
                }
                else
                {
                    MessageBox.Show("Selected ItemName not found or invalid.");
                }
            }
        }

        private void PlaceOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null && mainWindow.FindName("fContainer") is Frame fContainer)
            {
                fContainer.Navigate(new IndexPage());
            }
            else
            {
                Console.WriteLine("Navigation failed. fContainer not found.");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the DataGrid
            var selectedRow = orderDetailsDataGrid.SelectedItem;

            if (selectedRow != null)
            {
                // Access the properties of the anonymous type directly
                var itemName = (string)selectedRow.GetType().GetProperty("ItemName")!.GetValue(selectedRow)!;
                var quantity = (int)selectedRow.GetType().GetProperty("Quantity")!.GetValue(selectedRow)!;
                var unit = (string)selectedRow.GetType().GetProperty("Unit")!.GetValue(selectedRow)!;

                // Populate fields in the Grid with data from the selected row
                comboBox.SelectedItem = itemName;
                txtQuantity.Text = quantity.ToString();
                comboBoxUnit.SelectedItem = unit;

                // Change button text and behavior
                btnAddUpdateDynamo.Content = "Update";
                btnAddUpdateDynamo.Click -= AddOrderButton_Click;
                btnAddUpdateDynamo.Click += UpdateOrderButton_Click;
            }
        }

        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected row
            var selectedRow = orderDetailsDataGrid.SelectedItem;

            /*if (selectedRow != null)
            {
                // Access the properties of the anonymous type directly
                var selectedItemName = (string)comboBox.SelectedItem;
                var quantity = 0;
                if (!int.TryParse(txtQuantity.Text, out quantity))
                {
                    MessageBox.Show("Invalid quantity value.");
                    return;
                }
                var unit = (string)comboBoxUnit.SelectedItem;

                // Update the selected row
                selectedRow.GetType().GetProperty("ItemID").SetValue(selectedRow, GetItemIdFromName(selectedItemName));
                selectedRow.GetType().GetProperty("ItemName").SetValue(selectedRow, selectedItemName);
                selectedRow.GetType().GetProperty("Quantity").SetValue(selectedRow, quantity);
                selectedRow.GetType().GetProperty("Unit").SetValue(selectedRow, unit);

                // Save changes to the database
                using (var context = new AppDbContext())
                {
                    context.SaveChanges();
                }

                // Refresh the DataGrid
                orderDetailsDataGrid.Items.Refresh();

                // Reset the form
                ResetForm();
            }*/
        }

        private int GetItemIdFromName(string itemName)
        {
            using (var context = new AppDbContext())
            {
                return context.MAITEMS.Where(item => item.ITEMNAME == itemName).Select(item => item.ID).FirstOrDefault();
            }
        }


        /*private void UpdateButton_Click(object sender, RoutedEventArgs e)
{
    // Get the selected item from the DataGrid
    dynamic selectedRow = (TB_ORDERDTL)orderDetailsDataGrid.SelectedItem;

    if (selectedRow != null)
    {
        // Populate fields in the Grid with data from the selected row
        comboBox.SelectedItem = selectedRow.ItemName;
        txtQuantity.Text = selectedRow.Quantity.ToString();
        comboBoxUnit.SelectedItem = selectedRow.Unit;

        // Change button text and behavior
        btnAddUpdateDynamo.Content = "Update";
        btnAddUpdateDynamo.Click -= AddOrderButton_Click;
        btnAddUpdateDynamo.Click += UpdateOrderButton_Click;
    }
}
*/

        /* private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
         {
             // Create a new instance of DbContext
             using (var context = new AppDbContext())
             {
                 // Get the selected row
                 var selectedRow = orderDetailsDataGrid.SelectedItem as TB_ORDERDTL;

                 if (selectedRow != null)
                 {
                     // Get the selected ItemName
                     string selectedItemName = comboBox.SelectedItem as string;

                     // Find the corresponding ID from the database
                     int itemId = context.MAITEMS
                                         .Where(item => item.ITEMNAME == selectedItemName)
                                         .Select(item => item.ID)
                                         .FirstOrDefault();

                     if (itemId != default(int))
                     {
                         // Get the Quantity and Unit from the ComboBox and TextBox controls
                         int quantity;
                         if (!int.TryParse(txtQuantity.Text, out quantity))
                         {
                             MessageBox.Show("Invalid quantity value.");
                             return;
                         }

                         string unit = comboBoxUnit.SelectedItem?.ToString();

                         // Update the selected row
                         selectedRow.ItemID = itemId;
                         selectedRow.ItemName = selectedItemName;
                         selectedRow.Quantity = quantity;
                         selectedRow.Unit = unit;

                         // Save changes to the database
                         context.SaveChanges();

                         // Refresh the DataGrid
                         orderDetailsDataGrid.Items.Refresh();

                         // Reset the form
                         ResetForm();


                     }
                     else
                     {
                         MessageBox.Show("Selected ItemName not found or invalid.");
                     }
                 }
             }
         }*/

        private void ResetForm()
        {
            // Clear the selection in the DataGrid
            orderDetailsDataGrid.SelectedItem = null;

            // Clear the selection in the ComboBox and TextBox controls
            comboBox.SelectedItem = null;
            txtQuantity.Text = "";
            comboBoxUnit.SelectedItem = null;

            // Reset the button text and behavior
            btnAddUpdateDynamo.Content = "Add";
            btnAddUpdateDynamo.Click -= UpdateOrderButton_Click;
            btnAddUpdateDynamo.Click += AddOrderButton_Click;
        }


    }
}
