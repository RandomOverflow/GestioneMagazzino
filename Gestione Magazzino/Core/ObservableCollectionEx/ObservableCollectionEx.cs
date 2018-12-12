using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;


namespace Gestione_Magazzino.Core
{
    public class ObservableCollectionEx<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            Unsubscribe(e.OldItems);
            Subscribe(e.NewItems);
            base.OnCollectionChanged(e);
        }

        protected override void ClearItems()
        {
            foreach (T element in this)
                element.PropertyChanged -= ContainedElementChanged;

            base.ClearItems();
        }

        private void Subscribe(IEnumerable iList)
        {
            if (iList == null) return;
            foreach (T element in iList)
                element.PropertyChanged += ContainedElementChanged;
        }

        private void Unsubscribe(IEnumerable iList)
        {
            if (iList == null) return;
            foreach (T element in iList)
                element.PropertyChanged -= ContainedElementChanged;
        }

        private void ContainedElementChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventArgsEx ex = new PropertyChangedEventArgsEx(e.PropertyName, sender);
            OnPropertyChanged(ex);
        }
    }
}
