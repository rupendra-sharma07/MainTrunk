using TributesPortal.TributePortalAdmin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
using System;

namespace YourTributeTestProject
{
    
    
    /// <summary>
    ///This is a test class for TributePortalAdminControllerTest and is intended
    ///to contain all TributePortalAdminControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TributePortalAdminControllerTest
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
        ///A test for UpdateNewTributeUrlTributeTypeinAlltables
        ///</summary>
        [TestMethod()]
        public void UpdateNewTributeUrlTributeTypeinAlltablesTest()
        {
            TributePortalAdminController target = new TributePortalAdminController();
            UpdateTribute _objUpdateTribute = new UpdateTribute();
            _objUpdateTribute.TributeId = 58478;
            _objUpdateTribute.TributeUrl = "Memorial";
            _objUpdateTribute.TributeType = 2;
            _objUpdateTribute.TypeDescription = "New Baby";
            Tributes _newTribute = new Tributes();
            _newTribute.TributeUrl = "Memorial";
            _newTribute.TributeType = 2;
            _newTribute.TypeDescription = "New Baby";
            _newTribute.Date1 = DateTime.Parse("12/12/2012");
            _newTribute.Date2 = null;
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual ;
            actual = target.UpdateNewTributeUrlTributeTypeinAlltables(_objUpdateTribute, _newTribute);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateTributeExpiry
        ///</summary>
        [TestMethod()]
        public void UpdateTributeExpiryTest()
        {
            TributePortalAdminController target = new TributePortalAdminController(); 
            UpdateTribute updateTribute = new UpdateTribute();
            updateTribute.TributeId = 58478;
            updateTribute.EndDate = DateTime.Parse("12/12/2012");
            bool expected = true; 
            bool actual;
            actual = target.UpdateTributeExpiry(updateTribute);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateTributePackage
        ///</summary>
        [TestMethod()]
        public void UpdateTributePackageTest()
        {
            TributePortalAdminController target = new TributePortalAdminController();
            UpdateTribute updateTribute = new UpdateTribute();
            updateTribute.TributeId = 58478;
            updateTribute.PackageId = 5;
            updateTribute.EndDate = DateTime.Parse("12/12/2012");
            bool expected = true; 
            bool actual;
            actual = target.UpdateTributePackage(updateTribute);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAdminTransactions
        ///</summary>
        [TestMethod()]
        public void GetAdminTransactionsTest()
        {
            TributePortalAdminController target = new TributePortalAdminController(); 
            IList<AdminTributeUpdate> expected = new List<AdminTributeUpdate>();
            IList<AdminTributeUpdate> actual;
            actual = target.GetAdminTransactions();
            Assert.AreNotEqual(expected.Count, actual.Count);
        }

        /// <summary>
        ///A test for GetTributeDetailsOnTributeId
        ///</summary>
        [TestMethod()]
        public void GetTributeDetailsOnTributeIdTest()
        {
            TributePortalAdminController target = new TributePortalAdminController(); // TODO: Initialize to an appropriate value
            int _tributeId = 52795; // TODO: Initialize to an appropriate value
            UpdateTribute expected = new UpdateTribute();
            expected.TributeType = 7;
            UpdateTribute actual;
            actual = target.GetTributeDetailsOnTributeId(_tributeId);
            Assert.AreEqual(expected.TributeType, actual.TributeType);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
