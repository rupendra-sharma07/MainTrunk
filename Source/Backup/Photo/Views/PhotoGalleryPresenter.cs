///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Photo.Views.PhotoGalleryPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing photo gallery.
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using System.Xml;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
#endregion

namespace TributesPortal.Photo.Views
{
    public class PhotoGalleryPresenter : Presenter<IPhotoGallery>
    {
        #region CLASS VARIABLES
        private PhotoController _controller;
        #endregion

        #region CONSTRUCTOR
        public PhotoGalleryPresenter([CreateNew] PhotoController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        public void GetPhotoGallery(PhotoAlbum objPhotoAlbum)
        {
            string[] getPath = CommonUtilities.GetPath(); //GetPath();
            List<PhotoAlbum> objAlbumList = _controller.GetPhotoGalleryRecords(objPhotoAlbum);
            foreach (PhotoAlbum obj in objAlbumList)
            {
                if (obj.PhotoAlbumCaption.Length != 0)
                {
                    if (obj.PhotoAlbumCaption.Length > 25)
                        obj.PhotoAlbumCaption = obj.PhotoAlbumCaption.Remove(25) + "...";
                }

                //obj.PhotoImage = getPath[2] + "/" + getPath[3] + "/" + this.View.TributeName.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/" + obj.PhotoImage;
                obj.PhotoImage = getPath[2] + "/" + getPath[3] + "/" + this.View.TributeUrl.Replace(" ", "_") + "_" + this.View.TributeType.Replace(" ", "_") + "/" + obj.PhotoImage;
            }
            if (objAlbumList.Count > 0)
            {
                this.View.PhotoAlbumList = objAlbumList;
                this.View.TotalRecords = objAlbumList[0].TotalRecords;
                this.View.DrawPaging = GetPaging(objAlbumList[0].TotalRecords, objPhotoAlbum.PageSize, objPhotoAlbum.PageNumber);
                this.View.RecordCount = GetAlbumCount(objPhotoAlbum.PageNumber, objPhotoAlbum.PageSize, objAlbumList.Count, objAlbumList[0].TotalRecords);
            }
            else
            {
                this.View.TotalRecords = 0;
            }
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

            return objUtilities.DrawPaging(currentPage, pageCount, "photos.aspx");
        }

        /// <summary>
        /// Method to get the method count text
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="recordCount">Number of records to be displayed</param>
        /// <param name="totalRecords">Total number of records</param>
        /// <returns>String containing text to display</returns>
        private string GetAlbumCount(int currentPage, int pageSize, int recordCount, int totalRecords)
        {
            int endCount = 0;
            string strMessage;
            int startCount = currentPage == 1 ? 1 : currentPage * pageSize - (pageSize - 1);

            if (recordCount < pageSize)
                endCount = currentPage * pageSize - (pageSize - recordCount);
            else
                endCount = currentPage * pageSize;

            if (recordCount > 1)
                strMessage = ResourceText.GetString("txtPhotoAlbums_PG") + " " + startCount.ToString() + " " + ResourceText.GetString("txtTo_PG") + " " + endCount.ToString() + " " + ResourceText.GetString("txtOf_PG") + " " + totalRecords;
            else
                strMessage = ResourceText.GetString("txtPhotoAlbum_PG") + " " + startCount.ToString() + " " + ResourceText.GetString("txtTo_PG") + " " + endCount.ToString() + " " + ResourceText.GetString("txtOf_PG") + " " + totalRecords;

            return strMessage;
        }      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tributeId"></param>
        /// <returns></returns>
        public string GetTributeEndDate(int tributeId)
        {
            return _controller.GetTributeEndDate(tributeId);
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


        public int GetCurrentPhotoAlbums(int tributeId)
        {
            return _controller.GetCurrentPhotoAlbums(tributeId);
        }
        #endregion



    }//end class
}//end namespace




