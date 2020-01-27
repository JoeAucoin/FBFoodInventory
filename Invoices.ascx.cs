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
using GIBS.FBFoodInventory.Components;
using DotNetNuke.Web.Client;
using DotNetNuke.Common;
using System.Drawing;
using DotNetNuke.Common.Lists;
using System.Data;
using DotNetNuke.Framework.JavaScriptLibraries;
using System.Linq;

namespace GIBS.Modules.FBFoodInventory
{
    public partial class Invoices : PortalModuleBase, IActionable
    {

        private GridViewHelper helper;
        private List<int> mQuantities = new List<int>();
        public DataTable dt;
        public DataTable dtSupplier;
        public DataTable dtProductCategories;

        protected void Page_Load(object sender, EventArgs e)
        {

            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InputMasks", (this.TemplateSourceDirectory + "/JavaScript/jquery.maskedinput-1.3.js"));
            DotNetNuke.Web.Client.ClientResourceManagement.ClientResourceManager.RegisterStyleSheet(this.Page, this.TemplateSourceDirectory + "/Style.css");
         //   Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CustomCSS", (this.TemplateSourceDirectory + "/Style.css"));
         //    Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "JQueryCSS", ("https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/smoothness/jquery-ui.css"));


            if (!IsPostBack)
            {
                FillDropDowns();
                FillInvoiceGrid();
                FillProductDropDown();
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            ModuleConfiguration.ModuleTitle = Localization.GetString("ControlTitle", this.LocalResourceFile);
        }


        public void FillInvoiceGrid()
        {

            try
            {
                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();
                //items = controller.FBSuppliers_List(this.ModuleId);
                items = controller.FBInvoice_List(this.ModuleId);

                gvInvoices.DataSource = items;
                gvInvoices.DataBind();

                lblTotalRecordCount.Text = items.Count.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                FillInvoiceGrid();
                gvInvoices.PageIndex = e.NewPageIndex;
                gvInvoices.DataBind();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }


        }


        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            try
            {
                if (e.CommandName == "Delete")
                {
                    // get the ID of the clicked row

                    int ID = Convert.ToInt32(e.CommandArgument);
                    // Delete the record 

                    FBFoodInventoryController controller = new FBFoodInventoryController();
                    controller.FBInvoice_Delete(ID);
                    //  DeleteRecordByID(ID);
                    // Implement this on your own :) 

                    txtInvoiceID.Value = "0";

                    FillInvoiceGrid();

                }


                if (e.CommandName == "Edit")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);

                    //  int invoiceID = (int)gvInvoices.DataKeys[e.NewEditIndex].Value;

                    FBFoodInventoryController controller = new FBFoodInventoryController();
                    FBFoodInventoryInfo item = controller.FBInvoice_GetByID(this.ModuleId, ID);

                    if (item != null)
                    {
                        panelAddLineItems.Visible = true;
                        FillSupplierDropDown();
                        FillReportTypeDropDown();
                        btnSave.Text = "Update Record";
                        panelGrid.Visible = false;
                        panelEdit.Visible = true;
                        txtInvoiceNumber.Text = item.InvoiceNumber.ToString();
                        txtInvoiceDate.Text = item.InvoiceDate.ToShortDateString();
                        ddlOrganization.SelectedValue = item.Organization.ToString();
                        ListItem lstitem = ddlSupplier.Items.FindByValue(item.SupplierID.ToString());
                        if (lstitem != null)
                        {
                            ddlSupplier.SelectedValue = item.SupplierID.ToString();
                        }
                        else
                        {
                            AddInActiveSupplier(item.SupplierID);
                        }


                        txtInvoiceID.Value = item.InvoiceID.ToString();
                        //GroupIt();
                        //GetLineItems(item.InvoiceID);
                        GroupIt();
                        GetLineItems(Int32.Parse(txtInvoiceID.Value.ToString()));


                    }
                    else
                    {
                        txtInvoiceID.Value = "0";
                    }



                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }



        }


