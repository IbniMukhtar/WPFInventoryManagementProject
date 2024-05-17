using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp3.ViewModels
{
    public class PlaceOrderViewModel : INotifyPropertyChanged
    {
        private string? partyName;
        public string PartyName
        {
            get { return partyName!; }
            set { partyName = value; OnPropertyChanged(); }
        }

        private string? remarks;
        public string Remarks
        {
            get { return remarks!; }
            set { remarks = value; OnPropertyChanged(); }
        }

        private DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; OnPropertyChanged(); }
        }

        private string? orderNumber;
        public string OrderNumber
        {
            get { return orderNumber!; }
            set { orderNumber = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
