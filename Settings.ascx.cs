using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.FBFoodInventory.Components;

namespace GIBS.Modules.FBFoodInventory
{
    public partial class Settings : ModuleSettingsBase
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
                    FBFoodInventorySettings settingsData = new FBFoodInventorySettings(this.TabModuleId);

                    if (settingsData.Template != null)
                    {
                        txtTemplate.Text = settingsData.Template;
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
                FBFoodInventorySettings settingsData = new FBFoodInventorySettings(this.TabModuleId);
                settingsData.Template = txtTemplate.Text;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}