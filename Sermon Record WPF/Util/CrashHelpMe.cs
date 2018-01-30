using System;
using System.Diagnostics;

namespace Sermon_Record.UTIL
{
    internal static class CrashHelpMe
    {
        public static void CreateCrashReport(Exception _e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            string filename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "_ErrorReport_"
                + DateTime.Today.ToString("yyyyMMdd_HHmmss") + ".txt";

            string report = "*** Please include this report when reporting your issue ***\n";
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            report += string.Format("Version: {0}", fvi.FileVersion);
            report += string.Format("ProductVersion: {0}", fvi.ProductVersion);
            report += string.Format("IsDebug: {0}", fvi.IsDebug);

            Exception this_exception = _e;
            int i = 0;
            while (this_exception != null)
            {
                i++;

                report += string.Format("\n\n================ #{0} ================\n", i);
                report += this_exception.ToString();
                this_exception = this_exception.InnerException;
            }
            
            System.IO.File.WriteAllText(System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename), report);

        }
    }
}