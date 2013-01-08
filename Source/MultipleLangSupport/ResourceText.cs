///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MultipleLangSupport.ResourceText.cs
///Author          : 
///Creation Date   : 
///Description     : This is the class to get string values from App_GlobalResources.Resource
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Resources;
using System.Reflection;
using System.Collections;

namespace TributesPortal.MultipleLangSupport
{
    public class ResourceText
    {
        private ResourceText()
        { }

        private static ResourceManager _resourceManager;

        private static void InitializeResources()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            _resourceManager = new ResourceManager("TributesPortal.MultipleLangSupport.App_GlobalResources.Resource", assembly);             
            _resourceManager.IgnoreCase = true;
        }

        public static string GetString(string key)
        {
            try
            {
                InitializeResources();
                string strValue = _resourceManager.GetString(key);
                if (null == strValue) throw (new Exception());
                return strValue;
            }
            catch
            {
                //return String.Format("[?:{0}]", key);
                return "Not Found";
            }
        }
    }
}
