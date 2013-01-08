///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.RewriteModule.RewriteModule.cs
///Author          : 
///Creation Date   : 
///Description     : This is the class for rewriting the URLs. A third party code.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.IO;
using System.Collections.Specialized;

namespace RewriteModule
{
    class RewriteModule : IHttpModule
    {

        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            // it is necessary to 
            context.BeginRequest += new EventHandler(RewriteModule_BeginRequest);
            context.PreRequestHandlerExecute += new EventHandler(RewriteModule_PreRequestHandlerExecute);
        }

        void RewriteModule_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if ((app.Context.CurrentHandler is Page) && app.Context.CurrentHandler != null)
            {
                Page pg = (Page)app.Context.CurrentHandler;
                pg.PreInit += new EventHandler(Page_PreInit);
            }
        }

        void Page_PreInit(object sender, EventArgs e)
        {
            // restore internal path to original
            // this is required to handle postbacks
            if (HttpContext.Current.Items.Contains("OriginalUrl"))
            {
                string path = (string)HttpContext.Current.Items["OriginalUrl"];

                // save query string parameters to context
                RewriteContext con = new RewriteContext(HttpContext.Current.Request.QueryString, path);

                HttpContext.Current.Items["RewriteContextInfo"] =  con;

                if (path.IndexOf("?") == -1)
                    path += "?";
                HttpContext.Current.RewritePath(path);
            }
        }

        void RewriteModule_BeginRequest(object sender, EventArgs e)
        {

            RewriteModuleSectionHandler cfg = (RewriteModuleSectionHandler)ConfigurationManager.GetSection("modulesSection/rewriteModule");

            // module is turned off in web.config
            if (!cfg.RewriteOn) return;

            string path = HttpContext.Current.Request.Path;

            // there us nothing to process
            if (path.Length == 0) return; 

            // load rewriting rules from web.config
            // and loop through rules collection until first match
            XmlNode rules = cfg.XmlSection.SelectSingleNode("rewriteRules");
            foreach (XmlNode xml in rules.SelectNodes("rule"))
            {
                try
                {
                    Regex re = new Regex(cfg.RewriteBase + xml.Attributes["source"].InnerText, RegexOptions.IgnoreCase);
                    Match match = re.Match(path);
                    if (match.Success)
                    {
                        path = re.Replace(path, xml.Attributes["destination"].InnerText);
                        if (path.Length != 0)
                        {
                            // check for QueryString parameters
                            if (HttpContext.Current.Request.QueryString.Count != 0)
                            {
                                // if there are Query String papameters
                                // then append them to current path
                                string sign = (path.IndexOf('?') == -1) ? "?" : "&";
                                path = path + sign + HttpContext.Current.Request.QueryString.ToString();
                            }
                            // new path to rewrite to
                            string rew = cfg.RewriteBase + path;
                            // save original path to HttpContext for further use
                            HttpContext.Current.Items.Add(
                                "OriginalUrl", 
                                HttpContext.Current.Request.RawUrl);
                            // rewrite
                            HttpContext.Current.RewritePath(rew);
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw (new Exception("Incorrect rule.", ex));
                }
            }
            return;
        }

    }
}
