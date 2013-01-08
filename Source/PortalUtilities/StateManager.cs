///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Utilities.StateManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to maintain the state(Sessions etc) of the user/tribute
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TributesPortal.Utilities
{
    public class StateManager
    {
        public enum State
        {
            Application, Session, Cache, ViewState
        }
        private static StateManager obj = null;

        //private System.Web.UI.StateBag ViewState = new StateBag();


        private StateManager()
        {

        }
        private static StateManager objManager;
        public static StateManager Instance
        {
            get
            {
                if (objManager == null)
                    objManager = new StateManager();
                return objManager;
            }
        }

        public void Add(string key, object data, State state)
        {
            switch (state)
            {
                case State.Application:
                    HttpContext.Current.Application.Add(key, data);
                    break;
                case State.Session:
                    HttpContext.Current.Session.Add(key, data);
                    break;
                case State.Cache:
                    HttpContext.Current.Cache.Insert(key, data);
                    break;
                case State.ViewState:
                    HttpContext.Current.Session.Add("Vs" + key, data);
                    break;
            }
        }

        public void Remove(string key, State state)
        {
            switch (state)
            {
                case State.Application:
                    if (HttpContext.Current.Application[key]
                          != null)
                        HttpContext.Current.Application.
                        Remove(key);
                    break;
                case State.Session:
                    if (HttpContext.Current.Session[key] !=
                    null)
                        HttpContext.Current.Session.Remove(key);
                    break;
                case State.Cache:
                    if (HttpContext.Current.Cache[key] !=
                          null)
                        HttpContext.Current.Cache.Remove(key);
                    break;
                case State.ViewState:
                    if (HttpContext.Current.Session["Vs" + key] != null)
                        HttpContext.Current.Session.Remove("Vs" + key);
                    break;
            }
        }

        public object Get(string key, State state)
        {
            switch (state)
            {
                case State.Application:
                    if (HttpContext.Current.Application[key] != null)
                        return HttpContext.Current.
                        Application[key];
                    break;
                case State.Session:
                    if (HttpContext.Current.Session[key] != null)
                        return HttpContext.Current.Session[key];
                    break;
                case State.Cache:
                    if (HttpContext.Current.Cache[key] != null)
                        return HttpContext.Current.Cache[key];
                    break;
                case State.ViewState:
                    if (HttpContext.Current.Session["Vs" + key] != null)
                        return HttpContext.Current.Session["Vs" + key];
                    break;
                default: return null;
            }
            return null;
        }

        public int Count(State state)
        {
            switch (state)
            {
                case State.Application:
                    if (HttpContext.Current.Application != null)
                        return HttpContext.Current.Application.
                        Count;
                    break;
                case State.Session:
                    if (HttpContext.Current.Session != null)
                        return HttpContext.Current.Session.Count;
                    break;
                case State.Cache:
                    if (HttpContext.Current.Cache != null)
                        return HttpContext.Current.Cache.Count;
                    break;
                case State.ViewState:
                    if (HttpContext.Current.Session != null)
                        return HttpContext.Current.Session.Count;
                    break;
                default:
                    return 0;
            }
            return 0;
        }
    }
}
