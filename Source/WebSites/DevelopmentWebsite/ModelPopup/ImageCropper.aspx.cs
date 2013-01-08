///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.ImageCropper.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the user to upload an image in Story and Event
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;

#endregion

/// <summary>
///Tribute Portal-ImageCropper UI Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the UI class ModelPopup_ImageCropper for uploading a image in Story
// and Event image and will extend PageBase class which provides 
// 1. Error Event Handler
// 2. Exception handling
/// </summary>

public partial class ModelPopup_ImageCropper : PageBase
{

    #region CONSTANT

    private const string DefaultPath = "images/temp";
    private const string DefaultImage = "images/sample_ProfilePhoto.jpg";
    private const int DefaultWidth = 374;
    private const int DefaultHeight = 374;

    #endregion
    private string RequestUrl = "";

    #region EVENTS

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        lblErrMsg.InnerHtml = "";
        lblErrMsg.Visible = false;
        RequestUrl = Request.UrlReferrer.ToString();
        if (!this.IsPostBack)
        {
            string[] virtualDir = CommonUtilities.GetPath();
            if (RequestUrl.Contains("https://"))
                img.ImageUrl = virtualDir[9] + DefaultImage;
            else
                img.ImageUrl = virtualDir[2] + DefaultImage;
        }
    }

    protected void lbtnUpload_Click(object sender, EventArgs e)
    {
        System.Drawing.Image imgOriginal = null;
        System.Drawing.Bitmap bmpCompress = null;

        try
        {
            // Check that it contain a valid file name
            if (!ImageUploader.HasFile)
            {
                lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_IC"), ResourceText.GetString("valValidFile_IC"), 1);
                lblErrMsg.Visible = true;

                return;
            }

            // Check the file extension
            string extension = (System.IO.Path.GetExtension(ImageUploader.PostedFile.FileName)).ToLower();
            if (extension != ".jpg" && extension != ".jpeg" && extension != ".gif" && extension != ".bmp")
            {
                lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_IC"), ResourceText.GetString("valValidFormat_IC"), 1);
                lblErrMsg.Visible = true;

                return;
            }

            // Check the size of the Image it should be less than 4 MB
            int size = ImageUploader.PostedFile.ContentLength;
            int sizeinMB = size / (1024 * 1024);
            int maxFileSize = 0;
            int.TryParse(WebConfig.MaxFileSizeTributeImage, out maxFileSize);

            if (sizeinMB > maxFileSize)
            {
                lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_IC"), ResourceText.GetString("valLargeFile_IC"), 1);
                lblErrMsg.Visible = true;

                return;
            }

            string filePath = ImageUploader.PostedFile.FileName;

            string fileName = Path.GetFileNameWithoutExtension(filePath);

            // Path where you want to upload the file.
            string[] imagePath = CommonUtilities.GetPath();

            string destPath = imagePath[0] + "/" + imagePath[1] + "/" + imagePath[6];

            // If directory does not exists creates a directory
            DirectoryInfo dirObj = new DirectoryInfo(destPath);
            if (!dirObj.Exists)
            {
                dirObj.Create();
            }

            // Check whether directory exist or not
            if (dirObj.Exists)
            {
                string tempfileName = fileName;
                int counter = 1;

                // Check to see if a file already exists with the same name as the file to upload.  
                while (File.Exists(destPath + "/" + tempfileName + ".jpeg"))
                {
                    // If a file with this name already exists, add a numer in the filename.
                    tempfileName = fileName + counter.ToString();
                    counter = counter + 1;
                }

                fileName = tempfileName;

                ImageUploader.PostedFile.SaveAs(destPath + "/" + fileName + ".jpeg");

                // Create an Image object
                imgOriginal = System.Drawing.Image.FromFile(destPath + "/" + fileName + ".jpeg");


                // Comppressed and Stretch the image according to the size
                if (!(imgOriginal.Width <= 374 && imgOriginal.Height <= 247))
                    bmpCompress = CompressImage(imgOriginal);

                imgOriginal.Dispose();
                if (bmpCompress != null)
                {
                    bmpCompress.Save(destPath + "/" + fileName + ".jpeg", ImageFormat.Jpeg);
                    bmpCompress.Dispose();
                }
                if (RequestUrl.Contains("https://"))
                    img.ImageUrl = imagePath[9] + "/" + imagePath[6] + "/" + fileName + ".jpeg";
                else
                    img.ImageUrl = imagePath[2] + "/" + imagePath[6] + "/" + fileName + ".jpeg";
            }
        }
        catch (Exception ex)
        {
            lblErrMsg.InnerHtml = ShowMessage(ResourceText.GetString("valHeader_IC"), "Image file could not be loaded. Please try again.", 1);
            lblErrMsg.Visible = true;
        }
        finally
        {
            if (imgOriginal != null)
            {
                imgOriginal.Dispose();
            }

            if (bmpCompress != null)
            {
                bmpCompress.Dispose();
            }
        }
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        System.Drawing.Image imgOriginal = null;
        System.Drawing.Bitmap bmpCrop = null;

        try
        {
            string[] imagePath = CommonUtilities.GetPath();
            string imageFilePath = "";
            if (RequestUrl.Contains("https://"))
                imageFilePath = img.ImageUrl.Substring(img.ImageUrl.IndexOf(imagePath[9]) + imagePath[9].Length, img.ImageUrl.Length - imagePath[9].Length);
            else
                imageFilePath = img.ImageUrl.Substring(img.ImageUrl.IndexOf(imagePath[2]) + imagePath[2].Length, img.ImageUrl.Length - imagePath[2].Length);


            string strFilePath = imagePath[0] + "/" + imagePath[1] + "/" + imageFilePath;

            // Create an Image object
            imgOriginal = System.Drawing.Image.FromFile(strFilePath);

            // Crop the image
            bmpCrop = CropImage(imgOriginal);

            imgOriginal.Dispose();

            string destPath = imagePath[0] + "/" + imagePath[1] + "/" + imagePath[6];

            // If directory does not exists creates a directory
            DirectoryInfo dirObj = new DirectoryInfo(destPath);
            if (!dirObj.Exists)
            {
                dirObj.Create();
            }

            if ((bmpCrop != null) && (dirObj.Exists))
            {
                string fileName = Path.GetFileNameWithoutExtension(img.ImageUrl);

                string tempfileName = fileName;
                int counter = 1;

                // Check to see if a file already exists with the same name as the file to upload. 
                //COMDIFFRES: working fine.  why "/" is added in the following file path? It is not present in the .com version 
                while (File.Exists(destPath + "/" + tempfileName + ".jpeg"))
                {
                    // If a file with this name already exists, add a numer in the filename.
                    tempfileName = counter.ToString() + fileName;
                    counter = counter + 1;
                }

                fileName = tempfileName;

                // Save the Image
                bmpCrop.Save(destPath + "/" + fileName + ".jpeg", ImageFormat.Jpeg);

                bmpCrop.Dispose();

                String strJScript;
                string path;
                if (RequestUrl.Contains("https://"))
                    path = imagePath[9] + "/" + imagePath[6] + "/" + "/" + fileName + ".jpeg";
                else
                    path = imagePath[2] + "/" + imagePath[6] + "/" + "/" + fileName + ".jpeg";


                strJScript = "<script language=javascript>";
                strJScript += "parent.SetImage('" + path + "');";
                if (Request.RawUrl.ToLower().Contains("businessuserhome.aspx"))
                    strJScript += "parent.ReloadPage(); ";
                else
                    strJScript += "parent.ImageCroppermodalClose(); ";
                strJScript += "</script>";

                Response.Write(strJScript);
            }
        }
        catch (IOException ex)
        {
            throw ex;
        }
        finally
        {
            if (imgOriginal != null)
            {
                imgOriginal.Dispose();
            }

            if (bmpCrop != null)
            {
                bmpCrop.Dispose();
            }
        }
    }

    #endregion


    #region METHODS

    /// <summary>
    /// This method is for compressing the image
    /// </summary>
    /// <param name="imgOriginal">Image object which conatin the Image wants to compress</param>
    /// <returns>A Bitmap object which contains the compressed Image</returns>
    private System.Drawing.Bitmap CompressImage(System.Drawing.Image imgOriginal)
    {
        Graphics gphCompress = null;
        System.Drawing.Bitmap bmpCompress = null;

        try
        {
            // Create an bitmap for the compressed size
            /*   bmpCompress = new System.Drawing.Bitmap(DefaultWidth, DefaultHeight, PixelFormat.Format24bppRgb);

               // Set the resolution of the compressed image with the original one
               bmpCompress.SetResolution(imgOriginal.HorizontalResolution, imgOriginal.VerticalResolution);

               // Creates graphics object for the compressed image
               gphCompress = Graphics.FromImage(bmpCompress);
               gphCompress.Clear(Color.White);

               // To control the quality of the image, specify the smoothing, interpolation, and pixeloffset modes
               gphCompress.SmoothingMode = SmoothingMode.HighQuality;
               gphCompress.InterpolationMode = InterpolationMode.HighQualityBicubic;
               gphCompress.PixelOffsetMode = PixelOffsetMode.HighQuality;

               // Define the destination rectangle
               System.Drawing.Rectangle destRec = new Rectangle(0, 0, DefaultWidth, DefaultHeight);

               // Define the Source Rectangle
               System.Drawing.Rectangle srcRec = new Rectangle(0, 0, imgOriginal.Width, imgOriginal.Height);

               // Draw the Image with the destination and source rectangle
               gphCompress.DrawImage(imgOriginal, destRec, srcRec, GraphicsUnit.Pixel);

               // Dispose the objects
               gphCompress.Dispose();

               return bmpCompress; */


            int sourceWidth = imgOriginal.Width;
            int sourceHeight = imgOriginal.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;
            int Width = DefaultWidth;
            int Height = DefaultHeight;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            //if we have to pad the height pad both the top and the bottom
            //with the difference between the scaled height and the desired height
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = (int)((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = (int)((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgOriginal.HorizontalResolution, imgOriginal.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgOriginal,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This method is for cropping the image
    /// </summary>
    /// <param name="imgOriginal">Image object which conatin the Image wants to crop</param>
    /// <returns>A Bitmap object which contains the cropped Image</returns>
    private System.Drawing.Bitmap CropImage(System.Drawing.Image imgOriginal)
    {
        Graphics gphCrop = null;
        System.Drawing.Bitmap bmpCrop = null;

        try
        {
            // Create an bitmap for the Cropped Image size
            bmpCrop = new System.Drawing.Bitmap(int.Parse(hdnWidth.Value), int.Parse(hdnHeight.Value), PixelFormat.Format24bppRgb);

            // Set the resolution of the Cropped image with the original one
            bmpCrop.SetResolution(imgOriginal.HorizontalResolution, imgOriginal.VerticalResolution);

            // Creates graphics object for the cropped image
            gphCrop = Graphics.FromImage(bmpCrop);
            gphCrop.Clear(Color.White);

            // To control the quality of the image, specify the smoothing, interpolation, and pixeloffset modes
            gphCrop.SmoothingMode = SmoothingMode.HighQuality;
            gphCrop.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gphCrop.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Define the destination rectangle
            System.Drawing.Rectangle destRec = new Rectangle(0, 0, int.Parse(hdnWidth.Value), int.Parse(hdnHeight.Value));

            // Draw the Image with the destination and source rectangle
            gphCrop.DrawImage(imgOriginal, destRec, int.Parse(hdnX1.Value), int.Parse(hdnY1.Value), int.Parse(hdnWidth.Value), int.Parse(hdnHeight.Value), GraphicsUnit.Pixel);

            // Dispose the objects
            gphCrop.Dispose();

            return bmpCrop;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (gphCrop != null)
            {
                gphCrop.Dispose();
            }
        }
    }

    #endregion
}


