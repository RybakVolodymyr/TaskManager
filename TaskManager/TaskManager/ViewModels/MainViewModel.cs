using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using TaskManager.Models;
using TaskManager.Properties;
using TaskManager.Tools;

namespace TaskManager.ViewModels
{
    class MainViewModel: INotifyPropertyChanged
    {
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        private Thread _workingThread;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private ICommand _killCommand;
        private ICommand _moreInfoCommand;
        private ICommand _openFileFolderCommand;

        #region Properties
        public ObservableCollection<ProcessList> Processes { get; private set; }
        public ProcessList SelectedProcess { get; set; }

        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        private OtherModel OtherModel   
        {
            get;
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            OtherModel = new OtherModel();

            Processes = new ObservableCollection<ProcessList>();
            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    Processes.Add(new ProcessList(process));
                }
                catch (Exception)
                
                {
                    
                }
            }

            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            StationManager.StopThreads += StopWorkingThread;

            IsControlEnabled = true;
            Thread.Sleep(5000);

            StartWorkingThread();
        }


        #endregion

        #region Commands
        public ICommand OpenFileFolderCommand
        {
            get
            {
                if (_openFileFolderCommand == null)
                {
                    _openFileFolderCommand = new RelayCommand<object>(OpenFolderExecute, OpenFolderCanExecute);
                }
                return _openFileFolderCommand;
            }
            set
            {
                _openFileFolderCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand KillCommand
        {
            get
            {
                if (_killCommand == null)
                {
                    _killCommand = new RelayCommand<object>(KillExecute, KillCanExecute);
                }
                return _killCommand;
            }
            set
            {
                _killCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenMoreInfoCommand
        {
            get
            {
                if (_moreInfoCommand == null)
                {
                    _moreInfoCommand = new RelayCommand<object>(OpenMoreInfoExecute, MoreInfoCanExecute);
                }
                return _moreInfoCommand;
            }
            set
            {
                _moreInfoCommand = value;
                OnPropertyChanged();
            }
        }
        private bool OpenFolderCanExecute(object obj)
        {
            return true;
        }
        private bool KillCanExecute(object obj)
        {
            return true;
        }
        private bool MoreInfoCanExecute(object obj)
        {
            return true;
        }
        private void OpenFolderExecute(object obj)
        {
            OtherModel.OpenFileFolder(SelectedProcess);
        }
        private void KillExecute(object obj)
        {
            OtherModel.Kill(SelectedProcess);
        }

        private void OpenMoreInfoExecute(object obj)
        {
            OtherModel.OpenMoreInfo(SelectedProcess);
        }


        #endregion

        private void StartWorkingThread()
        {
            _workingThread = new Thread(WorkingThreadProcess);
            _workingThread.Start();
        }

        private void WorkingThreadProcess()
        {

            while (!_token.IsCancellationRequested)
            {
                for (int j = 0; j < 5; j++)
                {
                    Thread.Sleep(1000);
                    if (_token.IsCancellationRequested)
                        break;
                }
                if (_token.IsCancellationRequested)
                    break;

                IsControlEnabled = false;

                var all = Process.GetProcesses().Select(p => p.Id).ToList();
                var currentProcesses = Processes.Select(p => p.Id).ToList();
                var newProcesses = all.Except(currentProcesses).ToList();
                var redundantProcesses = currentProcesses.Except(all).ToList();

                foreach (int id in newProcesses)
                {
                    try
                    {
                        App.Current.Dispatcher.Invoke(delegate
                        {
                            Processes.Add(new ProcessList(Process.GetProcessById(id)));
                        });
                    }
                    catch (Exception)
                    {
                    }
                }
                foreach (int id in redundantProcesses)
                {
                    try
                    {
                        var process = Processes.First(x => x.Id == id);

                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            process.Killed = true;
                            Processes.RemoveAt(Processes.IndexOf(process));
                        });
                    }
                    catch (Exception)
                    {
                    }
                }

                IsControlEnabled = true;
            }
        }

        private void StopWorkingThread()
        {
            _tokenSource.Cancel();
            _workingThread.Join(2000);
            _workingThread.Abort();
            _workingThread = null;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
