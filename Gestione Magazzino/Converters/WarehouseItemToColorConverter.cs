using Gestione_Magazzino.Core;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Gestione_Magazzino.Converters
{
    public class WarehouseItemToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int quantity = ((WarehouseItem)value).Quantity;
            if (quantity == 0) return Brushes.Red;
            return quantity <= WarehouseItem.WarningQuantity ? Brushes.Orange : Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}