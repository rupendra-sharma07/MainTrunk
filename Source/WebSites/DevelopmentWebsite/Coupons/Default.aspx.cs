using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TributesPortal.Coupons.Views;
using Microsoft.Practices.ObjectBuilder;
using TributePortalSecurity;

public partial class Coupons_Default : System.Web.UI.Page, IDefaultView
{
    private DefaultViewPresenter _presenter;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this._presenter.OnViewInitialized();
        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public DefaultViewPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       // Security objsec = new Security();
        TextBox2.Text = Security.EncryptSymmetric(TextBox1.Text);
     
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = Security.DecryptSymmetric(TextBox2.Text);
    }
}

