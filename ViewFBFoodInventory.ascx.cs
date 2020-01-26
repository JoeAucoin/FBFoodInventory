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

namespace GIBS.Modules.FBFoodInventory
{
    public partial class ViewFBFoodInventory : PortalModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {



                }
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

                actions.Add(GetNextActionID(), Localization.GetString("ProductCategories", this.LocalResourceFile),
                    ModuleActionType.AddContent, "", "", EditUrl("ProductCategories"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);
                actions.Add(GetNextActionID(), Localization.GetString("Suppliers", this.LocalResourceFile),
                    ModuleActionType.AddContent, "", "", EditUrl("Suppliers"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true, false);

                actions.Add(GetNextActionID(), Localization.GetString("Invoices", this.LocalResourceFile),
    ModuleActionType.AddContent, "", "", EditUrl("Invoices"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
    true, false);

                return actions;
            }
        }

        #endregion


        /// <summary>
        /// Handles the items being bound to the datalist control. In this method we merge the data with the
        /// template defined for this control to produce the result to display to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        protected void btnSuppliers_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("Suppliers"));
        }

        protected void btnProductCategories_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("ProductCategories"));

        }

        protected void btnProducts_Click(object sender, EventArgs e)
        {
            Response.Redirect(EditUrl("Products"));
            
        }

        protected void btnInvoices_Click(object sender, EventArgs e)
        {
          //  Response.Redirect(Globals.NavigateURL("Invoices"), true);
            Response.Redirect(EditUrl("Invoices"));
        }

    }
}