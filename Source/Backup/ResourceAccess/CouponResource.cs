///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.CouponResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Coupons
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Data;

namespace TributesPortal.ResourceAccess
{
    public class CouponResource : PortalResourceAccess
    {


        public List<CouponsAvailable> GetCouponbyId(int Couponid)
        {
            List<CouponsAvailable> objUserInfo = new List<CouponsAvailable>();
            if (!Equals(Couponid, null))
            {
                object[] objParam = { Couponid };
                DataSet dsTributeAdmins = GetDataSet("usp_GetCouponsbyId", objParam);

                //to fill records in entity
                if (dsTributeAdmins.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributeAdmins.Tables[0].Rows)
                    {                        
                        CouponsAvailable objUser = new CouponsAvailable();
                        objUser.CouponCode = dr["CouponCode"].ToString();                       
                        objUserInfo.Add(objUser);
                        objUser = null;
                    }
                }
            }
            return objUserInfo;
        }

        public List<GetCoupondetails> GetCoupondetails(int userID)
        {

            List<GetCoupondetails> objUserInfo = new List<GetCoupondetails>();
            if (!Equals(userID, null))
            {
                object[] objParam = { userID };
                DataSet dsTributeAdmins = GetDataSet("usp_GetCoupondetails", objParam);

                //to fill records in entity
                if (dsTributeAdmins.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTributeAdmins.Tables[0].Rows)
                    {
                        //string UserName = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                        GetCoupondetails objUser = new GetCoupondetails();
                        objUser.PrimaryCouponID = int.Parse(dr["PrimaryCouponID"].ToString());
                        objUser.CouponName = dr["CouponName"].ToString();
                        objUser.CouponCount = int.Parse(dr["CouponCount"].ToString());
                        objUser.CouponDenomination = dr["CouponDenomination"].ToString();
                        objUser.Package = dr["Package"].ToString();
                        objUser.ApplicableFromDate = DateTime.Parse(dr["ApplicableFromDate"].ToString());
                        objUser.ExpiryDate = DateTime.Parse(dr["ExpiryDate"].ToString());
                        objUser.Usage = dr["Usage"].ToString();                        
                        objUserInfo.Add(objUser);
                        objUser = null;
                    }
                }
            }
            return objUserInfo;
        }


        public void CreateCouponDetails(object[] objTributes)
        {
            Coupons objcoupons = (Coupons)objTributes[0];
            object Identity = new object();
            if (!Equals(objTributes, null))
            {
                try
                {

                   
           
                    string[] strTributeParams ={"CouponName",
                                                "CouponDenomination",
                                                "IsPercentage",
                                                "ApplicableFromDate",
                                                "ExpiryDate",
                                                "MaxNoOfUses",
                                                "NoOfCoupons",
                                                "CreatedBy",
                        "CouponPackage"
                                                };
                    DbType[] dbType ={
                                            DbType.String,
                                            DbType.Decimal,
                                            DbType.Boolean, 
                                            DbType.DateTime,
                                            DbType.DateTime,
                                            DbType.Int64,
                                            DbType.Int64,
                                            DbType.Int64,DbType.Int64
                        
                        

                        };
                    Couponmaster objCouponMaster = objcoupons.CouponMaster;
                    object[] objValue ={
                        objCouponMaster.CouponName,objCouponMaster.CouponDenomination,objCouponMaster.IsPercentage,
                        objCouponMaster.ApplicableFromDate,objCouponMaster.ExpiryDate,objCouponMaster.MaxNoOfUses,
                        objCouponMaster.NoOfCoupons,objCouponMaster.CreatedBy,objCouponMaster.CouponPackage
                                         };
                    Identity = InsertDataAndReturnId("usp_CreateCouponDetails", strTributeParams, dbType, objValue);
                    InsertCoupons(objTributes, Identity.ToString());

                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number > 50000)
                    {
                        Errors errors = new Errors();
                        errors.ErrorMessage = sqlEx.Message;
                        objcoupons.CustomError = errors;          
                    }
                }
            }
          
        }

        public void InsertCoupons(object[] objTributes, string Couponid)
        {
            Coupons objcoupons = (Coupons)objTributes[0];            
            if (!Equals(objTributes, null))
            {
                try
                {                    

                    string[] strTributeParams ={"PrimaryCouponID",
                                                "SerialNumber",
                                                "CouponCode"                                                
                                                };
                    DbType[] dbType ={
                                            DbType.Int64,
                                            DbType.Decimal,DbType.String
                                           
                                           
                        
                        

                        };
                     CouponsAvailable objCouponAv = objcoupons.Couponsavailable;
                    int id= int.Parse(Couponid);
                    string[] coupons=objCouponAv.CouponCode.Split(';');
                    for (int i = 0; i < coupons.Length; i++)
                    {
                        object[] objValue ={ id, i, coupons[i] };
                        base.InsertRecord("usp_InsertCoupons", strTributeParams, dbType, objValue);
                    }

                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number > 50000)
                    {
                        Errors errors = new Errors();
                        errors.ErrorMessage = sqlEx.Message;
                        objcoupons.CustomError = errors;
                        
                    }
                }
            }            
        }

        public void DeleteCoupons(object[] objTributes)
        {
            Couponmaster tributes = (Couponmaster)objTributes[0];
            if (!Equals(objTributes, null))
            {
                try
                {


                    string[] strTributeParams ={
                                                "Userid" ,"CouponID"                     
                                                };
                    DbType[] dbType ={
                                      DbType.Int64,                                      
                                      DbType.Int64
                                      
                        };
                    object[] objValue ={tributes.ModifiedBy,tributes.PrimaryCouponID
                                         };
                    base.UpdateRecord("usp_DeteleCoupons", strTributeParams, dbType, objValue);

                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number > 50000)
                    {
                        Errors errors = new Errors();
                        errors.ErrorMessage = sqlEx.Message;                       

                    }
                }
            }

        }
    }
}
