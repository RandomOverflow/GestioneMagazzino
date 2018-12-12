using Gestione_Magazzino.Core;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Gestione_Magazzino
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private DispatcherTimer _timer;
        private decimal _totalPrice;

        public MainWindow()
        {
            InitializeComponent();
        }

        private ObservableCollectionEx<WarehouseItem> ShoppingItems { get; set; }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.InitializeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la connessione al Database: " + Environment.NewLine + ex.Message,
                    "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RefreshWarehouse();
            ShoppingItems = new ObservableCollectionEx<WarehouseItem>();
            DataGridShopping.ItemsSource = ShoppingItems;
            _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1, 0) };
            _timer.Tick += TimerOnTick;
            DataContext = this;
        }

        private void RefreshWarehouse()
        {
            ListViewWarehouse.Items.Clear();

            DataTable warehouseContent;
            try
            {
                warehouseContent = DB.GetWarehouseContent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la connessione al Database: " + Environment.NewLine + ex.Message,
                    "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (object row in warehouseContent.Rows)
            {
                DataRow r = (DataRow)row;

                ListViewWarehouse.Items.Add(new WarehouseItem((string)r.ItemArray[0], (string)r.ItemArray[1],
                    (decimal)r.ItemArray[2], (int)r.ItemArray[3]));
            }
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            string code = TextBoxBarcode.Text;
            _timer.Stop();
            TextBoxBarcode.Clear();

            WarehouseItem warehouseItemDb;

            try
            {
                warehouseItemDb = DB.GetWarehouseItemById(code);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la connessione al Database: " + Environment.NewLine + ex.Message,
                    "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (warehouseItemDb == null)
            {
                //L'elemento con quel codice non esiste nel DB

                MessageBoxResult msgBoxResult =
                    MessageBox.Show("Elemento con Id " + code + " non registrato in Magazzino." + Environment.NewLine +
                                    "Desidera registrarlo?", "Informazione", MessageBoxButton.YesNo,
                        MessageBoxImage.Information, MessageBoxResult.Yes);

                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    AddElementWindow addElementWindow = new AddElementWindow(code) { Owner = this };
                    addElementWindow.ShowDialog();
                    RefreshWarehouse();
                }
            }
            else
            {
                //L'elemento con quel codice ESISTE nel DB
                //Ne controllo la quantità presente

                WarehouseItem selectedWareHouseItem = ShoppingItems.FirstOrDefault(x => x.Id == warehouseItemDb.Id);

                if (selectedWareHouseItem == null)
                {
                    if (warehouseItemDb.Quantity <= 0)
                    {
                        MessageBox.Show("Elemento " + warehouseItemDb.Name + " (ID: " + warehouseItemDb.Id +
                                        ") non disponibile in Magazzino per la quantità desiderata.");
                    }
                    else
                    {
                        warehouseItemDb.Quantity = 1;
                        ShoppingItems.Add(warehouseItemDb);
                        TotalPrice += warehouseItemDb.Price;
                    }
                }
                else
                {
                    if (warehouseItemDb.Quantity < selectedWareHouseItem.Quantity + 1)
                    {
                        MessageBox.Show("Elemento " + warehouseItemDb.Name + " (ID: " + warehouseItemDb.Id +
                                        ") non disponibile in Magazzino per la quantità desiderata.");
                    }
                    else
                    {
                        selectedWareHouseItem.Quantity++;
                        TotalPrice += warehouseItemDb.Price;
                    }
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewWarehouse.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Selezionare gli elementi da eliminare dal Magazzino.", "Elimina", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            WarehouseItem[] selectedItems = new WarehouseItem[ListViewWarehouse.SelectedItems.Count];

            ListViewWarehouse.SelectedItems.CopyTo(selectedItems, 0);

            foreach (WarehouseItem selectedItem in selectedItems)
            {
                try
                {
                    DB.DeleteWarehouseItemById(selectedItem.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore durante la connessione al Database: " + Environment.NewLine + ex.Message,
                        "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ListViewWarehouse.Items.Remove(selectedItem);
            }
        }

        private void ButtonConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ShoppingItems.Count <= 0) return;
            try
            {
                DB.SubtractItemsFromWarehouse(ShoppingItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la connessione al Database: " + Environment.NewLine + ex.Message,
                    "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                DB.RegisterOrder(ShoppingItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la connessione al Database: " + Environment.NewLine + ex.Message,
                    "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ShoppingItems.Clear();
            RefreshWarehouse();
            TotalPrice = 0;
        }

        private void MenuItemOrders_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow ordersWindow = new OrdersWindow { Owner = this };
            ordersWindow.ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            DB.CloseConnection();
        }

        private void ButtonRemoveItem_OnClick(object sender, RoutedEventArgs e)
        {
            //DA FARE
        }

        private void ButtonCancelOrder_OnClick(object sender, RoutedEventArgs e)
        {
            ShoppingItems.Clear();
            TotalPrice = 0;
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            AddElementWindow addElementWindow = new AddElementWindow { Owner = this };
            addElementWindow.ShowDialog();
            RefreshWarehouse();
        }

        private void ButtonModify_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListViewWarehouse.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Selezionare l'elemento del Magazzino da modificare.", "Modifica", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            AddElementWindow addElementWindow = new AddElementWindow("", false,
                (WarehouseItem)ListViewWarehouse.SelectedItems[0])
            { Owner = this };
            addElementWindow.ShowDialog();
            RefreshWarehouse();
        }

        private void MenuItemLog_OnClick(object sender, RoutedEventArgs e)
        {
            LogWindow logWindow = new LogWindow { Owner = this };
            logWindow.ShowDialog();
        }

        private void TextBoxBarcode_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxBarcode.Text.Length > 0) _timer.Start();
        }
    }
}