using System;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using GIBS.FBFoodInventory.Components;

namespace GIBS.Modules.FBFoodInventory
{
    public partial class EditFBFoodInventory : PortalModuleBase
    {

        int itemId = Null.NullInteger;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["ItemId"] != null)
                {
                    itemId = Int32.Parse(Request.QueryString["ItemId"]);
                }

                if (!IsPostBack)
                {
                    //load the data into the control the first time
                    //we hit this page


                    cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" + Localization.GetString("DeleteItem") + "');");

                    //check we have an item to lookup
                    if (!Null.IsNull(itemId))
                    {
                        //load the item
                        FBFoodInventoryController controller = new FBFoodInventoryController();
                        FBFoodInventoryInfo item = controller.FBSuppliers_GetByID(this.ModuleId, itemId);

                        if (item != null)
                        {
                            txtContent.Text = item.SupplierName;
                            ctlAudit.CreatedByUser = item.CreatedByUserName;
                            ctlAudit.CreatedDate = item.CreatedOnDate.ToLongDateString();
                        }
                        else
                            Response.Redirect(Globals.NavigateURL(), true);
                    }
                    else
                    {
                        cmdDelete.Visible = false;
                        ctlAudit.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                FBFoodInventoryController controller = new FBFoodInventoryController();
                FBFoodInventoryInfo item = new FBFoodInventoryInfo();

                item.SupplierName = txtContent.Text;
                item.SupplierID = itemId;
                item.ModuleId = this.ModuleId;
                item.CreatedByUserID = this.UserId;

                //determine if we are adding or updating
                if (Null.IsNull(item.SupplierID))
                    controller.FBSuppliers_Insert(item);
                else
                    controller.FBSuppliers_Update(item);

                Response.Redirect(Globals.NavigateURL(), true);
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

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Null.IsNull(itemId))
                {
                    FBFoodInventoryController controller = new FBFoodInventoryController();
                    controller.DeleteFBFoodInventory(this.ModuleId, itemId);
                    Response.Redirect(Globals.NavigateURL(), true);
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}