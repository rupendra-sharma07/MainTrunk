/**
 * @author Sumon sumon@improsys.com, Mamun mamun@improsys.com
 * @history
 *          created  : Mamun : Date : 20-07-2008
 * @version 1.0
 *
 * Copyright Improsys.
 *
 * All rights reserved.
 *
 * This software is the confidential and proprietary information
 * of Improsys. ("Confidential Information").
 * You shall not disclose such Confidential Information and shall use
 * it only in accordance with the terms of the license agreement
 * you entered into with Improsys.
 */

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
	/// Summary description for HotmailImporter.
	/// </summary>
	public class HotmailImporter
	{
		private bool isLoggedIn = false;
		private String userId=String.Empty;
		private String password=String.Empty;
		private String htmlResponse = "";
		private String postString="";
		private String domain = "";
		private String n = "";
		private String mt = "";
		private WinHttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();

		public HotmailImporter()
		{
			httpUtils=new WinHttpUtils();
		}
		public HotmailImporter(String user,String pass)
		{
			httpUtils=new WinHttpUtils();
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

		public bool DoLogin(String userId,String password,String type)
		{
			this.UserId = userId+"@"+type;
			this.Password = password; 
			return DoLogin();
		}
		public bool DoLogin()
		{					   
			String startUrl = "http://login.live.com/login.srf?id=2&rru=%2fcgi%2dbin%2fhmhome%3ffti%3dyes&reason=nocookies";
			String loginUrl = "";
			String frm = "";
			String hiddenField = "";
			String redirectUrl = "";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				frm = htmlResponse.Substring(htmlResponse.IndexOf("action=\"")+"action=\"".Length);
				loginUrl = frm.Substring(0,frm.IndexOf("\""));
				frm = frm.Substring(0,frm.IndexOf("</form>"));
				hiddenField = StringUtils.HiddenFields(frm);
				hiddenField = hiddenField.Replace("PwdPad=","PwdPad=IfYouAreReadingThisYouHaveTooMuchF");
				postString = hiddenField+"&login="+this.userId+"&passwd="+HttpUtility.UrlEncode(this.password)+"&LoginOptions=3";
				htmlResponse = httpUtils.GetHttpResponse(loginUrl,false,postString,startUrl);
				if(htmlResponse.IndexOf("window.location.replace(\"")!=-1)
				{
					redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("window.location.replace(\"")+"window.location.replace(\"".Length);
					redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\")"));
					htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true,"","",false);
					if(htmlResponse.IndexOf("Location:")!=-1)
					{
						redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("Location: ")+"Location: ".Length);
						redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\r\n"));
						this.domain = redirectUrl.Substring(redirectUrl.IndexOf("http://")+"http://".Length);
						this.domain = this.domain.Substring(0,this.domain.IndexOf("/"));
						htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true,"","",false);
						if(htmlResponse.IndexOf("Location:")!=-1)
						{
							redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("Location: ")+"Location: ".Length);
							redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\r\n"));
							if(htmlResponse.IndexOf("mt")!=-1)
							{
								this.mt = htmlResponse.Substring(htmlResponse.IndexOf("mt=")+"mt=".Length);
								this.mt = this.mt.Substring(0,this.mt.IndexOf(";"));
							}
							htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true,"","",false);


							if(htmlResponse.IndexOf("self.location.href = '")!=-1)
							{
								string bUrl,ip,nval;
								bUrl = htmlResponse.Substring(htmlResponse.IndexOf("<base href=\"")+"<base href=\"".Length);
								bUrl = bUrl.Substring(0,bUrl.IndexOf("\""));
			
								ip = redirectUrl.Substring(redirectUrl.IndexOf("ip="));
								

								/*redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("self.location.href = '")+"self.location.href = '".Length);
								redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("'"));*/

								nval = htmlResponse.Substring(htmlResponse.IndexOf("n&#61;")+"n&#61;".Length);
								nval = nval.Substring(0,nval.IndexOf("\""));
								
		
								redirectUrl="http://"+bUrl+"/mail/TodayLight.aspx?&"+ip+"&n="+nval;

								htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true);
								if(htmlResponse.IndexOf("mt=")!=-1)
								{
									this.mt = htmlResponse.Substring(htmlResponse.IndexOf("mt=")+"mt=".Length);
									this.mt = this.mt.Substring(0,this.mt.IndexOf(";"));
								}

							}

							if(htmlResponse.IndexOf("Location:")!=-1)	
							{
								redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("Location: ")+"location: ".Length);
								redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\r\n"));
								this.n = redirectUrl.Substring(redirectUrl.IndexOf("n=")+"n=".Length);
								htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true);
								if(htmlResponse.IndexOf("mt=")!=-1)
								{
									this.mt = htmlResponse.Substring(htmlResponse.IndexOf("mt=")+"mt=".Length);
									this.mt = this.mt.Substring(0,this.mt.IndexOf(";"));
								}
								if(htmlResponse.IndexOf("Location:")!=-1)
								//if(htmlResponse.IndexOf("<form name=\"MessageAtLoginForm")!=-1)
								{
									redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("Location: ")+"location: ".Length);
									redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\r\n"));
									htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true);

									frm = htmlResponse.Substring(htmlResponse.IndexOf("action=\"")+"action=\"".Length);
									redirectUrl = frm.Substring(0,frm.IndexOf("\""));
									redirectUrl = redirectUrl.Replace("&amp;","&");
									redirectUrl = "http://"+this.domain+"/mail/"+redirectUrl;
									frm = frm.Substring(0,frm.IndexOf("</form>"));
									hiddenField = StringUtils.HiddenFields(frm);
									postString = hiddenField+"&TakeMeToInbox=Continue";
									htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true,postString,"",true);
									if(htmlResponse.IndexOf("Location:")!=-1)
									{
										redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("Location: ")+"location: ".Length);
										redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\r\n"));
										redirectUrl = "http://"+this.domain+redirectUrl;
										htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true);
									}
								}
							}
							else
							{
								redirectUrl = "http://"+this.domain+redirectUrl;
								this.n = redirectUrl.Substring(redirectUrl.IndexOf("n=")+"n=".Length);
								htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true);
							}
								isLoggedIn = true;
						}
						else if(htmlResponse.IndexOf("window.location = \"")!=-1)
						{
							redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("window.location = \"")+"window.location = \"".Length);
							redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\";"));
							this.n = redirectUrl.Substring(redirectUrl.IndexOf("n=")+"n=".Length);
							if(htmlResponse.IndexOf("mt")!=-1)
							{
								this.mt = htmlResponse.Substring(htmlResponse.IndexOf("mt=")+"mt=".Length);
								this.mt = this.mt.Substring(0,this.mt.IndexOf(";"));
							}
							redirectUrl = "http://"+this.domain+redirectUrl;
							htmlResponse = httpUtils.GetHttpResponse(redirectUrl,false);
							isLoggedIn = true;
						}
						else
						{
							isLoggedIn = false;
						}
					}
					else
					{
						isLoggedIn = false;
					}
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
			}
			return isLoggedIn;
		}
		public bool ImportContacts()
		{
			names = new ArrayList();
			emails = new ArrayList();
			String tmpName = "";
			String tmpEmail = "";
			int count=0,counter;
			String frm = "";
			String strHidden = "";
			//String exportUrl = "http://"+this.domain+"/mail/options.aspx?subsection=26&n="+this.n;
			String exportUrl = "http://"+this.domain+"/mail/ContactPickerLight.aspx?n="+this.n;
			
			
			
			String export = "";
			String redirectUrl = "";

			try
			{
				htmlResponse = httpUtils.GetHttpResponse(exportUrl,true);
				/*
				if(htmlResponse.IndexOf("Location:")!=-1)
				{
					redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("Location: ")+"Location: ".Length);
					redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\r\n"));
					if(redirectUrl.IndexOf("http://")==-1)
					{
						redirectUrl="http://"+this.domain+"/"+redirectUrl;
					}
					htmlResponse = httpUtils.GetHttpResponse(redirectUrl,true,"","",false); 
				}
				if(htmlResponse.IndexOf("<form name=\"Form1\"")!=-1) 
				{
					frm = htmlResponse.Substring(htmlResponse.IndexOf("<form name=\"Form1\""));
					frm = frm.Substring(0,frm.IndexOf("</form>"));
					export = frm.Substring(frm.IndexOf("action=\"")+"action=\"".Length);
					export = export.Substring(0,export.IndexOf("\""));
					export = export.Replace("&amp;","&");
					export = "http://"+this.domain+"/mail/"+export;
					strHidden = StringUtils.HiddenFields(frm);
					strHidden = strHidden.Replace("&mt=","");
					postString = strHidden+"&ctl02%24ExportButton=Export+contacts&mt="+this.mt;
					htmlResponse = httpUtils.GetHttpResponse(export,false,postString,exportUrl,false);
					if(htmlResponse.IndexOf("<title>Object moved</title>")!=-1)
					{
						export = htmlResponse.Substring(htmlResponse.IndexOf("<a href=\"")+"<a href=\"".Length);
						export = export.Substring(0,export.IndexOf("\""));
						export = "http://"+this.domain+export;
						htmlResponse = httpUtils.GetHttpResponse(export,false,"",exportUrl);
					}
					
					
					System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlResponse));
					System.IO.StreamReader str = new System.IO.StreamReader(strm);
					CSVReader csv = new CSVReader(str); 
					string[] fields;
					string[] temp_line;
				
					while ((fields = csv.GetCSVLine()) != null) {
						//					if(fields.Length < 5)
						//						continue;
						temp_line=new string[100];
						if(fields.Length< 81)
						{
							fields = StringUtils.Split(fields[0].ToString(),";");
						}
						if(count!=0) {
							counter=0;
							foreach (string field in fields) {   
								temp_line[counter]=field;
								counter++;
							}
							tmpName= (temp_line[1]!="" || temp_line[2]!="" || temp_line[3]!="") ? temp_line[1]+" "+temp_line[2]+" "+temp_line[3]:(((temp_line[78]!="")?temp_line[78]:((temp_line[79]!="")?temp_line[79]:""))); 
							tmpEmail = (temp_line[46]!="") ? temp_line[46]:(((temp_line[49]!="")? temp_line[49]:((temp_line[52]!="")? temp_line[52]:"")));
							
							if(tmpEmail!=null)
							{
								tmpName =  tmpName.Replace("\""," ").Trim();
								tmpEmail = tmpEmail.Replace("\""," ").Trim();

								if(StringUtils.IsValidEmail(tmpEmail) && tmpName=="") 
								{
									tmpName=tmpEmail.Substring(0,tmpEmail.IndexOf("@"));
								}
										
								if(StringUtils.IsValidEmail(tmpEmail)) 
								{
									names.Add(tmpName);
									emails.Add(tmpEmail);
								}
							}
						}
						count++;
					}
					if(names.Count>0) {
						return true;
					}
					else {
						return false;
					}
				}
				else 
				{
					return false;
				}
				*/
				if(htmlResponse.IndexOf("<table class=\"ContactPickerBodyTable\"")!=-1)
				{
		
					String contacts=htmlResponse.Substring(htmlResponse.IndexOf("<table class=\"ContactPickerBodyTable\""));
					contacts=contacts.Substring(0,contacts.IndexOf("<div class=\"ContactPickerFooter BorderBottom\">"));
					string[] rows;
					string[] col;
					string temp_name,temp_email;
			
					rows = StringUtils.Split(contacts,"<td class=\"dContactPickerBodyCheckBoxCol\">");
					for(int j=1;j<rows.Length;j++)
					{	
						col=StringUtils.Split(rows[j],"<td class");
						temp_name="";
						temp_email="";
						col[1]=col[1].Replace("\r\n","");
						temp_name=col[1].Substring(col[1].IndexOf("&#x200f;")+("&#x200f;").Length);
						if(temp_name.IndexOf("&#x200f;")!=-1)
							temp_name=temp_name.Substring(0,temp_name.IndexOf("&#x200f;"));
						temp_name=temp_name.Trim();
						if(temp_name!="")
							temp_name=temp_name.Replace("&#64;","@");

						temp_email=col[2].Substring(col[2].IndexOf("\">")+("\">").Length);
						temp_email=temp_email.Substring(0,temp_email.IndexOf("<"));
						temp_email=temp_email.Replace("&#64;","@");

						
						temp_email=temp_email.Trim();
					
						if(StringUtils.IsValidEmail(temp_email) && temp_name=="") 
						{
							temp_name=temp_email.Substring(0,temp_email.IndexOf("@"));
						}
										
						if(StringUtils.IsValidEmail(temp_email)) 
						{
							names.Add(temp_name);
							emails.Add(temp_email);
							count++;
						}

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
