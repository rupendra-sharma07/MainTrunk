///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.BillingResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Billing
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using System.Data;

namespace TributesPortal.ResourceAccess
{
    public class BillingResource : PortalResourceAccess//, IResourceAccess
    {
        #region IResourceAccess Members


        public IList<PaymentReceipt> PaymentReceipt(object[] objValue)
        {

            List<PaymentReceipt> objPaymentReceipt = new List<PaymentReceipt>();
            PaymentReceipt objpay = null;
            try
            {
                DataSet ds = GetDataSet("usp_PaymentReceipt", objValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //objpay = new PaymentReceipt();
                        //objpay.Tributename = ds.Tables[0].Rows[i]["Tributename"].ToString();
                        //objpay.TypeDescription = ds.Tables[0].Rows[i]["TypeDescription"].ToString();
                        //objpay.Enddate = ds.Tables[0].Rows[i]["Enddate"].ToString();
                        //objpay.CardholdersName = ds.Tables[0].Rows[i]["CardholdersName"].ToString();
                        //objpay.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                        //objpay.City = ds.Tables[0].Rows[i]["City"].ToString();
                        //objpay.State = ds.Tables[0].Rows[i]["State"].ToString();
                        //objpay.Country = ds.Tables[0].Rows[i]["Country"].ToString();
                        //objpay.Zip = ds.Tables[0].Rows[i]["Zip"].ToString();
                        //objpay.Telephone = ds.Tables[0].Rows[i]["Telephone"].ToString();
                        //objpay.StartDate = DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString());
                        //objpay.CreditCardType = ds.Tables[0].Rows[i]["CreditCardType"].ToString();
                        //objpay.CreditCardNo = ds.Tables[0].Rows[i]["CreditCardNo"].ToString();
                        //objpay.AmountPaid = int.Parse(ds.Tables[0].Rows[i]["AmountPaid"].ToString());
                        //objPaymentReceipt.Add(objpay);

                        objPaymentReceipt.Add(new PaymentReceipt(ds.Tables[0].Rows[i]["Tributename"].ToString(),
                                                                 ds.Tables[0].Rows[i]["TypeDescription"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Packagename"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Enddate"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CardholdersName"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Address"].ToString(),
                                                                 ds.Tables[0].Rows[i]["City"].ToString(),
                                                                 ds.Tables[0].Rows[i]["State"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Country"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Zip"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Telephone"].ToString(),
                                                                 DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString()),
                                                                 ds.Tables[0].Rows[i]["CreditCardType"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CreditCardNo"].ToString(),
                                                                 int.Parse(ds.Tables[0].Rows[i]["AmountPaid"].ToString())));


                    }
                }
                return objPaymentReceipt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IList<BillingHistory> GetBillingHistory(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            object[] objParam = { objUserReg.Users.UserId.ToString() };
            List<BillingHistory> objBillingHistory = new List<BillingHistory>();
            try
            {
                DataSet ds = GetDataSet("usp_GetBillingHistory", objParam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objBillingHistory.Add(new BillingHistory(DateTime.Parse(ds.Tables[0].Rows[i][BillingHistory.Billing.StartDate.ToString()].ToString()),
                                                                               ds.Tables[0].Rows[i][BillingHistory.Billing.TributeName.ToString()].ToString(),
                                                                               ds.Tables[0].Rows[i][BillingHistory.Billing.PackageName.ToString()].ToString().Replace("Life Time", "Lifetime"),
                                                                               decimal.Parse(ds.Tables[0].Rows[i][BillingHistory.Billing.AmountPaid.ToString()].ToString()),
                                                                               decimal.Parse(ds.Tables[0].Rows[i][BillingHistory.Billing.Tributeid.ToString()].ToString()),
                                                                               int.Parse(ds.Tables[0].Rows[i]["TributePackageId"].ToString())
                                                                 ));
                    }
                }
                return objBillingHistory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetTriputePackageInfo(object[] objValue)
        {
            TributePackage objUserReg = (TributePackage)objValue[0];
            try
            {
                object[] objParam = { objUserReg.UserTributeId };
                DataSet _objDataSet = GetDataSet("usp_GetTriputePackageInfo", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(_objDataSet.Tables[0].Rows[0]["EndDate"].ToString()))
                        objUserReg.EndDate = null;
                    else
                        objUserReg.EndDate = DateTime.Parse(_objDataSet.Tables[0].Rows[0]["EndDate"].ToString());
                    objUserReg.PackageId = int.Parse(_objDataSet.Tables[0].Rows[0]["PackageId"].ToString());
                    objUserReg.IsSponsor = bool.Parse(_objDataSet.Tables[0].Rows[0]["IsSponsor"].ToString());
                    objUserReg.IsSponserHide = bool.Parse(_objDataSet.Tables[0].Rows[0]["IsSponserHide"].ToString());
                    objUserReg.IsAutomaticRenew = bool.Parse(_objDataSet.Tables[0].Rows[0]["IsAutomaticRenew"].ToString());
                    objUserReg.UserId = int.Parse(_objDataSet.Tables[0].Rows[0]["UserId"].ToString());
                    objUserReg.TributePackageId = int.Parse(_objDataSet.Tables[0].Rows[0]["TributePackageId"].ToString());

                    //
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }

        }

        public void GetCreditCardDetails(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            try
            {
                object[] objParam = { objUserReg.Users.UserId };
                DataSet _objDataSet = GetDataSet("usp_GetCreditcardDetails", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    UserCreditcardDetails objCarddetails = new UserCreditcardDetails();
                    objCarddetails.CreditCardId = int.Parse(_objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.CreditCardId.ToString()].ToString());
                    objCarddetails.UserId = int.Parse(_objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.UserId.ToString()].ToString());
                    objCarddetails.CreditCardType = _objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.CreditCardType.ToString()].ToString();
                    objCarddetails.CreditCardNo = _objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.CreditCardNo.ToString()].ToString();
                    objCarddetails.CardholdersName = _objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.CardholdersName.ToString()].ToString();
                    objCarddetails.ExpirationDate = DateTime.Parse(_objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.ExpirationDate.ToString()].ToString());
                    objCarddetails.Address = _objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.Address.ToString()].ToString();
                    objCarddetails.City = _objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.City.ToString()].ToString();
                    objCarddetails.Zip = _objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.Zip.ToString()].ToString();
                    if (!(_objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.State.ToString()].ToString().Equals("")))
                        objCarddetails.State = int.Parse(_objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.State.ToString()].ToString());
                    else
                        objCarddetails.State = null;

                    objCarddetails.Country = int.Parse(_objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.Country.ToString()].ToString());
                    objCarddetails.Telephone = _objDataSet.Tables[0].Rows[0][UserCreditcardDetails.CardDetais.Telephone.ToString()].ToString();
                    objCarddetails.CVC = _objDataSet.Tables[0].Rows[0]["CVC"].ToString();
                    objCarddetails.SponsorEmailAddress = _objDataSet.Tables[0].Rows[0]["SponsorEmailAddress"].ToString();
                    objUserReg.UserCreditcardDetails = objCarddetails;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }

        }
        //Mohit Gupta Credit Cost Mapping grid in Step 5 of Tribute creation Page

        public IList<CreditCostMapping> GetCreditCostMapping()
        {
            object[] objParam = null;
            List<CreditCostMapping> objCreditCostMapping = new List<CreditCostMapping>();
            try
            {
                DataSet _objDataSet = GetDataSet("usp_GetCreditCostMappingDetails", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < _objDataSet.Tables[0].Rows.Count; i++)
                    {
                        objCreditCostMapping.Add(new CreditCostMapping(int.Parse(_objDataSet.Tables[0].Rows[i][CreditCostMapping.CreditCost.CreditPoints.ToString()].ToString()),
                                                                               double.Parse(_objDataSet.Tables[0].Rows[i][CreditCostMapping.CreditCost.CostPerCredit.ToString()].ToString()),
                                                                                int.Parse(_objDataSet.Tables[0].Rows[i][CreditCostMapping.CreditCost.TributeType.ToString()].ToString())
                                                                 ));
                    }
                }
                return objCreditCostMapping;
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }

        }

        public void GetCreditPointCount(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            try
            {
                object[] objParam = { objUserReg.Users.UserId };
                DataSet _objDataSet = GetDataSet("usp_GetCreditCount", objParam);

                if (_objDataSet.Tables[0].Rows.Count != 0)
                {
                    CreditPointTransaction objTranDetails = new CreditPointTransaction();
                    objTranDetails.TransactionId = Convert.ToInt32(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.TransactionId.ToString()].ToString());
                    objTranDetails.UserId = Convert.ToInt32(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.UserId.ToString()].ToString());
                    objTranDetails.NetCreditPoints = Convert.ToInt32(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.NetCreditPoints.ToString()].ToString());
                    objTranDetails.AmountPaid = Convert.ToInt32(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.AmountPaid.ToString()].ToString());
                    objTranDetails.CreditPackageId = Convert.ToInt32(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CreditPackageId.ToString()].ToString());
                    objTranDetails.PurchasedDate = DateTime.Parse(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.PurchasedDate.ToString()].ToString());
                    objTranDetails.IsDeleted = bool.Parse(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.IsDeleted.ToString()].ToString());
                    if (!(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.ModifiedDate.ToString()] == null || _objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.ModifiedDate.ToString()].ToString() == ""))
                    {
                        objTranDetails.ModifiedDate = DateTime.Parse(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.ModifiedDate.ToString()].ToString());
                    }
                    if (!(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CouponId.ToString()] == null || _objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CouponId.ToString()].ToString() == ""))
                    {
                        objTranDetails.CouponId = Convert.ToInt32(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CouponId.ToString()].ToString());
                    }
                    if (!(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CreditCardId.ToString()] == null || _objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CreditCardId.ToString()].ToString() == ""))
                    {
                        objTranDetails.CreditCardId = Convert.ToInt32(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CreditCardId.ToString()].ToString());
                    }
                    if (!(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CreatedDate.ToString()] == null || _objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CreatedDate.ToString()].ToString() == ""))
                    {
                        objTranDetails.CreatedDate = DateTime.Parse(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.CreatedDate.ToString()].ToString());
                    }
                    if (!(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.ConfirmationNo.ToString()] == null || _objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.ConfirmationNo.ToString()].ToString() == ""))
                    {
                        objTranDetails.ConfirmationNo = Convert.ToInt32(_objDataSet.Tables[0].Rows[0][CreditPointTransaction.CreditPointDetails.ConfirmationNo.ToString()].ToString());
                    }
                    else
                        objTranDetails.ConfirmationNo = 0;
                    objUserReg.CreditPointTransaction = objTranDetails;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        public object InsertCurrentCreditPoints(object[] objTributes)
        {
            object Identity = new object();
            CreditPointTransaction objCreditTrandetails = (CreditPointTransaction)objTributes[0];
            string confirmationId = objTributes[1].ToString();
            if (!Equals(objTributes, null))
            {
                try
                {
                    string[] strTributeParams ={
                                                CreditPointTransaction.CreditPointDetails.UserId.ToString(), 
                                                CreditPointTransaction.CreditPointDetails.NetCreditPoints.ToString(),
                                                 CreditPointTransaction.CreditPointDetails.AmountPaid.ToString(),
                                                 CreditPointTransaction.CreditPointDetails.CreditPackageId.ToString(),
                                                CreditPointTransaction.CreditPointDetails.PurchasedDate.ToString(),
                                                CreditPointTransaction.CreditPointDetails.IsDeleted.ToString(),                                                
                                                CreditPointTransaction.CreditPointDetails.ModifiedDate.ToString(),                                              
                                                CreditPointTransaction.CreditPointDetails.CouponId.ToString(),                                                
                                                CreditPointTransaction.CreditPointDetails.CreditCardId.ToString(),
                                                CreditPointTransaction.CreditPointDetails.CreatedDate.ToString(),
                                               CreditPointTransaction.CreditPointDetails.ConfirmationNo.ToString()                                    
                                                };
                    DbType[] dbType ={
                                      DbType.Int64,                                      
                                      DbType.Int64,
                                      DbType.Int64,
                                      DbType.Int64,
                                      DbType.DateTime,
                                      DbType.Boolean,
                                      DbType.DateTime,
                                      DbType.Int64,                                     
                                      DbType.Int64,   
                                      DbType.DateTime,
                                      DbType.String                                    
                                      
                        };
                    object[] objValue ={objCreditTrandetails.UserId,
                        objCreditTrandetails.NetCreditPoints,objCreditTrandetails.AmountPaid,objCreditTrandetails.CreditPackageId,objCreditTrandetails.PurchasedDate,
                        objCreditTrandetails.IsDeleted,objCreditTrandetails.ModifiedDate,objCreditTrandetails.CouponId,objCreditTrandetails.CreditCardId,
                        objCreditTrandetails.CreatedDate,confirmationId
                                         };
                    Identity = base.InsertDataAndReturnId("usp_InsertCurrentCreditPoints", strTributeParams, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number > 50000)
                    {
                        Errors errors = new Errors();
                        errors.ErrorMessage = sqlEx.Message;
                        return Identity;
                    }
                }
            }
            return Identity;
        }

        public object InsertPackageDetails(object[] objTributes)
        {
            object Identity = new object();
            TributePackage tributes = (TributePackage)objTributes[0];
            string confirmationId = objTributes[1].ToString();
            if (!Equals(objTributes, null))
            {
                try
                {
                    string[] strTributeParams ={
                                                TributePackage.PackageTribute.UserId.ToString(),
                                                TributePackage.PackageTribute.UserTributeId.ToString(),
                                                TributePackage.PackageTribute.StartDate.ToString(),
                                                TributePackage.PackageTribute.EndDate.ToString(),
                                                TributePackage.PackageTribute.IsAutomaticRenew.ToString(),
                                                TributePackage.PackageTribute.AmountPaid.ToString(),
                                                TributePackage.PackageTribute.PackageId.ToString(),
                                                TributePackage.PackageTribute.CouponId.ToString(),
                                                TributePackage.PackageTribute.IsSponsor.ToString(),
                                                TributePackage.PackageTribute.IsSponserHide.ToString(),
                                                TributePackage.PackageTribute.CreditCardId.ToString(),
                        "PaymentConfirmation"
                                                };
                    DbType[] dbType ={
                                      DbType.Int64,                                      
                                      DbType.Int64,
                                      DbType.DateTime,
                                      DbType.DateTime,
                                      DbType.Boolean,
                                      DbType.Currency,
                                      DbType.Int64,                                      
                                      DbType.Int64,
                                      DbType.Boolean,
                                      DbType.Boolean,
                                      DbType.Int64,
                                      DbType.String
                        };
                    object[] objValue ={tributes.UserId,
                        tributes.UserTributeId,tributes.StartDate,tributes.EndDate,tributes.IsAutomaticRenew,
                        tributes.AmountPaid,tributes.PackageId,tributes.CouponId,tributes.IsSponsor,
                        tributes.IsSponserHide,tributes.CreditCardId,confirmationId
                                         };
                    Identity = base.InsertDataAndReturnId("InsertPackageDetails", strTributeParams, dbType, objValue);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number > 50000)
                    {
                        Errors errors = new Errors();
                        errors.ErrorMessage = sqlEx.Message;
                        tributes.CustomError = errors;
                        return Identity;
                    }
                }
            }
            return Identity;
        }

        public object InsertRecord(object[] Params)
        {
            object Identity = new object();
            UserRegistration objUserReg = (UserRegistration)Params[0];
            try
            {

                string[] strParam = {
                                       UserCreditcardDetails.CardDetais.UserId.ToString(),
                                       UserCreditcardDetails.CardDetais.CardholdersName.ToString(),
                                       UserCreditcardDetails.CardDetais.CreditCardType.ToString(),
                                       UserCreditcardDetails.CardDetais.CreditCardNo.ToString(),                                       
                                       UserCreditcardDetails.CardDetais.ExpirationDate.ToString(),
                                       UserCreditcardDetails.CardDetais.Address.ToString(),
                                       UserCreditcardDetails.CardDetais.City.ToString(),
                                       UserCreditcardDetails.CardDetais.Zip.ToString(),
                                       UserCreditcardDetails.CardDetais.State.ToString(),
                                       UserCreditcardDetails.CardDetais.Country.ToString(),
                                       UserCreditcardDetails.CardDetais.Telephone.ToString(),
                                       UserCreditcardDetails.CardDetais.IsCardDetailsReusable.ToString(),
                                       "NotifyBeforeRenew","CVC", 
                //    "SponsorEmailAddress"                   
                                       UserCreditcardDetails.CardDetais.SponsorEmailAddress.ToString()
                                };
                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,                                    
                                    DbType.DateTime,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.Boolean,
                                    DbType.Boolean, DbType.String, DbType.String
                                 };
                object[] objValue ={
                                    objUserReg.UserCreditcardDetails.UserId,
                                    objUserReg.UserCreditcardDetails.CardholdersName,
                                    objUserReg.UserCreditcardDetails.CreditCardType,
                                    objUserReg.UserCreditcardDetails.CreditCardNo,                   
                                    objUserReg.UserCreditcardDetails.ExpirationDate,
                                    objUserReg.UserCreditcardDetails.Address,
                                    objUserReg.UserCreditcardDetails.City,
                                    objUserReg.UserCreditcardDetails.Zip,
                                    objUserReg.UserCreditcardDetails.State,
                                    objUserReg.UserCreditcardDetails.Country,
                                    objUserReg.UserCreditcardDetails.Telephone,
                                    objUserReg.UserCreditcardDetails.IsCardDetailsReusable,
                                    objUserReg.UserCreditcardDetails.NotifyBeforeRenew,objUserReg.UserCreditcardDetails.CVC,
                                    objUserReg.UserCreditcardDetails.SponsorEmailAddress
                                   };
                // base.UpdateRecord("usp_InsertCreditCardDetails", strParam, enumDbType, objValue);
                Identity = base.InsertDataAndReturnIdMinusIOVS("usp_InsertCreditCardDetails", strParam, enumDbType, objValue);

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                    return Identity;
                }
            }
            return Identity;
        }

        public void GetData(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void UpdateRecord(object[] Params)
        {
            UserRegistration objUserReg = (UserRegistration)Params[0];
            try
            {
                string[] strParam = {
                                        UserCreditcardDetails.CardDetais.UserId.ToString(),
                                        UserCreditcardDetails.CardDetais.CreditCardType.ToString(),
                                       UserCreditcardDetails.CardDetais.CreditCardNo.ToString(),
                                       UserCreditcardDetails.CardDetais.CardholdersName.ToString(),
                                       UserCreditcardDetails.CardDetais.ExpirationDate.ToString(),
                                       UserCreditcardDetails.CardDetais.Address.ToString(),
                                       UserCreditcardDetails.CardDetais.Zip.ToString(),
                                       UserCreditcardDetails.CardDetais.State.ToString(),
                                       UserCreditcardDetails.CardDetais.Country.ToString(),
                                       UserCreditcardDetails.CardDetais.Telephone.ToString(),
                                       UserCreditcardDetails.CardDetais.CreditCardId.ToString(),
                                        "CVC",
                                        UserCreditcardDetails.CardDetais.City.ToString()

               
                                        
                                };


                DbType[] enumDbType ={ 
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.String,
                                    DbType.String,
                                    DbType.DateTime,
                                    DbType.String,
                                    DbType.String,
                                    DbType.Int32,
                                    DbType.Int32,
                                    DbType.String,
                                    DbType.Int32,DbType.String,
                                    DbType.String
                                 };

                object[] objValue ={
                                    objUserReg.UserCreditcardDetails.UserId,
                   objUserReg.UserCreditcardDetails.CreditCardType,
                   objUserReg.UserCreditcardDetails.CreditCardNo,
                   objUserReg.UserCreditcardDetails.CardholdersName,
                   objUserReg.UserCreditcardDetails.ExpirationDate,
                   objUserReg.UserCreditcardDetails.Address,
                   objUserReg.UserCreditcardDetails.Zip,
                   objUserReg.UserCreditcardDetails.State,
                   objUserReg.UserCreditcardDetails.Country,
                   objUserReg.UserCreditcardDetails.Telephone,
                   objUserReg.UserCreditcardDetails.CreditCardId,
                   objUserReg.UserCreditcardDetails.CVC,
                       objUserReg.UserCreditcardDetails.City              
                                   };
                base.UpdateRecordMinusIovs("usp_UpdateCreditCardDetails", strParam, enumDbType, objValue);

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        public void Delete(object[] Params)
        {
            UserRegistration objUserReg = (UserRegistration)Params[0];
            try
            {
                //string[] strParam = {
                //                        UserCreditcardDetails.CardDetais.UserId.ToString()
                //                };


                //DbType[] enumDbType ={ 
                //                    DbType.Int32
                //                 };

                object[] objValue ={
                                    objUserReg.UserCreditcardDetails.UserId,                       
                    objUserReg.UserCreditcardDetails.CreditCardId
                                   };
                base.Delete("usp_DeleteCreditCardDetails", objValue);

            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }
        }

        public object InsertDataAndReturnId(object[] Params)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Update Auto renew.
        /// </summary>
        /// <param name="Params"></param>
        public void UpdateAutoRenew(object[] Params)
        {
            TributePackage objUserReg = (TributePackage)Params[0];
            try
            {
                string[] strParam = {
                                       "TributePackageId","IsAutomaticRenew"
                               };


                DbType[] enumDbType ={ 
                                   DbType.Int32,
                   DbType.Boolean
                                };

                object[] objValue ={
                                    objUserReg.TributePackageId, objUserReg.IsAutomaticRenew
                                   };
                base.UpdateRecord("usp_UpdateAutoRenew", strParam, enumDbType, objValue);


            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objUserReg.CustomError = objError;
                }
            }

        }

        /// <summary>
        /// Method to get the payment receipt based on the tribute package id
        /// </summary>
        /// <param name="objValue">Tribute package id</param>
        /// <returns>Details of payment receipt</returns>
        public IList<PaymentReceipt> GetPaymentReceipt(object[] objValue)
        {

            List<PaymentReceipt> objPaymentReceipt = new List<PaymentReceipt>();
            PaymentReceipt objpay = null;
            try
            {
                DataSet ds = GetDataSet("usp_GetPaymentReceipt", objValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int amount = 0;
                        int.TryParse(ds.Tables[0].Rows[i]["AmountPaid"].ToString(), out amount);
                        objPaymentReceipt.Add(new PaymentReceipt(ds.Tables[0].Rows[i]["Tributename"].ToString(),
                                                                 ds.Tables[0].Rows[i]["TypeDescription"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Packagename"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Enddate"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CardholdersName"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Address"].ToString(),
                                                                 ds.Tables[0].Rows[i]["City"].ToString(),
                                                                 ds.Tables[0].Rows[i]["State"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Country"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Zip"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Telephone"].ToString(),
                                                                 DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString()),
                                                                 ds.Tables[0].Rows[i]["CreditCardType"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CreditCardNo"].ToString(),
                                                                 amount));


                    }
                }
                return objPaymentReceipt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Method to get the Transaction details for the payment
        /// </summary>
        /// <param name="objValue">Package Id</param>
        /// <returns>Get the details of transaction.</returns>
        public PaymentReceipt GetTransactionDetails(object[] objValue)
        {

            PaymentReceipt objPaymentReceipt = new PaymentReceipt();
            //PaymentReceipt objpay = null;
            try
            {
                DataSet ds = GetDataSet("usp_GetTransactionDetails", objValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objPaymentReceipt = (new PaymentReceipt(int.Parse(ds.Tables[0].Rows[i]["TransactionId"].ToString()),
                                                                 ds.Tables[0].Rows[i]["Tributename"].ToString(),
                                                                 ds.Tables[0].Rows[i]["TypeDescription"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Packagename"].ToString(),
                                                                 (ds.Tables[0].Rows[i]["Enddate"].ToString().Length != 0 ? Convert.ToDateTime(ds.Tables[0].Rows[i]["Enddate"]).ToString("MMMM dd, yyyy") : "Never"),

                                                                 ds.Tables[0].Rows[i]["CardholdersName"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Address"].ToString(),
                                                                 ds.Tables[0].Rows[i]["City"].ToString(),
                                                                 ds.Tables[0].Rows[i]["State"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Country"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Zip"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Telephone"].ToString(),
                                                                 DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString()),
                                                                 ds.Tables[0].Rows[i]["CreditCardType"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CreditCardNo"].ToString(),
                                                                 Convert.ToDouble(ds.Tables[0].Rows[i]["AmountPaid"].ToString()),
                                                                 ds.Tables[0].Rows[i]["SponsorEmailAddress"].ToString(),
                                                                 Convert.ToBoolean(ds.Tables[0].Rows[i]["IsAutomaticRenew"].ToString())));
                    }
                }
                return objPaymentReceipt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Method to get the Transaction details for the payment of Video tribute upgrade
        public PaymentReceipt GetVideoTrUpgradeTransactionDetails(object[] objValue)
        {

            PaymentReceipt objPaymentReceipt = new PaymentReceipt();
            //PaymentReceipt objpay = null;
            try
            {
                DataSet ds = GetDataSet("usp_GetTransactionDetails", objValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objPaymentReceipt = (new PaymentReceipt(int.Parse(ds.Tables[0].Rows[i]["TransactionId"].ToString()),
                                                                 ds.Tables[0].Rows[i]["Tributename"].ToString(),
                                                                 ds.Tables[0].Rows[i]["TypeDescription"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Packagename"].ToString(),
                                                                 (ds.Tables[0].Rows[i]["Enddate"].ToString().Length != 0 ? Convert.ToDateTime(ds.Tables[0].Rows[i]["Enddate"]).ToString("MMMM dd, yyyy") : "Never"),

                                                                 ds.Tables[0].Rows[i]["CardholdersName"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Address"].ToString(),
                                                                 ds.Tables[0].Rows[i]["City"].ToString(),
                                                                 ds.Tables[0].Rows[i]["State"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Country"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Zip"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Telephone"].ToString(),
                                                                 DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString()),
                                                                 ds.Tables[0].Rows[i]["CreditCardType"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CreditCardNo"].ToString(),
                                                                 Convert.ToDouble(ds.Tables[0].Rows[i]["AmountPaid"].ToString()),
                                                                 ds.Tables[0].Rows[i]["SponsorEmailAddress"].ToString(),
                                                                 Convert.ToBoolean(ds.Tables[0].Rows[i]["IsAutomaticRenew"].ToString())));
                    }
                }
                return objPaymentReceipt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public PaymentReceipt GetVideoTributeTransactionDetails(object[] objValue)
        {

            PaymentReceipt objPaymentReceipt = new PaymentReceipt();
            //PaymentReceipt objpay = null;
            try
            {
                DataSet ds = GetDataSet("usp_GetVideoTributeTransactionDetails", objValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objPaymentReceipt = (new PaymentReceipt(int.Parse(ds.Tables[0].Rows[i]["TransactionId"].ToString()),
                                                                 ds.Tables[0].Rows[i]["Tributename"].ToString(),
                                                                 ds.Tables[0].Rows[i]["TypeDescription"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Packagename"].ToString(),
                                                                 (ds.Tables[0].Rows[i]["Enddate"].ToString().Length != 0 ? Convert.ToDateTime(ds.Tables[0].Rows[i]["Enddate"]).ToString("MMMM dd, yyyy") : "Never"),

                                                                 ds.Tables[0].Rows[i]["CardholdersName"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Address"].ToString(),
                                                                 ds.Tables[0].Rows[i]["City"].ToString(),
                                                                 ds.Tables[0].Rows[i]["State"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Country"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Zip"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Telephone"].ToString(),
                                                                 DateTime.Parse(ds.Tables[0].Rows[i]["StartDate"].ToString()),
                                                                 ds.Tables[0].Rows[i]["CreditCardType"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CreditCardNo"].ToString(),
                                                                 Convert.ToInt32(ds.Tables[0].Rows[i]["AmountPaid"]),
                                                                 ds.Tables[0].Rows[i]["SponsorEmailAddress"].ToString(),
                                                                 Convert.ToBoolean(ds.Tables[0].Rows[i]["IsAutomaticRenew"].ToString())));
                    }
                }
                return objPaymentReceipt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PaymentReceipt GetCreditPtTransactionDetails(object[] objValue)
        {

            PaymentReceipt objPaymentReceipt = new PaymentReceipt();
            //PaymentReceipt objpay = null;
            try
            {
                DataSet ds = GetDataSet("usp_GetCreditPtPurchaseDetails", objValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        objPaymentReceipt = (new PaymentReceipt(int.Parse(ds.Tables[0].Rows[i]["TransactionId"].ToString()),
                                                                 ds.Tables[0].Rows[i]["CardholdersName"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Address"].ToString(),
                                                                 ds.Tables[0].Rows[i]["City"].ToString(),
                                                                 ds.Tables[0].Rows[i]["State"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Country"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Zip"].ToString(),
                                                                 ds.Tables[0].Rows[i]["Telephone"].ToString(),
                                                                 DateTime.Parse(ds.Tables[0].Rows[i]["CreatedDate"].ToString()),
                                                                 ds.Tables[0].Rows[i]["CreditCardType"].ToString(),
                                                                 ds.Tables[0].Rows[i]["CreditCardNo"].ToString(),
                                                                 int.Parse(ds.Tables[0].Rows[i]["AmountPaid"].ToString()),
                                                                 ds.Tables[0].Rows[i]["SponsorEmailAddress"].ToString()));
                    }
                }
                return objPaymentReceipt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCreditPointOfVideoTributeOwner(object[] objValue)
        {
            UserRegistration objUserReg = (UserRegistration)objValue[0];
            string[] strParam = { 
                                    "videoTributeUserId"
                                    };
            DbType[] enumDbType ={ 
                                   DbType.Int32                   
                                };

            object[] objParam = { objUserReg.Users.UserId };

            UpdateRecord("usp_UpdateVideoTributeOwnerCredit", strParam, enumDbType, objParam);

        }
 		// Get Tribute Id of Video Tribute with whom the Memorial tribute is linked
        public int GetLinkedVideoTributeId(object[] objValue)
        {
            TributePackage objUserReg = (TributePackage)objValue[0];
            try
            {
                object[] objParam = { objUserReg.UserTributeId, objUserReg.UserId };
                DataSet _objDataSet = GetDataSet("usp_GetLinkedVideoTributeId", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    return int.Parse(_objDataSet.Tables[0].Rows[0]["videotributeid"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                throw sqlEx;
            }
        }

        #endregion

        public int GetTributePackageInfo(int tributeId)
        {
            //TributePackage objUserReg = (TributePackage)objValue[0];
            int packageId = 0;
            try
            {
                object[] objParam = { tributeId };
                DataSet _objDataSet = GetDataSet("usp_GetTriputePackageInfo", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                   packageId = int.Parse(_objDataSet.Tables[0].Rows[0]["PackageId"].ToString());
                }                
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
            }
            return packageId;
        }

        public int GetPackIdonPhotoId(int PhotoId)
        {
            int packageId = 0;
            try
            {
                object[] objParam = { PhotoId };
                DataSet _objDataSet = GetDataSet("usp_GetPackIdonPhotoId", objParam);
                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    packageId = int.Parse(_objDataSet.Tables[0].Rows[0]["PackageId"].ToString());
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
            }
            return packageId;
        }
    }
}
