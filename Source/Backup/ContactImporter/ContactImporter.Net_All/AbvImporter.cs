using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util;

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for AbvImporter.
	/// </summary>
	public class AbvImporter
	{
		private bool isLoggedIn = false;
		private String userId=String.Empty;
		private String password=String.Empty;
		private String htmlResponse = "";
		private String postString="";
		private String reffer = "";
		private String addressBookURL = "";
		private HttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();
		private String url="";

		public AbvImporter()
		{
			httpUtils=new HttpUtils();
		}
		public AbvImporter(String userid,String password)
		{
			httpUtils=new HttpUtils();
			this.userId=userid;
			this.password=password;
		}
		public String UserId
		{
			get
			{
				return userId;
			}
			set
			{
				Logout(true);
				userId = value;
			}
		}

		public String Password
		{
			get
			{
				return password;
			}
			set
			{
				Logout(true);
				password = value;
			}
		}
		public bool IsLoggedIn
		{
			get
			{
				return isLoggedIn;
			}
		}

		public String[] Names
		{
			get
			{
				return (String[])(names.ToArray(typeof(String)));
			}
		}

		public String[] Emails
		{
			get
			{
				return (String[])(emails.ToArray(typeof(String)));
			}
		}
		private void Logout(bool keepUserInfo)
		{
			isLoggedIn = false;
			if(!keepUserInfo)
			{
				userId = "";
				password = "";
			}
			names = null;
			emails = null;
		}

		public void Logout()
		{
			Logout(false);
		}
		public bool DoLogin(String userId,String password)
		{
			this.userId = userId;
			this.password = password; 
			return DoLogin();
		}
		public bool DoLogin()
		{
			Logout(true);
			String homePage = "http://www.abv.bg/";
			String loginUrl = "";
			String frm = "";
	     	String login="";
			String mailUrl="";

			try
			{
				htmlResponse=httpUtils.GetHttpResponse(homePage,false);
				frm=htmlResponse.Substring(htmlResponse.IndexOf("<form name=\"LogInForm\" method=\"post\" action=\"")+("<form name=\"LogInForm\" method=\"post\" action=\"").Length);
				loginUrl=frm.Substring(0,frm.IndexOf("\""));
				frm=frm.Substring(0,frm.IndexOf("</form>"));
				postString=StringUtils.HiddenFields(frm);
				postString=postString+"&hostname=abv.bg&username="+this.userId+"&password="+password;
				login=httpUtils.GetHttpResponse(loginUrl,false,postString,homePage);
				if(login.IndexOf("<form name=\"LogInForm\" method=\"post\" action=\"")!=-1)
				{
					isLoggedIn=false;
				}
				else
				{
					
					mailUrl=login.Substring(login.IndexOf("window.location.replace(\"")+("window.location.replace(\"").Length);
					mailUrl=mailUrl.Substring(0,mailUrl.IndexOf("\");"));	
					mailUrl=httpUtils.GetHttpResponse(mailUrl,true,"","","",true);

					if(mailUrl.IndexOf("window.location.replace(\"")!=-1)
					{
						isLoggedIn=false;
					}
					else
					{
						this.url=mailUrl.Substring(mailUrl.IndexOf("<a href=\"")+"<a href=\"".Length);
						this.url=this.url.Substring(0,this.url.IndexOf("\">"));
						mailUrl=httpUtils.GetHttpResponse(this.url,true,"","","",true);
						mailUrl=mailUrl.Substring(mailUrl.IndexOf("Location: ")+"Location: ".Length);
						
						this.url=mailUrl.Substring(0,mailUrl.IndexOf("."));
						mailUrl=mailUrl.Substring(0,mailUrl.IndexOf("\r\n"));
						htmlResponse=httpUtils.GetHttpResponse(mailUrl,false,"","","");
						
						isLoggedIn=true;
					}
				}
			}
			catch
			{
				isLoggedIn=false;
			}
			return isLoggedIn;
			
		}
		public bool ImportContacts()
		{
			this.names = new ArrayList();
			this.emails = new ArrayList();
			
			
		
			addressBookURL=this.url+".abv.bg/app/j/addrexport.jsp";
			String post="action=EXPORT&group_id=0&program=20";
			reffer=this.url+".abv.bg/app/j/addrexport.jsp";
			String ExportUrl=this.url+".abv.bg/app/servlet/addrimpex";

			try
			{
				htmlResponse = httpUtils.GetHttpResponse(this.addressBookURL,false);
				htmlResponse = httpUtils.GetHttpResponse(ExportUrl,false,post,addressBookURL,"");

				System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlResponse));
				System.IO.StreamReader str = new System.IO.StreamReader(strm);
				int count=0,counter;
				addresses b=new addresses();
				String temp_name="",temp_email="";
				CSVReader csv = new CSVReader(str); 
				string[] fields;
				string[] temp_line;
				while ((fields = csv.GetCSVLine()) != null) 
				{
					temp_line=new string[100];
					if(count!=0)
					{
						counter=0;
						foreach (string field in fields) 
						{   
							temp_line[counter]=field;
							counter++;
						}
						temp_name=(temp_line[0]+" "+temp_line[1]+" "+temp_line[2]); 
						
						temp_email=(temp_line[4]!="") ? temp_line[4]:(((temp_line[16]!="")? temp_line[16]:((temp_line[17]!="") ? temp_line[17]:"")));
						if(temp_email!=null && temp_name==null)
						{
							temp_name=temp_email.Substring(0,temp_email.IndexOf("@"));
						}
						if(temp_email!=null && temp_name!=null)
						{
							names.Add(temp_name);
							emails.Add(temp_email);
						}
					}
					count++;
				}
				if(names.Count>0) 
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch(WebException ex)
			{
				Logout();
				return false;
			}
			
		}


	}
}
