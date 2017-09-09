
using Microsoft.VisualBasic; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogWriter
{
    public class LogWriter
    {
        public static bool DebugMode;
        public static string Context;
        public static object SyncObject;

        private static string strLogPath;
        private static string strLastLine;

        public static void WriteLog(string strSource, string strMessage)
        {
            if (DebugMode)
            {
                OutputToLog(strSource, strMessage);
            }
            else
            {
                strLastLine = strSource + "~" + strMessage;
            }
        }

        public static void WriteError(string strSource, string strMessage)
        {
            OutputToLog(strSource, strMessage + "| LastLine[" + strLastLine + "]");
        }

        private static void OutputToLog(string strSource, string strMessage)
        {
            string strLogFile = null;
            System.IO.StreamWriter ts = default(System.IO.StreamWriter);

            lock ((SyncObject))
            {
                try
                {
                    if (string.IsNullOrEmpty(strLogPath))
                    {
                        strLogPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Log\\";
                        if (!System.IO.Directory.Exists(strLogPath))
                        {
                            System.IO.Directory.CreateDirectory(strLogPath);
                        }
                    }

                    strLogFile = (strLogPath) + String.Format(DateTime.Now, "yyyy-MM-dd") + "_";

                    ts = new System.IO.StreamWriter(strLogFile, true);


                    ts.Close();
                }
                catch (Exception ex)
                {
                    try
                    {
                        if ((ts != null))
                        {
                            ts.Close();
                        }
                    }
                    catch (Exception ex2) { }
                }
            }
        }
    }
}
