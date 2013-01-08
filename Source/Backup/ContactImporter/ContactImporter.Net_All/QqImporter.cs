using System;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util;


namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for QqImporter.
	/// </summary>
	public class QqImporter
	{
		private bool isLoggedIn = false;
		private String userId,password;
		private String htmlResponse = "";
		private String postString="";
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();
		private HttpUtils httpUtils = null;
		public String primKey = "";
		public String strHidden = "";
		private Image img;
		private String captcha = "";
		private String p = "";
		private String ts = "";
		private String starttime = "";
		private String cpText = "";
		private String sid = "";
		private String domain = "";
		private String loginUrl = "";

		public QqImporter()
		{
			httpUtils = new HttpUtils();
		}
		public QqImporter(String user,String pass)
		{
			httpUtils = new HttpUtils();
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
		public bool DoLogin(String userId,String password,String p,String ts,String starttime,String cpText)
		{
			this.UserId = userId;
			this.Password = password; 
			this.p = p;
			this.ts = ts;
			this.starttime = starttime;
			this.cpText = cpText;
			return DoLogin();
		}

		private void setImage(Image img)
		{
			this.img = img;
		}

		public Image getImage()
		{
			return this.img;
		}

		public void setCaptch(String cpt)
		{
			this.captcha = cpt;
		}

		public void Capture_Img()
		{
			String startUrl = "http://mail.qq.com";
			String frm = "";
			String imgUrl = "";
			String nextUrl="";
			Random r = new Random();
			String number = r.Next(1,9999).ToString("0000");
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,true);
				htmlResponse=httpUtils.GetHttpResponse("http://mail.qq.com/cgi-bin/loginpage",true);
				this.primKey = htmlResponse.Substring(htmlResponse.IndexOf("var PublicKey = \"")+"var PublicKey = \"".Length);
				this.primKey = this.primKey.Substring(0,this.primKey.IndexOf("\""));
				frm = htmlResponse.Substring(htmlResponse.IndexOf("<form name=\"form1\""));
				frm = frm.Substring(0,frm.IndexOf("</form"));
				this.loginUrl = frm.Substring(frm.IndexOf("action=\"")+"action=\"".Length);
				this.loginUrl = this.loginUrl.Substring(0,this.loginUrl.IndexOf("\""));
				this.strHidden = HiddenFields(frm);
				imgUrl = htmlResponse.Substring(htmlResponse.IndexOf("<img id='vfcode' src='")+"<img id='vfcode' src='".Length);
				imgUrl = imgUrl.Substring(0,imgUrl.IndexOf("&")+1);
				imgUrl = imgUrl+"0."+number;
				htmlResponse = httpUtils.GetHttpResponse(imgUrl,false);
				setImage(httpUtils.capImg);
			}
			catch(Exception e)
			{
				
			}
			
		}



		public bool DoLogin()
		{
			String pwd = "";
			String url = "";
			this.starttime = "&starttime="+this.starttime;
			this.p = "&p="+HttpUtility.UrlEncode(this.p);
			try
			{
			for(int i =0;i<this.password.Length;i++)
				{
					pwd+="0";
				}
				this.strHidden = this.strHidden.Replace("&starttime=",this.starttime);
				this.strHidden = this.strHidden.Replace("&p=",this.p);

				postString = this.strHidden+"&uin="+this.userId+"&pp="+pwd+"&aliastype=@qq.com&verifycode="+this.cpText;
				htmlResponse = httpUtils.GetHttpResponse(loginUrl,true,postString,"http://mail.qq.com/cgi-bin/loginpage");

				int t = htmlResponse.IndexOf("loginpage?");
				if(htmlResponse.IndexOf("loginpage?")!=-1)
				{
					isLoggedIn = false;
				}
				else
				{
					string baseurl=htmlResponse.Substring(htmlResponse.IndexOf("var urlHead=\"")+"var urlHead=\"".Length);
					baseurl=baseurl.Substring(0,baseurl.IndexOf("\";"));
					url = htmlResponse.Substring(htmlResponse.IndexOf("urlHead + \"")+"urlHead + \"".Length);
					url = url.Substring(0,url.IndexOf("\";"));
					baseurl+=url;
					this.domain = baseurl.Substring(0,baseurl.IndexOf("mail.qq.com")+"mail.qq.com".Length);
					this.sid = baseurl.Substring(baseurl.IndexOf("?sid=")+"?sid=".Length);
					if(this.sid.IndexOf("&")!=-1)
						this.sid = this.sid.Substring(0,this.sid.IndexOf("&"));
					htmlResponse = httpUtils.GetHttpResponse(baseurl,true);
					isLoggedIn = true;
				}
			}
			catch(Exception e)
			{
				isLoggedIn = false;
			}
			return isLoggedIn;
		}


		public static string HiddenFields(String str)
		{
			str = str.Replace(">",">\n");
			string pattern=@"<input.*hidden.*?>";
			MatchCollection mc=Regex.Matches(str, pattern);
			string text="",t_text="",name="",val="";
			string nmstr="name=\"",vlstr="value=\"";
			foreach (Match m in mc)
			{
				t_text=m.ToString();
				if(t_text.IndexOf(nmstr)!=-1)
				{
					name=t_text.Substring(t_text.IndexOf(nmstr)+nmstr.Length);
					name=name.Substring(0,name.IndexOf("\""));
				}
				else if(t_text.IndexOf("name=")!=-1)
				{
					name=t_text.Substring(t_text.IndexOf("name=")+"name=".Length);
					name=name.Substring(0,name.IndexOf(" "));
				}
				else
				{
					name=t_text.Substring(t_text.IndexOf("name='")+"name='".Length);
					name=name.Substring(0,name.IndexOf("'"));
				}
				if(t_text.IndexOf("value=\"")!=-1)
				{
					val=t_text.Substring(t_text.IndexOf(vlstr)+vlstr.Length);
					val=val.Substring(0,val.IndexOf("\""));
				}
				else if(t_text.IndexOf("value='")!=-1)
				{
					val=t_text.Substring(t_text.IndexOf("value='")+"value='".Length);
					val=val.Substring(0,val.IndexOf("'"));
				}
				else
				{
					//val=t_text.Substring(t_text.IndexOf("value=")+"value=".Length);
					//val=val.Substring(0,val.IndexOf(">"));
					//val = val.Replace("/","").Trim();
					val="";
				}
				if(text!="")
					text+="&"+name+"="+HttpUtility.UrlEncode(val);
				else
					text+=name+"="+HttpUtility.UrlEncode(val);
			}
			
			pattern=@"<INPUT.*hidden.*?>";
			mc=Regex.Matches(str, pattern);
			foreach (Match m in mc)
			{
				t_text=m.ToString();
				name=t_text.Substring(t_text.IndexOf(nmstr)+nmstr.Length);
				name=name.Substring(0,name.IndexOf("\""));
				if(t_text.IndexOf(vlstr)==-1)
				{
					val = "";
				}
				else
				{
					val=t_text.Substring(t_text.IndexOf(vlstr)+vlstr.Length);
					val=val.Substring(0,val.IndexOf("\""));
				}
				if(text!="")
					text+="&"+name+"="+ HttpUtility.UrlEncode(val);
				else
					text+=name+"="+HttpUtility.UrlEncode(val);
			}

			return text;
		}

		
		public bool ImportContacts()
		{
			names = new ArrayList();
			emails = new ArrayList();
			String tmpName = "";
			String tmpEmail = "";
			int count=0,counter;
			String csvUrl = this.domain+"/cgi-bin/addr_export?sid="+HttpUtility.UrlEncode(this.sid);
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(csvUrl,false);
				System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlResponse));
				System.IO.StreamReader str = new System.IO.StreamReader(strm);
				CSVReader csv = new CSVReader(str); 
				string[] fields;
				string[] temp_line;
				while ((fields = csv.GetCSVLine()) != null) 
				{
					//					if(fields.Length < 5)
					//						continue;
					temp_line=new string[1024];
					
					if(count!=0)
					{
						counter=0;
						foreach (string field in fields) 
						{   
							temp_line[counter]=field;
							counter++;
						}
						tmpName=temp_line[0]; 
						tmpEmail = temp_line[1];
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
			catch(Exception e)
			{
				return false;
			}
		}
	}
}
