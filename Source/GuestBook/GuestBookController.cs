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
///File Name       : TributesPortal.GuestBook.GuestBookController.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Controller class for the GuestBook
///Audit Trail     : Date of Modification  Modified By         Description



using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.BusinessEntities;
using TributesPortal.BusinessLogic;

namespace TributesPortal.GuestBook
{
    public class GuestBookController
    {
        public GuestBookController()
        {
            
        }

        public void SaveComment(Comments Comment)
        {
            FacadeManager.CommentMgr.SaveComment(Comment);
        }

        public void SaveComment(Comments Comment,string topUrl)
        {
            FacadeManager.CommentMgr.SaveComment(Comment, topUrl);
        }

        public void DeleteComment(Comments Comment)
        {
            FacadeManager.CommentMgr.DeleteComment(Comment);
        }

        public List<CommentTributeAdministrator> CommentList(CommentTributeAdministrator objSession)
        {
            return FacadeManager.CommentMgr.CommentList(objSession);
        }

        public int RecordCount(CommentTributeAdministrator objSesion)
        {
            return FacadeManager.CommentMgr.RecordCount(objSesion);
        }


    }
}
