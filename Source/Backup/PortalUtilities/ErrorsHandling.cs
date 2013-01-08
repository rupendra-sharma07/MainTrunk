///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Utilities.ErrorsHandling.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to log errors whenever an error occurs within the application
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Text;
using log4net;

namespace TributesPortal.Utilities
{
    public class ErrorsHandling 
    {
        protected static string strLogFilePath = string.Empty;

        static protected ILog Logger = LogManager.GetLogger("LogServiceLogger");
        static protected ILog PaymentLogger = LogManager.GetLogger("LogServicePaymentLogger");
        /// <summary>
        /// TributesPortal Static Constructor
        /// </summary>
        static ErrorsHandling()
        {
            log4net.Config.DOMConfigurator.Configure();
        }

        /// <summary>
        /// Info Add
        /// </summary>
        /// <param name="args"></param>
        static public void Info(params object[] args)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info(BuildLine(args));
            }
        }

        /// <summary>
        /// Error Add
        /// </summary>
        /// <param name="args"></param>
        static public void Error(params object[] args)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(BuildLine(args));
            }
        }

        /// <summary>
        /// Info Add
        /// </summary>
        /// <param name="args"></param>
        static public void PaymentInfo(params object[] args)
        {
            if (PaymentLogger.IsInfoEnabled)
            {
                PaymentLogger.Info(BuildLine(args));
            }
        }

        /// <summary>
        /// Error Add
        /// </summary>
        /// <param name="args"></param>
        static public void PaymentError(params object[] args)
        {
            if (PaymentLogger.IsErrorEnabled)
            {
                PaymentLogger.Error(BuildLine(args));
            }
        }

        /// <summary>
        /// Build Error String
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string BuildLine(object[] args)
        {
            StringBuilder sb = new StringBuilder();

            foreach (object item in args)
            {

                if (item is MethodBase)
                {
                    MethodBase m = (MethodBase)item;
                    sb.Append("[");
                    sb.Append(m.DeclaringType.Name);
                    sb.Append("::");
                    sb.Append(m.Name);
                    sb.Append("]");
                    sb.Append(" ");
                }
                else if (item is Exception)
                {
                    Exception e = (Exception)item;
                    sb.Append("\r\n\t");
                    sb.Append("Error : ");
                    sb.Append(e.GetType().Name);
                    sb.Append(" : ");
                    sb.Append(e.Message);
                    sb.Append("\r\n\t");
                    sb.Append("Stack trace : \r\n");
                    sb.Append(e.StackTrace);
                }
                else if (item is string)
                {
                    sb.Append((string)item);
                    sb.Append(" ");
                }
                else if (item != null)
                {
                    sb.Append(item.ToString());
                    sb.Append(" ");
                }
                else
                {
                    sb.Append("null");
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        #region "OLD CODE"
        //private static StreamWriter sw = null;
        
        ///// <summary>
        ///// Setting LogFile path. If the logfile path is null then it will update error info into LogFile.txt under
        ///// application directory.
        ///// </summary>
        //public  string LogFilePath
        //{
        //    set
        //    {
        //        strLogFilePath = value;
        //    }
        //    get
        //    {
        //        return strLogFilePath;
        //    }
        //}
        //private static Exception _exp = null;

        //public static Exception Exp
        //{
        //    get { return _exp; }
        //    set { _exp = value; }
        //}  
        ///// <summary>
        ///// this function will use to log error .
        ///// </summary>
        ///// <param name="objException"></param>
        //public  void Errorhandle(Exception objException)
        //{
        //    string strGetValue = WebConfig.LoggingEnabled;
            
        //     if (strGetValue == "1")
        //     {
        //         ErrorRoutine(objException);
        //     }
        //     else if (strGetValue == "2")
        //     {
        //         SaveErrorinDB(objException);
        //     }
        //     else if (strGetValue == "3")
        //     {
        //         ErrorRoutine(objException);
        //         SaveErrorinDB(objException);
        //     }
        //     else
        //     {
              
        //         ErrorRoutine(objException);
        //     }

        
        //}


        //public  bool ErrorRoutine(Exception objException)
        //{
        //    try
        //    {          
        //            if (true != CustomErrorRoutine(objException))
        //                return false;
          
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

      

        //private  string GetLoggingStatusConfigFileName()
        //{
        //    string strCheckinBaseDirecotry = AppDomain.CurrentDomain.BaseDirectory + "LoggingStatus.xml";

        //    return strCheckinBaseDirecotry;

        //}

        //private  bool GetValueFromXml(string strXmlPath)
        //{
        //    try
        //    { 
        //        string strGetValue =  WebConfig.LoggingEnabled;
        //        if (strGetValue.Equals("0"))
        //            return false;
        //        else if (strGetValue.Equals("1"))
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //private  bool CustomErrorRoutine(Exception objException)
        //{
        //    string strPathName = string.Empty;
        //    if (strLogFilePath.Equals(string.Empty))
        //    {
        //        //Get Default log file path "LogFile.txt"
        //        strPathName = GetLogFilePath();
        //    }
        //    else
        //    {

        //        //If the log file path is not empty but the file is not available it will create it
        //        if (false == File.Exists(strLogFilePath))
        //        {
        //            if (false == CheckDirectory(strLogFilePath))
        //                return false;

        //            FileStream fs = new FileStream(strLogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //            fs.Close();
        //        }
        //        strPathName = strLogFilePath;

        //    }

        //    bool bReturn = true;
        //    // write the error log to that text file
        //    if (true != WriteErrorLog(strPathName, objException))
        //    {
        //        bReturn = false;
        //    }
        //    return bReturn;
        //}

        //private  bool WriteErrorLog(string strPathName, Exception objException)
        //{
        //    bool bReturn = false;
        //    System.Net.IPAddress[] strClientIP = Dns.GetHostAddresses(Dns.GetHostName());
        //    StateManager obj = StateManager.Instance;
        //    string CLientIP = (string)obj.Get("CLientIP", StateManager.State.ViewState);

        //    try
        //    {
        //        sw = new StreamWriter(strPathName, true);
        //        sw.WriteLine("Source		: " + objException.Source.ToString().Trim());
        //        sw.WriteLine("Method		: " + objException.TargetSite.Name.ToString());
        //        sw.WriteLine("Time		: " + DateTime.Now.ToLongTimeString());
        //        sw.WriteLine("Date		: " + DateTime.Now.ToShortDateString());
        //        sw.WriteLine("Computer Name	: " + Dns.GetHostName().ToString());
        //        sw.WriteLine("Computer IP	: " + strClientIP[0].ToString());
        //        sw.WriteLine("Client IP	: " + CLientIP.ToString());
        //        sw.WriteLine("Error		: " + objException.Message.ToString().Trim());
        //        sw.WriteLine("Stack Trace	: " + objException.StackTrace.ToString().Trim());
        //        sw.WriteLine("-------------------------------------------------------------------");
        //        sw.Flush();
        //        sw.Close();
        //        bReturn = true;
        //    }
        //    catch(Exception ex)
        //    {
        //        bReturn = false;
        //    }
        //    return bReturn;
        //}

        //private  string GetLogFilePath()
        //{
        //    try
        //    {
                
        //        string retFilePath = GetFilePath();
        //        // search the file below the current directory                

        //        // if exists, return the path
        //        if (File.Exists(retFilePath) == true)
        //            return retFilePath;
        //        //create a text file
        //        else
        //        {
        //            if (false == CheckDirectory(retFilePath))
        //                return string.Empty;

        //            FileStream fs = new FileStream(retFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //            fs.Close();
        //        }

        //        return retFilePath;
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }
        //}

        //private  string GetFilePath()
        //{            
        //    string strGetValue = WebConfig.LogFilePath;
        //    return strGetValue;


        //}

        //private  bool CheckDirectory(string strLogPath)
        //{
        //    try
        //    {
        //        int nFindSlashPos = strLogPath.Trim().LastIndexOf("\\");
        //        string strDirectoryname = strLogPath.Trim().Substring(0, nFindSlashPos);

        //        if (false == Directory.Exists(strDirectoryname))
        //            Directory.CreateDirectory(strDirectoryname);

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;

        //    }
        //}

        //private  void SaveErrorinDB(Exception objException)
        //{
            

        //    ErrorResource obje = new ErrorResource();           
        //    System.Net.IPAddress[] strClientIP = Dns.GetHostAddresses(Dns.GetHostName());            
        //    string[] strParam = { "Message", "Source", "Method",
        //                          "Time", "ExceptionName", "CompName", 
        //                          "CompIP" 
        //                        };
        //    DbType[] enumDbType ={ DbType.String,DbType.String,DbType.String, 
        //                            DbType.DateTime,DbType.String,DbType.String,
        //                            DbType.String
        //                         };
        //    object[] objValue ={ objException.Message, objException.Source, objException.TargetSite.Name,
        //                         DateTime.Now.ToString(),objException.GetType().FullName.ToString(), Dns.GetHostName().ToString(),
        //                       strClientIP[0].ToString()
        //                       };
        //    obje.SaveError(strParam, enumDbType, objValue);


        //}
        #endregion
    }
}
