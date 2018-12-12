using Gestione_Magazzino.Core;
using System;
using System.Globalization;
using System.Windows;

namespace Gestione_Magazzino
{
    /// <summary>
    ///     Logica di interazione per AddElementWindow.xaml
    /// </summary>
    public partial class AddElementWindow
    {
        public AddElementWindow(string insertedId = "", bool addElement = true,
            WarehouseItem warehouseItemToModify = null)
        {
            InitializeComponent();
            AddElement = addElement;

            if (!AddElement)
            {
                TextBoxCode.IsReadOnly = true;

                if (warehouseItemToModify == null) return;
                TextBoxName.Text = warehouseItemToModify.Name;
                TextBoxCode.Text = warehouseItemToModify.Id;
                TextBoxPrice.Text = warehouseItemToModify.Price.ToString("0.00");
                TextBoxQuantity.Text = warehouseItemToModify.Quantity.ToString();
                TextBoxName.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(insertedId))
                {
                    TextBoxCode.Focus();
                }
                else
                {
                    TextBoxCode.Text = insertedId;
                    TextBoxCode.IsReadOnly = true;
                    TextBoxName.Focus();
                }
            }
        }

        private bool AddElement { get; }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            int quantity;
            decimal price;
            if (decimal.TryParse(TextBoxPrice.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out price) &&
                int.TryParse(TextBoxQuantity.Text, out quantity) &&
                !string.IsNullOrEmpty(TextBoxCode.Text) && !string.IsNullOrEmpty(TextBoxName.Text))
            {
                if (AddElement)
                {
                    try
                    {
                        DB.AddItemToWarehouse(TextBoxCode.Text, TextBoxName.Text, price, quantity);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore Database: " + ex.Message, "Errore", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }

                    Close();
                }
                else
                {
                    try
                    {
                        DB.ModifyWarehouseItem(TextBoxCode.Text, TextBoxName.Text, price, quantity);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Errore Database: " + ex.Message, "Errore", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }

                    Close();
                }
            }
            else
            {
                MessageBox.Show("Informazioni inserite non corrette.", "Errore", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}