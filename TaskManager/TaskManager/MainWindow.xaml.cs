using System.Windows.Controls;
using TaskManager.Tools;
using TaskManager.ViewModels;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentWindow
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }
        public MainWindow()
        {
            InitializeComponent();
            //MainWindow contentWindow = new MainWindow();
           // DataContext = new MainViewModel();
            NavigationModel navigationModel = new NavigationModel(this);
            NavigationManager.Instance.Initialize(navigationModel);
          
            navigationModel.Navigate(ModesEnum.Main);

        }
    }
}
