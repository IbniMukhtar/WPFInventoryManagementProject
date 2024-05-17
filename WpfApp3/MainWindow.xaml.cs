using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WpfApp3

{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        // Start: MenuLeft PopupButton //
        private void btnHome_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnHome;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Parties";
            }
        }

        private void btnHome_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }




        // Start: MenuLeft PopupButton //
        private void btnMainWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnMainWindow;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Home";
            }
        }

        private void btnMainWindow_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnUsersView_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnUsersView;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Users";
            }
        }

        private void btnUsersView_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void btnItems_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnItems;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Items";
            }
        }

        private void btnItems_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }



        private void btnOrderDetails_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnOrderDetails;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Order Details";
            }
        }

        private void btnOrderDetails_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }




        private void btnSetting_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = btnSetting;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Setting";
            }
        }

        private void btnSetting_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        // End: MenuLeft PopupButton //

        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize


        private void btnMainWindow_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/IndexPage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/PartyPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnUsersView_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/UserView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Items.xaml", UriKind.RelativeOrAbsolute));
        }
        private void btnOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/OrderPage.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}
