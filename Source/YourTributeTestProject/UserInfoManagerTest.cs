using TributesPortal.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
//using TributesPortal.ResourceAccess;
using System;
using System.Data;
using System.Xml;

//namespace YourTributeTestProject
namespace TributesPortal.ResourceAccess
{


    /// <summary>
    ///This is a test class for UserInfoManagerTest and is intended
    ///to contain all UserInfoManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserInfoManagerTest : PortalResourceAccess
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        public int InsertDummyBusinessUser(String DummyUser)
        {
            bool userNameExists = false;
            int UserId = -1;

            try
            {

                object[] objParam = { DummyUser };
                DataSet _objDataSet = GetDataSet("usp_AvailableUser", objParam);
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    String UserName = _objDataSet.Tables[0].Rows[0][0].ToString();
                    if (UserName != "0")
                    {
                        userNameExists = true;

                        object[] objParam1 ={ DummyUser,
                                            "zSpeN+GdR0Ey9VrM9QyvUA==",
                                     null
                                   };
                        //DataSet _objDataSet = GetDataSet("usp_ValidateUser", objParam);
                        DataSet _objDataSet1 = GetDataSetWithoutCheckingIOVS("usp_ValidateWebsiteUser", objParam1);
                        // ds.Tables[0].
                        if (_objDataSet1.Tables[0].Rows.Count > 0)
                        {
                            UserId = (int)_objDataSet1.Tables[0].Rows[0]["UserId"];
                        }


                    }

                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //objUser.CustomError = objError;
                    userNameExists = false;
                }
            }
            /////////////////////////////////////////////////////////////////
            if (!userNameExists)
            {
                TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
                UserRegistration objUserReg = new UserRegistration();

                objUsers.CountryName = "India";
                objUsers.Email = "tajinder.kaur@optimusinfo.com";
                objUsers.FacebookUid = null;
                objUsers.FirstName = "tajinder";
                objUsers.LastName = "kaur";
                objUsers.Password = "zSpeN+GdR0Ey9VrM9QyvUA==";
                objUsers.City = "Delhi";
                objUsers.UserName = DummyUser;
                objUsers.UserType = 2;
                objUsers.UserImage = null;
                objUsers.Country = null;
                objUsers.State = null;
                objUsers.AllowIncomingMsg = false;
                objUsers.VerificationCode = "";

                TributesPortal.BusinessEntities.UserBusiness objUserBusiness = new TributesPortal.BusinessEntities.UserBusiness();
                objUserBusiness.BusinessAddress = "optimus";
                objUserBusiness.BusinessType = 1;
                objUserBusiness.City = "Delhi";
                objUserBusiness.CompanyName = "optimus";
                objUserBusiness.Country = "India";
                objUserBusiness.Email = "tajinder.kaur@optimusinfo.com";
                objUserBusiness.Phone = "9911089140";
                objUserBusiness.Website = "www.yourtribute.com";
                objUserBusiness.ZipCode = "201301";

                objUserReg.Users = objUsers;
                objUserReg.UserBusiness = objUserBusiness;

                try
                {
                    string[] strParam = { 
                                    Users.UserEnum.UserName.ToString(),
                                    Users.UserEnum.Password.ToString(),
                                    Users.UserEnum.FirstName.ToString(),
                                    Users.UserEnum.LastName.ToString(),
                                    Users.UserEnum.Email.ToString(),
                                    Users.UserEnum.VerificationCode.ToString(),
                                    Users.UserEnum.AllowIncomingMsg.ToString(),
                                    Users.UserEnum.City.ToString(),
                                    Users.UserEnum.State.ToString(),
                                    Users.UserEnum.Country.ToString(),   
                                    Users.UserEnum.UserImage.ToString(),
                                    Users.UserEnum.UserType.ToString(),
                                    Users.UserEnum.FacebookUid.ToString()
                                };

                    DbType[] enumDbType ={ 
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Boolean,
                                    DbType.String,
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.Int32,
                                    DbType.Int64
                                 };
                    if (objUserReg.Users.State == -1)
                    {
                        objUserReg.Users.State = null;
                    }

                    object[] objValue ={
                                        objUserReg.Users.UserName.ToString(),
                                        objUserReg.Users.Password.ToString(),
                                        objUserReg.Users.FirstName.ToString(),
                                        objUserReg.Users.LastName.ToString(),
                                        objUserReg.Users.Email.ToString(),
                                        objUserReg.Users.VerificationCode.ToString(),
                                        objUserReg.Users.AllowIncomingMsg.ToString(),
                                        objUserReg.Users.City.ToString(),
                                        objUserReg.Users.State,
                                        objUserReg.Users.Country,
                                        objUserReg.Users.UserImage,
                                        objUserReg.Users.UserType,
                                        objUserReg.Users.FacebookUid 
                                   };

                    DataSet _objDataSet1 = GetDataSetWithoutCheckingIOVS("usp_SaveUserPersonalAccount", objValue);
                    // ds.Tables[0].
                    int count1 = _objDataSet1.Tables[0].Rows.Count;
                    if (count1 > 0)
                    {
                        UserId = (int)_objDataSet1.Tables[0].Rows[0]["UserId"];
                        objUserReg.Users.UserId = UserId;
                    }
                    string[] strParam1 = { 
                                    UserBusiness.UserRegistrationEnum.UserId.ToString(),
                                    UserBusiness.UserRegistrationEnum.Website.ToString(),
                                    UserBusiness.UserRegistrationEnum.CompanyName.ToString(),
                                    UserBusiness.UserRegistrationEnum.BusinessType.ToString(),
                                    UserBusiness.UserRegistrationEnum.BusinessAddress.ToString(),
                                    UserBusiness.UserRegistrationEnum.ZipCode.ToString(),
                                    "Phone"
                                    
                                };

                    DbType[] enumDbType1 ={ 
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Int64,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String          
                                 };

                    object[] objValue1 ={
                                    (Int64)objUserReg.Users.UserId,
                                    objUserReg.UserBusiness.Website.ToString(),
                                    objUserReg.UserBusiness.CompanyName.ToString(),
                                    objUserReg.UserBusiness.BusinessType.ToString(),
                                    objUserReg.UserBusiness.BusinessAddress.ToString(),
                                    objUserReg.UserBusiness.ZipCode.ToString(),                                   
                                    objUserReg.UserBusiness.Phone.ToString()
                               };
                    //base.InsertRecord("usp_SaveUserBusinessAccount", strParam, enumDbType, objValue);
                    base.InsertRecordMinusIovs("usp_SaveUserBusinessAccount", strParam1, enumDbType1, objValue1);
                    //UserId = objUserReg.Users.UserId;
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number >= 50000)
                    {
                        Errors objError = new Errors();

                        objUserReg.CustomError = objError;
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }



            return UserId;

        }
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        //[TestMethod()]
        //insert a user if it does not exist
        public int InsertDummyUser(String DummyUser)
        {
            bool userNameExists = false;
            int UserId = -1;
            try
            {

                object[] objParam = { DummyUser };
                DataSet _objDataSet = GetDataSet("usp_AvailableUser", objParam);
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    String UserName = _objDataSet.Tables[0].Rows[0][0].ToString();
                    if (UserName != "0")
                    {
                        userNameExists = true;

                        object[] objParam1 ={ DummyUser,
                                            "zSpeN+GdR0Ey9VrM9QyvUA==",
                                     null
                                   };
                        //DataSet _objDataSet = GetDataSet("usp_ValidateUser", objParam);
                        DataSet _objDataSet1 = GetDataSetWithoutCheckingIOVS("usp_ValidateWebsiteUser", objParam1);
                        // ds.Tables[0].
                        if (_objDataSet1.Tables[0].Rows.Count > 0)
                        {
                            UserId = (int)_objDataSet1.Tables[0].Rows[0]["UserId"];
                        }


                    }

                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //objUser.CustomError = objError;
                    userNameExists = false;
                }
            }
            /////////////////////////////////////////////////////////////////
            if (!userNameExists)
            {
                object[] objValue ={
                                            DummyUser,
                                            "zSpeN+GdR0Ey9VrM9QyvUA==",//objUserReg.Users.Password.ToString(),
                                            "Tajinder",
                                            "Kaur",//objUserReg.Users.LastName.ToString(),
                                            "tajinder.kaur@optimusinfo.com",//objUserReg.Users.Email.ToString(),
                                            "",//objUserReg.Users.VerificationCode.ToString(),
                                            "False",//objUserReg.Users.AllowIncomingMsg.ToString(),
                                            "Delhi",//objUserReg.Users.City.ToString(),
                                            null,//objUserReg.Users.State,
                                            null,//objUserReg.Users.Country,
                                            null,//objUserReg.Users.UserImage,
                                            1,//objUserReg.Users.UserType,
                                            null,//objUserReg.Users.FacebookUid 
                                       };
                //Identity = base.InsertDataAndReturnIdMinusIOVS("usp_SaveUserPersonalAccount", strParam, enumDbType, objValue);

                DataSet _objDataSet1 = GetDataSetWithoutCheckingIOVS("usp_SaveUserPersonalAccount", objValue);
                // ds.Tables[0].
                int count1 = _objDataSet1.Tables[0].Rows.Count;
                if (count1 > 0)
                {
                    UserId = (int)_objDataSet1.Tables[0].Rows[0]["UserId"];
                    String UserName = _objDataSet1.Tables[0].Rows[0]["UserName"].ToString();
                }
            }
            return UserId;
        }

        /// <summary>
        ///A test for UserInfoManager Constructor
        ///</summary>
        [TestMethod()]
        public void UserInfoManagerConstructorTest()
        {
            UserInfoManager target = new UserInfoManager();
            // Assert._objDataSet1Inconclusive("TODO: Implement code to verify target");
            Assert.AreNotEqual(null, target);
            /*object[] objParam = { 21080,1 };
            DataSet _objDataSet1 = GetDataSet("usp_DeleteUser", objParam);

            int count1 = _objDataSet1.Tables[0].Rows.Count;
            if (count1 > 0)
            {
                int UserId = (int)_objDataSet1.Tables[0].Rows[0]["UserId"];
                String UserName = _objDataSet1.Tables[0].Rows[0]["UserName"].ToString();
            }*/

        }

        /// <summary>
        ///A test for UserSummaryReport
        ///</summary>
        [TestMethod()]
        public void UserSummaryReportTest()
        {
            //UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            //UsersSummaryReport objSummary = new UsersSummaryReport(); // TODO: Initialize to an appropriate value
            //target.UserSummaryReport(objSummary);

            ////sp testing
            //DataSet dsData = GetDataSet("usp_GetUserSummaryReport", null);
            //int count = dsData.Tables[0].Rows.Count;
            //Assert.AreEqual(1, count);
            //if (count > 0)
            //{
            //    Assert.IsTrue(int.Parse(dsData.Tables[0].Rows[0]["PERSONAL_ACTIVE_ACCOUNTS"].ToString()) > 0);
            //    Assert.IsTrue(int.Parse(dsData.Tables[0].Rows[0]["BUSINESS_ACTIVE_ACCOUNTS"].ToString()) > 0);
            //    Assert.IsTrue(int.Parse(dsData.Tables[0].Rows[0]["TOTAL_ACTIVE_ACCOUNTS"].ToString()) > 0);
            //}
        }

        /// <summary>
        ///A test for UserSiteAdminLogin
        ///</summary>
        [TestMethod()]//tajinder- Function useless
        public void UserSiteAdminLoginTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            //InsertDummyUser("tj_op");
            GenralUserInfo objUser = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserName = "jason";
            objUserInfo.UserPassword = "Jason123";
            objUserInfo.FacebookUid = null;
            objUser.RecentUsers = objUserInfo;

            target.UserSiteAdminLogin(objUser);
            // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UserInboxCount
        ///</summary>
        [TestMethod()]
        public void UserInboxCountTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            int UserId = InsertDummyUser("tj_op");
            object[] objValue = { UserId }; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UserInboxCount(objValue);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UserAvailability
        ///</summary>
        [TestMethod()]
        public void UserAvailabilityTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            //UserRegistration _objUserRegistration = null; // TODO: Initialize to an appropriate value
            // target.UserAvailability(_objUserRegistration);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
            bool userNameExists = false;
            try
            {
                InsertDummyUser("tj_op");
                object[] objParam = { "tj_op" };
                DataSet _objDataSet = GetDataSet("usp_AvailableUser", objParam);
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    String UserName = _objDataSet.Tables[0].Rows[0][0].ToString();
                    if (UserName != "0")
                    {
                        userNameExists = true;
                    }

                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //objUser.CustomError = objError;
                    userNameExists = false;
                }
            }

            Assert.AreEqual(true, userNameExists);
        }

        /// <summary>
        ///A test for UpdatePrivacySettings
        ///</summary>
        [TestMethod()]
        public void UpdatePrivacySettingsTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            //UserRegistration _objUserRegistration = null; // TODO: Initialize to an appropriate value
            int UserId = InsertDummyUser("tj_op");
            UserRegistration objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = UserId;
            objUsers.IsUsernameVisiable = true;
            objUsers.IsLocationHide = false;
            objUsers.AllowIncomingMsg = true;
            objUserReg.Users = objUsers;
            target.UpdatePrivacySettings(objUserReg);

            UserRegistration objUserReg1 = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers1 = new TributesPortal.BusinessEntities.Users();
            objUsers1.UserId = UserId;
            objUserReg1.Users = objUsers1;
            target.GetUserDetails(objUserReg1);

            Assert.AreEqual(false, objUserReg1.Users.IsLocationHide);
            Assert.AreEqual(true, objUserReg1.Users.AllowIncomingMsg);
            Assert.AreEqual(true, objUserReg1.Users.IsUsernameVisiable);

            // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdatePersonalDetails
        ///</summary>
        [TestMethod()]//tajinder- Not Working
        ///IOVS.CS
        //_ruleBase.Load(Path.Combine(HttpContext.Current.Server.MapPath("~"), ConfigurationManager.AppSettings["IOVSRuleBase"]));
        public void UpdatePersonalDetailsTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserRegistration objUserReg = new UserRegistration(); // TODO: Initialize to an appropriate value
            int UserId = InsertDummyUser("tj_op");
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = UserId;
            objUsers.FirstName = "Tajinder1";
            objUsers.LastName = "Kaur1";
            objUsers.City = "Noida";
            objUsers.State = null;
            objUsers.Country = 5;
            objUsers.UserImage = null;
            objUserReg.Users = objUsers;
            target.UpdatePersonalDetails(objUserReg);


            UserRegistration objUserReg1 = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers1 = new TributesPortal.BusinessEntities.Users();
            objUsers1.UserId = UserId;
            objUserReg1.Users = objUsers1;
            target.GetUserDetails(objUserReg1);

            Assert.AreEqual("Tajinder1", objUserReg1.Users.FirstName);
            Assert.AreEqual("Kaur1", objUserReg1.Users.LastName);
            Assert.AreEqual("Noida", objUserReg1.Users.City);
            Assert.AreEqual(5, objUserReg1.Users.Country);
        }

        /// <summary>
        ///A test for UpdateMessageStstus
        ///</summary>
        [TestMethod()]//TODO
        public void UpdateMessageStstusTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] Params = null; // TODO: Initialize to an appropriate value
            target.UpdateMessageStstus(Params);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateFavouriteEmailAlert
        ///</summary>
        [TestMethod()]//Tajinder- not able to create any favorite tribute
        public void UpdateFavouriteEmailAlertTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            //object[] _Tributes = null; // TODO: Initialize to an appropriate value
            Tributes objTribute = new Tributes();
            int UserId = InsertDummyUser("tj_op");
            int TributeId = 38574;//dummy Id //remove hardcoding: use GetMyTributes-?already used in this file
            objTribute.UserTributeId = UserId;
            objTribute.TributeId = TributeId;
            objTribute.IsActive = true;
            object[] _tribute = { objTribute };
            target.UpdateFavouriteEmailAlert(_tribute);

            object[] objParam = { TributeId, null };
            DataSet dsTributeFav = GetDataSet("usp_GetFavouriteTributes", objParam);
            bool testPassed = false;
            //to fill records in entity
            if (dsTributeFav.Tables.Count > 0)
            {
                if (dsTributeFav.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributeFav.Tables[0].Rows)
                    {
                        if (int.Parse(dr["UserId"].ToString()) == UserId)
                            testPassed = true;
                    }
                }
            }

            Assert.AreEqual(true, testPassed);


            // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateFacebookAssociation
        ///</summary>
        [TestMethod()]//not tested
        public void UpdateFacebookAssociationTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserRegistration _objUserRegistration = null; // TODO: Initialize to an appropriate value
            target.UpdateFacebookAssociation(_objUserRegistration);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateEmailNotofication
        ///</summary>
        [TestMethod()]
        public void UpdateEmailNotoficationTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            //object[] param = null; // TODO: Initialize to an appropriate value
            //target.UpdateEmailNotofication(param);

            UserRegistration _objUserReg = new UserRegistration();
            EmailNotification _objEmaNoti = new EmailNotification();
            int UserId = InsertDummyUser("tj_op");
            _objEmaNoti.UserId = UserId;
            _objEmaNoti.StoryNotify = true;
            _objEmaNoti.NotesNotify = true;
            _objEmaNoti.EventsNotify = true;
            _objEmaNoti.GuestBookNotify = true;
            _objEmaNoti.GiftsNotify = true;
            _objEmaNoti.PhotosNotify = true;
            _objEmaNoti.PhotoAlbumNotify = true;
            _objEmaNoti.VideosNotify = true;
            _objEmaNoti.CommentsNotify = true;
            _objEmaNoti.MessagesNotify = true;
            _objEmaNoti.NewsLetterNotify = true;
            _objUserReg.EmailNotification = _objEmaNoti;
            object[] param = { _objUserReg };
            target.UpdateEmailNotofication(param);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");

            UserRegistration _objUserReg1 = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = UserId;
            _objUserReg1.Users = objUsers;
            target.GetEmailNotofication(_objUserReg1);

            Assert.AreEqual(true, _objUserReg1.EmailNotification.CommentsNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.EventsNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.GiftsNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.GuestBookNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.MessagesNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.NewsLetterNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.NotesNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.PhotoAlbumNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.PhotosNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.StoryNotify);
            Assert.AreEqual(true, _objUserReg1.EmailNotification.VideosNotify);
        }

        /// <summary>
        ///A test for UpdateEmailAlerts
        ///</summary>
        [TestMethod()]
        public void UpdateEmailAlertsTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            int UserId = InsertDummyUser("tj_op");

            //////////////////////////Get tribute of type 0 for userId////GetMyTributes unit test//
            int tributeId = 0;
            GetMyTributes _objtribute = new GetMyTributes();
            _objtribute.UserId = UserId;
            object[] _param = { _objtribute, 0, 1, 1 };
            if (_objtribute.CustomError == null)
            {
                List<GetMyTributes> Mytributes = new List<GetMyTributes>();
                Mytributes = target.GetMyTributes(_param);
                Assert.AreEqual(true, Mytributes.Count > 0);
                tributeId = Mytributes[0].TributeId;
            }
            ///////////////////////////

            Tributes objTribute = new Tributes();
            objTribute.UserTributeId = UserId;
            objTribute.TributeId = tributeId;
            objTribute.IsActive = false;
            object[] _Tributes = { objTribute }; // TODO: Initialize to an appropriate value
            target.UpdateEmailAlerts(_Tributes);

            /////GetMyTribute Unit test/////
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = UserId;
            object[] param = { objtribute, tributeId };
            List<GetMyTributes> mytribute = target.GetMyTribute(param);

            Assert.AreEqual(mytribute[0].TributeId, tributeId);
            if (mytribute[0].TributeId == tributeId)
            {
                Assert.AreEqual(false, mytribute[0].EmailAlert);
            }

            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SendMail
        ///</summary>
        [TestMethod()]//tajinder- not tested
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void SendMailTest()
        {
            UserInfoManager_Accessor target = new UserInfoManager_Accessor(); // TODO: Initialize to an appropriate value
            UserRegistration _objGenralUserInfo = null; // TODO: Initialize to an appropriate value
            int Userid = 0; // TODO: Initialize to an appropriate value
            UserInfoResource objUser = null; // TODO: Initialize to an appropriate value
            target.SendMail(_objGenralUserInfo, Userid, objUser);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SendEmailForAdminConfirmation
        ///</summary>
        [TestMethod()]//tajinder- not tested
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void SendEmailForAdminConfirmationTest()
        {
            UserInfoManager_Accessor target = new UserInfoManager_Accessor(); // TODO: Initialize to an appropriate value
            SessionValue objUserDetails = null; // TODO: Initialize to an appropriate value
            Tributes objTributeDetails = null; // TODO: Initialize to an appropriate value
            target.SendEmailForAdminConfirmation(objUserDetails, objTributeDetails);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SavePersonalAccount
        ///</summary>
        [TestMethod()]//tajinder: SendEmail in UserInfoManager::SavePersonalAccount fails
        public void SavePersonalAccountTest()
        {

            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserRegistration _UserRegistration = new UserRegistration(); // TODO: Initialize to an appropriate value
            //object expected = null; // TODO: Initialize to an appropriate value
            //object actual;

            bool userNameExists = false;
            int UserId = -1;
            String DummyUser = "tj_op_business1";
            try
            {

                object[] objParam = { DummyUser };
                DataSet _objDataSet = GetDataSet("usp_AvailableUser", objParam);
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    String UserName = _objDataSet.Tables[0].Rows[0][0].ToString();
                    if (UserName != "0")
                    {
                        userNameExists = true;

                        object[] objParam1 ={ DummyUser,
                                            "zSpeN+GdR0Ey9VrM9QyvUA==",
                                     null
                                   };
                        //DataSet _objDataSet = GetDataSet("usp_ValidateUser", objParam);
                        DataSet _objDataSet1 = GetDataSetWithoutCheckingIOVS("usp_ValidateWebsiteUser", objParam1);
                        // ds.Tables[0].
                        if (_objDataSet1.Tables[0].Rows.Count > 0)
                        {
                            UserId = (int)_objDataSet1.Tables[0].Rows[0]["UserId"];
                        }


                    }

                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //objUser.CustomError = objError;
                    userNameExists = false;
                }
            }
            /////////////////////////////////////////////////////////////////
            if (!userNameExists)
            {
                TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();

                objUsers.CountryName = "India";
                objUsers.Email = "tajinder.kaur@optimusinfo.com";
                objUsers.FacebookUid = null;
                objUsers.FirstName = "tajinder";
                objUsers.LastName = "kaur";
                objUsers.Password = "zSpeN+GdR0Ey9VrM9QyvUA==";
                objUsers.City = "Delhi";
                objUsers.UserName = DummyUser;
                objUsers.UserType = 2;
                objUsers.UserImage = null;
                objUsers.Country = null;
                objUsers.State = null;
                objUsers.AllowIncomingMsg = false;
                objUsers.VerificationCode = "";

                TributesPortal.BusinessEntities.UserBusiness objUserBusiness = new TributesPortal.BusinessEntities.UserBusiness();
                objUserBusiness.BusinessAddress = "optimus";
                objUserBusiness.BusinessType = 1;
                objUserBusiness.City = "Delhi";
                objUserBusiness.CompanyName = "optimus";
                objUserBusiness.Country = "India";
                objUserBusiness.Email = "tajinder.kaur@optimusinfo.com";
                objUserBusiness.Phone = "9911089140";
                objUserBusiness.Website = "www.yourtribute.com";
                objUserBusiness.ZipCode = "201301";

                _UserRegistration.Users = objUsers;
                _UserRegistration.UserBusiness = objUserBusiness;

                object UserId1 = target.SavePersonalAccount(_UserRegistration);
                UserId = (int)UserId1;
                //Assert.AreEqual(expected, actual);
                //Assert.Inconclusive("Verify the correctness of this test method.");
            }
            Assert.AreEqual(true, UserId > 0);
        }

        /// <summary>
        ///A test for EmailAvailable through signup modal pop up
        ///</summary>
        [TestMethod()]//Udham attri: check EMail Availability in database:passed
        public void EmailAvailablesignupTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            string Email = "udham.attry@gmail.com"; // TODO: Initialize to an appropriate value
            int expected = 1; // TODO: Initialize to an appropriate value
            int actual;
            //actual = target.EmailAvailable(Email);
           // Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }



        /// <summary>
        ///A test for SavePersonalAccount through sign up pop window
        ///</summary>
        [TestMethod()]//Udham Attri: SendEmail in UserInfoManager::SavePersonalAccount Passed: But give exception due to no sessions
        public void SavePersonalAccountsignupTest()
        {

            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserRegistration _UserRegistration = new UserRegistration(); // TODO: Initialize to an appropriate value

            int UserId = -1;
            string DummyEmail = "udhams3399@gmail.com";
            string pass = "5+gyEtBYa+xsqIjQHL2Xyw==";
            /////////////////////////////////////////////////////////////////
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();

            objUsers.Country = 404;
            objUsers.CreatedOn = System.DateTime.Now;
            objUsers.FacebookUid = null;
            objUsers.FirstName = "udham";
            objUsers.LastName = "attri";
            objUsers.Password = pass;
            objUsers.State = null;
            objUsers.UserImage = "images/bg_ProfilePhoto.gif";
            objUsers.UserName = DummyEmail;
            objUsers.UserType = 1;
            objUsers.VerificationCode = "";
            objUsers.Email = DummyEmail;
            objUsers.AllowIncomingMsg = false;
            objUsers.City = "";

            _UserRegistration.Users = objUsers;

            object UserId1 = target.SavePersonalAccount(_UserRegistration);
            UserId = (int)UserId1;


            Assert.AreEqual(true, UserId > 0);
        }





        /// <summary>
        ///A test for SaveMessage
        ///</summary>
        [TestMethod()]// IOVS failing
        public void SaveMessageTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserBusiness objBusinessUser = new UserBusiness(); // TODO: Initialize to an appropriate value

            int UserId = InsertDummyBusinessUser("tj_op_business2");
            objBusinessUser.UserId = UserId;
            objBusinessUser.UserName = "tj_op_business2";
            objBusinessUser.WelcomeMessage = "Hello TJ";
            string AppDomain = "yourtribute";
            target.SaveMessage(objBusinessUser,AppDomain);

            UserRegistration objUserReg1 = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers1 = new TributesPortal.BusinessEntities.Users();
            objUsers1.UserId = UserId;
            objUserReg1.Users = objUsers1;
            target.GetUserDetails(objUserReg1);

            Assert.AreEqual("Hello TJ", objUserReg1.UserBusiness.WelcomeMessage);
            // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SaveImage
        ///</summary>
        [TestMethod()]//IOVS failing
        public void SaveImageTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value

            UserBusiness objBusinessUser = new UserBusiness(); // TODO: Initialize to an appropriate value

            int UserId = InsertDummyBusinessUser("tj_op_business2");
            objBusinessUser.UserId = UserId;
            objBusinessUser.UserName = "tj_op_business2";
            objBusinessUser.CompanyLogo = "images/bbb.gif";
            target.SaveImage(objBusinessUser);

            UserRegistration objUserReg1 = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers1 = new TributesPortal.BusinessEntities.Users();
            objUsers1.UserId = UserId;
            objUserReg1.Users = objUsers1;
            target.GetUserDetails(objUserReg1);

            Assert.AreEqual("images/bbb.gif", objUserReg1.UserBusiness.CompanyLogo);

            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RemoveFacebookAssociation
        ///</summary>
        [TestMethod()]//not tested
        public void RemoveFacebookAssociationTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserRegistration _objUserRegistration = null; // TODO: Initialize to an appropriate value
            target.RemoveFacebookAssociation(_objUserRegistration);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsUserOwner
        ///</summary>
        [TestMethod()]
        //Remove hardcoding if feasible
        //GetMyTributes 
        //usp_GetPhotoAlbumListInTribute
        //GetPhotos
        public void IsUserOwnerTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            //UserAdminOwnerInfo objUserInfo = null; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            int UserId = InsertDummyUser("tj_op");
            objUserInfo.UserId = UserId;

            //Find Tributes for User ID
            int tributeId = 0;
            GetMyTributes _objtribute = new GetMyTributes();
            _objtribute.UserId = UserId;
            object[] _param = { _objtribute, 0, 1, 1 };
            if (_objtribute.CustomError == null)
            {
                List<GetMyTributes> Mytributes = new List<GetMyTributes>();
                Mytributes = target.GetMyTributes(_param);
                Assert.AreEqual(true, Mytributes.Count > 0);
                tributeId = Mytributes[0].TributeId;
            }
            objUserInfo.TributeId = tributeId;

            //FINd Photo Album ID for Tribute id
            object[] objParam = { tributeId };
            DataSet dsPhotoAlbumList = GetDataSet("usp_GetPhotoAlbumListInTribute", objParam);
            List<PhotoAlbum> objListOfPhotoAlbum = new List<PhotoAlbum>();
            //to fill records in the Photo Gallery list
            if (dsPhotoAlbumList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPhotoAlbumList.Tables[0].Rows)
                {
                    PhotoAlbum objAlbum = new PhotoAlbum();
                    objAlbum.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());

                    objListOfPhotoAlbum.Add(objAlbum);
                    objAlbum = null;
                }
            }
            int PhotoAlbumId = 0;
            if (objListOfPhotoAlbum.Count > 0)
            {
                PhotoAlbumId = objListOfPhotoAlbum[0].PhotoAlbumId;
            }

            //Get Photo id in PhotoAlbumId/////////////
            List<Photos> objListPhotos = new List<Photos>();
            try
            {



                object[] objParam1 = {PhotoAlbumId,
                                            1,
                                            1,
                                            "ASC"
                                        };
                DataSet dsPhotos = GetDataSet("usp_GetPhotos", objParam1);
                int totalRecords = 0;
                //objGetPhotos.SortOrder
                if (dsPhotos.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsPhotos.Tables[0].Rows[0];
                    totalRecords = int.Parse(dr["TotalRecords"].ToString());
                }
                //to fill records in the Photo list
                if (dsPhotos.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsPhotos.Tables[1].Rows)
                    {
                        Photos objPhoto = new Photos();
                        objPhoto.PhotoId = int.Parse(dr["UserPhotoId"].ToString());
                        objPhoto.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());
                        objPhoto.PhotoImage = dr["PhotoImage"].ToString();
                        objPhoto.CommentCount = int.Parse(dr["CommentCount"].ToString());
                        objPhoto.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                        objPhoto.TotalRecords = totalRecords;

                        objListPhotos.Add(objPhoto);
                        objPhoto = null;
                    }
                }
            }


            catch (Exception ex)
            {
                throw ex;
            }

            //////////////////////////////////////////

            objUserInfo.TypeId = objListPhotos[0].PhotoId;
            objUserInfo.TypeName = "ManagePhoto";

            actual = target.IsUserOwner(objUserInfo);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }


        /// <summary>
        ///A test for IsUserAdmin
        ///</summary>
        [TestMethod()]
        //Remove hardcoding if feasible
        //GetMyTributes 
        //usp_GetPhotoAlbumListInTribute
        //GetPhotos
        public void IsUserAdminTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            /*UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            objUserInfo.UserId = 21085;
            objUserInfo.TributeId = 38575;
            objUserInfo.TypeId = 15199;
            objUserInfo.TypeName = "ManagePhoto";*/
            UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            int UserId = InsertDummyUser("tj_op");
            objUserInfo.UserId = UserId;

            //Find Tributes for User ID
            int tributeId = 0;
            GetMyTributes _objtribute = new GetMyTributes();
            _objtribute.UserId = UserId;
            object[] _param = { _objtribute, 0, 1, 1 };
            if (_objtribute.CustomError == null)
            {
                List<GetMyTributes> Mytributes = new List<GetMyTributes>();
                Mytributes = target.GetMyTributes(_param);
                Assert.AreEqual(true, Mytributes.Count > 0);
                tributeId = Mytributes[0].TributeId;
            }
            objUserInfo.TributeId = tributeId;

            //FINd Photo Album ID for Tribute id
            object[] objParam = { tributeId };
            DataSet dsPhotoAlbumList = GetDataSet("usp_GetPhotoAlbumListInTribute", objParam);
            List<PhotoAlbum> objListOfPhotoAlbum = new List<PhotoAlbum>();
            //to fill records in the Photo Gallery list
            if (dsPhotoAlbumList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsPhotoAlbumList.Tables[0].Rows)
                {
                    PhotoAlbum objAlbum = new PhotoAlbum();
                    objAlbum.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());

                    objListOfPhotoAlbum.Add(objAlbum);
                    objAlbum = null;
                }
            }
            int PhotoAlbumId = 0;
            if (objListOfPhotoAlbum.Count > 0)
            {
                PhotoAlbumId = objListOfPhotoAlbum[0].PhotoAlbumId;
            }

            //Get Photo id in PhotoAlbumId/////////////
            List<Photos> objListPhotos = new List<Photos>();
            try
            {



                object[] objParam1 = {PhotoAlbumId,
                                            1,
                                            1,
                                            "ASC"
                                        };
                DataSet dsPhotos = GetDataSet("usp_GetPhotos", objParam1);
                int totalRecords = 0;
                //objGetPhotos.SortOrder
                if (dsPhotos.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsPhotos.Tables[0].Rows[0];
                    totalRecords = int.Parse(dr["TotalRecords"].ToString());
                }
                //to fill records in the Photo list
                if (dsPhotos.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsPhotos.Tables[1].Rows)
                    {
                        Photos objPhoto = new Photos();
                        objPhoto.PhotoId = int.Parse(dr["UserPhotoId"].ToString());
                        objPhoto.PhotoAlbumId = int.Parse(dr["PhotoAlbumId"].ToString());
                        objPhoto.PhotoImage = dr["PhotoImage"].ToString();
                        objPhoto.CommentCount = int.Parse(dr["CommentCount"].ToString());
                        objPhoto.CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                        objPhoto.TotalRecords = totalRecords;

                        objListPhotos.Add(objPhoto);
                        objPhoto = null;
                    }
                }
            }


            catch (Exception ex)
            {
                throw ex;
            }

            //////////////////////////////////////////

            objUserInfo.TypeId = objListPhotos[0].PhotoId;
            objUserInfo.TypeName = "ManagePhoto";


            actual = target.IsUserAdmin(objUserInfo);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for InsertSession
        ///</summary>
        [TestMethod()]// not tested
        public void InsertSessionTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            SessionValue _objSessionValue = null; // TODO: Initialize to an appropriate value
            string strId = string.Empty; // TODO: Initialize to an appropriate value
            target.InsertSession(_objSessionValue, strId);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetuserSentMessages
        ///</summary>
        [TestMethod()]//Not tested: HOW TO SEND MESSAGES?
        public void GetuserSentMessagesTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] objValue = null; // TODO: Initialize to an appropriate value
            List<MailMessage> expected = null; // TODO: Initialize to an appropriate value
            List<MailMessage> actual;
            actual = target.GetuserSentMessages(objValue);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserProfile
        ///</summary>
        [TestMethod()]//BUG FOUND!! Assert.AreEqual("Tajinder", objUserReg.FirstName); returns UserName
        public void GetUserProfileTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            // TODO: Initialize to an appropriate value
            UserProfile objUserReg = new UserProfile();
            objUserReg.UserId = InsertDummyUser("tj_op");
            object[] objValue = { objUserReg };
            target.GetUserProfile(objValue);
            objUserReg = (UserProfile)objValue[0];
            Assert.AreEqual("Tajinder1", objUserReg.FirstName);//coming as "tj_op" which is UserName
            Assert.AreEqual("tj_op", objUserReg.UserName);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetUserDetails
        ///</summary>
        [TestMethod()]//Already done
        public void GetUserDetailsTest()
        {
            /*UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserRegistration _objUserRegistration = null; // TODO: Initialize to an appropriate value
            target.GetUserDetails(_objUserRegistration);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");*/
        }

        /// <summary>
        ///A test for GetUserData
        ///</summary>
        [TestMethod()]
        public void GetUserDataTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            GenralUserInfo _objGenralUserInfo = new GenralUserInfo(); // TODO: Initialize to an appropriate value
            _objGenralUserInfo.RecentUsers = new UserInfo();
            _objGenralUserInfo.RecentUsers.UserID = InsertDummyUser("tj_op");
            target.GetUserData(_objGenralUserInfo);
            Assert.AreEqual("tajinder.kaur@optimusinfo.com", _objGenralUserInfo.RecentUsers.UserEmail);
            Assert.AreEqual("zSpeN+GdR0Ey9VrM9QyvUA==", _objGenralUserInfo.RecentUsers.UserPassword);

            // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetUserCompleteDetails
        ///</summary>
        [TestMethod()]
        public void GetUserCompleteDetailsTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserRegistration _objUserRegistration = new UserRegistration(); // TODO: Initialize to an appropriate value
            _objUserRegistration.Users = new Users();
            _objUserRegistration.Users.UserId = InsertDummyUser("tj_op");
            target.GetUserCompleteDetails(_objUserRegistration);
            Assert.AreEqual("Tajinder1", _objUserRegistration.Users.FirstName);
            Assert.AreEqual("Kaur", _objUserRegistration.Users.LastName);
            Assert.AreEqual("tj_op", _objUserRegistration.Users.UserName);
            Assert.AreEqual("tajinder.kaur@optimusinfo.com", _objUserRegistration.Users.Email);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetTributeOnId
        ///</summary>
        [TestMethod()]// BUG in sp usp_GetTributeOnId
        //  when tributeID = 38575 -> no record found
        // when tributeID = 21085 -> wrong record picked up
        public void GetTributeOnIdTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            int UserId = InsertDummyUser("tj_op");
            int tributeId = 0;
            GetMyTributes _objtribute = new GetMyTributes();
            _objtribute.UserId = UserId;
            object[] _param = { _objtribute, 0, 1, 1 };
            if (_objtribute.CustomError == null)
            {
                List<GetMyTributes> Mytributes = new List<GetMyTributes>();
                Mytributes = target.GetMyTributes(_param);
                Assert.AreEqual(true, Mytributes.Count > 0);
                tributeId = Mytributes[0].TributeId;
            }
            TributesUserInfo _objTributeUserinfo = new TributesUserInfo(); // TODO: Initialize to an appropriate value
            _objTributeUserinfo.Tributes = new Tributes();
            _objTributeUserinfo.Tributes.TributeId = tributeId;
            target.GetTributeOnId(_objTributeUserinfo);

            //Write Asserts here once the stored procedure is fixed

            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetTributeByID
        ///</summary>
        [TestMethod()]
        public void GetTributeByIDTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            int UserId = InsertDummyUser("tj_op");
            int tributeId = 0;
            GetMyTributes _objtribute = new GetMyTributes();
            _objtribute.UserId = UserId;
            object[] _param = { _objtribute, 0, 1, 2 };
            if (_objtribute.CustomError == null)
            {
                List<GetMyTributes> Mytributes = new List<GetMyTributes>();
                Mytributes = target.GetMyTributes(_param);
                Assert.AreEqual(true, Mytributes.Count > 0);
                tributeId = Mytributes[0].TributeId;

            }
            TributesUserInfo _objTributeUserinfo = new TributesUserInfo(); // TODO: Initialize to an appropriate value
            _objTributeUserinfo.Tributes = new Tributes();
            _objTributeUserinfo.Tributes.TributeId = tributeId;

            target.GetTributeByID(_objTributeUserinfo);
            Assert.AreEqual("tj", _objTributeUserinfo.Tributes.TributeName);
            Assert.AreEqual("xyz", _objTributeUserinfo.Tributes.TributeUrl);
            Assert.AreEqual("Birthday", _objTributeUserinfo.Tributes.TypeDescription);
            Assert.AreEqual("tj", _objTributeUserinfo.Tributes.TributeName);
            Assert.AreEqual("11-03-1983 AM 12:00:00", _objTributeUserinfo.Tributes.Date1.ToString());
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetSessionDetail
        ///</summary>
        [TestMethod()]//Not tested
        public void GetSessionDetailTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            string SessionValues = string.Empty; // TODO: Initialize to an appropriate value
            List<SessionValue> expected = null; // TODO: Initialize to an appropriate value
            List<SessionValue> actual;
            actual = target.GetSessionDetail(SessionValues);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMyTributes
        ///</summary>
        [TestMethod()]//Already done
        public void GetMyTributesTest()
        {
            /*UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] _MyTributes = null; // TODO: Initialize to an appropriate value
            List<GetMyTributes> expected = null; // TODO: Initialize to an appropriate value
            List<GetMyTributes> actual;
            actual = target.GetMyTributes(_MyTributes);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");*/
        }

        /// <summary>
        ///A test for GetMyTribute
        ///</summary>
        [TestMethod()]//Already done
        public void GetMyTributeTest()
        {
            /*UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] _MyTributes = null; // TODO: Initialize to an appropriate value
            List<GetMyTributes> expected = null; // TODO: Initialize to an appropriate value
            List<GetMyTributes> actual;
            actual = target.GetMyTribute(_MyTributes);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");*/
        }

        /// <summary>
        ///A test for GetMyEvents
        ///</summary>
        [TestMethod()]
        public void GetMyEventsTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            Events onjevents = new Events();
            onjevents.UserId = InsertDummyUser("tj_op");
            object[] objvalue = { onjevents, 1, 1 };
            //object[] objValue = {,1,1}; // TODO: Initialize to an appropriate value
            List<Events> expected = new List<Events>(); // TODO: Initialize to an appropriate value
            Events objMyEvents = new Events();
            objMyEvents.EventID = 431;
            objMyEvents.EventName = "tj";
            objMyEvents.EventDesc = "Birthday";
            objMyEvents.EventDate = DateTime.Parse("03-11-2011 AM 12:00:00");
            objMyEvents.EventRsvp = "Awaiting Response";
            objMyEvents.TributeId = 38575;
            expected.Add(objMyEvents);

            List<Events> actual;
            actual = target.GetMyEvents(objvalue);
            Assert.AreEqual(expected[0].EventID, actual[0].EventID);
            Assert.AreEqual(expected[0].EventName, actual[0].EventName);
            Assert.AreEqual(expected[0].EventDesc, actual[0].EventDesc);
            Assert.AreEqual(expected[0].EventDate, actual[0].EventDate);
            Assert.AreEqual(expected[0].EventRsvp, actual[0].EventRsvp);
            Assert.AreEqual(expected[0].TributeId, actual[0].TributeId);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMyFavourites
        ///</summary>
        [TestMethod()]//nOT TESTED-> HOW TO SET FAVORITES
        public void GetMyFavouritesTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] _MyTributes = null; // TODO: Initialize to an appropriate value
            List<GetMyTributes> expected = null; // TODO: Initialize to an appropriate value
            List<GetMyTributes> actual;
            actual = target.GetMyFavourites(_MyTributes);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMailThread
        ///</summary>
        [TestMethod()]// nOT TESTED: COULDN'T GET FUNCTIONALITY
        public void GetMailThreadTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] objValue = null; // TODO: Initialize to an appropriate value
            List<MailMessage> expected = null; // TODO: Initialize to an appropriate value
            List<MailMessage> actual;
            actual = target.GetMailThread(objValue);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEventName
        ///</summary>
        [TestMethod()]
        public void GetEventNameTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            Events actual;

            Events onjevents = new Events();
            onjevents.UserId = InsertDummyUser("tj_op");
            object[] objvalue = { onjevents, 1, 1 };

            actual = target.GetEventName(target.GetMyEvents(objvalue)[0].EventID);
            Assert.AreEqual("tj", actual.EventName);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetImage
        ///</summary>
        [TestMethod()]//NOT working PhotoConfiguration.xml not found in C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\Common\xml
        public void GetImageTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            List<GiftImage> expected = null; // TODO: Initialize to an appropriate value
            List<GiftImage> actual;
            actual = target.GetImage();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMailMessage
        ///</summary>
        [TestMethod()]//NOT TESTED: no mail message in Inbox
        public void GetMailMessageTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] objValue = null; // TODO: Initialize to an appropriate value
            List<MailMessage> expected = null; // TODO: Initialize to an appropriate value
            List<MailMessage> actual;
            actual = target.GetMailMessage(objValue);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEmailNotofication
        ///</summary>
        [TestMethod()]//already tested with UpdateEmailNotoficationTest
        public void GetEmailNotoficationTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            UserRegistration _objUserRegistration = new UserRegistration(); // TODO: Initialize to an appropriate value
            _objUserRegistration.Users = new Users();
            _objUserRegistration.Users.UserId = InsertDummyUser("tj_op");
            target.GetEmailNotofication(_objUserRegistration);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.CommentsNotify);
            Assert.AreEqual(21080, _objUserRegistration.EmailNotification.EmailNotifyId);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.EventsNotify);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.GiftsNotify);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.GuestBookNotify);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.MessagesNotify);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.NewsLetterNotify);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.NotesNotify);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.PhotoAlbumNotify);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.PhotosNotify);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.StoryNotify);
            Assert.AreEqual(21085, _objUserRegistration.EmailNotification.UserId);
            Assert.AreEqual(true, _objUserRegistration.EmailNotification.CommentsNotify);
            // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetEmailBodyOnEmailAndPasswordChanged
        ///</summary>
        [TestMethod()]//not tested
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void GetEmailBodyOnEmailAndPasswordChangedTest()
        {
            UserInfoManager_Accessor target = new UserInfoManager_Accessor(); // TODO: Initialize to an appropriate value
            UserInfo objUserInfo = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetEmailBodyOnEmailAndPasswordChanged(objUserInfo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEmailBodyForAdminAcceptDecline
        ///</summary>
        [TestMethod()]//Not tested
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void GetEmailBodyForAdminAcceptDeclineTest()
        {
            UserInfoManager_Accessor target = new UserInfoManager_Accessor(); // TODO: Initialize to an appropriate value
            SessionValue objUserInfo = null; // TODO: Initialize to an appropriate value
            string mail = string.Empty; // TODO: Initialize to an appropriate value
            bool _Accept = false; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetEmailBodyForAdminAcceptDecline(objUserInfo, mail, _Accept);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEmailBody
        ///</summary>
        [TestMethod()]//Not tested
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void GetEmailBodyTest()
        {
            UserInfoManager_Accessor target = new UserInfoManager_Accessor(); // TODO: Initialize to an appropriate value
            UserInfo objUserInfo = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetEmailBody(objUserInfo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEmailAccountBody
        ///</summary>
        [TestMethod()]//not tested
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void GetEmailAccountBodyTest()
        {
            UserInfoManager_Accessor target = new UserInfoManager_Accessor(); // TODO: Initialize to an appropriate value
            Users objUserInfo = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetEmailAccountBody(objUserInfo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEmail
        ///</summary>
        [TestMethod()]//not tested
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void GetEmailTest()
        {
            UserInfoManager_Accessor target = new UserInfoManager_Accessor(); // TODO: Initialize to an appropriate value
            int UserId = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetEmail(UserId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBusinessUserTributeList
        ///</summary>
        [TestMethod()]//NOT being used no page as BusinessUserHome.aspx
        //crashing : PhotoConfiguration.xml not found
        public void GetBusinessUserTributeListTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            SearchTribute objTributeParam = new SearchTribute(); // TODO: Initialize to an appropriate value
            objTributeParam.SearchString = "taj";
            objTributeParam.UserName = "tj_op_business2";
            objTributeParam.TributeType = "All";
            objTributeParam.PageSize = 1;
            objTributeParam.PageNumber = 1;
            //objTributeParam.SortOrder = "ASC";
            //List<SearchTribute> expected = null; // TODO: Initialize to an appropriate value
            string AppDomain = "yourtribute";
            List<SearchTribute> actual = target.GetBusinessUserTributeList(objTributeParam, AppDomain);
            Assert.AreEqual("tajinder", actual[0].TributeName);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EmailAvailable
        ///</summary>
        [TestMethod()]//NOT tested: couldn't get functionality
        public void EmailAvailableTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            string Email = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            //actual = target.EmailAvailable(Email);
           // Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EmailNotifications
        ///</summary>
        [TestMethod()]//NOT tested: Functionality not understood
        public void EmailNotificationsTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] param = null; // TODO: Initialize to an appropriate value
            IList<ParameterTypesCodes> expected = null; // TODO: Initialize to an appropriate value
            IList<ParameterTypesCodes> actual;
            actual = target.EmailNotifications(param);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteTributeAdminis
        ///</summary>
        [TestMethod()]//NOT tested: couldn't get the functionality
        public void DeleteTributeAdminisTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] Params = null; // TODO: Initialize to an appropriate value
            target.DeleteTributeAdminis(Params);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DeleteSessionKeyDetails
        ///</summary>
        [TestMethod()]//not tested
        public void DeleteSessionKeyDetailsTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            string SessionID = string.Empty; // TODO: Initialize to an appropriate value
            target.DeleteSessionKeyDetails(SessionID);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DeleteSentMessages
        ///</summary>
        [TestMethod()]//not tested
        public void DeleteSentMessagesTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] Params = null; // TODO: Initialize to an appropriate value
            target.DeleteSentMessages(Params);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DeleteMyFavourite
        ///</summary>
        [TestMethod()]//NO tested: no favorites could be made
        public void DeleteMyFavouriteTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] _Tributes = null; // TODO: Initialize to an appropriate value
            target.DeleteMyFavourite(_Tributes);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DeleteMessages
        ///</summary>
        [TestMethod()]// NOT tested: No messages
        public void DeleteMessagesTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            object[] Params = null; // TODO: Initialize to an appropriate value
            target.DeleteMessages(Params);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ConformAdmin
        ///</summary>
        [TestMethod()]//NOT tested: session related
        public void ConformAdminTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            Tributes objTributesUserInfo = null; // TODO: Initialize to an appropriate value
            SessionValue objGenralUserInfo = null; // TODO: Initialize to an appropriate value

            bool _Accept = false; // TODO: Initialize to an appropriate value
            target.ConformAdmin(objTributesUserInfo, objGenralUserInfo, _Accept);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CheckFacebookAccountAvailability
        ///</summary>
        [TestMethod()]
        public void CheckFacebookAccountAvailabilityTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value

            GenralUserInfo _objGenralUserInfo = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserName = "yourtribute";
            objUserInfo.UserPassword = "pF+30xcCC/6QBQ0cwBd0Nw==";
            objUserInfo.FacebookUid = null;
            _objGenralUserInfo.RecentUsers = objUserInfo;

            //testing CheckFacebookAccountAvailability function
            target.CheckFacebookAccountAvailability(_objGenralUserInfo);

            //UserInfo objUserInfo = new UserInfo();
            objUserInfo.UserName = "debbi_henkel";
            objUserInfo.UserPassword = "0+pZGcukoCUuTGkyn0Ll2Q==";
            objUserInfo.FacebookUid = 1598003497;
            _objGenralUserInfo.RecentUsers = objUserInfo;

            //testing CheckFacebookAccountAvailability function inside CheckFacebookAccountAvailability function
            object[] objParam = { _objGenralUserInfo.RecentUsers.FacebookUid };
            DataSet _objDataSet = GetDataSet("usp_GetFacebookUser", objParam);

            Assert.AreEqual(1, _objDataSet.Tables[0].Rows.Count);

            if (_objDataSet.Tables[0].Rows.Count > 0)
            {
                Assert.AreEqual(21058, (int)_objDataSet.Tables[0].Rows[0]["UserId"]);
                Assert.AreEqual("debbi_henkel", _objDataSet.Tables[0].Rows[0]["UserName"].ToString());
                Assert.AreEqual("Debbi", _objDataSet.Tables[0].Rows[0]["FirstName"].ToString());
                Assert.AreEqual("Henkel", _objDataSet.Tables[0].Rows[0]["LastName"].ToString());
            }
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CheckBusinessUser
        ///</summary>
        [TestMethod()]
        public void CheckBusinessUserTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            int UserId = InsertDummyBusinessUser("tj_op_business2");
            actual = target.CheckBusinessUser("tj_op_business2");
            Assert.AreEqual(UserId, int.Parse(actual));
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CheckAndSendPassword
        ///</summary>
        [TestMethod()]//NOT tested properly
        //one email is allowed for many users created?? shdnt be allowed.
        // NO _Reset = true case found in CheckAndSendPassword UserInfoResource.cs
        //MailBodies functionality not working
        public void CheckAndSendPasswordTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            GenralUserInfo _objGenralUserInfo = new GenralUserInfo(); // TODO: Initialize to an appropriate value
            _objGenralUserInfo.RecentUsers = new UserInfo();
            _objGenralUserInfo.RecentUsers.UserEmail = "tajinder.kaur@optimusinfo.com";
            _objGenralUserInfo.RecentUsers.UserPassword = "zSpeN+GdR0Ey9VrM9QyvUA==";
            _objGenralUserInfo.RecentUsers.UserName = "tj_op";

            bool _Reset = true; // TODO: Initialize to an appropriate value
            target.CheckAndSendPassword(_objGenralUserInfo, _Reset);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ChangeEmailPassword
        ///</summary>
        [TestMethod()]//NOT tested:Email functionality
        public void ChangeEmailPasswordTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            GenralUserInfo _objGenralUserInfo = null; // TODO: Initialize to an appropriate value
            target.ChangeEmailPassword(_objGenralUserInfo);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UserLogin
        ///</summary>
        [TestMethod()]
        public void UserLoginTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            GenralUserInfo objUser = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();

            int UserId = InsertDummyUser("tj_op");
            objUserInfo.UserName = "tj_op";
            objUserInfo.UserPassword = "zSpeN+GdR0Ey9VrM9QyvUA==";

            objUserInfo.FacebookUid = null;
            objUser.RecentUsers = objUserInfo;

            //testing UserLogin Function
            target.UserLogin(objUser);

            //Testing CheckLogin function inside UserLogin Function
            object[] objParam ={ objUser.RecentUsers.UserName.ToString(), 
                                     objUser.RecentUsers.UserPassword.ToString(),
                                     objUser.RecentUsers.FacebookUid
                                   };

            DataSet _objDataSet = GetDataSetWithoutCheckingIOVS("usp_ValidateWebsiteUser", objParam);

            int count = _objDataSet.Tables[0].Rows.Count;
            Assert.AreEqual(1, count);
            if (count > 0)
            {
                Assert.AreEqual(UserId, (int)_objDataSet.Tables[0].Rows[0]["UserId"]);
                Assert.AreEqual("tj_op", _objDataSet.Tables[0].Rows[0]["UserName"].ToString());
            }
        }
        

        /// <summary>
        ///Bu Ud: test for user login from pop up window
        ///</summary>
        [TestMethod()]// successfully tested for user login
        public void UserLoginPopUpTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            GenralUserInfo objUser = new GenralUserInfo();
            UserInfo objUserInfo = new UserInfo();

            int UserId = GetUserId("udattri");
            objUserInfo.UserName = "udattri";
            objUserInfo.UserPassword = "pF+30xcCC/6QBQ0cwBd0Nw==";

            objUserInfo.FacebookUid = null;
            objUser.RecentUsers = objUserInfo;

            //testing UserLogin Function
            target.UserLogin(objUser);

            //Testing CheckLogin function inside UserLogin Function
            object[] objParam ={ objUser.RecentUsers.UserName.ToString(), 
                                     objUser.RecentUsers.UserPassword.ToString(),
                                     objUser.RecentUsers.FacebookUid
                                   };

            DataSet _objDataSet = GetDataSetWithoutCheckingIOVS("usp_ValidateWebsiteUser", objParam);

            int count = _objDataSet.Tables[0].Rows.Count;
            Assert.AreEqual(1, count);
            if (count > 0)
            {
                Assert.AreEqual(UserId, (int)_objDataSet.Tables[0].Rows[0]["UserId"]);
                Assert.AreEqual("udattri", _objDataSet.Tables[0].Rows[0]["UserName"].ToString());
            }
        }
        /// <summary>
        /// by Ud: Get the UserId of the user.
        /// </summary>
        /// <param name="DummyUser"></param>
        /// <returns></returns>
        public int GetUserId(String DummyUser)
        {
            
            int UserId = -1;
            try
            {

                object[] objParam = { DummyUser };
                DataSet _objDataSet = GetDataSet("usp_AvailableUser", objParam);
                int count = _objDataSet.Tables[0].Rows.Count;
                if (count > 0)
                {
                    String UserName = _objDataSet.Tables[0].Rows[0][0].ToString();
                    if (UserName != "0")
                    {
                        object[] objParam1 ={ DummyUser,
                                            "pF+30xcCC/6QBQ0cwBd0Nw==",
                                     null
                                   };
                        //DataSet _objDataSet = GetDataSet("usp_ValidateUser", objParam);
                        DataSet _objDataSet1 = GetDataSetWithoutCheckingIOVS("usp_ValidateWebsiteUser", objParam1);
                        // ds.Tables[0].
                        if (_objDataSet1.Tables[0].Rows.Count > 0)
                        {
                            UserId = (int)_objDataSet1.Tables[0].Rows[0]["UserId"];
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    //objUser.CustomError = objError;
                    
                }
            }
            return UserId;
        }


        /// <summary>
        ///A test for send password for user  email in case he/she forgot password
        ///</summary>
        [TestMethod()]      // it can't be tested through unit testing because we don't have session at that time
        public void CheckAndSendPasswordPopUpTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            GenralUserInfo _objGenralUserInfo = new GenralUserInfo(); // TODO: Initialize to an appropriate value
            UserInfo objUserInfo = new UserInfo();

            objUserInfo.UserEmail = "yudi.gaurav@gmail.com";
            _objGenralUserInfo.RecentUsers = objUserInfo;           

            bool _Reset = false; // TODO: Initialize to an appropriate value
            target.CheckAndSendPassword(_objGenralUserInfo, _Reset);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");    
                
        }

    }
}