///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.TemplateResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Templates
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Data;

namespace TributesPortal.ResourceAccess
{
    public partial class TemplateResource : PortalResourceAccess
    {
        public List<Templates> GetTemplates(string strTheme)
        {
            try
            {
                DataSet dsTemplates = GetDataSet("usp_GetThemes", new string[] { strTheme });
                List<Templates> lstTemplates = new List<Templates>();
                if (dsTemplates.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemplates.Tables[0].Rows)
                    {
                        Templates objTemplates = new Templates();
                        objTemplates.TemplateID = int.Parse(dr["ThemeId"].ToString());
                        objTemplates.TemplateName = dr["ThemeName"].ToString();
                        objTemplates.TemplatePath = dr["ThemePath"].ToString();
                        objTemplates.ThemeCssClass = dr["ThemeClassId"].ToString();
                        objTemplates.ThemeValue = dr["ThemeValue"].ToString();
                        lstTemplates.Add(objTemplates);
                        objTemplates = null;
                    }
                }
                return lstTemplates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Templates> GetThemesTemplates(string strTheme, string applicationType)
        {
            try
            {
                DataSet dsTemplates = GetDataSet("usp_GetThemesTemplates", new string[] { strTheme, applicationType });
                List<Templates> lstTemplates = new List<Templates>();
                if (dsTemplates.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemplates.Tables[0].Rows)
                    {
                        Templates objTemplates = new Templates();
                        objTemplates.TemplateID = int.Parse(dr["ThemeId"].ToString());
                        objTemplates.TemplateName = dr["ThemeName"].ToString();
                        objTemplates.TemplatePath = dr["ThemePath"].ToString();
                        objTemplates.FolderName = dr["FolderName"].ToString();
                        objTemplates.ThemeValue = dr["ThemeValue"].ToString();
                        lstTemplates.Add(objTemplates);
                        objTemplates = null;
                    }
                }
                return lstTemplates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Themes> GetThemes(string strTheme)
        {
            try
            {
                DataSet dsTemplates = GetDataSet("usp_GetThemes", new string[] { strTheme });
                List<Themes> objThemes = new List<Themes>();
                if (dsTemplates.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemplates.Tables[0].Rows)
                    {
                        objThemes.Add(new Themes(int.Parse(dr["ThemeId"].ToString()), dr["ThemeName"].ToString(), dr["ThemePath"].ToString(),
                            dr["Tributetype"].ToString(), dr["ThemeClassId"].ToString(), dr["ThemeValue"].ToString()));

                    }
                }
                return objThemes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Themes> GetThemesForCategory(string strTheme, string categoryName, string applicationType)
        {
            try
            {
                DataSet dsTemplates = GetDataSet("usp_GetThemesForSubCategory", new string[] { strTheme, categoryName, applicationType });
                List<Themes> objThemes = new List<Themes>();
                if (dsTemplates.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTemplates.Tables[0].Rows)
                    {
                        objThemes.Add(new Themes(int.Parse(dr["ThemeId"].ToString()), dr["ThemeName"].ToString(), dr["ThemePath"].ToString(),
                            dr["Tributetype"].ToString(), dr["ThemeClassId"].ToString(), dr["ThemeValue"].ToString(), dr["SubCategory"].ToString(), dr["FolderName"].ToString()));

                    }
                }
                return objThemes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to update Tribute theme
        /// </summary>
        /// <param name="objTheme">Object containing Tribute entity.</param>
        public void UpdateTributeTheme(object[] objTheme)
        {
            Tributes objTributeTheme = (Tributes)objTheme[0];

            if (!Equals(objTributeTheme, null))
            {
                try
                {
                    //sets the parameters
                    string[] strParam = {Tributes.TributeEnum.ThemeId.ToString(),  
                                            Tributes.TributeEnum.TributeId.ToString(),
                                            Tributes.TributeEnum.ModifiedBy.ToString(),
                                            Tributes.TributeEnum.ModifiedDate.ToString()
                                        };
                    //sets the types of parameters
                    DbType[] dbType = {DbType.Int64,
                                        DbType.Int64,
                                        DbType.Int64,
                                        DbType.DateTime
                                      };
                    //sets the values in the entity to the parameters
                    object[] objValue = { objTributeTheme.ThemeId,
                                            objTributeTheme.TributeId,
                                            objTributeTheme.ModifiedBy,
                                            objTributeTheme.ModifiedDate
                                        };

                    //sends request to insert record and get the identity of the record inserted
                    base.UpdateRecord("usp_UpdateTributeTheme", strParam, dbType, objValue);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Method to get the Theme for selected tribute
        /// </summary>
        /// <param name="objTheme">object conatining tribute id</param>
        /// <returns>Template entity containing Theme id</returns>
        public Templates GetThemeForTribute(object[] objTheme)
        {
            try
            {
                Templates objTributeTheme = new Templates();
                object[] objParam = { ((Tributes)objTheme[0]).TributeId };

                DataSet dsTheme = GetDataSet("usp_GetTributeTheme", objParam);

                if (dsTheme.Tables[0].Rows.Count > 0)
                {
                    objTributeTheme.TemplateID = int.Parse(dsTheme.Tables[0].Rows[0]["ThemeId"].ToString());
                    objTributeTheme.TemplateName = dsTheme.Tables[0].Rows[0]["ThemeName"].ToString();
                    objTributeTheme.ThemeValue = dsTheme.Tables[0].Rows[0]["ThemeValue"].ToString();
                }
                return objTributeTheme;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Templates GetThemeFolderForTribute(object[] objTheme)
        {
            try
            {
                Templates objTributeTheme = new Templates();
                object[] objParam = { ((Tributes)objTheme[0]).TributeId };

                DataSet dsTheme = GetDataSet("usp_GetTributeThemeFolder", objParam);

                if (dsTheme.Tables[0].Rows.Count > 0)
                {
                    objTributeTheme.TemplateID = int.Parse(dsTheme.Tables[0].Rows[0]["ThemeId"].ToString());
                    objTributeTheme.TemplateName = dsTheme.Tables[0].Rows[0]["ThemeName"].ToString();
                    objTributeTheme.ThemeValue = dsTheme.Tables[0].Rows[0]["ThemeValue"].ToString();
                    objTributeTheme.FolderName = dsTheme.Tables[0].Rows[0]["FolderName"].ToString();
                }
                return objTributeTheme;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTheme"></param>
        /// <returns></returns>
        public Templates GetExistingFolderName(object[] objTheme)
        {
            try
            {
                Templates objTributeTheme = new Templates();
                object[] objParam = { ((Tributes)objTheme[0]).TributeId };

                DataSet dsTheme = GetDataSet("usp_GetTributeThemeFoldername", objParam);

                if (dsTheme.Tables[0].Rows.Count > 0)
                {
                    objTributeTheme.TemplateID = int.Parse(dsTheme.Tables[0].Rows[0]["ThemeId"].ToString());
                    objTributeTheme.TemplateName = dsTheme.Tables[0].Rows[0]["ThemeName"].ToString();
                    objTributeTheme.ThemeValue = dsTheme.Tables[0].Rows[0]["ThemeValue"].ToString();
                    objTributeTheme.FolderName = dsTheme.Tables[0].Rows[0]["FolderName"].ToString();
                }
                return objTributeTheme;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // by ud for getting Default theme for business type user
        public int GetDefaultTheme(int UserId, string strTributeType)
        {

            object[] objParam = { UserId, strTributeType };
            try
            {
                DataSet dsTemplates = GetDataSet("usp_GetDefaultTheme", objParam);
                int defaultTheme = 0;
                if (dsTemplates.Tables[0].Rows.Count > 0)
                {
                    defaultTheme = Convert.ToInt32(dsTemplates.Tables[0].Rows[0]["ThemeId"]);
                }
                return defaultTheme;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // by ud for getting Default theme for business type user
        public void SaveDefaultTheme(int UserId,String TributeType,int ThemeId)
        {

           Tributes objtributes = new Tributes();
           objtributes.UserTributeId= UserId;
           objtributes.TypeDescription = TributeType;
           objtributes.ThemeId = ThemeId;
            try
            {
                string[] strDefaultThemesParams = { "UserId", "ThemeId","TributeType" };
                DbType[] dbtype = { DbType.Int64,
                                      DbType.Int64,
                                      DbType.String
                                     };
                object[] objValue = { objtributes.UserTributeId, objtributes.ThemeId, objtributes.TypeDescription };
                InsertRecordMinusIovs("usp_InsertDefaultTheme", strDefaultThemesParams, dbtype, objValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }//end class
}//end namespace
