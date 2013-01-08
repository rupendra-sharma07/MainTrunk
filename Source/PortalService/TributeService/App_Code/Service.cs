using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool Login(string strUsername,string strPassword)
    {
        
        if (strUsername == "sopra" && strPassword == "123")
            return true;
        else
            return false;

    
    }
    [WebMethod]
    public string GetVideoPath(string strUsername, string strPassword)
    {
        return "<tributeservice><path>c:\\</path><pid>100</pid></tributeservice>";

    }
    [WebMethod]
    public bool SetVideoUploadStatus(string pid,string status)
    {
        return true;
    }
    
}
