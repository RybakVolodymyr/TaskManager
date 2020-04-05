using System;

namespace TaskManager.Tools
{
    public static class StationManager
    {
        public static event Action StopThreads;

        public static void InvokeStopThreads()
        {
            try
            {
                StopThreads?.Invoke();
            }
            catch { }
        }


    }
}
