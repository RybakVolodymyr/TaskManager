using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using TaskManager.Properties;
using System.Management;
using TaskManager.Tools;


namespace TaskManager.Models
{
    public class ProcessList : INotifyPropertyChanged
    {
        #region Properties
        private bool _isActive;
        private double _cpu;
        private double _ram;
        private int _threadsN;
        private bool _killed;

        private Thread _workingThread;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        #endregion

        public ProcessList (Process systemProcess)
        {
            Process = systemProcess;
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StationManager.StopThreads += StopWorkingThread;
            _killed = false;
            Name = systemProcess.ProcessName;
            Id = systemProcess.Id;
            Username = GetProcessOwner (Id);
            FilePath = systemProcess.MainModule.FileName;
            StartedDateTime = systemProcess.StartTime;
            StartWorkingThread();


        }

        public string Name
        {
            get;
        }

        public int Id
        {
            get;
        }

        public string Username
        {
            get;
        }

        public string FilePath
        {
            get;
        }

        public DateTime StartedDateTime
        {
            get;
        }

        public Process Process
        {
            get;
        }
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            private set
            {
                _isActive = value;
                OnPropertyChanged();

            }
        }

        public double Cpu
        {
            get
            {
                return _cpu;
            }
            private set
            {
                _cpu = value;
                OnPropertyChanged();

            }
        }

        public double Ram
        {
            get
            {
                return _ram;
            }
            private set
            {
                _ram = value;
                OnPropertyChanged();

            }
        }

        public int ThreadsNumber
        {
            get
            {
                return _threadsN;
            }
            set
            {
                _threadsN = value;
                OnPropertyChanged();

            }
        }

        public bool Killed
        {
            get
            {
                return _killed;
            }
            set
            {
                _killed = value;
                StopWorkingThread();
                StationManager.StopThreads -= StopWorkingThread;
            }
        }
        private void StartWorkingThread()
        {
            _workingThread = new Thread(WorkingThreadProcess);
            _workingThread.Start();
        }
        internal void StopWorkingThread()
        {
            _tokenSource.Cancel();
            _workingThread.Join(2000);
            _workingThread.Abort();
            _workingThread = null;
        }

        private void WorkingThreadProcess ()
        {
            while (!_token.IsCancellationRequested)
            {
                try
                {
                    var cpuCounter = new PerformanceCounter ("Process", "% Processor Time", Process.ProcessName);
                    var ramCounter = new PerformanceCounter("Process", "Working Set", Process.ProcessName);
                    cpuCounter.NextValue ();
                    Cpu = Math.Round((cpuCounter.NextValue () / Environment.ProcessorCount)*2, 2);
                    Ram = Math.Round((ramCounter.NextValue () / 1024 / 1024), 2);
                    IsActive = Process.Responding;
                    ThreadsNumber = Process.Threads.Count;
                }catch (Exception)
                {
                }

            }
        }
        private static string GetProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    return argList[1] + "\\" + argList[0];
                }
            }

            return "----";
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
