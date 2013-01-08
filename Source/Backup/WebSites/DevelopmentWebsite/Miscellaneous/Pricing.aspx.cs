///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Miscellaneous.Pricing.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page informs the users about the pricing policies followed by YourTribute
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Configuration;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Miscellaneous.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;


public partial class Miscellaneous_Pricing : PageBase, IPricing
{
    private PricingPresenter _presenter;
    public string AppendString = string.Empty;
    public string ValidityText = "7";
    public int UserType = 1;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments")
        {
            //LHK: new title
            pricingTitle.InnerHtml = "Pricing & Sign Up - Create a free Website for your event with Your Moments";
        }
        StateManager stateManager = StateManager.Instance;
        SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

        //lblPhotoYearlyAmount.Text = WebConfig.PhotoOneyearAmount;
        //lblPhotoLifeTimeAmount.Text = WebConfig.PhotoLifeTimeAmount;
        //lblTributeYearlyAmount.Text = WebConfig.TributeOneyearAmount;
        //lblTributeLifeTimeAmount.Text = WebConfig.TributeLifeTimeAmount;
        lblPrsnlUserTributeYearly.Text = WebConfig.TributeOneyearAmount;
        lblPrsnlUserTributeLifeTime.Text = WebConfig.TributeLifeTimeAmount;

        if (!Equals(objSessionvalue, null))
        {
            UserType = objSessionvalue.UserType;
            if (objSessionvalue.UserType == 2)
            {
                lblPhotoYearlyAmount.Text = WebConfig.PhotoYearlyCreditCost;
                lblPhotoLifeTimeAmount.Text = WebConfig.PhotoLifeTimeCreditCost;
                lblTributeYearlyAmount.Text = WebConfig.TributeYearlyCreditCost;
                lblTributeLifeTimeAmount.Text = WebConfig.TributeLifeTimeCreditCost;
                ValidityText = "30";

                divBusinessUser.Visible = true;
                divPersonalUser.Visible = false;
            }
        }
        if (Request.QueryString["TributeType"] != null)
        {
            AppendString = "&TributeType=" + Request.QueryString["TributeType"];
        }
        if (Request.QueryString["Type"] != null)
        {
            AppendString = AppendString + "&Type=" + Request.QueryString["Type"];
        }
        if (Request.QueryString["VideoTributeId"] != null)
        {
            AppendString = "&VideoTributeId=" + Request.QueryString["VideoTributeId"];
        }
    }

    [CreateNew]
    public PricingPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    // TODO: Forward events to the presenter and show state to the user.
    // For examples of this, see the View-Presenter (with Application Controller) QuickStart:
    //		ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.wcsf.2007jun/wcsf/html/02-480-ViewPresenter.htm

}


