using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using DotNetNuke.Common;
using System.Drawing;
using Telerik;
using Telerik.Web.UI;
using System.Data;
using System.ComponentModel;
using DotNetNuke.Framework.JavaScriptLibraries;
using GIBS.FBFoodInventory.Components;


namespace GIBS.Modules.FBFoodInventory
{
    public partial class Products : PortalModuleBase, IActionable
    {

        
        // To show custom operations...
        private List<int> mQuantities = new List<int>();
        public DataTable dt;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            DotNetNuke.Framework.jQuery.RequestRegistration();
            DotNetNuke.Framework.jQuery.RequestUIRegistration();

            //JavaScript.RequestRegistration(CommonJs.jQuery);
            //JavaScript.RequestRegistration(CommonJs.jQueryUI);
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InputMasks", (this.TemplateSourceDirectory + "/JavaScript/jquery.maskedinput-1.3.js"));
            
            if (!IsPostBack)
            {
           //     GroupIt();
                
                
                FillProductCategoryDropDown();

                FillProductsGrid();
             
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ModuleConfiguration.ModuleTitle = Localization.GetString("ControlTitle", this.LocalResourceFile);
        }


        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();

                actions.Add(GetNextActionID(), Localization.GetString("Suppliers", this.LocalResourceFile),
                ModuleActionType.AddContent, "", "", EditUrl("Suppliers"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                true, false);

                actions.Add(GetNextActionID(), Localization.GetString("ProductCategories", this.LocalResourceFile),
                ModuleActionType.AddContent, "", "", EditUrl("ProductCategories"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                true, false);

                actions.Add(GetNextActionID(), Localization.GetString("Invoices", this.LocalResourceFile),
                ModuleActionType.AddContent, "", "", EditUrl("Invoices"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                true, false);

                return actions;
            }
        }

        #endregion







        public void FillProductsGrid()
        {

            try
            {

                List<FBFoodInventoryInfo> items;

                FBFoodInventoryController controller = new FBFoodInventoryController();

                items = controller.FBProducts_List(this.ModuleId);

                dt = Components.GridViewTools.ToDataTable(items);

                DataView dv = dt.DefaultView;

                dv.RowFilter = "IsActive = 1";

                if (ddlFilterCategory.SelectedValue.ToString() != "0")
                {
                    dv.RowFilter += " AND ProductCategoryID = " + ddlFilterCategory.SelectedValue;
                }
                if (cbxShowInActive.Checked == true)
                {
                    dv.RowFilter += " or IsActive = 0";
                }

                
                gvProducts.DataSource = dv;
                gvProducts.DataBind();

                lblTotalRecordCount.Text = dv.Count.ToString();

                

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }





        private void SaveQuantity(string column, string group, object value)
        {
            mQuantities.Add(Convert.ToInt32(value));
        }

        private object GetMinQuantity(string column, string group)
        {
            int[] qArray = new int[mQuantities.Count];
            mQuantities.CopyTo(qArray);
            Array.Sort(qArray);
            return qArray[0];
        }

        private void helper_Bug(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == null) return;

            row.BackColor = Color.LightCyan;
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = values[0] + " TOTALS&nbsp;";
        }


        public void FillProductCategoryDropDown()
        {

            try
            {
                // CreatePrefixSuffixDropdowns();

                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();

                items = controller.FBProductCategory_List(this.ModuleId);


                ddlProductCategory.DataTextField = "ProductCategory";
                ddlProductCategory.DataValueField = "ProductCategoryID";
                ddlProductCategory.DataSource = items;
                ddlProductCategory.DataBind();
                ddlProductCategory.Items.Insert(0, new ListItem("-- Select --", "0"));


                ddlFilterCategory.DataTextField = "ProductCategory";
                ddlFilterCategory.DataValueField = "ProductCategoryID";
                ddlFilterCategory.DataSource = items;
                ddlFilterCategory.DataBind();
                ddlFilterCategory.Items.Insert(0, new ListItem("Filter Category", "0"));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //GridViewEditEventArgs
        protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillProductsGrid();
            gvProducts.PageIndex = e.NewPageIndex;
            gvProducts.DataBind();
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            try
            {
                if (e.CommandName == "Delete")
                {
                    // get the ID of the clicked row
                    int ID = Convert.ToInt32(e.CommandArgument);

                    FBFoodInventoryController controller = new FBFoodInventoryController();
                    // Delete the record 
                    controller.FBProducts_Delete(ID);

                    FillProductsGrid();

                }


                if (e.CommandName == "Edit")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);


                    FBFoodInventoryController controller = new FBFoodInventoryController();
                    FBFoodInventoryInfo item = controller.FBProducts_GetByID(this.ModuleId, ID);

                    if (item != null)
                    {
                        panelGrid.Visible = false;
                        panelEdit.Visible = true;
                        txtProductName.Text = item.ProductName.ToString();
                        txtCasePrice.Text = string.Format("{0:0.00}", item.CasePrice.ToString());
                        //txtCasePrice.Text = item.CasePrice.ToString();
                        txtCaseCount.Text = item.CaseCount.ToString();
                        ddlProductCategory.SelectedValue = item.ProductCategoryID.ToString();
                        txtCaseWeight.Text = item.CaseWeight.ToString();
                        rblIsActive.SelectedValue = item.IsActive.ToString();
                        txtProductID.Value = item.ProductID.ToString();
                    }
                    else
                    {
                        txtProductID.Value = "";
                    }


                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }



        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                e.Cancel = true;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                e.Cancel = true;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = new FBFoodInventoryInfo();


                item.ModuleId = this.ModuleId;
                item.PortalId = this.PortalId;

                item.ProductName = txtProductName.Text.ToString();

                item.CaseWeight = double.Parse(txtCaseWeight.Text.ToString());
                item.CasePrice = double.Parse(txtCasePrice.Text.ToString());
                item.CaseCount = Int32.Parse(txtCaseCount.Text.ToString());
                item.ProductCategoryID = Int32.Parse(ddlProductCategory.SelectedValue.ToString());

                item.LastModifiedByUserID = this.UserId;
                item.IsActive = bool.Parse(rblIsActive.SelectedValue.ToString());


                if (txtProductID.Value.Length > 0)
                {
                    item.ProductID = Int32.Parse(txtProductID.Value.ToString());
                    controller.FBProducts_Update(item);
                    


                }
                else
                {
                    item.CreatedByUserID = this.UserId;
                    controller.FBProducts_Insert(item);

                }

                
                //Response.Redirect(EditUrl("Products"));
                ResetForm();
                FillProductsGrid();
                panelGrid.Visible = true;
                panelEdit.Visible = false;


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void ResetForm()
        {

            try
            {
                txtProductID.Value = "";
                txtProductName.Text = "";
                txtCaseWeight.Text = "0";
                txtCasePrice.Text = "0";
                txtCaseCount.Text = "0";
                ddlProductCategory.SelectedIndex = 0;
                rblIsActive.SelectedValue = "True";



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect(EditUrl("Products"));
                ResetForm();
                panelGrid.Visible = true;
                panelEdit.Visible = false;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                panelGrid.Visible = false;
                panelEdit.Visible = true;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
            
        }


        protected void btnReturnToFrontDesk_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void ddlFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                gvProducts.PageIndex = 0;
                FillProductsGrid();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
                 

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(txtProductID.Value.ToString());

                FBFoodInventoryController controller = new FBFoodInventoryController();
                // Delete the record 
                controller.FBProducts_Delete(ID);

                txtProductID.Value = "";

                FillProductsGrid();
                panelGrid.Visible = true;
                panelEdit.Visible = false;


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void cbxShowInActive_CheckedChanged(object sender, EventArgs e)
        {
            FillProductsGrid();
        }


    }
}