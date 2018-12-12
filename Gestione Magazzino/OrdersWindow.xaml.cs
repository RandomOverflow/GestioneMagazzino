using Gestione_Magazzino.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gestione_Magazzino
{
    /// <summary>
    ///     Logica di interazione per OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow
    {
        public OrdersWindow()
        {
            InitializeComponent();
        }

        private List<Order> Orders { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Orders = DB.GetOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante la connessione al Database: " + Environment.NewLine + ex.Message,
                    "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            ListViewOrders.ItemsSource = Orders;
            ComboBoxYears.Items.Add("");
            ComboBoxMonths.Items.Add("");
            foreach (Order order in Orders)
            {
                if (!ComboBoxYears.Items.Contains(order.DateTime.Date.Year))
                {
                    ComboBoxYears.Items.Add(order.DateTime.Date.Year);
                }

                if (
                    !ComboBoxMonths.Items.Contains(order.DateTime.Date.Month + " (" + order.DateTime.ToString("MMM") +
                                                   ")"))
                {
                    ComboBoxMonths.Items.Add(order.DateTime.Month + " (" + order.DateTime.ToString("MMM") + ")");
                }
            }
        }

        private void ListViewOrders_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewOrders.SelectedItems.Count > 0)
            {
                ExpanderMain.IsExpanded = true;
                ListViewItems.ItemsSource = ((Order)ListViewOrders.SelectedItems[0]).Items;
            }
            else
            {
                ExpanderMain.IsExpanded = false;
            }
        }

        private void Filter()
        {
            if (ComboBoxMonths.SelectedItem == null || ComboBoxYears.SelectedItem == null) return;
            if (ComboBoxMonths.SelectedIndex == 0)
            {
                if (ComboBoxYears.SelectedIndex == 0)
                {
                    //VISUALIZZA TUTTO / NESSUN FILTRO
                    ListViewOrders.ItemsSource = Orders;
                }
                else
                {
                    //FILTRA ANNI
                    ListViewOrders.ItemsSource =
                        Orders.Where(
                            x =>
                                x.DateTime.Year.ToString() == ComboBoxYears.SelectedItem.ToString());
                }
            }
            else
            {
                if (ComboBoxYears.SelectedIndex == 0)
                {
                    //FILTRA MESI
                    ListViewOrders.ItemsSource =
                        Orders.Where(
                            x =>
                                x.DateTime.Month.ToString() == ComboBoxMonths.SelectedItem.ToString().Split(' ')[0]);
                }
                else
                {
                    //FILTRA SIA MESI CHE ANNI
                    ListViewOrders.ItemsSource =
                        Orders.Where(
                            x =>
                                x.DateTime.Year.ToString() == ComboBoxYears.SelectedItem.ToString() &&
                                x.DateTime.Month.ToString() == ComboBoxMonths.SelectedItem.ToString().Split(' ')[0]);
                }
            }
        }

        private void ComboBoxYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ComboBoxMonths_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void MenuItemExport_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Resoconto",
                Filter = "Excel File |*.xlsx"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                OrderSaver orderSaver = new OrderSaver(ListViewOrders.ItemsSource.Cast<Order>(), saveFileDialog.FileName);
                try
                {
                    orderSaver.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore durante il salvataggio del File: " + ex.Message);
                }
            }
        }
    }
}