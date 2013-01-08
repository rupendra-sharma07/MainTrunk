using System;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Xml.XPath;

namespace Improsys.ContactImporter
{
    internal sealed class XMLReader
    {
        private string _xmlToParse;
        private ArrayList name = new ArrayList();
        private ArrayList email = new ArrayList();

		// Properties
		public ArrayList Name
		{
			get
			{
				return this.name;
			}
		}
		public ArrayList Email
		{
			get
			{
				return this.email;
			}
		}

        // Methods
        internal XMLReader(string xmlToParse)
        {
            this._xmlToParse = xmlToParse;
        }
        public void ReadValue()
        {
            XPathNavigator navigator;
            XPathDocument docNav;
            XmlTextReader reader = null;
            reader = new XmlTextReader(new StringReader(this._xmlToParse));
            docNav = new XPathDocument(reader);
            navigator = docNav.CreateNavigator();
            navigator.MoveToRoot();
            navigator.MoveToFirstChild();
            navigator.MoveToFirstChild();
			navigator.MoveToFirstChild();
            do
            {
				string tmpName="";
				navigator.MoveToFirstChild();
				do
				{
					if(navigator.Name=="Email")
						email.Add(navigator.Value);
					else if(navigator.Name=="FirstName")
						tmpName += navigator.Value;
					else if(navigator.Name=="LastName")
						tmpName += " " + navigator.Value;
					else if(navigator.Name=="MiddleName")
						tmpName += " " + navigator.Value;
					else if(navigator.Name=="NickName")
					{
						if(tmpName!="")
							name.Add(tmpName);
						else
							name.Add(navigator.Value);
					}

				}while(navigator.MoveToNext());
				navigator.MoveToParent();
				
            } while (navigator.MoveToNext());
        }
    }
}