#region USING DIRECTIVES
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using TributeService.BusinessEntities;
using TributeService.BusinessLogic;
using TributePortalSecurity;
#endregion

namespace TributeService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Service : System.Web.Services.WebService
    {
        [WebMethod]
        public int CheckForLogin(string username, string password)
        {
            return CheckUser(username, password);
            //Users objUser = new Users();
            //UserManager objUserManager = new UserManager();
            //objUser.UserName = userName;
            //objUser.Password = Security.EncryptSymmetric(password);
            //return objUserManager.CheckForLogin(objUser);
        }


        [WebMethod]
        public string UpdateVideoStatus(string username, string password, string fileName)
        {
            int userId = CheckUser(username, password);
            if (userId > 0)
            {
                VideoStatus objStatus = new VideoStatus();
                objStatus.CreatedBy = userId;
                objStatus.CreatedDate = DateTime.Now;
                objStatus.FileName = fileName;
                objStatus.IsActive = true;
                objStatus.IsDeleted = false;
                objStatus.Status = true;
                objStatus.UserId = userId;

                VideoUploadManager objVideoUploadManager = new VideoUploadManager();
                return objVideoUploadManager.UpdateVideoStatus(objStatus);
            }
            else
            {
                return "error";
            }
        }
        /*[WebMethod]
        public string Login(string strUsername, string strPassword)
        {
            string strPathName = @"D:\Userlogin.txt";
            string resp="done";
            try
            {
//            ServiceMethods obje = new ServiceMethods();
            StreamWriter sw =  new StreamWriter(strPathName, true);
            sw.WriteLine("UserName	: " + strUsername.ToString().Trim());
            sw.WriteLine("Password	: " + strUsername.ToString().Trim());
            sw.WriteLine("Time		: " + DateTime.Now.ToLongTimeString());
            sw.WriteLine("Date		: " + DateTime.Now.ToShortDateString());
            sw.WriteLine("-------------------------------------------------------------------");
            sw.Flush();
            sw.Close();
                return resp;
            }
            catch (Exception ex)
            {
              return ex.Message;
            }
           
         //   string val = obje.EnterUserPassword(strUsername, strPassword);
           // return val;
            //if ((strUsername == "sopra") && (strPassword == "123"))
            //    return true;
            //else
            //    return false;

        }*/


        public string GetVideoPath(string userName, string password)
        {
            Users objUser = new Users();
            UserManager objUserManager = new UserManager();
            objUser.UserName = userName;
            objUser.Password = Security.EncryptSymmetric(password);
            if (objUserManager.CheckForLogin(objUser) > 0)
            {
                string path = GetPath(userName);
                DirectoryInfo objDir = new DirectoryInfo(path);
                if (!objDir.Exists)
                    objDir.Create();
                return path;
                //return "<tributeservice><path>c:\\</path><pid>100</pid></tributeservice>";
            }
            else
            {
                return "error";
            }
        }

        public bool SetVideoUploadStatus(string pid, string status)
        {
            return true;
        }

        /// <summary>
        /// Method to get the path where the file is to be uploaded.
        /// It gets the information from  the XML file
        /// </summary>
        /// <param name="userName">Logged in user name</param>
        /// <returns>Full path of the folder where the file is to be placed.</returns>
        private string GetPath(string userName)
        {
            string videoFilesFolderPath = "";
            DataSet dsConfig = new DataSet();
            dsConfig.ReadXml(ConfigFileFullPath());

            if (dsConfig.Tables.Count > 0)
            {
                string strServeName = dsConfig.Tables["StorageDrive"].Rows[0]["DriveNameOnApplicationServer"].ToString();
                string strNMMRootFolder = dsConfig.Tables["StorageDrive"].Rows[0]["TributePortalRootFolder"].ToString();
                string strVideoFilesFolder = dsConfig.Tables["StorageDrive"].Rows[0]["VideoFilesFolder"].ToString();
                videoFilesFolderPath = strServeName + "\\" + strNMMRootFolder + "\\" + strVideoFilesFolder + "\\" + userName;
            }
            return videoFilesFolderPath;
            //return HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + HttpContext.Current.Request.ApplicationPath + "videos/" + userName + "/";
        }

        /// <summary>
        /// Method to read the Config file for getting the path
        /// </summary>
        /// <returns>Path where the Config file containing pah info is placed.</returns>
        private static string ConfigFileFullPath()
        {
            string strFilePath = System.AppDomain.CurrentDomain.BaseDirectory;
            //strFilePath = strFilePath.Substring(0, strFilePath.LastIndexOf("\\"));
            //strFilePath = strFilePath.Substring(0, strFilePath.LastIndexOf("\\") + 1);
            strFilePath += "Resources/XmlFiles/VideoConfiguration.xml";

            return strFilePath;
        }

        /// <summary>
        /// Method to check username and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private int CheckUser(string userName, string password)
        {
            Users objUser = new Users();
            UserManager objUserManager = new UserManager();
            objUser.UserName = userName;
            objUser.Password = Security.EncryptSymmetric(password);
            return objUserManager.CheckForLogin(objUser);
        }
    }
}
