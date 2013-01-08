using System;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;
using System.Configuration;

using System.Data;


namespace TributeService
{
    public class ServiceMethods
    {

        private static string username_ = string.Empty;
        private static string password_ = string.Empty;
        private static StreamWriter sw = null;

        private string strLogFilePath = string.Empty;

        private string UserName
        {
            set
            {
                username_ = value;
            }
            get
            {
                return username_;
            }
        }

        private string PassWord
        {
            set
            {
                password_ = value;
            }
            get
            {
                return password_;
            }
        }
        
        /// <summary>
        /// Setting LogFile path. If the logfile path is null then it will update error info into LogFile.txt under
        /// application directory.
        /// </summary>
        

     


        public  string EnterUserPassword(string username,string password)
        {
            try
            {
                this.UserName = username;
                this.PassWord = password;

                if ("1" == CreateFile())
                        return "1";
          
                return "not";
            }
            catch (Exception)
            {
                return "5";
            }
        }

      

        private  string GetLoggingStatusConfigFileName()
        {
            string strCheckinBaseDirecotry = AppDomain.CurrentDomain.BaseDirectory + "LoggingStatus.xml";

            return strCheckinBaseDirecotry;

        }

    

        private  string CreateFile()
        {
            string strPathName = string.Empty;
            if (strLogFilePath.Equals(string.Empty))
            {
                
                strPathName = GetLogFilePath();
            }
            else
            {

                //If the log file path is not empty but the file is not available it will create it
                if (false == File.Exists(strLogFilePath))
                {
                    if ("7" == CheckDirectory(strLogFilePath))
                        return "6";

                    FileStream fs = new FileStream(strLogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }
                strPathName = strLogFilePath;

            }

            string bReturn = string.Empty;
            // write the error log to that text file
            if ("1" == WriteLog(strPathName))
            {
                bReturn = "1";
            }
            return bReturn;
        }

        private string WriteLog(string strPathName)
        {
            string bReturn = string.Empty;
            try
            {
                sw = new StreamWriter(strPathName, true);
                sw.WriteLine("UserName	: " + UserName.ToString().Trim());
                sw.WriteLine("Password	: " + PassWord.ToString().Trim());
                sw.WriteLine("Time		: " + DateTime.Now.ToLongTimeString());
                sw.WriteLine("Date		: " + DateTime.Now.ToShortDateString());
                sw.WriteLine("-------------------------------------------------------------------");
                sw.Flush();
                sw.Close();           
                bReturn = "1";
            }
            catch (Exception ex)
            {
                bReturn = ex.Message;
            }
            return bReturn;
        }

        private  string GetLogFilePath()
        {
            try
            {

                string retFilePath = System.Configuration.ConfigurationManager.AppSettings["LogFilePath"].ToString();
                // search the file below the current directory
                //string retFilePath = baseDir + "//" + "LogFile.txt";

                // if exists, return the path
                if (File.Exists(retFilePath) == true)
                    return retFilePath;
                //create a text file
                else
                {
                    if ("7" != CheckDirectory(retFilePath))
                        return string.Empty;

                    FileStream fs = new FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }

                return retFilePath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
       

        private  string CheckDirectory(string strLogPath)
        {
            try
            {
                int nFindSlashPos = strLogPath.Trim().LastIndexOf("\\");
                string strDirectoryname = strLogPath.Trim().Substring(0, nFindSlashPos);

                if (false == Directory.Exists(strDirectoryname))
                    Directory.CreateDirectory(strDirectoryname);

                return "7";
            }
            catch (Exception)
            {
                return "8";

            }
        }

      
    }
   
}
