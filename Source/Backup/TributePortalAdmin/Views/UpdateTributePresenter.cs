#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.TributePortalAdmin.Views;
using TributesPortal.Utilities;
using System.IO;

#endregion

namespace TributesPortal.TributePortalAdmin.Views
{
    public class UpdateTributePresenter : Presenter<IUpdateTribute>
    {
        #region CLASS VARIABLES
        private TributePortalAdminController _controller;
        #endregion

        public UpdateTributePresenter([CreateNew] TributePortalAdminController controller)
        {
            _controller = controller;
        }

        public void GetTributeDetailsOnTributeId()
        {
            this.View.objUpdateTribute = _controller.GetTributeDetailsOnTributeId(this.View.TributeId);
        }

        public void UpDateTributeExpiryDate()
        {
            this.View.UpdateStatus = _controller.UpdateTributeExpiry(this.View.objUpdateTribute);
        }

        public void UpdateAdminTributeUpdate()
        {
            _controller.UpdateAdminTributeUpdate(this.View.objAdminTributeUpdate);
        }

        public void UpdateTributePackage()
        {
            this.View.UpdateStatus = _controller.UpdateTributePackage(this.View.objUpdateTribute);
        }

        public void GetAdminTransactions()
        {
            this.View.objAdminTributeUpdateList = _controller.GetAdminTransactions();
        }

        public bool IsNewURLValid(Tributes objTribute)
        {
            return _controller.IsNewTypeURLValid(objTribute);
        }

        /// <summary>
        ///  Method to create a folder in the root directory with TributeUrl as Folder name containing default.aspx file.
        /// </summary>
        /// <param name="applicationPath">Application physical path.</param>
        public void CreateDefaultFolder(string applicationPath, string _newTributeUrl)
        {
            //to create complete file path by appending TributeUrl with application path
            string filePath = applicationPath + _newTributeUrl;
            //to get File name from web config file
            string fileName = WebConfig.DefaultFileName;
            //to create full path where the file is to be copied
            string fullPath = filePath + "\\" + fileName;
            //to create directory for tribute with tributeurl as folder name.
            DirectoryInfo objDir = new DirectoryInfo(filePath);
            if (!objDir.Exists) //if directory does not exists creates a directory
                objDir.Create();

            //to create Default.aspx file in the folder
            if (!File.Exists(fullPath)) //if file does not exists creates a file
                File.Copy(applicationPath + WebConfig.DefaultFolderUrl_ToGetDefaultFile, fullPath, false);
        }

        public void CopyOldURlFolderToNewURLFolder(string sourceFolder, string destFolder)
        {
            try
            {
                int CopyIteration = 0;
                int MaxCopyIteration = 0; //= 5;
                int.TryParse(WebConfig.VideoFileCopyIteration.ToString(), out MaxCopyIteration);
                if (Directory.Exists(sourceFolder))
                {
                    if (!Directory.Exists(destFolder))
                        Directory.CreateDirectory(destFolder);

                    string[] files = Directory.GetFiles(sourceFolder);
                    foreach (string file in files)
                    {
                        CopyIteration = 0;
                        string name = Path.GetFileName(file);
                        string dest = Path.Combine(destFolder, name);

                        //LHK : Added check MaxCopyIteration times if Video Copied successfully.
                        while (CopyIteration < MaxCopyIteration)
                        {
                            if (!(File.Exists(dest)) && (File.Exists(file)))
                            {
                                File.Copy(file, dest);
                                //Delete the old file
                                if (File.Exists(dest))
                                    File.Delete(file);
                            }
                            else
                            {
                                CopyIteration = 5;
                            }
                            CopyIteration++;

                        }
                    }
                    string[] folders = Directory.GetDirectories(sourceFolder);
                    foreach (string folder in folders)
                    {
                        string name = Path.GetFileName(folder);
                        string dest = Path.Combine(destFolder, name);
                        CopyOldURlFolderToNewURLFolder(folder, dest);
                    }
                }
                //delete the source folder.
                if (Directory.Exists(destFolder))
                    DeleteDirectory(sourceFolder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeleteDirectory(string target_dir)
        {
            try
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    if (Directory.Exists(dir))
                        DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateNewTributeUrlTributeTypeinAlltables(UpdateTribute _objUpdateTribute, Tributes _newTribute)
        {
            return _controller.UpdateNewTributeUrlTributeTypeinAlltables(_objUpdateTribute, _newTribute);
        }
    }
}
