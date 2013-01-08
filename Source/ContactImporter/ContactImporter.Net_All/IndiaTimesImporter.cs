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
	/// Summary description for IndiaTimes.
	/// </summary>
	public class IndiaTimesImporter
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
		private String domain="";
		public IndiaTimesImporter()
		{
			httpUtils=new HttpUtils();
		}
		public IndiaTimesImporter(String userid,String password)
		{
			httpUtils=new HttpUtils();
			this.userId=userId;
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
			//String homePage = "http://mail.indiatimes.com/";
			String homePage = "http://in.indiatimes.com/default1.cms";

			
			String loginUrl = "";
			String frm = "";
			
			
			
			try
			{
				htmlResponse=httpUtils.GetHttpResponse(homePage,false);
				frm=htmlResponse.Substring(htmlResponse.IndexOf("form name=\"loginfrm\"")+ "form name=\"loginfrm\"".Length);
				frm=frm.Substring(0,frm.IndexOf("</form>"));
				loginUrl=frm.Substring(frm.IndexOf("action=\"") + "action=\"".Length);
				loginUrl=loginUrl.Substring(0,loginUrl.IndexOf("\""));
				loginUrl=loginUrl.Replace("&amp;","&");
				postString="login="+UserId+"&passwd="+ HttpUtility.UrlEncode(Password)+"&Sign in.x=15&Sign in.y=9";
				//htmlResponse=httpUtils.GetHttpResponse(loginUrl,false,postString,homePage);

				htmlResponse=httpUtils.GetHttpResponse(loginUrl,true,postString,homePage,"",true);
				
				htmlResponse=htmlResponse.Substring(htmlResponse.IndexOf("Location: ")+"Location: ".Length);
				domain=htmlResponse.Substring(0,htmlResponse.IndexOf("."));
				htmlResponse=httpUtils.GetHttpResponse(loginUrl,false,postString,homePage,"",false);
				
					if(htmlResponse.IndexOf("Invalid User \r\nName or Password")!=-1)
					{
						isLoggedIn=false;
					}
					else
					{
						isLoggedIn=true;
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
			
			

			addressBookURL=domain+".indiatimes.com/service/home/~/Contacts?auth=co&fmt=csv";

			try
			{
				htmlResponse = httpUtils.GetHttpResponse(this.addressBookURL,false);
				
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
						if(fields.Length==5)
						{
							temp_name=temp_line[2]+" "+temp_line[3];
							temp_email=temp_line[0];
						}
						else
						{
							temp_name=(temp_line[5]+" "+temp_line[10]+" "+temp_line[9]); 
							temp_email=(temp_line[1]!="") ? temp_line[1]:(((temp_line[2]!="")? temp_line[2]:((temp_line[3]!="") ? temp_line[3]:"")));
						}
						
						
						
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
