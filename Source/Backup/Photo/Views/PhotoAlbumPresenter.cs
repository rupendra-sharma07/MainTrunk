///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.PhotoAlbumPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing photo album.
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
    public class PhotoAlbumPresenter : Presenter<IPhotoAlbum>
    {
        #region CLASS VARIABLES
        private PhotoController _controller;
        PhotoAlbum objAlbumDetails = new PhotoAlbum();
        #endregion

        #region CONSTRUCTOR
        public PhotoAlbumPresenter([CreateNew] PhotoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Method to get photo album data.
        /// </summary>
        /// <param name="objPhoto">Filled photo entity.</param>
        public void GetPhotoAlbumData(Photos objPhoto)
        {
            string[] getPath = CommonUtilities.GetPath();
            
            objAlbumDetails = _controller.GetPhotoAlbumDetail(objPhoto);
            this.View.PhotoAlbumDetails = objAlbumDetails;  //_controller.GetPhotoAlbumDetail(objPhoto);
            List<Photos> objPhotoList = _controller.GetPhotos(objPhoto);
            foreach (Photos obj in objPhotoList)
            {
                //obj.PhotoImage = getPath[2] + "/" + getPath[3] + "/" + this.View.TributeName.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/" + obj.PhotoImage;
                obj.PhotoImage = getPath[2] + "/" + getPath[3] + "/" + this.View.TributeUrl.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/" + obj.PhotoImage;
            }
            if (objPhotoList.Count > 0)
            {
                this.View.PhotosList = objPhotoList;
                this.View.TotalRecords = objPhotoList[0].TotalRecords;
                this.View.DrawPaging = GetPaging(objPhotoList[0].TotalRecords, objPhoto.PageSize, objPhoto.PageNumber);
                this.View.RecordCount = GetPhotoCount(objPhoto.PageNumber, objPhoto.PageSize, objPhotoList.Count, objPhotoList[0].TotalRecords);
            }
            else
            {
                this.View.TotalRecords = 0;
            }

            this.View.XmlFilePath = GetPhotoImageList(objPhoto);
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
            /*using (StreamWriter sw = File.CreateText(pathToSave))
            {
                sw.Write(xmlData);
                sw.Close();
            }*/

            //return getPathForXml[2] + getPathForXml[4] + "/" + newFileName;
            return pathToSave;
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

            return objUtilities.DrawPaging(currentPage, pageCount, "photoalbum.aspx");
        }

        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <param name="totalRecords">Total number of records</param>
        /// <returns>String containing text to display</returns>
        private string GetPhotoCount(int currentPage, int pageSize, int recordCount, int totalRecords)
        {
            int endCount = 0;
            string strMessage;
            int startCount = currentPage == 1 ? 1 : currentPage * pageSize - (pageSize - 1);

            if (recordCount < pageSize)
                endCount = currentPage * pageSize - (pageSize - recordCount);
            else
                endCount = currentPage * pageSize;

            if (recordCount > 1)
                strMessage = ResourceText.GetString("txtPhotos_PA") + " " + startCount.ToString() + " " + ResourceText.GetString("txtTo_PA") + " " + endCount.ToString() + " " + ResourceText.GetString("txtOf_PA") + " " + totalRecords;
            else
                strMessage = ResourceText.GetString("txtPhoto_PA") + " " + startCount.ToString() + " " + ResourceText.GetString("txtTo_PA") + " " + endCount.ToString() + " " + ResourceText.GetString("txtOf_PA") + " " + totalRecords;

            return strMessage;
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
            return objUtilities.GetXmlData(objImageList, path, objAlbumDetails.PhotoAlbumCaption, objAlbumDetails.PhotoAlbumDesc);
            /*StringBuilder sb = new StringBuilder();

            sb.Append("<?xml version='1.0' encoding='UTF-8' ?>");
            sb.Append("<gallery>");
            sb.Append("<album title='" + objAlbumDetails.PhotoAlbumCaption + "' description='" + objAlbumDetails.PhotoAlbumDesc + "' ");
            sb.Append("lgpath='" + getPath[2] + this.View.TributeName.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/'>");
            foreach (Photos obj in objImageList)
            {
                sb.Append("<img src='" + obj.PhotoImage + "' />");
            }
            sb.Append("</album>");
            sb.Append("</gallery>");

            return sb.ToString();*/
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate( int _tributeId)
        {
            return _controller.GetTributeEndDate( _tributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public bool GetCustomHeaderDetail(int tributeId)
        {
            return _controller.GetCustomHeaderDetail(tributeId);
        }
        #endregion

        public int GetPackIdonPhotoAlbumId(int photoAlbumId)
        {
            return _controller.GetPackIdonPhotoAlbumId(photoAlbumId);
        }
    }//end class
}//end namespace




