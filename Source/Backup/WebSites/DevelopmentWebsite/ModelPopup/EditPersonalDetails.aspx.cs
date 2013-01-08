
using System;
using System.Data;
using System.Configuration;
using System.Collections;
#region USING DIRECTIVES


using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Story.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Video.Views;

#endregion
namespace ModelPopup
{
    public partial class ModelPopup_EditPersonalDetails : PageBase, IEditPersonalDetails
    {
        #region CLASS VARIABLES

        private SessionValue objSessionValue = null;
        protected string todayDay = DateTime.Now.Day.ToString();
        protected string todayMonth = DateTime.Now.Month.ToString();
        protected string todayYear = DateTime.Now.Year.ToString();
        private string _FirstName = "";
        private string _LastName = "";
        protected string _TributeName;
       
        private string _TributeURL;
        private int _UserId;
        protected int _TributeId;
        protected string _TributeType = "Video";
        private EditPersonalDetailsPresenter _presenter;
        private bool _IsAdmin;
        private Tributes objTribute = null;
        protected string _NewBaby;
        protected string _Memorial= "Video";
        protected string _Birhday;
        protected bool _isActive;
        private int _age;        
        private const int DefaultWidth = 374;
        private const int DefaultHeight = 247;
        


        private string RequestUrl = "";





        #endregion

        #region Create Presenter

        [CreateNew]
        public EditPersonalDetailsPresenter Presenter
        {
            set
            {
                this._presenter = value;
                this._presenter.View = this;
            }
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

            error1.Visible = false;
            error2.Visible = false;
            GetValuesFromSession();

            if (TributeID != 0)
            {
                if (!this.IsPostBack)
                {

                    //lbtnSavePersonalDetail.Attributes.Add("onclick", "javascript:return false;");
                    //ctl00_ModuleContentPlaceHolder_panPersonalDetailView.Visible = true;
                    //editprofile.Visible = true;
                    //crropper.Visible = false;
                    FillValuesDate1Month();
                    FillValuesDate2Month();

                    this._presenter.GetCountriesList();
                    this._presenter.GetStateList();

                    GetTributePersonalEditDetails();                    

                }
            }

        }

        protected void lbtnCancelPersonalDetail_Click(object sender, EventArgs e)
        {
            string strJScript = string.Empty;
            strJScript = "<script language=javascript>";
            strJScript += "parent.modalClose();";
            strJScript += "</script>";

            Response.Write(strJScript);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                // get the State list for selected country
                this._presenter.GetStateList();

            }
        }


