using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MoreInfoWindow.xaml
    /// </summary>
    public partial class MoreInfoWindow 
    {
        public MoreInfoWindow(ProcessList process)
        {
            InitializeComponent();
            DataContext = new MoreViewModel(process);

        }
    }
}
