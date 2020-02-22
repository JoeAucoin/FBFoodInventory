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
using System.Linq;

using GIBS.FBFoodInventory.Components;
using DotNetNuke.Common;
using System.Drawing;
using System.Data;

namespace GIBS.Modules.FBFoodInventory
{
    public partial class FoodReport : PortalModuleBase
    {
        //GIBS.FBFoodInventory.Components
        private GridViewHelper helper;
        // To show custom operations...
        private List<int> mQuantities = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEndDate.Text = DateTime.Today.ToShortDateString();
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                //GroupIt();
                //Fill_Grid();
            }

        }


        protected void Page_Init(object sender, EventArgs e)
        {
            ModuleConfiguration.ModuleTitle = Localization.GetString("ControlTitle", this.LocalResourceFile);
        }


        protected void btnRunReport_Click(object sender, EventArgs e)
        {
            try
            {

                //     GetTotalInHouseholdServed();
                //     Fill_Age_Groups_AFM();
                //     Fill_Age_Groups_Clients();
                GroupIt();
                Fill_Grid();

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        //gv_ClientDetails

        public void Fill_Grid()
        {

            try
            {
                DateTime _startDate = DateTime.Parse(txtStartDate.Text.ToString());
                DateTime _endDate = DateTime.Parse(txtEndDate.Text.ToString());


                List<FBFoodInventoryInfo> items;

                FBFoodInventoryController controller = new FBFoodInventoryController();

                items = controller.FBReports_Food_Inventory(_startDate, _endDate, this.PortalId);

                if (rblGroupBy.SelectedValue.ToString().ToLower() == "reporttype")
                {
                    var so = from FBFoodInventoryInfo s in items orderby s.ReportType ascending select s;

                    gv_ClientDetails.DataSource = so;
                    gv_ClientDetails.DataBind();
                }
                else
                {
                    var so = from FBFoodInventoryInfo s in items orderby s.SupplierName, s.ReportType, s.ProductName select s;
                    gv_ClientDetails.DataSource = so;
                    gv_ClientDetails.DataBind();
                }






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


                this.gv_ClientDetails.Columns[0].Visible = true;
                this.gv_ClientDetails.Columns[1].Visible = true;
                this.gv_ClientDetails.Columns[2].Visible = true;
                this.gv_ClientDetails.Columns[3].Visible = true;


                helper = new GridViewHelper(this.gv_ClientDetails);

                if (rblGroupBy.SelectedValue.ToString().ToLower() == "suppliername")
                {

                    helper.RegisterGroup("SupplierName", true, true);
                    helper.RegisterGroup("ReportType", true, false);

                    helper.RegisterSummary("TotalProductWeight", SummaryOperation.Sum, "SupplierName");
                    helper.RegisterSummary("TotalProductWeight", SummaryOperation.Sum, "ReportType");
                    helper.RegisterSummary("TotalProductCost", SummaryOperation.Sum, "SupplierName");
                    helper.RegisterSummary("TotalProductCost", SummaryOperation.Sum, "ReportType");





                }

                if (rblGroupBy.SelectedValue.ToString().ToLower() == "productcategory")
                {
                    helper.RegisterGroup("ProductCategory", true, true);
                    helper.RegisterSummary("TotalProductWeight", SummaryOperation.Sum, "ProductCategory");
                    helper.RegisterSummary("TotalProductCost", SummaryOperation.Sum, "ProductCategory");
                }

                if (rblGroupBy.SelectedValue.ToString().ToLower() == "reporttype")
                {
                    helper.RegisterGroup("ReportType", true, true);
                    helper.RegisterSummary("Cases", SummaryOperation.Sum, "ReportType");
                    helper.RegisterSummary("TotalProductWeight", SummaryOperation.Sum, "ReportType");
                    helper.RegisterSummary("TotalProductCost", SummaryOperation.Sum, "ReportType");
                    //  helper.RegisterSummary("TotalProductWeight", SummaryOperation.Custom);
                }

                if (rblGroupBy.SelectedValue.ToString().ToLower() == "both")
                {
                    helper.RegisterGroup("SupplierName", true, true);
                    helper.RegisterSummary("TotalProductWeight", SummaryOperation.Sum, "SupplierName");
                    helper.RegisterSummary("TotalProductCost", SummaryOperation.Sum, "SupplierName");

                    helper.RegisterGroup("ProductCategory", true, true);
                    helper.RegisterSummary("TotalProductWeight", SummaryOperation.Sum, "ProductCategory");
                    helper.RegisterSummary("TotalProductCost", SummaryOperation.Sum, "ProductCategory");

                    helper.RegisterGroup("ReportType", true, true);
                    helper.RegisterSummary("TotalProductWeight", SummaryOperation.Sum, "ReportType");
                    helper.RegisterSummary("TotalProductCost", SummaryOperation.Sum, "ReportType");
                }

                helper.RegisterSummary("TotalProductWeight", SummaryOperation.Sum);
                helper.RegisterSummary("TotalProductCost", SummaryOperation.Sum);


                if (rblGroupBy.SelectedValue.ToString().ToLower() == "reporttype")
                {
                    helper.SetInvisibleColumnsWithoutGroupSummary();
                }


                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                helper.GroupSummary += new GroupEvent(helper_Bug);

                helper.ApplyGroupSort();




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        protected void gv_ClientDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            //gv_ClientDetails.DataSource = // Call to your data-binding routine
            //gv_ClientDetails.DataBind();
        }

        private void helper_ManualSummary(GridViewRow row)
        {
            GridViewRow newRow = helper.InsertGridRow(row);
            newRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            newRow.Cells[0].Text = String.Format("Total: {0} items, ", helper.GeneralSummaries["ClientCity"].Value, helper.GeneralSummaries["HouseholdTotal"].Value);
        }

        private void helper_GroupSummary(string groupName, object[] values, GridViewRow row)
        {
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[0].Text = "Média";
        }

        private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == "SupplierName")
            {
                row.BackColor = Color.LightGray;
                row.Cells[0].Text = "&nbsp;&nbsp;<h5>" + row.Cells[0].Text + "</h5>";

            }
            else if (groupName == "ProductCategory")
            {
                row.BackColor = Color.FromArgb(236, 236, 236);
                row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + row.Cells[0].Text + "</b>";
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
            row.Cells[0].Text = values[0] + " Totals &nbsp;";
        }

    }
}