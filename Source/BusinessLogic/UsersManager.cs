///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.UserManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic user methods
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.ResourceAccess;
//using System.Data;
using System.Collections.ObjectModel;
using System.Transactions;

namespace TributesPortal.BusinessLogic
{
    public class UserManager
    {
        List<Locations> _LocationList;

        public List<Locations> LocationList
        {
            //get { return StateList(); }
            set { _LocationList = value; }
        }

        public IList<Locations> Locations(Locations locaton)
        {

            LocationResource objLocationResource = new LocationResource();
            return objLocationResource.LocationList(locaton);
        }

        public void UserLogin(GeneralUser _GeneralUser)
        {
            UserResource objUser = new UserResource();
            object[] param ={ _GeneralUser };
            objUser.CheckLogin(param);
            
        }

        public void UserRole(UserRole userid)
        {
            
            UserResource obj = new UserResource();
            object[] param ={ userid };
            obj.GetData(param);
        }
        public IList<ParameterTypesCodes> BusinessTypes()
        {
            ParameterResource objParameterResource = new ParameterResource();
            return objParameterResource.BusinessTypes();
        }

        /// <summary>
        /// Method to search user based on the entered criteria.
        /// </summary>
        /// <param name="objUser">Filled Users entity.</param>
        /// <returns>List of users.</returns>
        public List<Users> SearchUsers(Users objUser)
        {
            UserResource objUserResource = new UserResource();
            object[] param = {objUser};
            return objUserResource.SearchUsers(param);
        }

        /// <summary>
        /// Method to delete user.
        /// </summary>
        /// <param name="objUser">User entity containing userid to be deleted.</param>
        public void DeleteUser(Users objUser)
        {
            UserResource objUserResource = new UserResource();
            object[] param = { objUser };

            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    objUserResource.Delete(param);
                    trans.Complete();  // added by ANKI 05-Feb-09
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public AddRemoveCreditInfo AddOrDebitCredits(AddRemoveCreditInfo objUser)
        {
            UserResource objUserResource = new UserResource();
            object[] param = { objUser };
            return objUserResource.AddOrDebitCredits(param);
        }

        public EnableRSSFeedInfo EnableRSSFeedForBussUser(EnableRSSFeedInfo objFeed)
        {
            UserResource objUserResource = new UserResource();
            object[] param = { objFeed };
            return objUserResource.EnableRSSFeedForBussUser(param);
        }
        public int GetCountryIdByName(string countryName)
        {
            LocationResource objLocationResource = new LocationResource();
            return objLocationResource.GetCountryIdByName(countryName);
        }
        public int GetstateIdByName(object[] objValue)
        {
            LocationResource objLocationResource = new LocationResource();
            return objLocationResource.GetstateIdByName(objValue);
        }

        public EnableRSSFeedInfo EnableXmlFeedForBussUser(EnableRSSFeedInfo enableRSSFeedInfo)
        {
            UserResource objUserResource = new UserResource();
            object[] param = { enableRSSFeedInfo };
            return objUserResource.EnableXmlFeedForBussUser(param);
        }
    }
}
