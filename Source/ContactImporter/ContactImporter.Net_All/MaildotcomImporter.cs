using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util; 

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for MaildotcomImporter.
	/// </summary>
	public class MaildotcomImporter
	{
		private String userId,password;
		private bool isLoggedIn=false;
		private ArrayList names=null;
		private ArrayList emails=null;
		private HttpUtils httpUtils = null;
		
		public MaildotcomImporter()
		{
			httpUtils = new HttpUtils();
		}

		public MaildotcomImporter(String userId,String password)
		{
				httpUtils = new HttpUtils();
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

		public bool DoLogin()
		{
			Logout(true);
			String htmlResponse="",postString;
			String startUrl = "http://www.mail.com/";
			String nextUrl;
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				
				nextUrl = "http://www2.mail.com/scripts/common/proxy.main?signin=1&lang=us"; 
				postString = "login="+this.userId+"&password="+this.password+"&redirlogin=1"+"&siteselected=normal";
				htmlResponse = httpUtils.GetHttpResponse(nextUrl, false, postString, "");
				if(htmlResponse.IndexOf("<input type=\"text\" name=\"login\"")==-1)
				{
					isLoggedIn = true;
				}
				else
				{
					isLoggedIn = false;
				}
			}
			catch(Exception e)
			{
				Logout();
				isLoggedIn = false;
				// Console.WriteLine("Exception occured in DoLogin : " + e.Message );
			}
			return isLoggedIn;
		}

		public bool DoLogin(String userId,String password)
		{
			this.UserId = userId;
			this.Password = password; 
			return DoLogin();
		}

		public bool ImportContacts()
		{
			String adrUrl,htmlResponse;
			names = new ArrayList();
			emails = new ArrayList();
			adrUrl = "http://mail01.mail.com/scripts/addr/addressbook.cgi?showaddressbook=1";
			
			htmlResponse = httpUtils.GetHttpResponse(adrUrl,false);
			String tempStr="<a href=\"http://mail01.mail.com/scripts/addr/external.cgi?.ob=";
			String ob="";
			if(htmlResponse.IndexOf(tempStr)!=-1)
			{
				ob=htmlResponse.Substring(htmlResponse.IndexOf(tempStr)+tempStr.Length);
				ob=ob.Substring(0,ob.IndexOf("&gab"));
			}
			
			String exportUrl="http://mail01.mail.com/scripts/addr/external.cgi?.ob="+ob+"&gab=1";
			String postStrig="showexport=showexport&action=export&format=csv";
			htmlResponse=httpUtils.GetHttpResponse(exportUrl,false,postStrig,"");
			System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlResponse));
			System.IO.StreamReader str = new System.IO.StreamReader(strm);

			int count=0,counter;
			addresses b=new addresses();
			string temp_name="",temp_email="";

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
						
					temp_name=(temp_line[2]=="") ? temp_line[0]:temp_line[2]; 
					temp_name=(temp_line[2]=="" && temp_line[0]=="") ? temp_line[3]:((temp_line[2]!="" && temp_line[0]!="") ? temp_line[2]+", "+temp_line[0]+" "+temp_line[1]:temp_name) ;
					
					temp_email=(temp_line[4]!="") ? temp_line[4]:(((temp_line[12]!="")? temp_line[12]:((temp_line[13]!="") ? temp_line[13]:"")));
					
					if(StringUtils.IsValidEmail(temp_email) && temp_name=="")
					{
						temp_name=temp_email.Substring(0,temp_email.IndexOf("@"));
					}
					
					
					if(StringUtils.IsValidEmail(temp_email))
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

		public void Dispose() 
		{
			Logout();
			GC.SuppressFinalize(this);
		}

	}
}
