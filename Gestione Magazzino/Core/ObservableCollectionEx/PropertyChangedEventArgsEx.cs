using System.ComponentModel;

namespace Gestione_Magazzino.Core
{
    public class PropertyChangedEventArgsEx : PropertyChangedEventArgs
    {
        public object Sender { get; private set; }

        public PropertyChangedEventArgsEx(string propertyName, object sender)
            : base(propertyName)
        {
            Sender = sender;
        }
    }
}
