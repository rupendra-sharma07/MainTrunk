///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Story.Views.IViewStory.cs
///Author          : 
///Creation Date   : 
///Description     :  This is the Interface for the Story.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

#endregion

/// <summary>
///Tribute Portal-Story Interface Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

//
/// </summary>
/// 

namespace TributesPortal.Story.Views
{
    public interface IViewStory
    {
        #region PROPERTIES

        int UserID { get; set; }
        int TributeID { get; set; }
        string Operation { get; set;}

        // Properties for Personal detail section
        string TributeName { get; set; }
        string TributeImage { get; set; }
        string TributeType { get; set; }
        string Date1Value { set; }
        //string Date1ValueEdit { set; }
        string Date2Value { set; }
        //string Date2ValueEdit { set; }
        string Date1Text { set; }
        bool Date1VisibleView { set; }
        bool Date2VisibleView { set; }
        string Date2Text { set; }
        bool Date2VisibleEdit { set; }
        string Date1Day { get; set; }
        string Date1Month { get; set; }
        string Date1Year { get; set; }
        string Date2Day { get; set; }
        string Date2Month { get; set; }
        string Date2Year { get; set; }
        string Age { set; }
        string City { get; set;}
        string State { get; set; }
        string Country { get;  set; }
        string Location { get; set;}
        IList<Locations> CountryList { set; }
        IList<Locations> StateList { set; }
        bool isVisibleDate2 { set; }

        // Properties for stories and MoreAbout section
        int SectionId { get; set; }
        int UserBiographyId { get; set; }
        string StoryDetail { get; set;}
        string PrimaryTitle { get; set;}
        string SecondaryTitle { get; set;}
        string SectionAnswer { get; }
        string NewTopic { get;}
        IList<StoryMoreAbout> MoreAbout { set; }
        IList<StoryMoreAbout> TopicList { set; }

        // Properties to visible and invisible the all the three section
        bool IsVisiblePersonalDetailView { set; }
        bool IsVisibleStoryView { set; }
        bool IsVisibleMoreAboutView { set; }
        bool IsVisibleEdit { set; }
        bool IsVisibleAddNewTopic { set; }

        // Properties to set the CSS class of the panel
        string CSSClassMoreAboutRows { set; }
        string CSSClassPersonalDetail { set; }
        string CSSClassStory { set; }
        string CSSClassMoreAbout { set; }

        bool isRequiredFieldDate1 { set;}
        bool isRequiredFieldDate2 { set;}

        bool IsAdmin { get; set;}

        string StoryImagePrevURL { set; }

        object[] storyViewState { get; set; }
        object[] LocationViewState { get; set; }

        string FirstName { get;}
        string LastName { get;}
        string UrlToEmail { get;}

        // Properties to visible and invisible the Obituary section
        bool IsVisibleObituaryView { set; }

        string ObPostMessage { get; set; }
        string ObMessageWithoutHtml { get; set; }

        #endregion
    }
}




