///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Video.Views.VideoUploadPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for uploading the video tributes.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SWFToImage;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Video.Views
{
    public class VideoUploadPresenter : Presenter<IVideoUpload>
    {
        #region CLASS VARIABLES
        private VideoController _controller;
        #endregion

        #region CONSTRUCTOR
        public VideoUploadPresenter([CreateNew] VideoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to get tribute list
        /// </summary>
        public void GetTributesList()
        {
            Tributes objTribute = new Tributes();
            objTribute.UserTributeId = this.View.UserId;
            this.View.TributesList = _controller.GetListOfTributesForVideoTribute(objTribute);
        }

        /// <summary>
        /// Method to save video tribute.
        /// </summary>
        public void SaveVideoTribute()
        {
            Videos objVid = GetVideoObject();
            string strTributeUrl = string.Empty;
            string strTributeType = string.Empty;
            StateManager stateManager = StateManager.Instance;

            List<Tributes> objList = (List<Tributes>)stateManager.Get("objTributeList", StateManager.State.Session);

            foreach (Tributes obj in objList)
            {
                if (obj.TributeId == this.View.TributeId)
                {
                    strTributeType = obj.TypeDescription;
                    strTributeUrl = obj.TributeUrl;
                    break;
                }
            }

            //to copy video from ftp folder to tributeportal folder
            string[] paths = CommonUtilities.GetVideoTributePath();
            string srcPath = paths[0] + this.View.UserDetails.Users.UserName + "\\" + objVid.TributeVideoId + "_files";
            string destPath;
            if (strTributeType.ToLower().ToString() != "video")
            {
                destPath = paths[1] + strTributeUrl + "_" + strTributeType; // +"/" + objVid.TributeVideoId;
            }
            else
            {
                destPath = paths[1] + this.View.TributeName+"_" +this.View.UserId + "_" + strTributeType.ToLower();
            }

            Directory.CreateDirectory(destPath);
            foreach (string strFileName in Directory.GetFileSystemEntries(srcPath))
            {
                File.Copy(strFileName, destPath + "/" + Path.GetFileName(strFileName), true); // strFileName.Substring(strFileName.LastIndexOf(@"\")));
            }

            SWFToImageObject objSwf = new SWFToImageObject();
            SWFToImageObject objSwfThumb = new SWFToImageObject();

            objSwf.InitLibrary("demo", "demo");
            objSwfThumb.InitLibrary("demo", "demo");
            //FIND ALL FOLDERS IN FOLDER
            string Location = destPath;
            DirectoryInfo dir = new DirectoryInfo(Location);
            //foreach (DirectoryInfo innerdir in dir.GetDirectories())
            //{
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                //Directory.SetCurrentDirectory(Location + "\\" + innerdir.Name);
                Directory.SetCurrentDirectory(Location);
                if (file.Extension.Equals(".swf"))
                {
                    string safeFileName = string.Empty;
                    objSwf.InputSWFFileName = file.FullName; // input SWF file
                    objSwf.FrameIndex = 35;
                    objSwf.ImageWidth = 480;
                    objSwf.ImageHeight = 320;
                    objSwf.ImageOutputType = (TImageOutputType)1;
                    objSwf.JPEGQuality = 100;
                    objSwf.Execute();

                    //String fileName = file.Name.Substring(0, file.Name.LastIndexOf("."));
                    String fileName = file.Name.Substring(0, file.Name.IndexOf("."));
                    //LHK: to avoid special characters in the file name.
                    safeFileName = RemoveSpecialCharacter(fileName);
                    if (safeFileName != fileName)
                    {
                        objSwfThumb.InputSWFFileName = file.FullName; // input SWF file
                        objSwfThumb.FrameIndex = 35;
                        objSwfThumb.ImageWidth = 144;
                        objSwfThumb.ImageHeight = 96;
                        objSwfThumb.ImageOutputType = (TImageOutputType)1;
                        objSwfThumb.JPEGQuality = 100;
                        objSwfThumb.Execute();

                        objSwf.SaveToFile(safeFileName + "_big.jpg"); // save to jpg file
                        objSwfThumb.SaveToFile(safeFileName + ".jpg"); // save to jpg file

                        if (!File.Exists(destPath + "\\" + safeFileName + ".swf"))
                            file.CopyTo(destPath + "\\" + safeFileName + ".swf");

                        objVid.TributeVideoId = safeFileName;
                        //this.View.VideoTributeId = fileName;
                    }
                    else
                    {
                        objSwf.SaveToFile(fileName + "_big.jpg"); // save to jpg file
                    }
                }
            }
            //}
            //Directory.Move(srcPath, destPath);
            //File.Copy(srcPath, destPath);

            //to save record in database
            _controller.SaveVideo(objVid, "VideoTribute");
        }

        /// <summary>
        /// Method to get token details.
        /// </summary>
        public void GetTokenDetails()
        {
            VideoToken objTokenId = new VideoToken();
            objTokenId.TokenId = this.View.TokenId;
            this.View.TokenDetails = _controller.GetTokenDetails(objTokenId);
            this.View.UserId = this.View.TokenDetails.UserId;
        }

        /// <summary>
        /// method to get user details.
        /// </summary>
        public void GetUserDetails()
        {
            UserRegistration objUser = new UserRegistration();
            Users objUsers = new Users(this.View.UserId);
            objUser.Users = objUsers;
            _controller.GetUserInfo(objUser);
            this.View.UserDetails = objUser;
        }

        /// <summary>
        /// Method to delete video tribute based on the tribute id.
        /// </summary>
        public void DeleteVideoTribute()
        {
            Videos objVideo = GetVideoObject();
            objVideo.IsDeleted = true;
            _controller.DeleteVideoTribute(objVideo);
        }

        /// <summary>
        /// Method to get the video object to save
        /// </summary>
        /// <returns>Filled Video entity</returns>
        private Videos GetVideoObject()
        {
            Videos objVideo = new Videos();
            objVideo.CreatedBy = this.View.UserId;
            objVideo.CreatedDate = DateTime.Now;
            objVideo.IsActive = true;
            objVideo.IsDeleted = false;
            objVideo.TributeVideoId = this.View.VideoTributeId;
            objVideo.UserId = this.View.UserId;
            if (this.View.UserDetails.Users.FirstName == string.Empty || this.View.UserDetails.Users.FirstName == null)
                objVideo.UserName = this.View.UserDetails.Users.UserName;
            else
                objVideo.UserName = this.View.UserDetails.Users.FirstName;

            objVideo.UserTributeId = this.View.TributeId;
            objVideo.VideoCaption = this.View.VideoCaption;
            objVideo.VideoDesc = this.View.VideoDesc;
            objVideo.TributeName = this.View.TributeName;
            objVideo.TributeType = this.View.TributeType;
            objVideo.TributeUrl = this.View.TributeUrl;
            return objVideo;
        }
        private string RemoveSpecialCharacter(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'A' && str[i] <= 'z' || str[i] == '_'))
                    sb.Append(str[i]);
            }
            return sb.ToString();
        }
        #endregion
    }
}




