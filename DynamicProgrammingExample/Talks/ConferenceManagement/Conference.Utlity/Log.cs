using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Common
{
    public class Log
    {
        private static string folderPath = AppDomain.CurrentDomain.BaseDirectory;
        private static object _lock = new object();

        public static void LogInfo(string messsage)
        {
            lock (_lock)
            {
                if (!File.Exists(folderPath + @"\\" + "Log" + ".txt"))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(folderPath + @"\\" + "Log" + ".txt"))
                    {
                        sw.WriteLine(DateTime.Now + ":" + messsage + Environment.NewLine);
                    }
                }

                else
                {
                    using (StreamWriter sw = File.AppendText(folderPath + @"\\" + "Log" + ".txt"))
                    {
                        sw.WriteLine(DateTime.Now + ":" + messsage + Environment.NewLine);
                    }
                }
            }
           
        }
    }
}
