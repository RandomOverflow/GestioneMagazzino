using System.ComponentModel;

namespace Gestione_Magazzino.Core
{
    public class WarehouseItem : INotifyPropertyChanged
    {
        public const int WarningQuantity = 10;
        private int _quantity;

        public WarehouseItem(string id, string name, decimal price, int quantity = 0)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public string Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == value) return;
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public decimal TotalPrice => Price * Quantity;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}