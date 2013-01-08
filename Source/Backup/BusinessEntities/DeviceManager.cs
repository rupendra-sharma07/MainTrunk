using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    public class DeviceManager
    {
        private const string Iphone = "iphone";
        private const string Android = "android";
        /// <summary>
        /// 
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMobileBrowser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsMobileDevice()
        {
            if (IsMobileBrowser || UserAgent.ToLower().Contains(Iphone) || UserAgent.ToLower().Contains(Android))
            {
                return true;
            }
            return false;
        }

    }//end class
}