        public void AddInActiveSupplier(int SupplierID)
        {

            try
            {

                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = controller.FBSuppliers_GetByID(this.ModuleId, SupplierID);

                if (item != null)
                {
                    ListItem lst = new ListItem(item.SupplierName.ToString(), SupplierID.ToString());

                    ddlSupplier.Items.Add(lst);
                    ddlSupplier.SelectedValue = SupplierID.ToString();
                }
                else
                {
                    //  txtSupplierID.Value = "";
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void gvInvoices_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // No requirement to implement code here
            e.Cancel = true;
        }
        protected void gvInvoices_OnRowDeleting(object sender, GridViewEditEventArgs e)
        {

            e.Cancel = true;
        }


        protected void gvInvoices_RowEditing(object sender, GridViewEditEventArgs e)
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

        public void FillSupplierDropDown()
        {

            try
            {
                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();
                items = controller.FBSuppliers_List(this.ModuleId);

                dtSupplier = Components.GridViewTools.ToDataTable(items);

                DataView dv = dtSupplier.DefaultView;

                dv.RowFilter = "IsActive = 1";





                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "SupplierID";
                ddlSupplier.DataSource = dv;
                ddlSupplier.DataBind();

                ddlSupplier.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void FillReportTypeDropDown()
        {

            try
            {
                var ReportType = new ListController().GetListEntryInfoItems("InventoryReportingType", "", this.PortalId);
                if(ReportType.ToList().Count == 0)
                {
                    CreateList();
                    ReportType = new ListController().GetListEntryInfoItems("InventoryReportingType", "", this.PortalId);
                }

                ddlReportType.DataTextField = "Text";
                ddlReportType.DataValueField = "Value";
                ddlReportType.DataSource = ReportType;
                ddlReportType.DataBind();

                ddlReportType.Items.Insert(0, new ListItem("--Select--", ""));


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void CreateList()
        {

            try
            {
                //create a placeholder entry for Inventory Reporting Type
                const string listName = "InventoryReportingType";
                var listController = new ListController();
                var entry = new ListEntryInfo();
                {
                    entry.DefinitionID = -1;
                    entry.ParentID = 0;
                    entry.Level = 0;
                    entry.PortalID = this.PortalId;
                    entry.ListName = listName;
                    entry.Value = "USDA";
                    entry.Text = "USDA";
                    entry.SystemList = false;
                    entry.SortOrder = 1;
                }

                listController.AddListEntry(entry);


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void CreateOrganizationList()
        {

            try
            {
                //create a placeholder entry - uses the most common 5 character password (seed list is 6 characters and above)
                const string listName = "ClientOrganizations";
                var listController = new ListController();
                var entry = new ListEntryInfo();
                {
                    entry.DefinitionID = -1;
                    entry.ParentID = 0;
                    entry.Level = 0;
                    entry.PortalID = this.PortalId;
                    entry.ListName = listName;
                    entry.Value = "Main Location";
                    entry.Text = "Main Location";
                    entry.SystemList = false;
                    entry.SortOrder = 1;
                }

                listController.AddListEntry(entry);


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void FillProductDropDown()
        {

            try
            {
                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();

                //items = controller.FBSuppliers_List(this.ModuleId);
                items = controller.FBProducts_List(this.ModuleId);


                ddlProducts.DataTextField = "ProductName";
                ddlProducts.DataValueField = "ProductID";

                dt = Components.GridViewTools.ToDataTable(items);
                DataView dv = dt.DefaultView;
                dv.RowFilter = "IsActive = 1";

                if (ddlFilterCategory.SelectedValue.ToString() != "0")
                {
                    dv.RowFilter += " AND ProductCategoryID = " + ddlFilterCategory.SelectedValue.ToString() + "";
                }

                ddlProducts.DataSource = dv;
                ddlProducts.DataBind();

                ddlProducts.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void FillDropDowns()
        {

            try
            {
                // CreatePrefixSuffixDropdowns();

                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();

                items = controller.FBProductCategory_List(this.ModuleId);

                dtProductCategories = Components.GridViewTools.ToDataTable(items);

                DataView dv = dtProductCategories.DefaultView;

                dv.RowFilter = "IsActive = 1";


                ddlFilterCategory.DataTextField = "ProductCategory";
                ddlFilterCategory.DataValueField = "ProductCategoryID";
                ddlFilterCategory.DataSource = dv;
                ddlFilterCategory.DataBind();
                ddlFilterCategory.Items.Insert(0, new ListItem("- Select Category -", "0"));


                var Organizations = new ListController().GetListEntryInfoItems("ClientOrganizations", "", this.PortalId);

                if (Organizations.ToList().Count == 0)
                {
                    CreateOrganizationList();
                    Organizations = new ListController().GetListEntryInfoItems("ClientOrganizations", "", this.PortalId);
                }

                ddlOrganization.DataTextField = "Text";
                ddlOrganization.DataValueField = "Text";
                ddlOrganization.DataSource = Organizations;
                ddlOrganization.DataBind();
              //  ddlOrganization.Items.Insert(0, new ListItem("--Select--", ""));

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
                FillProductDropDown();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }


        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                // get the ID of the clicked row

                int ID = Convert.ToInt32(e.CommandArgument);
                // Delete the record 

                FBFoodInventoryController controller = new FBFoodInventoryController();
                controller.FBLineItems_Delete(ID);
                //  DeleteRecordByID(ID);
                // Implement this on your own :) 

            }


            if (e.CommandName == "Edit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = controller.FBLineItems_GetByID(ID);

                if (item != null)
                {
                    txtLineItemID_Edit.Value = item.LineItemID.ToString();
                    txtCasesAddNewEdit.Text = item.Cases.ToString();
                    txtCountPerCaseAddNewEdit.Text = item.CountPerCase.ToString();
                    txtWeightPerCaseAddNewEdit.Text = item.WeightPerCase.ToString();
                    txtPricePerCaseAddNewEdit.Text = item.PricePerCase.ToString();
                    ddlProducts.SelectedValue = item.ProductID.ToString();
                    if (item.ReportType != null)
                    {
                        ddlReportType.SelectedValue = item.ReportType.ToString();
                    }


                    btnAdd.Text = "Update Item";

                }
                else
                {
                    txtLineItemID_Edit.Value = "0";
                }



            }


            GroupIt();
            GetLineItems(Int32.Parse(txtInvoiceID.Value.ToString()));


        }

        //GridView1_RowCommand



        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // No requirement to implement code here
            e.Cancel = true;
        }
        protected void GridViewLineItems_OnRowDeleting(object sender, GridViewEditEventArgs e)
        {

            e.Cancel = true;
        }



        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
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




        public void GetLineItems(int InvoiceID)
        {

            try
            {


                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();

                items = controller.FBInvoice_GetInvoiceLineItems(InvoiceID);

                GridViewLineItems.DataSource = items;
                GridViewLineItems.DataBind();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

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

                actions.Add(GetNextActionID(), Localization.GetString("Products", this.LocalResourceFile),
ModuleActionType.AddContent, "", "", EditUrl("Products"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
true, false);

                actions.Add(GetNextActionID(), Localization.GetString("ProductCategories", this.LocalResourceFile),
ModuleActionType.AddContent, "", "", EditUrl("ProductCategories"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
true, false);

                return actions;
            }
        }

        #endregion

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                FillSupplierDropDown();
                FillReportTypeDropDown();
                panelGrid.Visible = false;
                panelEdit.Visible = true;
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

                item.InvoiceNumber = txtInvoiceNumber.Text.ToString();

                item.SupplierID = Int32.Parse(ddlSupplier.SelectedValue.ToString());

                item.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text.ToString());

                item.Organization = ddlOrganization.SelectedValue.ToString();

                item.LastModifiedByUserID = this.UserId;



                if (txtInvoiceID.Value.Length > 0)
                {
                    item.InvoiceID = Int32.Parse(txtInvoiceID.Value.ToString());
                    controller.FBInvoice_Update(item);

                    lblFormMessage.Text = Localization.GetString("InvoiceUpdateSuccessful", this.LocalResourceFile);
                    lblFormMessage.Visible = true;

                }
                else
                {
                    item.CreatedByUserID = this.UserId;


                    int MyNewID = Null.NullInteger;
                    MyNewID = controller.FBInvoice_Insert(item);
                    txtInvoiceID.Value = Convert.ToString(MyNewID);

                    lblFormMessage.Text = Localization.GetString("InvoiceInsertSuccessful", this.LocalResourceFile);
                    lblFormMessage.Visible = true;

                }

                panelAddLineItems.Visible = true;

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
                Response.Redirect(EditUrl("Invoices"));
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void AddNewLineItem(object sender, EventArgs e)
        {
            try
            {
                lblFormMessage.Visible = false;

                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = new FBFoodInventoryInfo();

                item.ModuleId = this.ModuleId;
                item.PortalId = this.PortalId;

                item.InvoiceID = Int32.Parse(txtInvoiceID.Value.ToString());

                item.ProductID = Int32.Parse(ddlProducts.SelectedValue.ToString());

                item.Cases = Int32.Parse(txtCasesAddNewEdit.Text.ToString());
                item.CountPerCase = Int32.Parse(txtCountPerCaseAddNewEdit.Text.ToString());
                item.WeightPerCase = double.Parse(txtWeightPerCaseAddNewEdit.Text.ToString());
                item.PricePerCase = Convert.ToDouble(txtPricePerCaseAddNewEdit.Text.ToString());
                item.ReportType = ddlReportType.SelectedValue.ToString();
                item.LastModifiedByUserID = this.UserId;

                if (txtLineItemID_Edit.Value.Length > 0)
                {
                    item.LineItemID = Int32.Parse(txtLineItemID_Edit.Value.ToString());
                    controller.FBLineItems_Update(item);
                }
                else
                {
                    controller.FBLineItems_Insert(item);
                }
                GroupIt();
                GetLineItems(Int32.Parse(txtInvoiceID.Value.ToString()));

                btnAdd.Text = "Add New Item";
                txtCasesAddNewEdit.Text = "";
                txtCountPerCaseAddNewEdit.Text = "";
                txtPricePerCaseAddNewEdit.Text = "";
                ddlProducts.SelectedValue = "0";
                txtWeightPerCaseAddNewEdit.Text = "";
                ddlReportType.SelectedValue = "";

                txtLineItemID_Edit.Value = "";
                ddlFilterCategory.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GroupIt()
        {

            try
            {

                helper = new GridViewHelper(this.GridViewLineItems);

                helper.RegisterSummary("Cases", SummaryOperation.Sum);
                helper.RegisterSummary("TotalWeightPerCase", SummaryOperation.Sum);
                helper.RegisterSummary("TotalCostExtended", SummaryOperation.Sum);

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        private void helper_ManualSummary(GridViewRow row)
        {
            GridViewRow newRow = helper.InsertGridRow(row);
            newRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            newRow.Cells[0].Text = String.Format("Total: {0} items, ", helper.GeneralSummaries["Cases"].Value, helper.GeneralSummaries["TotalCostExtended"].Value);
        }

        private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = "Média";
        }

        private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == "ClientCity")
            {
                row.BackColor = Color.LightGray;
                row.Cells[0].Text = "&nbsp;&nbsp;<b>" + row.Cells[0].Text + "</b>";

            }
            else if (groupName == "ShipName")
            {
                row.BackColor = Color.FromArgb(236, 236, 236);
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + row.Cells[0].Text;
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


        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int productID = Int32.Parse(ddlProducts.SelectedValue.ToString());

                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = controller.FBProducts_GetByID(this.ModuleId, productID);

                if (item != null)
                {

                    txtPricePerCaseAddNewEdit.Text = item.CasePrice.ToString();
                    txtCountPerCaseAddNewEdit.Text = item.CaseCount.ToString();
                    txtWeightPerCaseAddNewEdit.Text = item.CaseWeight.ToString();

                }
                txtCasesAddNewEdit.Focus();


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





    }
}