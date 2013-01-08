///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.PhotoViewPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing a photo.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Photo.Views
{
    public class PhotoViewPresenter : Presenter<IPhotoView>
    {
        #region CLASS VARIABLES
        private PhotoController _controller;
        private int photoAlbumId = 0;
        private string photoAlbumCaption = string.Empty;
        #endregion

        #region CONSTRUCTOR
        public PhotoViewPresenter([CreateNew] PhotoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        public void GetPhotoDetails()
        {
            Photos objUpdatedPhoto = new Photos();
            string[] getPath = CommonUtilities.GetPath();

            objUpdatedPhoto = this._controller.GetPhotoDetail(GetPhotoObject());
            //objUpdatedPhoto.PhotoImage = getPath[2] + "/" + this.View.TributeName.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/" + objUpdatedPhoto.PhotoImage;
            objUpdatedPhoto.PhotoImage = getPath[2] + "/" + this.View.TributeUrl.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/" + objUpdatedPhoto.PhotoImage;

            this.View.PhotoDetails = objUpdatedPhoto;
            this.View.NextCount = objUpdatedPhoto.NextRecordCount;
            this.View.PrevCount = objUpdatedPhoto.PrevRecordCount;
            this.View.SetRecordCount = ResourceText.GetString("lblPhotoCount_PV") + " " + objUpdatedPhoto.RecordNumber  + " " + ResourceText.GetString("lblOf_PV") + " " + objUpdatedPhoto.TotalRecords;

            this.View.Comments = this._controller.GetModuleComments(GetCommentObject());
            this.View.CommentCount = objUpdatedPhoto.CommentCount;
            LoadComments(GetCommentObject());

            photoAlbumId = objUpdatedPhoto.PhotoAlbumId;
            photoAlbumCaption = objUpdatedPhoto.PhotoAlbumCaption;
            this.View.XmlFilePath = GetPhotoImageList(GetPhotoObject());
            this.View.RecordNumber = objUpdatedPhoto.RecordNumber; //used to set the start photo in slideshow.
        }

        /// <summary>
        /// Method to get the photo id and user id to get the details of photo
        /// </summary>
        /// <returns>Photo entity containing PhotoId and UserId</returns>
        public Photos GetPhotoObject()
        {
            Photos objPhoto = new Photos();
            objPhoto.PhotoId = this.View.PhotoId;
            objPhoto.UserId = this.View.UserId;
            objPhoto.PhotoAlbumId = photoAlbumId;
            return objPhoto;
        }

        /// <summary>
        /// Method to get the comment details to get list of comments for photo
        /// </summary>
        /// <returns>Photo entity containing PhotoId and UserId</returns>
        public CommentTributeAdministrator GetCommentObject()
        {
            CommentTributeAdministrator objComment = new CommentTributeAdministrator();
            objComment.CommentTypeId = this.View.PhotoId;
            objComment.UserId = this.View.UserId;
            objComment.TypeCodeName = "Photo";
            objComment.TributeId = this.View.TributeId;
            objComment.PageSize = this.View.PageSize;
            objComment.CurrentPage = this.View.CurrentPage;

            ////to get the comments list
            //List<CommentTributeAdministrator> objComments = _controller.GetModuleComments(objComment); // _guestBookController.CommentList(objNoteComment);

            ////to display the Message count
            //this.View.RecordCount = GetMessageCount(objComment.CurrentPage, objComment.PageSize, objComments.Count, this.View.CommentCount);

            ////to display the Paging
            //this.View.DrawPaging = GetPaging(this.View.CommentCount, objComment.PageSize, objComment.CurrentPage);

            return objComment;
        }

        public void LoadComments(CommentTributeAdministrator objPhotoComment)
        {
            //to get the comments list
            List<CommentTributeAdministrator> objComments = _controller.GetModuleComments(objPhotoComment); // _guestBookController.CommentList(objNoteComment);

            //to change the \n from message to </br>
            List<CommentTributeAdministrator> objUpdatedComments = new List<CommentTributeAdministrator>();
            foreach (CommentTributeAdministrator obj in objComments)
            {
                CommentTributeAdministrator objComment = new CommentTributeAdministrator();
                objComment.City = obj.City;
                objComment.CommentId = obj.CommentId;
                objComment.CommentTypeId = obj.CommentTypeId;
                objComment.Country = obj.Country;
                objComment.CreatedBy = obj.CreatedBy;
                objComment.CreatedDate = obj.CreatedDate;
                objComment.CurrentPage = obj.CurrentPage;
                objComment.IsAdmin = obj.IsAdmin;
                objComment.IsPrivate = obj.IsPrivate;
                objComment.Message = obj.Message.Replace("\n", "</br>");
                objComment.PageSize = obj.PageSize;
                objComment.State = obj.State;
                objComment.TotalRecords = obj.TotalRecords;
                objComment.TributeId = obj.TributeId;
                objComment.TypeCodeId = obj.TypeCodeId;
                objComment.UserId = obj.UserId;
                objComment.UserImage = CommonUtilities.GetPath()[2].ToString() + obj.UserImage;
                objComment.UserName = obj.UserName;
                objComment.UserType = obj.UserType;

                if (obj.IsLocationHide)
                {
                    objComment.Location = string.Empty;
                }
                else
                {
                    if (obj.City == string.Empty && obj.State == string.Empty)
                        objComment.Location = "(" + obj.Country + ")";
                    else
                        if (obj.City == string.Empty && obj.State != string.Empty)
                            objComment.Location = "(" + obj.State + ", " + obj.Country + ")";
                        else if (obj.City != string.Empty && obj.State == string.Empty)
                            objComment.Location = "(" + obj.City + ", " + obj.Country + ")";
                        else
                            objComment.Location = "(" + obj.City + ", " + obj.State + ", " + obj.Country + ")";
                }

                objUpdatedComments.Add(objComment);
            }

            this.View.Comments = objUpdatedComments;

            //to display the Message count
            this.View.RecordCount = GetMessageCount(objPhotoComment.CurrentPage, objPhotoComment.PageSize, objComments.Count, this.View.CommentCount);

            //to display the Paging
            this.View.DrawPaging = GetPaging(this.View.CommentCount, objPhotoComment.PageSize, objPhotoComment.CurrentPage);
        }

        /// <summary>
        /// Method to save comment on photo
        /// </summary>
        /// <param name="objComment">Filled Comment entity</param>
        public void SaveComment(Comments objComment)
        {
            _controller.SaveComment(objComment);
        }

        /// <summary>
        /// Method to save comment on photo
        /// </summary>
        /// <param name="objComment">Filled Comment entity</param>
        public void SaveComment(Comments objComment, string _topUrl)
        {
            _controller.SaveComment(objComment,_topUrl);
        }

        /// <summary>
        /// Method to delete comment on photo
        /// </summary>
        /// <param name="objComment">Filled Comment entity</param>
        public void DeleteComment(Comments objComment)
        {
            _controller.DeleteComment(objComment);
        }

        /// <summary>
        /// Method to create string for paging
        /// </summary>
        /// <param name="totalRecords">Total number of records</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="currentPage">Current size</param>
        /// <returns>string containing paging</returns>
        private string GetPaging(int totalRecords, int pageSize, int currentPage)
        {
            int pageCount = 0;
            if (totalRecords % pageSize == 0)
                pageCount = totalRecords / pageSize;
            else
                pageCount = (totalRecords / pageSize) + 1;

            CommonUtilities objUtilities = new CommonUtilities();
            return objUtilities.DrawPaging(currentPage, pageCount, "photo.aspx");
        }

        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <param name="totalRecords">Total number of records</param>
        /// <returns>String containing text to display</returns>
        private string GetMessageCount(int currentPage, int pageSize, int recordCount, int totalRecords)
        {
            int endCount = 0;
            string strMessage;
            int startCount = currentPage == 1 ? 1 : currentPage * pageSize - (pageSize - 1);

            if (recordCount < pageSize)
                endCount = currentPage * pageSize - (pageSize - recordCount);
            else
                endCount = currentPage * pageSize;

            if (recordCount > 1)
                strMessage = ResourceText.GetString("strMessages_GB") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GB") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_GB") + " " + totalRecords;
            else
                strMessage = ResourceText.GetString("strMessage_GB") + " " + startCount.ToString() + " " + ResourceText.GetString("strTo_GB") + " " + endCount.ToString() + " " + ResourceText.GetString("strOf_GB") + " " + totalRecords;

            return strMessage;
        }

        /// <summary>
        /// Method to get list of images in the album.
        /// </summary>
        /// <param name="objPhoto">Photo entity containing PhotoAlbumId.</param>
        public string GetPhotoImageList(Photos objPhoto)
        {
            string xmlData = GetXmlData(this._controller.GetPhotoImagesList(objPhoto));
            string xmlFileName = "FilesMetaData.xml";
            string[] getPathForXml = CommonUtilities.GetPath();
            //to create directory for xml files.
            string xmlFilePath = getPathForXml[0] + "/" + getPathForXml[1] + "/" + getPathForXml[4] + "/";
            DirectoryInfo objDir = new DirectoryInfo(xmlFilePath);
            if (!objDir.Exists) //if directory does not exists creates a directory
                objDir.Create();

            //to get safe file name
            int j = 1;
            string newFileName = xmlFileName;
            while (File.Exists(xmlFilePath + newFileName))
            {
                newFileName = j + "_" + objPhoto.PhotoAlbumId.ToString() + "_" + xmlFileName;
                //LHK: 12/19/2011 -added to resolve photo page slow Page load issue.
                if (File.GetCreationTime(xmlFilePath + newFileName) < DateTime.Now.AddHours(-2))
                {
                    File.Delete(xmlFilePath + newFileName);
                }
                j++;
            }

            string pathToSave = xmlFilePath + newFileName;

            FileStream outFile = new System.IO.FileStream(pathToSave, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(outFile);
            sw.Write(xmlData);
            sw.Flush();
            sw.Close();

            return getPathForXml[2] + getPathForXml[4] + "/" + newFileName;
        }

        /// <summary>
        /// Method to create the xml data for xml file.
        /// </summary>
        /// <param name="objImageList">List of photo images.</param>
        /// <returns>Xml data in string format.</returns>
        public string GetXmlData(List<Photos> objImageList)
        {
            string[] getPath = CommonUtilities.GetPath();

            string path = getPath[2] + this.View.TributeUrl.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/'>";

            CommonUtilities objUtilities = new CommonUtilities();
            return objUtilities.GetXmlData(objImageList, path, photoAlbumCaption, "");
        }

        public bool GetCustomHeaderDetail(int tributeId)
        {
            return _controller.GetCustomHeaderDetail(tributeId);
        }

        public int GetTributePackageId(int _tributeId)
        {
            return _controller.TributePackageId(_tributeId);
        }
        #endregion

    }
}




