using Gestione_Magazzino.Core.Logging;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Gestione_Magazzino.Converters
{
    public class MessageToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Message.EventTypes)value)
            {
                case Message.EventTypes.Errore:
                    return Brushes.Red;

                case Message.EventTypes.Info:
                    return Brushes.Black;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}