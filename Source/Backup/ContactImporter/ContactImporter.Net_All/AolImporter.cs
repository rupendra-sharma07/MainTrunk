using System;
using System.IO;
using System.Collections;  
using System.Web; 
using Improsys.ContactImporter.Util; 

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for AolImporter.
	/// </summary>
	public class AolImporter : IDisposable 
	{
		private String userId,password;
		private bool isLoggedIn=false;
		private ArrayList names=null;
		private ArrayList emails=null;
		
		private WinHttpUtils httpUtils = null;
		private String domain,uid,verString;
		
		public AolImporter()
		{
			httpUtils = new WinHttpUtils();
		}

		public AolImporter(String userId,String password)
		{
			httpUtils = new WinHttpUtils();
			this.userId = userId;
			this.password = password;
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
			String startUrl = "http://webmail.aol.com";
			String nextUrl,referrer;
			uid="";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,true,"","",false);
				
//				nextUrl = htmlResponse.Substring( htmlResponse.IndexOf("http://my.screenname.aol.com/_cqr/login/login.psp"));
//				nextUrl=nextUrl.Substring(0,nextUrl.IndexOf("\r\nSet-Cookie"));
				nextUrl = StringUtils.ParseValueBetween(htmlResponse, "http://my.screenname.aol.com/_cqr/login/login.psp", "\r\nSet-Cookie", true,false);
				if(nextUrl.StartsWith("http"))
				{
					htmlResponse = httpUtils.GetHttpResponse(nextUrl,true,"","",false);
					//nextUrl = htmlResponse.Substring( htmlResponse.IndexOf("https://my.screenname.aol.com/_cqr/login/login.psp"));
					//nextUrl=nextUrl.Substring(0,nextUrl.IndexOf("\\r\\nSet-Cookie"));
					nextUrl = StringUtils.ParseValueBetween(htmlResponse, "https://my.screenname.aol.com/_cqr/login/login.psp", "\r\nContent-Type:", true,false);
					htmlResponse = httpUtils.GetHttpResponse(nextUrl,true,"","",false);
					String form= htmlResponse.Substring(htmlResponse.IndexOf("<form name=\"AOLLoginForm\""));
					form=form.Substring(0,form.IndexOf("</form>"));
					
					string pstfields=StringUtils.HiddenFields(form);

					String siteState = StringUtils.ParseValueBetween(htmlResponse,"siteState\" value=\"","\"");
					postString =  pstfields+"&password=" + HttpUtility.UrlEncode(password) + "&loginId=" + HttpUtility.UrlEncode(userId);
				
					//postString = postString.Replace("mail-first-en-us","mail-second-en-us");
					referrer = nextUrl;
					//referrer="";
					nextUrl = "https://my.screenname.aol.com/_cqr/login/login.psp";
					htmlResponse = httpUtils.GetHttpResponse(nextUrl, true, postString, referrer,false);
					if(htmlResponse.IndexOf("type=\"text\" name=\"loginId")>=0)
						return false;
					
					referrer = nextUrl;
					if(htmlResponse.IndexOf("Location:")!=-1)
					{
							nextUrl = StringUtils.ParseValueBetween(htmlResponse, "http://", "\r\nContent-Type:", true,false);
					}
					else
					{
						nextUrl = StringUtils.ParseValueBetween(htmlResponse, "http://", "'", true,false);
					}
					
					htmlResponse = httpUtils.GetHttpResponse(nextUrl, true);
					if(htmlResponse.IndexOf("uid:")!=-1)
						uid = StringUtils.ParseValueBetween(htmlResponse, "uid:", "&");
					if(htmlResponse.IndexOf("Location:")!=-1)
					{
						nextUrl = StringUtils.ParseValueBetween(htmlResponse, "http://", "\r\nCache-Control:", true,false);
						htmlResponse = httpUtils.GetHttpResponse(nextUrl, true);
						if(htmlResponse.IndexOf("Location:")!=-1)
						{
							nextUrl = StringUtils.ParseValueBetween(htmlResponse, "http://", "\r\nSet-Cookie:", true,false);
							htmlResponse = httpUtils.GetHttpResponse(nextUrl, true);
							if(htmlResponse.IndexOf("Location:")!=-1)
							{
								nextUrl = StringUtils.ParseValueBetween(htmlResponse, "http://", "\r\nContent-Type:", true,false);
								htmlResponse = httpUtils.GetHttpResponse(nextUrl, true);
							}
						}
					}
					if(htmlResponse.IndexOf("uid:")!=-1 && uid=="")
						uid = StringUtils.ParseValueBetween(htmlResponse, "uid:", "&");
					verString=StringUtils.ParseValueBetween(htmlResponse, "gHostCheckPath = \"/", "/", false,false);
					nextUrl=StringUtils.ParseValueBetween(htmlResponse, "gHostCheckPath = \"/", "\";", false,false);
					domain=StringUtils.ParseValueBetween(htmlResponse, "gPreferredHost = \"", "\";", false,false);
					//domain="webmail"+domain;
					//nextUrl="http://"+domain+"/"+nextUrl;
					
					nextUrl = StringUtils.ParseValueBetween(htmlResponse,"gSuccessPath = \"","\";");
					nextUrl = "http://"+domain+nextUrl;
					referrer = nextUrl;
					htmlResponse = httpUtils.GetHttpResponse(nextUrl, true);
					nextUrl = StringUtils.ParseValueBetween(htmlResponse,"\"ipt src=\\\"","\\\"");
					htmlResponse = httpUtils.GetHttpResponse(nextUrl,true,"",referrer);
					if(uid=="")
					{
						uid = StringUtils.ParseValueBetween(htmlResponse, "\"UserUID\":\"", "\"");
					}
					
					
						
					isLoggedIn = true;
				}
			}
			catch(Exception e)
			{
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

			adrUrl = "http://" + domain + "/"+verString+"/aol/en-us/common/rpc/RPC.aspx?user="+uid+"&r=0."+utimestamp().ToString();
						 
			String pStr="dojo.transport=xmlhttp&automatic="+false+"&requests=%5B%7B%22hash%22%3A%22%22%2C%22action%22%3A%22AutoCompleteContacts%22%7D%2C%7B%22action%22%3A%22GetScreenNames%22%7D%5D";
			htmlResponse = httpUtils.GetHttpResponse(adrUrl, false,pStr,"");

			htmlResponse=htmlResponse.Substring(htmlResponse.IndexOf("listString"));

			
			ParsePageAndGetContacts(htmlResponse);
			
			if(names.Count>0) 
				return true;
			else
				return false;
		}

		private void ParsePageAndGetContacts(String pageContent)
		{
			
			String fName="";
			String lName="";
			String eMail="";
			String firstname="";
			String lastname="";
			String nickname="";
			
			
			String patarn="Standard";
			String[] token =StringUtils.Split(pageContent,patarn);
			for(int i=0;i<token.Length;i++)
			{

				


				if(token[i].IndexOf("\\u0010")!=-1)
				{
					firstname=token[i].Substring(token[i].IndexOf("\\u0010")+"\\u0010\\u0002".Length);
					firstname=firstname.Trim();
					if(firstname.IndexOf("\\u0002")!=-1)
					{
						firstname=firstname.Substring(0,firstname.IndexOf("\\u0002"));						
					}
				}

				if(token[i].IndexOf("\\u0011")!=-1)
				{
					lastname=token[i].Substring(token[i].IndexOf("\\u0011")+"\\u0011\\u0002".Length);
					lastname=lastname.Trim();
					if(lastname.IndexOf("\\u0002")!=-1)
					lastname=lastname.Substring(0,lastname.IndexOf("\\u0002"));						
				}
				if(token[i].IndexOf("\\u0012")!=-1)
				{
					nickname=token[i].Substring(token[i].IndexOf("\\u0012")+"\\u0012\\u0002".Length);
					nickname=nickname.Trim();
					if(nickname.IndexOf("\\u0002")!=-1)
					nickname=nickname.Substring(0,nickname.IndexOf("\\u0002"));						
				}

				if(token[i].IndexOf("\\u0014")!=-1)
				{
					
					fName=token[i].Substring(token[i].IndexOf("\\u0014")+"\\u0014\\u0002".Length);
					fName=fName.Substring(0,fName.IndexOf("\\u0002"));
					if(StringUtils.IsValidEmail(fName))
					{
						eMail=fName;
						fName=fName.Substring(0,fName.IndexOf("@"));
						if(firstname=="")
							firstname=fName;
					}
				}
				
				if(token[i].IndexOf("\\u0015")!=-1)
				{
					lName=token[i].Substring(token[i].IndexOf("\\u0015")+"\\u0015\\u0002".Length);
					lName=lName.Substring(0,lName.IndexOf("\\u0002"));
					if(StringUtils.IsValidEmail(lName))
					{
						eMail=lName;
						lName=lName.Substring(0,lName.IndexOf("@"));
						if(lastname=="")
							lastname=lName;
					}
				}

				if(token[i].IndexOf("\\u0013")!=-1)
				{
					lName=token[i].Substring(token[i].IndexOf("\\u0013")+"\\u0013\\u0002".Length);
					lName=lName.Substring(0,lName.IndexOf("\\u0002"));
					if(StringUtils.IsValidEmail(lName))
					{
						eMail=lName;
						lName=lName.Substring(0,lName.IndexOf("@"));
						if(lastname=="")
							lastname=lName;
					}
				}

				

				String finalName="";			
				if(firstname!=""&&lastname!="")
				{
					finalName=firstname+" "+lastname;
					//names.Add(finalName);
				}
				else if(firstname!=""&&lastname=="")
				{
					finalName=firstname;
					//names.Add(finalName);
				}
				else if(firstname==""&&lastname!="")
				{
					finalName=lastname;
					//names.Add(finalName);
				}
				else if(nickname!="")
				{
					finalName=nickname;
					//names.Add(finalName);
				}
				else if(eMail!="")
				{
					finalName=eMail.Substring(0,eMail.IndexOf("@"));
					//names.Add(finalName);
				}
				else
					continue;

				if((eMail!="")&& (finalName!=""))
				{
					names.Add(finalName);
					emails.Add(eMail);
				}
				//else
				//	emails.Add("");
				
				fName="";
				lName="";
				eMail="";
				firstname="";
				lastname="";

			}
		}

		private static long utimestamp()
		{
			System.DateTime UnixBase = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
			System.DateTime dtm = System.DateTime.Now;
			return (long)dtm.Subtract( UnixBase ).TotalSeconds;
		}

		public void Dispose() 
		{
			Logout();
			GC.SuppressFinalize(this);
		}
																			  
	}
}
