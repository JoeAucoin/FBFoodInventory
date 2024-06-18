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
using System.Data;
using DotNetNuke.Common.Lists;

namespace GIBS.Modules.FBFoodInventory
{
    public partial class ProductCategories : PortalModuleBase, IActionable
    {

        public DataTable dt;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillProductCategoryGrid();
                
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            ModuleConfiguration.ModuleTitle = Localization.GetString("ControlTitle", this.LocalResourceFile);
        }


        public void FillProductCategoryGrid()
        {

            try
            {

                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();
                items = controller.FBProductCategory_List(this.ModuleId);


                dt = Components.GridViewTools.ToDataTable(items);

                DataView dv = dt.DefaultView;

                dv.RowFilter = "IsActive = 1";


                if (cbxShowInActive.Checked == true)
                {
                    dv.RowFilter += " or IsActive = 0";
                }

                gvProductCategory.DataSource = dv;
                gvProductCategory.DataBind();
                lblTotalRecordCount.Text = items.Count.ToString();

                //// FILL LANGUAGE DROPDOWN
                //var cLanguage = new ListController().GetListEntryInfoItems("ClientLanguage", "", this.PortalId);

                //ddlLanguage.DataTextField = "Text";
                //ddlLanguage.DataValueField = "Value";
                //ddlLanguage.DataSource = cLanguage;
                //ddlLanguage.DataBind();
                //ddlLanguage.Items.Insert(0, new ListItem("--", ""));



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void gvProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillProductCategoryGrid();
            gvProductCategory.PageIndex = e.NewPageIndex;
            gvProductCategory.DataBind();
        }

        protected void gvProductCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                int productCategoryID = (int)gvProductCategory.DataKeys[e.NewEditIndex].Value;

                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = controller.FBProductCategory_GetByID(this.ModuleId, productCategoryID);

                if (item != null)
                {

                    panelGrid.Visible = false;
                    panelEdit.Visible = true;
                    
                    txtProductCategory.Text = item.ProductCategory.ToString();
                    txtProductCategoryID.Value = item.ProductCategoryID.ToString();
                    rblIsActive.SelectedValue = item.IsActive.ToString();
                    txtSortOrder.Text = item.SortOrder.ToString();
                    txtOrderingInstructions.Text = item.OrderingInstructions.ToString();

                //    FillTranslationsGrid();
                }
                else
                {
                    txtProductCategoryID.Value = "";
                }


                e.Cancel = true;

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

                actions.Add(GetNextActionID(), Localization.GetString("Suppliers", this.LocalResourceFile),
ModuleActionType.AddContent, "", "", EditUrl("Suppliers"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
true, false);

                return actions;
            }
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = new FBFoodInventoryInfo();


                item.ModuleId = this.ModuleId;
                item.PortalId = this.PortalId;

                item.ProductCategory = txtProductCategory.Text.ToString();
                item.LastModifiedByUserID = this.UserId;
                item.IsActive = bool.Parse(rblIsActive.SelectedValue.ToString());
                item.SortOrder = Int16.Parse("0" + txtSortOrder.Text.ToString());
                item.OrderingInstructions = txtOrderingInstructions.Text.ToString();
                if (txtProductCategoryID.Value.Length > 0)
                {
                    item.ProductCategoryID = Int32.Parse(txtProductCategoryID.Value.ToString());
                    controller.FBProductCategory_Update(item);

                }
                else
                {
                    item.CreatedByUserID = this.UserId;
                    controller.FBProductCategories_Insert(item);
                    
                }

                txtProductCategoryID.Value = "";
                txtProductCategory.Text = "";
                txtSortOrder.Text = "";
                txtOrderingInstructions.Text = "";
                rblIsActive.SelectedValue = "True";

                FillProductCategoryGrid();
                panelGrid.Visible = true;
                panelEdit.Visible = false;

               // Response.Redirect(EditUrl("ProductCategories"));

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
                Response.Redirect(EditUrl("ProductCategories"));
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
                // string pageUrl = navigationManager.NavigateURL(ti.TabID)
              //  Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID, "mygallery", "mid=" + ModuleId.ToString()));

                  Response.Redirect(Globals.NavigateURL(), true);
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
                txtProductCategoryID.Value = "";
                txtProductCategory.Text = "";
                txtSortOrder.Text = "";
                txtOrderingInstructions.Text = "";
                rblIsActive.SelectedValue = "True";
                panelGrid.Visible = false;
                panelEdit.Visible = true;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void cbxShowInActive_CheckedChanged(object sender, EventArgs e)
        {
            FillProductCategoryGrid();
        }

        protected void btnSaveTranslation_Click(object sender, EventArgs e)
        {
            try
            {
                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = new FBFoodInventoryInfo();


                item.ProductCategoryID = Int32.Parse(txtProductCategoryID.Value.ToString());
                item.ProductCategory = txtTranslation.Text.ToString();

                item.LanguageCode = ddlLanguage.SelectedValue.ToString();
                controller.FBProductCategoryTranslate_InsertUpdate(item);


           //     FillTranslationsGrid();
           //     ddlLanguage.SelectedIndex = 0;
           //     txtTranslation.Text = string.Empty;

                // Response.Redirect(EditUrl("ProductCategories"));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);

            }
        }


        public void FillTranslationsGrid()
        {

            try
            {

                List<FBFoodInventoryInfo> items;
                FBFoodInventoryController controller = new FBFoodInventoryController();
                items = controller.FBProductCategory_Translations(Int32.Parse(txtProductCategoryID.Value.ToString()));


                gvLanguages.DataSource = items;
                gvLanguages.DataBind();
               

              


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        protected void btnCancelTranslation_Click(object sender, EventArgs e)
        {

        }

        protected void gvLanguages_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}