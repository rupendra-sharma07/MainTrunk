using TributesPortal.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
using TributesPortal.ResourceAccess;
namespace YourTributeTestProject
{
    
    
    /// <summary>
    ///This is a test class for EventManagerTest and is intended
    ///to contain all EventManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EventManagerTest
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
        ///A test for AddRsvp
        ///</summary>
        [TestMethod()]
        public void AddRsvpTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            CompleteGuestList objGuestToAdd = null; // TODO: Initialize to an appropriate value
            int EventId = 0; // TODO: Initialize to an appropriate value
            int UserId = 0; // TODO: Initialize to an appropriate value
            target.AddRsvp(objGuestToAdd, EventId, UserId);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CreateBody
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void CreateBodyTest()
        {
            EventManager_Accessor target = new EventManager_Accessor(); // TODO: Initialize to an appropriate value
            string strBody = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.CreateBody(strBody);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEmailIdsForEvent
        ///</summary>
        [TestMethod()]
        public void GetEmailIdsForEventTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            int GuestId = 0; // TODO: Initialize to an appropriate value
            List<CompleteGuestList> expected = null; // TODO: Initialize to an appropriate value
            List<CompleteGuestList> actual;
            actual = target.GetEmailIdsForEvent(GuestId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCompleteGuestList
        ///</summary>
        [TestMethod()]
        public void GetCompleteGuestListTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            int EventId = 0; // TODO: Initialize to an appropriate value
            string Hashcode = string.Empty; // TODO: Initialize to an appropriate value
            bool isCreator = false; // TODO: Initialize to an appropriate value
            List<CompleteGuestList> expected = null; // TODO: Initialize to an appropriate value
            List<CompleteGuestList> actual;
            actual = target.GetCompleteGuestList(EventId, Hashcode, isCreator);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EventThemeInfo
        ///</summary>
        [TestMethod()]
        public void EventThemeInfoTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            int eventInvitationCategoryID = 0; // TODO: Initialize to an appropriate value
            string tributeType = string.Empty; // TODO: Initialize to an appropriate value
            IList<EventTheme> expected = null; // TODO: Initialize to an appropriate value
            IList<EventTheme> actual;
            actual = target.EventThemeInfo(eventInvitationCategoryID, tributeType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EventInvitationCategories
        ///</summary>
        [TestMethod()]
        public void EventInvitationCategoriesTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            string tributeType = string.Empty; // TODO: Initialize to an appropriate value
            IList<EventInvitationCategory> expected = null; // TODO: Initialize to an appropriate value
            IList<EventInvitationCategory> actual;
            actual = target.EventInvitationCategories(tributeType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteRsvp
        ///</summary>
        [TestMethod()]
        public void DeleteRsvpTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            int guestId = 0; // TODO: Initialize to an appropriate value
            int additionalGuestId = 0; // TODO: Initialize to an appropriate value
            target.DeleteRsvp(guestId, additionalGuestId);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DeleteEvent
        ///</summary>
        [TestMethod()]
        public void DeleteEventTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            Events objEvent = null; // TODO: Initialize to an appropriate value
            target.DeleteEvent(objEvent);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateRsvp
        ///</summary>
        [TestMethod()]
        public void UpdateRsvpTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            CompleteGuestList objGuestToAdd = null; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.UpdateRsvp(objGuestToAdd);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateEvent
        ///</summary>
        [TestMethod()]
        public void UpdateEventTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            Events objEvent = null; // TODO: Initialize to an appropriate value
            target.UpdateEvent(objEvent);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SendEmail
        ///</summary>
        [TestMethod()]
        public void SendEmailTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            int TribuetId = 0; // TODO: Initialize to an appropriate value
            string strSubject = string.Empty; // TODO: Initialize to an appropriate value
            string strBody = string.Empty; // TODO: Initialize to an appropriate value
            target.SendEmail(TribuetId, strSubject, strBody);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SaveRsvp
        ///</summary>
        [TestMethod()]
        public void SaveRsvpTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            IList<CompleteGuestList> objGuestToAdd = null; // TODO: Initialize to an appropriate value
            int EventId = 0; // TODO: Initialize to an appropriate value
            IList<CompleteGuestList> expected = null; // TODO: Initialize to an appropriate value
            IList<CompleteGuestList> actual;
            actual = target.SaveRsvp(objGuestToAdd, EventId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SaveInvitationCategory
        ///</summary>
        [TestMethod()]
        public void SaveInvitationCategoryTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            EventInvitationCategory objEventInvitationCategory = null; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.SaveInvitationCategory(objEventInvitationCategory);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SaveEventTheme
        ///</summary>
        [TestMethod()]
        public void SaveEventThemeTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            EventTheme objEventTheme = null; // TODO: Initialize to an appropriate value
            object expected = null; // TODO: Initialize to an appropriate value
            object actual;
            actual = target.SaveEventTheme(objEventTheme);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SaveEvent
        ///</summary>
        [TestMethod()]
        public void SaveEventTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            Events objEvent = new Events(); // TODO: Initialize to an appropriate value
            objEvent.UserId = 10345;
            objEvent.TributeType="memorial";
            objEvent.TributeId=4574;            
            objEvent.EventDesc = "iif";
            objEvent.EventStartTime = "2:30 pm";
            objEvent.EventEndTime = "3:30 pm";
            objEvent.EventImage = "babyqaim_New_Baby/Event/1prss001805.jpeg";
            objEvent.EventName = "40th Birthday Celebration";
            objEvent.HostName = "lax";
            objEvent.IsPrivate = true;
            objEvent.Location = "kjuk";
            objEvent.PhoneNumber = "43434";
            objEvent.Address = "gdgg";
            objEvent.IsPrivate = true;
            objEvent.City = "er";
            objEvent.EventTypeName = "fsdfs";
            objEvent.State = "2";
            objEvent.Country = "rr";
            objEvent.FirstName = "mohit";
            objEvent.LastName = "gupta";
            objEvent.ServerURL = "fdfd";
            objEvent.InviteGuestURL = "gdfd";
            objEvent.EmailId = "rr@rr.com";
            objEvent.TributeURL = "fsfd";
            objEvent.TributeName = "gdd";
            objEvent.AllowAdditionalPeople = true;
            objEvent.SendEmailReminder = false;
            objEvent.ShowRsvpStatistics = true;
            objEvent.MealOptions = "yes";
            objEvent.IsAskForMeal = true;
            target.SaveEvent(objEvent);
            Events OEvent= new Events();
            OEvent.UserId = 10345;
            OEvent.TributeType = "memorial";
            OEvent.TributeId = 4574;
            OEvent.EventID = 55;
             target.GetFullEvent(OEvent);
             int expected = 1;
             int actual = 1;
             Assert.AreEqual(expected, actual);
  
        }

        /// <summary>
        ///A test for InviteGuest
        ///</summary>
        [TestMethod()]
        public void InviteGuestTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            Events objEvent = null; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.InviteGuest(objEvent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserIdByTributeId
        ///</summary>
        [TestMethod()]
        public void GetUserIdByTributeIdTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            int tributeId = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetUserIdByTributeId(tributeId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMealOptions
        ///</summary>
        [TestMethod()]
        public void GetMealOptionsTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            int GuestId = 0; // TODO: Initialize to an appropriate value
            CompleteGuestList expected = null; // TODO: Initialize to an appropriate value
            CompleteGuestList actual;
            actual = target.GetMealOptions(GuestId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetImageURL
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void GetImageURLTest()
        {
            EventManager_Accessor target = new EventManager_Accessor(); // TODO: Initialize to an appropriate value
            string imageURL = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetImageURL(imageURL);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TributesPortal.BusinessLogic.dll")]
        public void GetHashCodeTest()
        {
            EventManager_Accessor target = new EventManager_Accessor(); // TODO: Initialize to an appropriate value
            int Source = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetHashCode(Source);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetFullEvent
        ///</summary>
        [TestMethod()]
        public void GetFullEventTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            Events objEvent = null; // TODO: Initialize to an appropriate value
            Events expected = null; // TODO: Initialize to an appropriate value
            Events actual;
            actual = target.GetFullEvent(objEvent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEventThemeByID
        ///</summary>
        [TestMethod()]
        public void GetEventThemeByIDTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            int eventThemeID = 0; // TODO: Initialize to an appropriate value
            EventTheme expected = null; // TODO: Initialize to an appropriate value
            EventTheme actual;
            actual = target.GetEventThemeByID(eventThemeID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEventList
        ///</summary>
        [TestMethod()]
        public void GetEventListTest()
        {
            EventManager target = new EventManager(); // TODO: Initialize to an appropriate value
            Events objEvent = null; // TODO: Initialize to an appropriate value
            IList<Events> expected = null; // TODO: Initialize to an appropriate value
            IList<Events> actual;
            actual = target.GetEventList(objEvent);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetEventInfo
        ///</summary>
        [TestMethod()]
        public void GetEventInfoTest()
        {
            EventResource target = new EventResource();  // TODO: Initialize to an appropriate value
            Events objEvent = new Events();
            objEvent.UserId = 10867;
            objEvent.TributeId = 37794;
            objEvent.TributeType = "Memorial";
            objEvent.EventID = 432;// TODO: Initialize to an appropriate value
            Events expected = null; // TODO: Initialize to an appropriate value
            Events actual;
            actual = target.GetEventInfo(objEvent);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
