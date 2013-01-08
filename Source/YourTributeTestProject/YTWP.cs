using Microsoft.VisualStudio.TestTools.UnitTesting;
using TributesPortal.BusinessLogic;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;

namespace YourTributeTestProject
{

    /// <summary>
    ///This is a test class for UserInfoManagerTest and is intended
    ///to contain all UserInfoManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserInfoManagerAtomTest
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


        /// <summary>
        ///A test for GetTributesFeed
        ///</summary>
        [TestMethod()]
        public void GetTributesFeedTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            Tributes objtribute = new Tributes();
            objtribute.UserTributeId = 9;
            object[] _MyTributes = { objtribute, 1, 1, 1000 };// TODO: Initialize to an appropriate value
            //object[] _MyTributes = new object(); 
            int expectedCount = 265;
            //List<GetTributesFeed> lstTributes = target.GetTributesFeed(_MyTributes);
            //lstTributes = target.GetTributesFeed(_MyTributes);
            List<GetTributesFeed> actual = new List<GetTributesFeed>();
            //actual = target.GetTributesFeed(_MyTributes);
            Assert.AreEqual(expectedCount, actual.Count);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserDetailsOnUserId
        ///</summary>
        [TestMethod()]
        public void GetUserDetailsOnUserIdTest()
        {
            UserInfoManager target = new UserInfoManager(); // TODO: Initialize to an appropriate value
            int userId = 9; // TODO: Initialize to an appropriate value
            Users expected = new Users();
            expected.UserType = 2;// TODO: Initialize to an appropriate value
            Users actual = new Users();
            actual = target.GetUserDetailsOnUserId(userId);
            Assert.AreEqual(expected.UserType, actual.UserType);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EnableRSSFeedForBussUser
        ///</summary>
        [TestMethod()]
        public void EnableRSSFeedForBussUserTest1()
        {
            UserManager target = new UserManager(); // TODO: Initialize to an appropriate value
            EnableRSSFeedInfo objFeed = new EnableRSSFeedInfo(); // TODO: Initialize to an appropriate value
            objFeed.UserId = 9;
            objFeed.AtomEnabled = true;
            EnableRSSFeedInfo expected = new EnableRSSFeedInfo(); // TODO: Initialize to an appropriate value
            expected.UpdateOutput = 1;
            EnableRSSFeedInfo actual = new EnableRSSFeedInfo(); ;
            actual = target.EnableRSSFeedForBussUser(objFeed);
            Assert.AreEqual(expected.UpdateOutput, actual.UpdateOutput);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }
}
