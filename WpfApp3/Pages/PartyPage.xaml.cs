using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.Data;
using WpfApp3.Models;
using WpfApp3.Windows;
namespace WpfApp3.Pages
{
    public partial class PartyPage : Page
    {
        private readonly AppDbContext _dbContext;
        public ObservableCollection<MAPARTY> Parties { get; set; }
        public PartyPage()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            Parties = new ObservableCollection<MAPARTY>();
            Loaded += Party_Loaded;
        }
        private void Party_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateParties();
        }

        private void PopulateParties()
        {
            try
            {
                var parties = _dbContext.MAPARTIES.ToList();
                // Add parties to the ObservableCollection
                foreach (var party in parties)
                {
                    Parties.Add(party);
                }
                datagridParties.ItemsSource = Parties;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while accessing the database: " + ex.Message);
            }
        }

        public void RefreshParties()
        {
            Parties.Clear();
            PopulateParties();
        }

        private void btnAddParty_Click(object sender, RoutedEventArgs e)
        {

            AddNewPartyWindow addNewPartyWindow = new AddNewPartyWindow(this);
            addNewPartyWindow.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MAPARTY selectedParty = (MAPARTY)datagridParties.SelectedItem;

            if (selectedParty != null)
            {
                EditPartyWindow editPartyWindow = new EditPartyWindow(selectedParty, _dbContext, this);
                editPartyWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a party to edit.");
            }
        }

    }
}
