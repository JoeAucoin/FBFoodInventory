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
using DotNetNuke.Common;
using DotNetNuke.Common.Lists;
using System.Data;

namespace GIBS.Modules.FBFoodInventory
{
    public partial class Suppliers :  PortalModuleBase, IActionable
    {

        public DataTable dt;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            DotNetNuke.Framework.jQuery.RequestRegistration();
            DotNetNuke.Framework.jQuery.RequestUIRegistration();
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InputMasks", (this.TemplateSourceDirectory + "/JavaScript/jquery.maskedinput-1.3.js"));

            if (!IsPostBack)
            {
                    FillSuppliersGrid();
                    GetStates();        
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ModuleConfiguration.ModuleTitle = Localization.GetString("ControlTitle_suppliers", this.LocalResourceFile);
        }


        public void FillSuppliersGrid()
        {

            try
            {

                //DonationUserId = Int32.Parse(Request.QueryString["UserId"]);

                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();
                items = controller.FBSuppliers_List(this.ModuleId);

                dt = Components.GridViewTools.ToDataTable(items);

                DataView dv = dt.DefaultView;

                dv.RowFilter = "IsActive = 1";


                if (cbxShowInActive.Checked == true)
                {
                    dv.RowFilter += " or IsActive = 0";
                }


                gvSuppliers.DataSource = dv;
                gvSuppliers.DataBind();
          

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void gvSuppliers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

             //   cmdDeleteDonation.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteDonationItemMessage.Confirm", this.LocalResourceFile) + "');");

                panelEdit.Visible = true;
                panelGrid.Visible = false;
                
                int supplierID = (int)gvSuppliers.DataKeys[e.NewEditIndex].Value;

                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = controller.FBSuppliers_GetByID(this.ModuleId, supplierID);

                if (item != null)
                {
                    txtSupplierName.Text = item.SupplierName.ToString();
                 //   cbxGBFB.Checked = item.GBFB;
                    txtAddress.Text = item.Address.ToString();
                    txtCity.Text = item.City.ToString();
                    ddlState.SelectedValue = item.State.ToString();
                    txtZip.Text = item.Zip.ToString();
                    txtSupplierPhone.Text = item.SupplierPhone.ToString();
                    txtSalesman.Text = item.Salesman.ToString();
                    txtSalesmanPhone.Text = item.SalesmanPhone.ToString();
                    rblIsActive.SelectedValue = item.IsActive.ToString();
                    txtSupplierID.Value = item.SupplierID.ToString();
                }
                else
                {
                    txtSupplierID.Value = "";
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }



        }


        public void GetStates()
        {

            try
            {
                ListController lc = new ListController();
                ListEntryInfoCollection leic = lc.GetListEntryInfoCollection("Region", "Country.US");
                ddlState.DataTextField = "Text";
                ddlState.DataValueField = "Value";
                ddlState.DataSource = leic;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
                ddlState.SelectedValue = "MA";

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void cmdCancel_Click(object sender, EventArgs e)
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



        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();

                actions.Add(GetNextActionID(), Localization.GetString("Products", this.LocalResourceFile),
ModuleActionType.AddContent, "", "", EditUrl("Products"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
true, false);

                actions.Add(GetNextActionID(), Localization.GetString("Invoices", this.LocalResourceFile),
ModuleActionType.AddContent, "", "", EditUrl("Invoices"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
true, false);

                actions.Add(GetNextActionID(), Localization.GetString("ProductCategories", this.LocalResourceFile),
ModuleActionType.AddContent, "", "", EditUrl("ProductCategories"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
true, false);

                return actions;
            }
        }

        #endregion

        //btnAddNewRecord_Click
        protected void btnAddNewRecord_Click(object sender, EventArgs e)
        {
            try
            {
                panelEdit.Visible = true;
                panelGrid.Visible = false;

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
                
                item.SupplierName = txtSupplierName.Text.ToString();
           //     item.GBFB = Convert.ToBoolean(cbxGBFB.Checked);
                item.Address = txtAddress.Text.ToString();
                item.City = txtCity.Text.ToString();
                item.State = ddlState.SelectedValue.ToString();
                item.Zip = txtZip.Text.ToString();
                item.SupplierPhone = txtSupplierPhone.Text.ToString();
                item.Salesman = txtSalesman.Text.ToString();
                item.SalesmanPhone = txtSalesmanPhone.Text.ToString();
                item.LastModifiedByUserID = this.UserId;

                item.IsActive = Convert.ToBoolean(rblIsActive.SelectedValue.ToString());

                if (txtSupplierID.Value.Length > 0)
                {
                    item.SupplierID = Int32.Parse(txtSupplierID.Value.ToString());
                    controller.FBSuppliers_Update(item);
                    

                }
                else
                {
                    item.CreatedByUserID = this.UserId;
                    controller.FBSuppliers_Insert(item);
                    

                }


                Response.Redirect(EditUrl("Suppliers"));



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
                Response.Redirect(EditUrl("Suppliers"));
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

        protected void cbxShowInActive_CheckedChanged(object sender, EventArgs e)
        {
            FillSuppliersGrid();
        }


    }
}