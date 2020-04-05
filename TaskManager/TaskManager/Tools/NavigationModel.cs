using System;
using TaskManager.Views;

namespace TaskManager.Tools
{
    public enum ModesEnum
    {
        Main,
    }

    class NavigationModel
    {
        private IContentWindow _contentWindow;
        private ProcessListView _mainView;

        public NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
            _mainView = new ProcessListView();
        }

        public void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.Main:
                    _contentWindow.ContentControl.Content = _mainView;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}