        protected void lbtnSavePersonalDetail_Click(object sender, EventArgs e)
        {
            try
            {
                bool pass1 = true;
                bool pass2 = false;
                bool pass3 = true;
                DateTime objDob = new DateTime();
                DateTime objDod = new DateTime();
                

                DateTime.TryParse(ddlDate2Month.SelectedValue + "-" + ddlDate2Day.SelectedValue + "-" + txtDate2Year.Text, out objDod);
                if ((objDod.Year > 1800) && ((objDod.Date - DateTime.Today.Date).Days < 0))
                {
                    pass2 = true;
                    error2.Visible = false;
                }
                else
                {
                    pass2 = false;
                    error2.Visible = true;
                }
                if ((!string.IsNullOrEmpty(txtDate1Year.Text)))
                {
                    pass1 = DateTime.TryParse(ddlDate1Month.SelectedValue + "-" + ddlDate1Day.SelectedValue + "-" + txtDate1Year.Text, out objDob);
                    if (objDob.Year > 1800 && ((objDob.Date - DateTime.Today.Date).Days < 0))
                    {
                        if ((objDod.Date - objDob.Date).Days > 0)
                        {
                            pass3 = true;
                            pass1 = true;
                            error1.Visible = false;
                        }
                        else
                        {
                            pass3 = false;
                            pass1 = false;
                            error1.Visible = true;
                        }
                    }
                    else
                    {
                        pass1 = false;
                        error1.Visible = true;
                    }
                }

                if ((_TributeId > 0) && pass1 && pass2 && pass3)
                {
                    string strJScript = string.Empty;
                    this._presenter.UpdateVideoTributeDetail(_TributeId, _UserId);

                    strJScript = "<script language=javascript>";
                    //strJScript += "parent.modalClose();";
                    strJScript += "parent.ReloadPage();";
                    //strJScript += "window.location.href='video/videotribute.aspx?tributeId=" + _TributeId + "';";
                    //strJScript += "parent.window.location.href=parent.window.location.href";

                    //strJScript += "parent.window.location.reload(true);";
                    //strJScript += "window.location.reload(true);";
                    //strJScript += "parent.location.reload(true);";
                    strJScript += "</script>";

                    Response.Write(strJScript);

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        public string TributeName
        {
            get
            {
                string name = txtName.Text.ToString().Trim();
                if (name.Length > 40)
                {
                    _TributeName = name.Substring(0, 40);
                    return name.Substring(0, 40);
                }
                else
                {
                    _TributeName = name;
                    return name;
                }

            }
            set
            {
                _TributeName = value;
                txtName.Text = value;
            }
        }

       

        public string Date1Day
        {
            set
            {
                ddlDate1Day.SelectedValue = value;
            }

            get
            {
                if (ddlDate1Day.Visible == true)
                {
                    return ddlDate1Day.SelectedValue;
                }
                else
                {
                    return null;
                }

            }
        }

        public string Date1Month
        {
            set
            {
                ddlDate1Month.SelectedValue = value;
                //ddlDateDOBMonth.SelectedValue = value;
                //ddlDate1Month.SelectedItem.Value = value;
            }

            get
            {
                if (ddlDate1Month.Visible == true)
                {
                    return ddlDate1Month.SelectedValue;
                }
                else
                {
                    return null;
                }
            }
        }

        public string Date1Year
        {
            set
            {
                txtDate1Year.Text = value;
            }

            get
            {
                if (txtDate1Year.Visible == true)
                {
                    if (string.IsNullOrEmpty(txtDate1Year.Text))
                    {
                        txtDate1Year.Text = "1780";
                    }

                    return txtDate1Year.Text;
                }
                return null;
            }
        }

        public string Date2Day
        {
            set
            {
                ddlDate2Day.SelectedValue = value;
            }

            get
            {
                if (ddlDate2Day.Visible == true)
                {
                    return ddlDate2Day.SelectedValue;
                }
                else
                {
                    return null;
                }
            }
        }

        public string Date2Month
        {
            set
            {
                ddlDate2Month.SelectedValue = value;
            }

            get
            {
                if (ddlDate2Month.Visible == true)
                {
                    return ddlDate2Month.SelectedValue;
                }
                else
                {
                    return null;
                }
            }
        }

        public string Date2Year
        {
            set
            {
                txtDate2Year.Text = value;
            }

            get
            {
                if (txtDate2Year.Visible == true)
                {
                    return txtDate2Year.Text;
                }
                else
                {
                    return null;
                }
            }
        }

       

        public int Age
        {
            set
            {
                _age = value;
                if (_age != null)
                    lblAge.Visible = true;
                if (_age < 1)
                {
                    lblAge.Text = "<1";

                }
                else
                {
                    lblAge.Text = _age.ToString();
                }
            }
        }

        public string City
        {
            set
            {
                txtCity.Text = value;
            }

            get
            {
                string city = txtCity.Text.ToString().Trim();
                if (city.Length > 50)
                {
                    return city.Substring(0, 50);
                }
                else
                {
                    return city;
                }
            }
        }

        public string State
        {
            set
            {
                //ddlState.SelectedValue = value;
                if (value != null && value.Length > 0)
                {
                    this._presenter.GetStateList();
                    if (ddlState.Items.FindByValue(value) != null)
                    {
                        ddlState.Items.FindByValue(value).Selected = true;
                    }
                }
            }

            get
            {
                return ddlState.SelectedValue.ToString();
            }
        }

        public string Country
        {
            set
            {
                ddlCountry.SelectedValue = value;
            }

            get
            {
                return ddlCountry.SelectedValue.ToString();
            }
        }

        public int UserID
        {
            get
            {
                int kl = 88;
                return kl;
                //_UserId;
            }

            set
            {
                UserID = value;
            }
        }


        public int TributeID
        {
            get
            {
                int k = 4;
                return k;
            }

            set
            {
                TributeID = value;
            }
        }
        public IList<Locations> CountryList
        {
            set
            {
                ddlCountry.DataSource = value;
                ddlCountry.DataTextField = Locations.Location.LocationName.ToString();
                ddlCountry.DataValueField = Locations.Location.LocationId.ToString();
                ddlCountry.DataBind();
            }
        }

        public IList<Locations> StateList
        {
            set
            {
                ddlState.Items.Clear();
                if (value.Count > 0)
                {
                    ddlState.DataSource = value;
                    ddlState.DataTextField = Locations.Location.LocationName.ToString();
                    ddlState.DataValueField = Locations.Location.LocationId.ToString();
                    ddlState.DataBind();
                    ddlState.Enabled = true;
                }
                else
                    ddlState.Enabled = false;
            }
        }
        
        #endregion

        
       

        


        #region Location

        /// <summary>
        /// This method will return the location by combining the city, state and country
        /// </summary>
        /// <param name="objStory">A Story object which contain teh city, state and country</param>
        /// <returns>Return the location</returns>
        private string GetLocation(Stories objStory)
        {
            string location = "";

            try
            {
                if (objStory.City != "")
                {
                    location = objStory.City;
                }

                if (objStory.StateName != "")
                {
                    if (location != "")
                    {
                        location += ", ";
                    }

                    location += objStory.StateName;
                }

                if (location != "")
                {
                    location += ", ";
                }

                location += objStory.CountryName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return location;
        }



        /// <summary>
        /// This method will create a Location object 
        /// </summary>
        /// <param name="ID">A int variable which contain location ID</param>
        /// <returns>This method will return the Location object</returns>
        private Locations Locationid(string ID)
        {
            try
            {
                Locations objLocations = new Locations();

                if (ID != null)
                {
                    objLocations.LocationParentId = int.Parse(ID);
                }
                else
                {
                    objLocations.LocationParentId = 0;
                }

                return objLocations;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Date formate function
        /// <summary>
        /// This method will format a Date object by passed date, month and year value
        /// </summary>
        /// <param name="day">A string object which contain the day</param>
        /// <param name="month">A string object which contain the month</param>
        /// <param name="year">A string object which contain the Year</param>
        /// <returns>This method will return the DateTime object</returns>
        private DateTime FormatDate(string day, string month, string year)
        {
            DateTime Date1;

            // Format the created after Date and time
            string afterDate = month + "/" + day + "/" + year;

            try
            {
                Date1 = DateTime.Parse(afterDate.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Date1;
        }

        #endregion

        #region Populate Tribute data in editable  mode


        private void GetTributePersonalEditDetails()
        {
            _presenter.populateTributeDetail(_TributeId);
        }
        #endregion

        #region Fill controls during page load


    
        private void FillValuesDate1Month()
        {
            if (ddlDate1Month.Items.Count <= 0)
            {
                int i = 0;
                ListItem item = new ListItem("", i.ToString());

                ddlDate1Month.Items.Add(item);
                //ddlDateDOBMonth.Items.Add(item);
                for (i = 1; i <= 12; i++)
                {
                    string month = "Month" + i + "_ST";
                    item = new ListItem(ResourceText.GetString(month), i.ToString());
                    ddlDate1Month.Items.Add(item);
                }
            }
        }
        private void FillValuesDate2Month()
        {
            if (ddlDate2Month.Items.Count <= 0)
            {
                int i = 0;
                ListItem item = new ListItem("", i.ToString());

                ddlDate2Month.Items.Add(item);
                for (i = 1; i <= 12; i++)
                {
                    string month = "Month" + i + "_ST";
                    item = new ListItem(ResourceText.GetString(month), i.ToString());
                    ddlDate2Month.Items.Add(item);
                }
            }
        }
        /// <summary>
        /// This function will get the values ( Tribute Detail) from the session 
        /// </summary>
        private void GetValuesFromSession()
        {
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                // get values from session
                StateManager objStateManager = StateManager.Instance;

                //to get user id from session as user is logged in user
                objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
                if (objSessionValue != null)
                {
                    _UserId = objSessionValue.UserId;
                    _FirstName = objSessionValue.FirstName;
                    _LastName = objSessionValue.LastName;

                }
                else
                {
                    _IsAdmin = false;
                }

                if (Session["TributeId"] != null)
                {
                    _TributeId = Convert.ToInt32(Session["TributeId"].ToString());

                }
                else
                {
                    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
                }
                //if (Session["TributeName"] != null)
                //{
                //    _TributeName = Session["TributeName"].ToString();
                //    _TributeName = "asdf";

                //}
                //else
                //{
                //    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
                //}
                //if (Session["UserId"] != null)
                //{
                //    _UserId = Convert.ToInt32(Session["UserId"].ToString());

                //}
                //else
                //{
                //    Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()), false);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


       

    }

}
