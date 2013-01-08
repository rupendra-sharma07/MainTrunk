/**

 * @author Sumon sumon@improsys.com
 * @history
 *          created  : Sumon : Date : 11-03-2004
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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Xml;
using System.IO;
using System.Drawing;


namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class ContactImporter : IDisposable 
	{
		private CookieContainer cookies = new CookieContainer();
		private GmailImporter gmailImp = new GmailImporter();
		private HotmailImporter hotmailImp = new HotmailImporter();
		private AolImporter aolImp = new AolImporter();
		private LinkedInImporter linkedinImp = new LinkedInImporter();
		private MyspaceImporter myspaceImp = new MyspaceImporter();
		private RediffImporter rediffImp = new RediffImporter();
		private WebImporter WebImp = new WebImporter();
		private MailruImporter mailruImp = new MailruImporter();
		private MaildotcomImporter maildotcomImp = new MaildotcomImporter();
		private PlaxoImporter plaxoImp = new PlaxoImporter();
		private OneSixthreeImporter onesixthreeImp = new OneSixthreeImporter();
		private QqImporter qqImp = new QqImporter();
		private SinaImporter sinaImp = new SinaImporter();
		private BolUolImporter bolUolImp = new BolUolImporter();
		private MynetImporter mynetImp = new MynetImporter();
		private IndiaTimesImporter indiatimesImp= new IndiaTimesImporter();
		private OneTwoSixImporter OneTwoSixImp=new OneTwoSixImporter();
		private AbvImporter AbvImp=new AbvImporter();
		private WpImporter WpImp=new WpImporter();
		private RamblerruImporter RmbImp=new RamblerruImporter();
		private InteriaImporter IntereaImp=new InteriaImporter();
		private LycosImporter LycosImp=new LycosImporter();
		private liberoitImporter LiberoImp=new liberoitImporter();
		
		public string[] nameArray=new string[0];
		public string[] emailArray=new string[0];
		public string[] emArray=new string[0];


		public bool logged_in = false;
		public string userID="";
		public string password="";
		public string type="";
		public string test="";
		private String p = "";
		private String ts = "";
		private String starttime = "";
		private String cpText = "";

		public ContactImporter(string username, string password, string type )
		{
			this.userID=username;
			this.password=password;
			this.type=type;
		}
		
		public void setData(String p,String ts,String starttime,String cpText)
		{
			this.p = p;
			this.ts = ts;
			this.starttime = starttime;
			this.cpText = cpText;
		}
		
		public Image getImage()
		{
			if(this.type=="qq.com")
				return qqImp.getImage();
			else
				return null;
		}
		
		public void setCaptch(String cp)
		{
			if(this.type=="qq.com")
				 qqImp.setCaptch(cp);
		}

		public String getPrimkey()
		{
			if(this.type=="qq.com")
				return qqImp.primKey;
			else
				return "";
		}

//		public String getHidden()
//		{
//			if(this.type=="qq.com")
//				return qqImp.strHidden;
//			else
//				return "";
//		}
		
		public void captcha()
		{
			if(this.type == "qq.com")
				qqImp.Capture_Img();
			else
				throw new Exception("Contact Importer for " + this.type + " is not supported");
			
		}
		
		public bool Compare(String val)
		{
			if((val.CompareTo("live")==0)||(val.CompareTo("hotmail")==0)||(val.CompareTo("msn")==0))
			{
				return true;
			}
				return false;
		}

		public void login()
		{
			//if((this.type.Substring(0,5).ToLower().CompareTo("live.")==0) || (this.type=="msn.com") || (this.type=="live.nl") || (this.type=="live."))
			if(Compare(this.type.Substring(0,this.type.IndexOf(".")).ToLower()))	
				this.logged_in = hotmailImp.DoLogin(this.userID,this.password,this.type);   
			else if(this.type=="gmail.com")
				this.logged_in = gmailImp.DoLogin(this.userID,this.password);   
			else if(this.type=="aol.com")
				this.logged_in = aolImp.DoLogin(this.userID,this.password); 
			else if(this.type=="myspace.com")
				this.logged_in = myspaceImp.DoLogin(this.userID,this.password);
			else if(this.type=="rediff.com")
				this.logged_in = rediffImp.DoLogin(this.userID,this.password);
			else if(this.type=="web.de")
				this.logged_in = WebImp.DoLogin(this.userID,this.password); 
			else if(this.type=="mail.ru")
				this.logged_in = mailruImp.DoLogin(this.userID,this.password); 
			else if(this.type=="mail.com")
				this.logged_in = maildotcomImp.DoLogin(this.userID,this.password);
			else if(this.type == "plaxo.com")
				this.logged_in = plaxoImp.DoLogin(this.userID,this.password);
			else if(this.type == "linkedin.com")
				this.logged_in = linkedinImp.DoLogin(this.userID,this.password);
			else if(this.type == "163.com")
				this.logged_in = onesixthreeImp.DoLogin(this.userID,this.password);
			else if(this.type == "qq.com")
				this.logged_in = qqImp.DoLogin(this.userID,this.password,this.p,this.ts,this.starttime,this.cpText);
			else if(this.type == "sina.com")
				this.logged_in = sinaImp.DoLogin(this.userID,this.password);
			else if(this.type == "bol.com.br")
				this.logged_in = bolUolImp.DoLogin(this.userID,this.password);
			else if(this.type == "mynet.com")
				this.logged_in = mynetImp.DoLogin(this.userID,this.password);
			else if(this.type == "indiatimes.com")				
				this.logged_in = indiatimesImp.DoLogin(this.userID,this.password);
			else if(this.type == "126.com")				
				this.logged_in = OneTwoSixImp.DoLogin(this.userID,this.password);
			else if(this.type == "abv.bg")				
				this.logged_in = AbvImp.DoLogin(this.userID,this.password);
			else if(this.type == "wp.pl")				
				this.logged_in = WpImp.DoLogin(this.userID,this.password);
			else if(this.type == "rambler.ru")				
				this.logged_in = RmbImp.DoLogin(this.userID,this.password);
			else if(this.type == "interia.pl")				
				this.logged_in = IntereaImp.DoLogin(this.userID,this.password);
			else if(this.type == "lycos.com")				
				this.logged_in = LycosImp.DoLogin(this.userID,this.password);
			else if(this.type == "libero.it")				
				this.logged_in = LiberoImp.DoLogin(this.userID,this.password);
			else if(this.type == "yahoo.com")
				yahoo_login();
			else
				throw new Exception("Contact Importer for " + this.type + " is not supported");
			
		}

		public void getcontacts()
		{
			if(Compare(this.type.Substring(0,this.type.IndexOf(".")).ToLower()))
				get_hotmail_address_page();
			else if(this.type=="gmail.com")
				get_gmail_address_page();
			else if(this.type=="yahoo.com")
				yahoo_address_page();
			else if(this.type=="aol.com")
				get_aol_address_page();
			else if(this.type=="linkedin.com")
				get_linkedin_address_page();
			else if(this.type.IndexOf("myspace")!=-1)
				get_myspace_address_page();
			else if(this.type=="rediff.com")
				get_rediff_address_page();
			else if(this.type=="web.de")
				get_web_address_page();
			else if(this.type=="mail.ru")
				get_mailru_address_page();
			else if(this.type.IndexOf("mail.com")!=-1)
				get_maildotcom_address_page();
			else if(this.type.IndexOf("plaxo.com")!=-1)
				get_plaxo_address_page();
			else if(this.type.IndexOf("163.com")!=-1)
				get_onesixthree_address_page();
			else if(this.type.IndexOf("qq.com")!=-1)
				get_qq_address_page();
			else if(this.type.IndexOf("sina.com")!=-1)
				get_sina_address_page();
			else if(this.type.IndexOf("bol.com.br")!=-1)
				get_bolUol_address_page();
			else if(this.type.IndexOf("mynet.com")!=-1)
				get_mynet_address_page();
			else if(this.type.IndexOf("indiatimes.com")!=-1)
				get_indiatimes_address_page();
			else if(this.type.IndexOf("126.com")!=-1)
				get_126_address_page();
			else if(this.type.IndexOf("abv.bg")!=-1)
				get_abv_address_page();
			else if(this.type.IndexOf("wp.pl")!=-1)
				get_wp_address_page();
			else if(this.type.IndexOf("rambler.ru")!=-1)
				get_rambler_address_page();
			else if(this.type.IndexOf("interia.pl")!=-1)
				get_interia_address_page();
			else if(this.type.IndexOf("lycos.com")!=-1)
				get_lycos_address_page();
			else if(this.type.IndexOf("libero.it")!=-1)
				get_libero_address_page();
			else
				throw new Exception("Contact Importer for " + this.type + " is not supported"); 

		}
		//Get address page for mynet.com
		private void get_mynet_address_page()
		{
			if(!mynetImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(mynetImp.ImportContacts())
			{
				nameArray = mynetImp.Names;
				emailArray = mynetImp.Emails;
			}
		}
		//Get address page for indiatimes.com
		private void get_indiatimes_address_page()
		{
			if(!indiatimesImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(indiatimesImp.ImportContacts())
			{
				nameArray = indiatimesImp.Names;
				emailArray = indiatimesImp.Emails;
			}
		}
		//Get address page for 126.com
		private void get_126_address_page()
		{
			if(!OneTwoSixImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(OneTwoSixImp.ImportContacts())
			{
				nameArray = OneTwoSixImp.Names;
				emailArray = OneTwoSixImp.Emails;
			}
		}
		//Get address page for abv.bg
		private void get_abv_address_page()
		{
			if(!AbvImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(AbvImp.ImportContacts())
			{
				nameArray = AbvImp.Names;
				emailArray = AbvImp.Emails;
			}
		}
		//Get address page for wp.pl
		private void get_wp_address_page()
		{
			if(!WpImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(WpImp.ImportContacts())
			{
				nameArray = WpImp.Names;
				emailArray = WpImp.Emails;
			}
		}
		//Get address page for qq.com
		private void get_sina_address_page()
		{
			if(!sinaImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(sinaImp.ImportContacts())
			{
				nameArray = sinaImp.Names;
				emailArray = sinaImp.Emails;
			}
		}

		private void get_bolUol_address_page()
		{
			if(!bolUolImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(bolUolImp.ImportContacts())
			{
				nameArray = bolUolImp.Names;
				emailArray = bolUolImp.Emails;
			}
		}

		//Get address page for qq.com
		private void get_qq_address_page()
		{
			if(!qqImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(qqImp.ImportContacts())
			{
				nameArray = qqImp.Names;
				emailArray = qqImp.Emails;
			}
		}
		//Get address page for 163.com
		private void get_onesixthree_address_page()
		{
			if(!onesixthreeImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(onesixthreeImp.ImportContacts())
			{
				nameArray = onesixthreeImp.Names;
				emailArray = onesixthreeImp.Emails;
			}
		}
		//Get address page for linkedin.com
		private void get_linkedin_address_page()
		{
			if(!linkedinImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(linkedinImp.ImportContacts())
			{
				nameArray = linkedinImp.Names;
				emailArray = linkedinImp.Emails;
			}
		}
		//Get address page for plaxo.com
		private void get_plaxo_address_page()
		{
			if(!plaxoImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(plaxoImp.ImportContacts())
			{
				nameArray = plaxoImp.Names;
				emailArray = plaxoImp.Emails;
			}
		}
		//Get address page for mail.ru
		private void get_mailru_address_page()
		{
			if(!mailruImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(mailruImp.ImportContacts())
			{
				nameArray = mailruImp.Names;
				emailArray = mailruImp.Emails;
			}
		}

		//Get address page for mail.com
		private void get_maildotcom_address_page()
		{
			if(!maildotcomImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(maildotcomImp.ImportContacts())
			{
				nameArray = maildotcomImp.Names;
				emailArray = maildotcomImp.Emails;
			}
		}
		//Get address page for web.de
		private void get_web_address_page()
		{
			if(!WebImp.IsLoggedIn) return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(WebImp.ImportContacts())
			{
				nameArray = WebImp.Names;
				emailArray = WebImp.Emails;
			}
		}
		//Get address page for rediff.com
		private void get_rediff_address_page()
		{
			if(!rediffImp.IsLoggedIn) 
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(rediffImp.ImportContacts())
			{
				nameArray = rediffImp.Names;
				emailArray = rediffImp.Emails;
			}
		}
		//Get address page for myspace.com
		private void get_myspace_address_page()
		{
			if(!myspaceImp.IsLoggedIn)
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(myspaceImp.ImportContacts())
			{
				nameArray = myspaceImp.Names;
				emailArray = myspaceImp.Emails;
			}
		}
		//Get address page for hotmail.com and msn.com
		private void get_hotmail_address_page()
		{
			if(!hotmailImp.IsLoggedIn) 
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(hotmailImp.ImportContacts())
			{
				nameArray = hotmailImp.Names;
				emailArray = hotmailImp.Emails;
			}
		}

		//Get address page for aol.com
		private void get_aol_address_page()
		{
			if(!aolImp.IsLoggedIn) 
				return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(aolImp.ImportContacts())
			{
				nameArray = aolImp.Names;
				emailArray = aolImp.Emails;
			}
		}
		
		//Get address page for gmail.com
		private void get_gmail_address_page()
		{
			if(!gmailImp.IsLoggedIn) return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(gmailImp.ImportContacts())
			{
				nameArray = gmailImp.Names;
				emailArray = gmailImp.Emails;
			}
		}
//Get address page for interia.pl
		private void get_interia_address_page()
		{
			if(!IntereaImp.IsLoggedIn) return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(IntereaImp.ImportContacts())
			{
				nameArray = IntereaImp.Names;
				emailArray = IntereaImp.Emails;
			}
		}

		private void get_lycos_address_page()
		{
			if(!LycosImp.IsLoggedIn) return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(LycosImp.ImportContacts())
			{
				nameArray = LycosImp.Names;
				emailArray = LycosImp.Emails;
			}
		}

		private void get_libero_address_page()
		{
			if(!LiberoImp.IsLoggedIn) return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(LiberoImp.ImportContacts())
			{
				nameArray = LiberoImp.Names;
				emailArray = LiberoImp.Emails;
			}
		}

//Get address page for rambler.ru
		private void get_rambler_address_page()
		{
			if(!RmbImp.IsLoggedIn) return;
			nameArray = new String[0];
			emailArray = new String[0];
			if(RmbImp.ImportContacts())
			{
				nameArray = RmbImp.Names;
				emailArray = RmbImp.Emails;
			}
		}

		private void yahoo_address_page()
		{
			System.Uri ADD_URL1=new Uri("http://address.mail.yahoo.com");
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(ADD_URL1);

			webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.0.3705)";
			

			webRequest.CookieContainer = cookies;


			StreamReader responseReader = new StreamReader(
				webRequest.GetResponse().GetResponseStream()
				);

			string responseData = responseReader.ReadToEnd();         
			responseReader.Close();
	
			System.Uri ADD_URL2=new Uri("http://address.mail.yahoo.com/?1&VPC=import_export&A=B&.rand=149816628");
			webRequest = (HttpWebRequest)WebRequest.Create(ADD_URL2);

			webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.0.3705)";
			

			webRequest.CookieContainer = cookies;


			responseReader = new StreamReader(
				webRequest.GetResponse().GetResponseStream()
				);
			responseData = responseReader.ReadToEnd();         
			responseReader.Close();

			responseData=responseData.Substring(responseData.IndexOf("<form method=\"POST\" action=\"index.php\""));
			responseData=responseData.Substring(0,responseData.IndexOf("</form>"));
			string postfields=getHiddenFields(responseData)+"&submit[action_export_yahoo]=Export Now";
			System.Uri ADD_URL3=new Uri("http://address.mail.yahoo.com/index.php");
			webRequest = (HttpWebRequest)WebRequest.Create(ADD_URL3);
			webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.0.3705)";

			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.CookieContainer = cookies; 
   
			StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream());
			requestWriter.Write(postfields);
			requestWriter.Close();

			



			responseReader = new StreamReader(
				webRequest.GetResponse().GetResponseStream()
				);
			yahoo_parser(responseReader);

		}
		
		//Parse yahoo csv address file
		private void yahoo_parser(StreamReader str)
		{        
			int count=0,counter,ncount=0;
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
					temp_name=(temp_line[2]=="" && temp_line[0]=="") ? temp_line[7]:((temp_line[2]!="" && temp_line[0]!="") ? temp_line[2]+", "+temp_line[0]+" "+temp_line[1]:temp_name) ;
					
					temp_email=(temp_line[4]!="") ? temp_line[4]:temp_line[7]+"@yahoo.com";
					try
					{
						if(temp_email.IndexOf(':')!=-1)
							temp_email=temp_email.Substring(temp_email.IndexOf(':')+1);
					}
					catch
					{}
					if(temp_email!=null && temp_email!="@yahoo.com")
						b.Add(temp_name+"<html></tr></html>"+temp_email);
					else
						ncount++;

						
				}
				count++;
			}
			
			count=count-ncount;
			
			this.emailArray=new string[count-1];
			this.nameArray=new string[count-1];
			count=0;

			Regex r=new Regex(@"\s*<html></tr></html>\s*"); 
			
			
			string[] addressD;
			foreach(string s in b)
			{
				addressD=r.Split(s);


				nameArray[count]=addressD[0];
				emailArray[count]=addressD[1];
				count++;
				
			}
		}
		
		private string getHiddenFields(String str)
		{
			string pattern=@"<input.*hidden.*?>";
			MatchCollection mc=Regex.Matches(str, pattern);
			string text="",t_text="",name="",val="";
			string nmstr="name=\"",vlstr="value=\"";
			foreach (Match m in mc)
			{
				t_text=m.ToString();
				name=t_text.Substring(t_text.IndexOf(nmstr)+nmstr.Length);
				name=name.Substring(0,name.IndexOf("\""));
				val=t_text.Substring(t_text.IndexOf(vlstr)+vlstr.Length);
				val=val.Substring(0,val.IndexOf("\""));
				if(text!="")
					text+="&"+name+"="+val;
				else
					text+=name+"="+val;
			}
			return text;
		}
		
		protected string stripHtml(string strHtml) 
		{ 
			//Strips the HTML tags from strHTML 
			System.Text.RegularExpressions.Regex objRegExp 
				= new System.Text.RegularExpressions.Regex("<(.|\n)+?>"); 

			// Replace all tags with a space, otherwise words either side 
			// of a tag might be concatenated 
			string strOutput = objRegExp.Replace(strHtml, " "); 

			// Replace all < and > with < and > 
			strOutput = strOutput.Replace("<", "<"); 
			strOutput = strOutput.Replace(">", ">");
			strOutput = strOutput.Replace("&nbsp;",""); 

			return strOutput; 
		}
		
		private int RandomNumber(int min, int max)
		{
			Random random = new Random();
			return random.Next(min, max); 
		}

		public static string MD5(string password) 
		{
			byte[] textBytes = System.Text.Encoding.Default.GetBytes(password);
			try 
			{
				System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
				cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
				byte[] hash = cryptHandler.ComputeHash (textBytes);
				string ret = "";
				foreach (byte a in hash) 
				{
					if (a<16)
						ret += "0" + a.ToString ("x");
					else
						ret += a.ToString ("x");
				}
				return ret ;
			}
			catch 
			{
				throw;
			}
		}
		

		private void yahoo_login()
		{
			string userID=this.userID;
			string password=this.password;
			System.Uri LOGIN_URL=new Uri("http://mail.yahoo.com");
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(LOGIN_URL);
			webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.0.3705)";
			

			webRequest.CookieContainer = cookies;


			StreamReader responseReader = new StreamReader(
				webRequest.GetResponse().GetResponseStream()
				);

			string responseData = responseReader.ReadToEnd();         
			responseReader.Close();
			responseData=responseData.Substring(responseData.IndexOf("<form"));
			responseData=responseData.Substring(0,responseData.IndexOf("</form"));

			string postfields=getHiddenFields(responseData)+"&login="+HttpUtility.UrlEncode(userID)+"&passwd="+HttpUtility.UrlEncode(password)+"&=Sign In";

			
			System.Uri LOGINURL =new Uri("https://login.yahoo.com/config/login?");

			webRequest = WebRequest.Create(LOGINURL) as HttpWebRequest;

			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.CookieContainer = cookies; 
			webRequest.Referer="http://mail.yahoo.com";
   
			StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream());
			requestWriter.Write(postfields);
			requestWriter.Close();

			responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
			responseData = responseReader.ReadToEnd();
			responseReader.Close();
			if(responseData.IndexOf("location.replace(\"")==-1)
				this.logged_in=false;
			else
				this.logged_in=true;
		}

		public void Dispose() 
		{
			if(gmailImp != null) gmailImp.Logout();
			if(hotmailImp != null) hotmailImp.Logout();
			
			gmailImp = null;
			hotmailImp = null;
			
			GC.SuppressFinalize(this);
		}

	}
}
