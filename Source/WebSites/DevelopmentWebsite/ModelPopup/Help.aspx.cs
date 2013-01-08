///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.Help.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the help content to a user
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ModelPopup_Help : System.Web.UI.Page
{
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Create the Menu
            CreateMenuTree();
        }
    }

    /// <summary>
    /// Create Menu Tree
    /// </summary>
    private void CreateMenuTree()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + @"Common\XML\HelpMenuConfiguration.xml";
        DataSet ds = new DataSet();
        ds.ReadXml(path);

        //Check if any Menu exists in XML file
        if (ds.Tables.Count != 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //Prepare the Parent Node
                TreeNode parentNode = new TreeNode(Convert.ToString(ds.Tables[0].Rows[i]["Text"]));
                parentNode.Value = Convert.ToString(ds.Tables[0].Rows[i]["Value"]);
                parentNode.Expanded = false;

                //Check whether any Sub Menu exists or not
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count != 0)
                {
                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                    {
                        //If Sub Menu exists, then add under its parent Menu
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[j][2])) &&
                            Convert.ToInt32(ds.Tables[1].Rows[j][2]) == Convert.ToInt32(ds.Tables[0].Rows[i][0]))
                        {
                            //Prepare the Child Node
                            TreeNode childNode = new TreeNode(Convert.ToString(ds.Tables[1].Rows[j]["Text"]));
                            childNode.Value = Convert.ToString(ds.Tables[1].Rows[j]["Value"]);
                            childNode.Expanded = false;

                            //Add the Child Node under Parent Node
                            parentNode.ChildNodes.Add(childNode);
                        }
                    }
                }

                //Add Parent Node to the Tree
                menuTree.Nodes.Add(parentNode);

                /*string parentNodeId = Convert.ToString(ds.Tables[0].Rows[i][Convert.ToInt32(ds.Tables[0].Columns.Count - 1)]);
                if (string.IsNullOrEmpty(parentNodeId))
                    menuTree.Nodes.Add(node);
                else
                    menuTree.Nodes[Convert.ToInt32(parentNodeId)].ChildNodes.Add(node);*/
            }
        }

        if (menuTree.Nodes.Count > 0)
        {
            if (menuTree.Nodes[0].ChildNodes.Count > 0)
                lblHeader.Text = menuTree.Nodes[0].ChildNodes[0].Text;
            else
                lblHeader.Text = menuTree.Nodes[0].Text;
            frameDiv.InnerHtml = @"<iframe src='..\Help\" + menuTree.Nodes[0].Value + "' width='480px'  height='400px' scrollbar='hidden' frameborder='no' ></iframe>";
        }
    }

    protected void menuTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (menuTree.SelectedNode.ChildNodes.Count > 0)
            lblHeader.Text = menuTree.SelectedNode.ChildNodes[0].Text;
        else
            lblHeader.Text = menuTree.SelectedNode.Text;

        //Open the page on the frame
        frameDiv.InnerHtml = @"<iframe src='..\Help\" + menuTree.SelectedNode.Value + "' width='480px' height='400px' scrollbar='hidden' frameborder='no' ></iframe>";

        //Collapse the Full Tree
        menuTree.CollapseAll();
        //Expand the Selected Node
        menuTree.SelectedNode.Expand();

        //If the selected node is having any Parent, then Expand the Parent also
        if (menuTree.SelectedNode.Parent != null)
            menuTree.SelectedNode.Parent.Expand();
    }
}
