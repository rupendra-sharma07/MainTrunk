///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Utilities.WebConfig.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to reference the application constants (defined in web.config) 
///                     from within the application
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TributesPortal.Utilities
{
    /// <summary>
    /// This class is an adapter to the web.config file
    /// </summary>
    public static class WebConfig
    {
        public static string ApplicationMode = ConfigurationManager.AppSettings["ApplicationMode"];
        public static string OptimalPaymentsServiceURL = ConfigurationManager.AppSettings["com.optimalpayments.test.webservices.v1"];
        public static string AccountNumber = ConfigurationManager.AppSettings["AccountNumber"];
        public static string StoteId = ConfigurationManager.AppSettings["StoteId"];
        public static string StotePwd = ConfigurationManager.AppSettings["StotePwd"];
        public static string ReferancePrefix = ConfigurationManager.AppSettings["ReferancePrefix"];
        public static string LogFilePath = ConfigurationManager.AppSettings["LogFilePath"];
        public static string LoggingEnabled = ConfigurationManager.AppSettings["LoggingEnabled"];
        public static string BlogUrl = ConfigurationManager.AppSettings["BlogUrl"];
        public static string LifeTimeAmount = ConfigurationManager.AppSettings["LifeTimeAmount"];
        public static string OneyearAmount = ConfigurationManager.AppSettings["OneyearAmount"];
        public static string LifeTimeVideoTributeUpgrade = ConfigurationManager.AppSettings["LifeTimeVideoTributeUpgrade"];
        public static string OneYearVideoTributeUpgrade = ConfigurationManager.AppSettings["OneYearVideoTributeUpgrade"];
        public static string Pagesize_myTributes = ConfigurationManager.AppSettings["Pagesize_myTributes"];
        public static string Size_myInbox = ConfigurationManager.AppSettings["Size_myInbox"];
        public static string Pagesize_myInbox = ConfigurationManager.AppSettings["Pagesize_myInbox"];
        public static string Pagesize_myEvents = ConfigurationManager.AppSettings["Pagesize_myEvents"];
        public static string Size_myEvents = ConfigurationManager.AppSettings["Size_myEvents"];
        public static string Pagesize_myFavourite = ConfigurationManager.AppSettings["Pagesize_myFavourite"];
        public static string Pagesize_guestBook = ConfigurationManager.AppSettings["Pagesize_guestBook"];
        public static string PagingSize_guestBook = ConfigurationManager.AppSettings["PagingSize_guestBook"];
        public static string Pagesize_Gift = ConfigurationManager.AppSettings["Pagesize_Gift"];
        public static string PagingSize_Gift = ConfigurationManager.AppSettings["PagingSize_Gift"];
        public static string TextSize_TributeNotes = ConfigurationManager.AppSettings["TextSize_TributeNotes"];
        public static string Pagesize_Notes = ConfigurationManager.AppSettings["Pagesize_Notes"];
        public static string Pagesize_Notes_Comments = ConfigurationManager.AppSettings["Pagesize_Notes_Comments"];
        public static string Pagesize_VideoGallery = ConfigurationManager.AppSettings["Pagesize_VideoGallery"];
        public static string Pagesize_Videos_Comments = ConfigurationManager.AppSettings["Pagesize_Videos_Comments"];
        public static string Pagesize_PhotoGallery = ConfigurationManager.AppSettings["Pagesize_PhotoGallery"];
        public static string Pagesize_PhotoAlbum = ConfigurationManager.AppSettings["Pagesize_PhotoAlbum"];
        public static string MaxPhotosInAlbum_PhotoAlbum = ConfigurationManager.AppSettings["MaxPhotosInAlbum_PhotoAlbum"];
        public static string DBName = ConfigurationManager.AppSettings["DBName"];
        public static string MailServer = ConfigurationManager.AppSettings["MailServer"];
        public static string AdministratorMail = ConfigurationManager.AppSettings["AdministratorMail"];
        public static string SIGNUPAdmin = ConfigurationManager.AppSettings["SIGNUPAdmin"];
        public static string TributeCreationAdmin = ConfigurationManager.AppSettings["TributeCreationAdmin"];
        public static string ForgetPassAdmin = ConfigurationManager.AppSettings["ForgetPassAdmin"];
        public static string SponsorEmail = ConfigurationManager.AppSettings["SponsorEmail"];
        public static string DefaultFolderUrl_ToGetDefaultFile = ConfigurationManager.AppSettings["DefaultFolderUrl_ToGetDefaultFile"];
        public static string DefaultFileName = ConfigurationManager.AppSettings["DefaultFileName"];
        public static string NewBabyFolderPath = ConfigurationManager.AppSettings["NewBabyFolderPath"];
        public static string BirthdayFolderPath = ConfigurationManager.AppSettings["BirthdayFolderPath"];
        public static string GraduationFolderPath = ConfigurationManager.AppSettings["GraduationFolderPath"];
        public static string WeddingFolderPath = ConfigurationManager.AppSettings["WeddingFolderPath"];
        public static string AnniversaryFolderPath = ConfigurationManager.AppSettings["AnniversaryFolderPath"];
        public static string MemorialFolderPath = ConfigurationManager.AppSettings["MemorialFolderPath"];
        public static string VideoFolderPath = ConfigurationManager.AppSettings["VideoFolderPath"];
        public static string Launch_Day = ConfigurationManager.AppSettings["Launch_Day"];
        public static string IOVSRuleBase = ConfigurationManager.AppSettings["IOVSRuleBase"];
        public static string TopLevelDomain = ConfigurationManager.AppSettings["TopLevelDomain"];
        public static string AddressSeparator = ConfigurationManager.AppSettings["Address_Separator"];
        public static string AddressSeparatorDisplay = ConfigurationManager.AppSettings["Address_Separator_Display"];

        public static string NoreplyEmail = ConfigurationManager.AppSettings["NoreplyEmail"];
        public static string InfoEmail = ConfigurationManager.AppSettings["InfoEmail"];
        public static string SupportEmail = ConfigurationManager.AppSettings["SupportEmail"];
        public static string PrivacyEmail = ConfigurationManager.AppSettings["PrivacyEmail"];
        public static string BillingEmail = ConfigurationManager.AppSettings["BillingEmail"];
        public static string FeedbackEmail = ConfigurationManager.AppSettings["FeedbackEmail"];
        public static string NotificationEmail = ConfigurationManager.AppSettings["NotificationEmail"];
        public static string YourTributeEmail = ConfigurationManager.AppSettings["YourTributeEmail"];
        public static string PhotoUploaderKey = ConfigurationManager.AppSettings["PhotoUploaderKey"];
        public static string GoogleAPIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

        public static string BlogUrlMain = ConfigurationManager.AppSettings["BlogUrlMain"];
        public static string CopyrightYear = ConfigurationManager.AppSettings["CopyrightYear"];
        public static string DonationURL = ConfigurationManager.AppSettings["DonationURL"];
        public static string DonationTermsURL = ConfigurationManager.AppSettings["DonationTermsURL"];
        public static string DonationPrivacyURL = ConfigurationManager.AppSettings["DonationPrivacyURL"];

        public static string AppBaseDomain = ConfigurationManager.AppSettings["APP_BASE_DOMAIN"];
        public static string CurrentSubDomain = ConfigurationManager.AppSettings["CURRENT_SUBDOMAIN"];

        public static string LifeTimeCreditCost = ConfigurationManager.AppSettings["LifeTime_CreditCost"];
        public static string OneYearCreditCost = ConfigurationManager.AppSettings["OneYear_CreditCost"];

        public static string DefaultCustomeHeaderBGColor = ConfigurationManager.AppSettings["DefaultCustomeHeaderBGColor"];
        public static string DefaultColorPickerValue = ConfigurationManager.AppSettings["DefaultColorPickerValue"];


        public static string LifeTimePhotoTypeUpgrade = ConfigurationManager.AppSettings["LifeTimePhotoTypeUpgrade"];
        public static string YearlyPhotoTypeUpgrade = ConfigurationManager.AppSettings["YearlyPhotoTypeUpgrade"];

        public static string LifeTimeTributeTypeUpgrade = ConfigurationManager.AppSettings["LifeTimeTributeTypeUpgrade"];
        public static string YearlyTributeTypeUpgrade = ConfigurationManager.AppSettings["YearlyTributeTypeUpgrade"];

        public static string PhotoYearlyCreditCost = ConfigurationManager.AppSettings["PhotoYearlyCreditCost"];
        public static string PhotoLifeTimeCreditCost = ConfigurationManager.AppSettings["PhotoLifeTimeCreditCost"];

        public static string TributeYearlyCreditCost = ConfigurationManager.AppSettings["TributeYearlyCreditCost"];
        public static string TributeLifeTimeCreditCost = ConfigurationManager.AppSettings["TributeLifeTimeCreditCost"];

        public static string PhotoOneyearAmount = ConfigurationManager.AppSettings["PhotoOneyearAmount"];
        public static string PhotoLifeTimeAmount = ConfigurationManager.AppSettings["PhotoLifeTimeAmount"];

        public static string TributeOneyearAmount = ConfigurationManager.AppSettings["TributeOneyearAmount"];
        public static string TributeLifeTimeAmount = ConfigurationManager.AppSettings["TributeLifeTimeAmount"];

        //LHK:
        public static string PhotoNotesLimit = ConfigurationManager.AppSettings["PhotoNotesLimit"];
        public static string PhotoEventLimit = ConfigurationManager.AppSettings["PhotoEventLimit"];
        public static string PhotoAlbumLimit = ConfigurationManager.AppSettings["PhotoAlbumLimit"];
        public static string PhotoVideoLimit = ConfigurationManager.AppSettings["PhotoVideoLimit"];
        //Till Here.

        public static string ApplicationType = ConfigurationManager.AppSettings["ApplicationType"];
        public static string CssDir = ConfigurationManager.AppSettings["CssDir"];
        public static string ApplicationWord = ConfigurationManager.AppSettings["ApplicationWord"];
        public static string ApplicationWordForInternalUse = ConfigurationManager.AppSettings["ApplicationWordForInternalUse"];
        public static string VideoFileCopyIteration = ConfigurationManager.AppSettings["VideoFileCopyIteration"];

        //LHK:for secured link
        public static string SecuredAppBaseDomain = ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"];
        public static string DevAdminEmail = ConfigurationManager.AppSettings["DevAdminEmail"];
        public static string captureFrameIndexNumber = ConfigurationManager.AppSettings["captureFrameIndexNumber"];


        //LHK: for smtpclient
        public static string UseSMTPAdvanced = ConfigurationManager.AppSettings["UseSMTPAdvanced"];
        public static string SMTPServer = ConfigurationManager.AppSettings["SMTPServer"];
        public static string SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
        public static string SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

        //public static string ErrorLogFilePath = ConfigurationManager.AppSettings["ErrorLogFilePath"];

        //LHK: MailChimp API Implemnetation.
        public static string MailChimpApiKey = ConfigurationManager.AppSettings["MailChimpApiKey"];
        public static string NewsLettersListID = ConfigurationManager.AppSettings["NewsLettersListID"];
        //public static string PersonalUserListID = ConfigurationManager.AppSettings["PersonalUserListID"];
        //public static string BussinessUserListID = ConfigurationManager.AppSettings["BussinessUserListID"];
        public static string MaxFileSizeTributeImage = ConfigurationManager.AppSettings["MaxFileSizeTributeImage"];
        public static string SmtpPort = ConfigurationManager.AppSettings["SmtpPort"];

        //Phase -1 Enhancement chnages for mailchimp
        public static string MailChimpApiKeyNew = ConfigurationManager.AppSettings["MailChimpApiKeyNew"];
        public static string UserNewsLetterListID = ConfigurationManager.AppSettings["UserNewsLetterListID"];
        public static string TributeNewsLetterListID = ConfigurationManager.AppSettings["TributeNewsLetterListID"];
        
        public static string MaxStoryLimit = ConfigurationManager.AppSettings["MaxStoryLimit"];
        public static string MaxNotesLimit = ConfigurationManager.AppSettings["MaxNotesLimit"];
        public static string MaxGuestbookLimit = ConfigurationManager.AppSettings["MaxGuestbookLimit"];
        public static string MaxGiftsLimit = ConfigurationManager.AppSettings["MaxGiftsLimit"];
        public static string MaxEventsLimit = ConfigurationManager.AppSettings["MaxEventsLimit"];
        public static string MaxPhotosLimit = ConfigurationManager.AppSettings["MaxPhotosLimit"];
        public static string MaxVideosLimit = ConfigurationManager.AppSettings["MaxVideosLimit"];
        public static string MaxVideoTributeLimit = ConfigurationManager.AppSettings["MaxVideoTributeLimit"];
        public static string MaxCommentsLimit = ConfigurationManager.AppSettings["MaxCommentsLimit"];
        public static string MaxNoteCommentsLimit = ConfigurationManager.AppSettings["MaxNoteCommentsLimit"];
        public static string MaxPhotoCommentsLimit = ConfigurationManager.AppSettings["MaxPhotoCommentsLimit"];
        public static string MaxVideoCommentsLimit = ConfigurationManager.AppSettings["MaxVideoCommentsLimit"];
        public static string MaxVideoTributeCommentsLimit = ConfigurationManager.AppSettings["MaxVideoTributeCommentsLimit"];


        public static string IsMobileRedirectOn = ConfigurationManager.AppSettings["IsMobileRedirectOn"];
    }
}