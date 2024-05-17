using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<string> _partyIds;
        private string _selectedPartyId = string.Empty;
        private string _remarks = string.Empty;
        private string _orderNumber = string.Empty;
        private DateTime _orderDate = DateTime.Now;
        private int _nextOrderNumber = 1;

        public ObservableCollection<TB_ORDERDTL> OrderDetails { get; } = new ObservableCollection<TB_ORDERDTL>();
        public ObservableCollection<string> ItemNames { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> PartyIds
        {
            get { return _partyIds; }
            set { _partyIds = value; OnPropertyChanged(); }
        }

        public string SelectedPartyId
        {
            get { return _selectedPartyId; }
            set { _selectedPartyId = value; OnPropertyChanged(); }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; OnPropertyChanged(); }
        }

        public string OrderNumber
        {
            get { return _orderNumber; }
            set { _orderNumber = value; OnPropertyChanged(); }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; OnPropertyChanged(); }
        }

        public string SelectedItemName { get; set; } = string.Empty;

        // Constructor
        public OrderViewModel()
        {
            _partyIds = new ObservableCollection<string>(GetPartyIdsFromDatabase());
            _nextOrderNumber = GetNextOrderNumber();
            OrderNumber = "ORD" + _nextOrderNumber.ToString().PadLeft(4, '0');
            _nextOrderNumber++;
            LoadItemNames();
            LoadOrderDetails();
        }

        private void LoadItemNames()
        {
            using (var context = new AppDbContext())
            {
                var itemNames = context.MAITEMS
                    .Select(item => item.ITEMNAME)
                    .Where(itemName => itemName != null)
                    .ToList()!;
                ItemNames = new ObservableCollection<string>(itemNames!);
            }
        }

        private int GetNextOrderNumber()
        {
            using (var context = new AppDbContext())
            {
                var lastOrder = context.TB_ORDER.OrderByDescending(o => o.Id).FirstOrDefault();
                if (lastOrder != null)
                {
                    string lastOrderNumber = lastOrder.OrderNumber!;
                    if (int.TryParse(lastOrderNumber.Substring(3), out int lastOrderNumberInt))
                    {
                        return lastOrderNumberInt + 1;
                    }
                }
                return 1;
            }
        }

        private List<string> GetPartyIdsFromDatabase()
        {
            using (var context = new AppDbContext())
            {
                return context.MAPARTIES
                    .Select(p => p.PNAME)
                    .Where(pname => pname != null)
                    .ToList()!;
            }
        }

        public void Save()
        {
            using (var context = new AppDbContext())
            {
                var order = new TB_ORDER
                {
                    PartyId = GetPartyIdFromName(SelectedPartyId),
                    Remarks = Remarks,
                    OrderNumber = OrderNumber,
                    OrderDate = OrderDate
                };

                context.TB_ORDER.Add(order);
                context.SaveChanges();
            }
        }

        public int GetLastOrderId()
        {
            using (var context = new AppDbContext())
            {
                var lastOrder = context.TB_ORDER.OrderByDescending(o => o.Id).FirstOrDefault();
                return lastOrder?.Id ?? -1;
            }
        }

        private void LoadOrderDetails()
        {
            using (var context = new AppDbContext())
            {
                var orderDetails = context.TB_ORDERDTL.ToList();
                foreach (var detail in orderDetails)
                {
                    OrderDetails.Add(detail);
                }
            }
        }

        private int GetItemIdFromName(string itemName)
        {
            using (var context = new AppDbContext())
            {
                return context.MAITEMS
                    .Where(item => item.ITEMNAME == itemName)
                    .Select(item => item.ID)
                    .FirstOrDefault();
            }
        }

        private int GetPartyIdFromName(string partyName)
        {
            using (var context = new AppDbContext())
            {
                return context.MAPARTIES
                    .Where(p => p.PNAME == partyName)
                    .Select(p => p.ID)
                    .FirstOrDefault();
            }
        }

        // OnPropertyChanged method
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
