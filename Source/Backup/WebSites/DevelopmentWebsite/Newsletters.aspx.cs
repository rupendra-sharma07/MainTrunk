using System;
using System.Linq;
using PerceptiveMCAPI;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;
//using OrbitOne.MailChimp;

public partial class Newsletters : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //  string contacts = "lhari.k@optimusinfo.com";
            //var chimp = new or();
            //  var result = chimp.Update(contacts);
            //  foreach (var guid in result)
            //  {
            //      string x = guid.ToString();
            //  }
            //Console.ReadLine();
        }
        catch (Exception ex)
        {
            //Console.Out.WriteLine(e);
            //Console.ReadLine();
        }
    }
    // A real simple request (using all default settings)
    public void lists_method()
    {
        // input parameters, using default apikey
        listsInput input = new listsInput(MCAPISettings.default_apikey);
        // execution
        lists cmd = new lists(input);
        listsOutput output = cmd.Execute();
        // format output (Assuming a User control named show_lists)

        //show_lists1.Display(output);
    }
    //public bool SubscribeToList(string EmailAddress, string FirstName,string LastName, 
    //    string Subject, string Body, string EmailFrom, string DisplayName)
    //{
    //    lblError.Text=string.Empty;
    //    listSubscribeInput input = new listSubscribeInput();
    //    input.api_AccessType = PerceptiveMCAPI.EnumValues.AccessType.Serial;
    //    input.api_CustomErrorMessages = true;
    //    input.parms.email_address = txtEmail.Text.ToString();
    //    input.parms.send_welcome = true;
    //    input.parms.update_existing = false;
    //    input.parms.replace_interests = true;
    //    input.parms.double_optin = false;
    //    //input.parms.merge_vars.Add("FNAME", FirstName);
    //    //input.parms.merge_vars.Add("LNAME", LastName);
    //    input.api_MethodType = PerceptiveMCAPI.EnumValues.MethodType.POST;
    //    input.api_Validate = true;
    //    input.api_OutputType = PerceptiveMCAPI.EnumValues.OutputType.XML;
    //    input.parms.apikey = System.Configuration.ConfigurationSettings.AppSettings["MailChimpApiKey"].ToString();
    //    //input.parms.id = List();
    //    listSubscribe Subscribe = new listSubscribe();
    //    listSubscribeOutput output = Subscribe.Execute(input);
    //    if (output.api_ErrorMessages.Count > 0)
    //    {
    //        string ErrorCode = output.api_ErrorMessages.FirstOrDefault().code;
    //        string Error = "Error occured. " + output.api_ErrorMessages.FirstOrDefault().error;
    //        lblError.Text = ErrorCode + "</br>" + error;
    //        return false;
    //    }
    //    else
    //    {
    //        lblError.Text = output.result;
    //    }
    //    return output.result;
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;
        listSubscribeInput input = new listSubscribeInput();
        input.api_AccessType = PerceptiveMCAPI.EnumValues.AccessType.Serial;
        input.api_CustomErrorMessages = true;
        input.parms.email_address = txtEmail.Text.ToString();
        input.parms.send_welcome = true;
        input.parms.update_existing = false;
        input.parms.replace_interests = true;
        input.parms.double_optin = false;
        input.parms.merge_vars.Add("EMAIL", txtEmail.Text.ToString());
        //input.parms.merge_vars.Add("FNAME", FirstName);
        //input.parms.merge_vars.Add("LNAME", LastName);
        input.api_MethodType = PerceptiveMCAPI.EnumValues.MethodType.POST;
        input.api_Validate = true;
        input.api_OutputType = PerceptiveMCAPI.EnumValues.OutputType.XML;
        input.parms.apikey = System.Configuration.ConfigurationSettings.AppSettings["MailChimpApiKey"].ToString();
        input.parms.id = "98b361abcb";
        listSubscribe Subscribe = new listSubscribe();
        listSubscribeOutput output = Subscribe.Execute(input);
        if (output.api_ErrorMessages.Count > 0)
        {
            string ErrorCode = output.api_ErrorMessages.FirstOrDefault().code;
            string Error = "Error occured. " + output.api_ErrorMessages.FirstOrDefault().error;
            lblError.Text = ErrorCode + "</br>" + Error;
            //return false;
        }
        else
        {
            lblError.Text = output.result.ToString();
        }
        txtEmail.Text = string.Empty;
        //return output.result;
    }
}
