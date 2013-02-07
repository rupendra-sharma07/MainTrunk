<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent"  Runat="Server">
    <div id="divShowModalPopup"></div>
<script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
      
             //HttpApplication app = (HttpApplication)sender;
             string strHostDomain = "";

            string subDomainName = string.Empty;

            strHostDomain = Request.ServerVariables["SERVER_NAME"];
            string[] urlArr = strHostDomain.Split(".".ToCharArray());

            if (urlArr.Length > 2)
            {
                  if (urlArr.Length == 4)
                    subDomainName = urlArr[1];
                else if (urlArr.Length == 3)
                    subDomainName = urlArr[0];

                  if (subDomainName != "www" && subDomainName != "your-tribute-dev") 
                  {
                      //Added condition to avoid redirection in case of tribute domain sites
                      if (subDomainName != "memorial" && subDomainName != "newbaby" && subDomainName != "wedding" && subDomainName != "anniversary" && subDomainName != "graduation" && subDomainName != "birthday" && subDomainName != "video")
                      {
                          Response.Redirect("businessuserhome.aspx?username=" + subDomainName);
                          //Server.Transfer("myhome/businessuserhome.aspx?username=" + subDomainName);
                      }
                  }
            }
            else
            {
                // Validate for Mobile
                if (Request.Browser.IsMobileDevice)
                {
                    if (Request.QueryString["NoRedirection"] == null)
                    {
                        Server.Transfer("Tribute/Home.aspx", false);
                    }
                    else
                    {
                        Server.Transfer("Tribute/Home.aspx?NoRedirection=" + Request.QueryString["NoRedirection"]);
                    }
                }
           //     //Response.Redirect("Home.aspx");
            }
           // //Response.Redirect("Home.aspx");
           //Server.Transfer("Tribute/Home.aspx");
        }
    </script>
</asp:Content>
