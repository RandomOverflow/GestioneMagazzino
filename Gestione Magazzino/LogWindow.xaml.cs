using Gestione_Magazzino.Core.Logging;

namespace Gestione_Magazzino
{
    /// <summary>
    ///     Logica di interazione per LogWindow.xaml
    /// </summary>
    public partial class LogWindow
    {
        public LogWindow()
        {
            InitializeComponent();
            DataGridLog.ItemsSource = Logger.LogData;
        }
    }
}