using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.FBFoodInventory.Components;

namespace GIBS.Modules.FBFoodInventory
{
    public partial class Settings : FBFoodInventorySettings 
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Template != null)
                    {
                        if (Template.ToString().Length > 0)
                        {
                            txtTemplate.Text = Template;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
               
                Template = txtTemplate.Text.ToString();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}