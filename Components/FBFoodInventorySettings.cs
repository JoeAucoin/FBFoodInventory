
using System.Collections;
using DotNetNuke.Entities.Modules;

//namespace GIBS.FBFoodInventory.Components
namespace GIBS.FBFoodInventory.Components
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class FBFoodInventorySettings : ModuleSettingsBase
    {
       







        #region public properties

        /// <summary>
        /// get/set template used to render the module content
        /// to the user
        /// </summary>

        public string Template
        {
            get
            {
                if (Settings.Contains("Template"))
                    return Settings["Template"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "Template", value.ToString());
            }
        }



        #endregion
    }
}
