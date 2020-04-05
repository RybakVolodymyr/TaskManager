using System.Diagnostics;

namespace TaskManager.Models
{
    class OtherModel
    {
        public void OpenMoreInfo (ProcessList selectedProcess)
        {
            MoreInfoWindow moreInfoWindow = new MoreInfoWindow (selectedProcess);
            moreInfoWindow.Show ();
        }

        public void OpenFileFolder (ProcessList selectedProcess)
        {
           
 Process.Start (System.IO.Path.GetDirectoryName (selectedProcess.FilePath));
        }

        public void Kill (ProcessList selectedProcess)
        {
            selectedProcess.Process.Kill ();
        }
    }

}
