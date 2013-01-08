///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.IOVS.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the methods to check for IOVS of all data going to the database
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web;
using System.IO;

namespace TributesPortal.ResourceAccess
{
    /// <summary>
    /// This class is used for all IOVS
    /// </summary>
    public class IOVS
    {       

        #region Locals
        private static XmlDocument _ruleBase;
        #endregion

        #region Static Constructor
        /// <summary>
        /// To intialize the IOVS class
        /// </summary>
        static IOVS()
        {
            if (_ruleBase == null)
            {
                _ruleBase = new XmlDocument();
                _ruleBase.Load(Path.Combine(HttpContext.Current.Server.MapPath("~"), ConfigurationManager.AppSettings["IOVSRuleBase"]));
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// To validate the value for SQL Injection, Special Characters validation and Scripting attacks
        /// </summary>
        /// <param name="valueToSanitise"></param>
        /// <returns>Return TRUE if value is correct else FASLE</returns>
        //public static bool Sanitise(string valueToSanitise, string fieldName)
        public static bool Sanitise(string valueToSanitise, ref string message)
        {
            /*messages = new Messages();
            if (!string.IsNullOrEmpty(valueToSanitise) && !string.IsNullOrEmpty(fieldName))
                return (CheckForSQLInjection(valueToSanitise, messages, fieldName) &&
                        CheckSpecialCharacters(valueToSanitise, messages, fieldName) &&
                        CheckForScriptingAttacks(valueToSanitise, messages, fieldName));
            else
                return true;*/
            if (!string.IsNullOrEmpty(valueToSanitise))
                return (CheckForSQLInjection(valueToSanitise, ref message)
                    &&  CheckSpecialCharacters(valueToSanitise, ref message) &&
                        CheckForScriptingAttacks(valueToSanitise, ref message));
            else
                return true;
        }


        #endregion

        #region Private Methods
        /// <summary>
        /// To validate the Scripting Attacks
        /// </summary>
        /// <returns>Return TRUE if value is correct else FASLE</returns>
        private static bool CheckForScriptingAttacks(string valueToCheck, ref string message)
        {
            Match matPattern;
            XmlNode eleField = _ruleBase.SelectSingleNode("/IOVS/Section[@Id='Sanitation']/Field [@Id='XSSAttack']");
            if (eleField != null)
            {
                XmlNodeList colRegEx = eleField.SelectNodes("RegEx");
                foreach (XmlNode objRegEx in colRegEx)
                {
                    matPattern = Regex.Match(valueToCheck, objRegEx.InnerText, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                    if (matPattern.Success)
                    {
                        XmlNode objError = eleField.SelectSingleNode("ErrorMessage");
                        if (objError != null)
                            message = objError.InnerText;
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// To validate the SQL Injection
        /// </summary>
        /// <returns>Return TRUE if value is correct else FASLE</returns>
        //private static bool CheckForSQLInjection(string valueToCheck, Messages messages, string fieldName)
        private static bool CheckForSQLInjection(string valueToCheck, ref string message)
        {
            Match matPattern;
            XmlNode eleField = _ruleBase.SelectSingleNode("/IOVS/Section[@Id='Sanitation']/Field [@Id='SQLInjectionAttack']");
            if (eleField != null)
            {
                XmlNodeList colRegEx = eleField.SelectNodes("RegEx");
                foreach (XmlNode objRegEx in colRegEx)
                {
                    //matPattern = Regex.Match(valueToCheck, objRegEx.InnerText, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                    matPattern = Regex.Match(valueToCheck, objRegEx.InnerText, RegexOptions.IgnoreCase);
                    if (matPattern.Success)
                    {
                        XmlNode objError = eleField.SelectSingleNode("ErrorMessage");
                        if (objError != null)
                            message = objError.InnerText;
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// To validate the Special Characters 
        /// </summary>
        /// <returns>Return TRUE if value is correct else FASLE</returns>
        //private static bool CheckSpecialCharacters(string valueToCheck, Messages messages, string fieldName)
        private static bool CheckSpecialCharacters(string valueToCheck, ref string message)
        {
            Match matchPattern;
            XmlNode parentField = _ruleBase.SelectSingleNode("/IOVS/Section[@Id='Validation']/Field [@Id='SpecialCharacters']");
            if (parentField != null)
            {
                XmlNodeList regExpresspions = parentField.SelectNodes("RegEx");
                foreach (XmlNode regExpression in regExpresspions)
                {
                    matchPattern = Regex.Match(valueToCheck, regExpression.InnerText, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                    if (!matchPattern.Success)
                    {
                        XmlNode objError = parentField.SelectSingleNode("ErrorMessage");
                        if (objError != null)
                            message = objError.InnerText;
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// To get the validation error message from IOVS.config file
        /// </summary>
        /// <param name="strFieldXPath"></param>
        /// <returns>Return TRUE if value is correct else FASLE</returns>
        private static string GetFieldMessage(string fieldXPath)
        {
            string strMessage = "";
            XmlNode eleField, eleMsg;
            try
            {
                eleField = _ruleBase.SelectSingleNode(fieldXPath);
                if (eleField != null)
                {
                    eleMsg = eleField.SelectSingleNode("Message");

                    strMessage = (eleMsg == null ? "" : (eleMsg.InnerText.ToString()));

                }
                return strMessage;
            }
            catch
            {
                return "";
            }
            finally
            {
                eleField = null;
                eleMsg = null;
            }
        }
        #endregion
    }

    public class IovsException : Exception
    {
        public IovsException(string message) : base(message) { }
    }
}
