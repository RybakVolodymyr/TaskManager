using System.Diagnostics;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    public class MoreViewModel
    {
        public ProcessModuleCollection Modules
        {
            get;
        }

        public ProcessThreadCollection Threads
        {
            get;
        }

        public MoreViewModel(ProcessList process)
        {
            Threads = process.Process.Threads;
            Modules = process.Process.Modules;

        }

    }
}
