//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.PortalController.PortalControllerController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the files under PortalController.
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;


namespace TributesPortal.PortalController
{
    public class PortalControllerController
    {
    
        public PortalControllerController()
        {
        }

        public void OnLogin(GeneralUser _GeneralUser)
        {

            FacadeManager.UserManager.UserLogin(_GeneralUser);
          
        }

        public void OnValidateUserRole(UserRole userrole)
        {
           FacadeManager.UserManager.UserRole(userrole);
     
        }
        public void OnValidateUser(GeneralUser _GeneralUser)
        {

            FacadeManager.UserManager.UserLogin(_GeneralUser);
           
            //else
            //{
            //    userrole.roles =objResponse.ResponseDetails[0];
            //}

        }
        public void SaveCustomer(Customer customer)
        {
            FacadeManager.CustManager.SaveCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            FacadeManager.CustManager.UpdateCustomer(customer);
        }

        

        public ReadOnlyCollection<CustomerCountry> CountryList()
        {
            return FacadeManager.CustManager.Country.AsReadOnly();
        }
        public List<CustomerState> StateList(string CountryID)
        {
            //CustomerManager objcust;
            //objcust.State = FacadeManager.CustManager.StateList(CountryID);
            //return FacadeManager.CustManager.
            return FacadeManager.CustManager.StateList(CountryID);
        }

        public ReadOnlyCollection<Customer> CustomerList()
        {
            return FacadeManager.CustManager.Customers.AsReadOnly();
        }
    }
}
