using System.Windows.Controls;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for ProcessListView.xaml
    /// </summary>
    public partial class ProcessListView 
    {
        public ProcessListView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

        }
    }
}
