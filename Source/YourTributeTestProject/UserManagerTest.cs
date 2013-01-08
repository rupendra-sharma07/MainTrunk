using TributesPortal.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;

namespace YourTributeTestProject
{
    
    
    /// <summary>
    ///This is a test class for UserManagerTest and is intended
    ///to contain all UserManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserManagerTest
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


        /// <summary>
        ///A test for UserRole
        ///</summary>
        [TestMethod()]
        public void UserRoleTest()
        {
            UserManager target = new UserManager(); // TODO: Initialize to an appropriate value
            UserRole userid = null; // TODO: Initialize to an appropriate value
            target.UserRole(userid);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UserLogin
        ///</summary>
        [TestMethod()]
        public void UserLoginTest()
        {
            UserManager target = new UserManager(); // TODO: Initialize to an appropriate value
            GeneralUser _GeneralUser = null; // TODO: Initialize to an appropriate value
            target.UserLogin(_GeneralUser);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Locations
        ///</summary>
        [TestMethod()]
        public void LocationsTest()
        {
            UserManager target = new UserManager(); // TODO: Initialize to an appropriate value
            Locations locaton = null; // TODO: Initialize to an appropriate value
            IList<Locations> expected = null; // TODO: Initialize to an appropriate value
            IList<Locations> actual;
            actual = target.Locations(locaton);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteUser
        ///</summary>
        [TestMethod()]
        public void DeleteUserTest()
        {
            UserManager target = new UserManager(); // TODO: Initialize to an appropriate value
            Users objUser = null; // TODO: Initialize to an appropriate value
            target.DeleteUser(objUser);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BusinessTypes
        ///</summary>
        [TestMethod()]
        public void BusinessTypesTest()
        {
            UserManager target = new UserManager(); // TODO: Initialize to an appropriate value
            IList<ParameterTypesCodes> expected = null; // TODO: Initialize to an appropriate value
            IList<ParameterTypesCodes> actual;
            actual = target.BusinessTypes();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SearchUsers
        ///</summary>
        [TestMethod()]
        public void SearchUsersTest()
        {
            UserManager target = new UserManager(); // TODO: Initialize to an appropriate value
            Users objUser = null; // TODO: Initialize to an appropriate value
            List<Users> expected = null; // TODO: Initialize to an appropriate value
            List<Users> actual;
            actual = target.SearchUsers(objUser);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
