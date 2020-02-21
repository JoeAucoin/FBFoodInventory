using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using System.Data;
using System.ComponentModel;

namespace GIBS.FBFoodInventory.Components
{
    public class FBFoodInventoryController :  IPortable
    {

        #region public method




        
        
        
        /// <summary>
        /// Gets all the FBFoodInventoryInfo objects for items matching the this moduleId
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<FBFoodInventoryInfo> FBSuppliers_List(int moduleId)
        {
            return CBO.FillCollection<FBFoodInventoryInfo>(DataProvider.Instance().FBSuppliers_List(moduleId));
        }

        /// <summary>
        /// Get an info object from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public FBFoodInventoryInfo FBSuppliers_GetByID(int moduleId, int supplierID)
        {
            //return (FBFoodInventoryInfo)CBO.FillObject(DataProvider.Instance().FBSuppliers_GetByID(moduleId, supplierID), typeof(FBFoodInventoryInfo));
            return CBO.FillObject<FBFoodInventoryInfo>(DataProvider.Instance().FBSuppliers_GetByID(moduleId, supplierID));
        }


        /// <summary>
        /// Adds a new FBFoodInventoryInfo object into the database
        /// </summary>
        /// <param name="info"></param>
        public void FBSuppliers_Insert(FBFoodInventoryInfo info)
        {
            //check we have some content to store
            if (info.SupplierName != string.Empty)
            {
                DataProvider.Instance().FBSuppliers_Insert(info.CreatedByUserID, info.ModuleId, info.SupplierName, info.GBFB, info.Address, info.City, info.State, info.Zip, info.SupplierPhone, info.Salesman, info.SalesmanPhone, info.PortalId, info.IsActive);
            }
        }

        /// <summary>
        /// update a info object already stored in the database
        /// </summary>
        /// <param name="info"></param>
        public void FBSuppliers_Update(FBFoodInventoryInfo info)
        {
            //check we have some content to update
            if (info.SupplierName != string.Empty)
            {
                DataProvider.Instance().FBSuppliers_Update(info.SupplierID, info.ModuleId, info.SupplierName, info.GBFB, info.Address, info.City, info.State, info.Zip, info.SupplierPhone, info.Salesman, info.SalesmanPhone, info.LastModifiedByUserID, info.PortalId, info.IsActive );
            }
        }


        /// <summary>
        /// Delete a given item from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        public void DeleteFBFoodInventory(int moduleId, int itemId)
        {
            DataProvider.Instance().DeleteFBFoodInventory(moduleId, itemId);
        }



        // ProductCategories
        public void FBProductCategories_Insert(FBFoodInventoryInfo info)
        {
            //check we have some content to store
            if (info.ProductCategory != string.Empty)
            {
                DataProvider.Instance().FBProductCategories_Insert(info.CreatedByUserID, info.ModuleId, info.ProductCategory, info.PortalId, info.IsActive);
            }
        }

        public List<FBFoodInventoryInfo> FBProductCategory_List(int moduleId)
        {
            return CBO.FillCollection<FBFoodInventoryInfo>(DataProvider.Instance().FBProductCategory_List(moduleId));
        }

        public FBFoodInventoryInfo FBProductCategory_GetByID(int moduleId, int productCategoryID)
        {
           // return (FBFoodInventoryInfo)CBO.FillObject(DataProvider.Instance().FBProductCategory_GetByID(moduleId, productCategoryID), typeof(FBFoodInventoryInfo));
            return CBO.FillObject<FBFoodInventoryInfo>(DataProvider.Instance().FBProductCategory_GetByID(moduleId, productCategoryID));
        }

        public void FBProductCategory_Update(FBFoodInventoryInfo info)
        {
            //check we have some content to update
            if (info.ProductCategory != string.Empty)
            {
                DataProvider.Instance().FBProductCategory_Update(info.ProductCategoryID, info.ModuleId, info.ProductCategory, info.LastModifiedByUserID, info.PortalId, info.IsActive);
            }
        }


        // Products
        public void FBProducts_Insert(FBFoodInventoryInfo info)
        {
            //check we have some content to store
            if (info.ProductName != string.Empty)
            {
                DataProvider.Instance().FBProducts_Insert(info.ProductName, info.CasePrice, info.CaseCount, info.ProductCategoryID, info.CreatedByUserID, info.ModuleId, info.PortalId, info.CaseWeight, info.IsActive);
            }
        }

        public List<FBFoodInventoryInfo> FBProducts_List(int moduleId)
        {
            return CBO.FillCollection<FBFoodInventoryInfo>(DataProvider.Instance().FBProducts_List(moduleId));
        }

        public FBFoodInventoryInfo FBProducts_GetByID(int moduleId, int productID)
        {
         //   return (FBFoodInventoryInfo)CBO.FillObject(DataProvider.Instance().FBProducts_GetByID(moduleId, productID), typeof(FBFoodInventoryInfo));
            return CBO.FillObject<FBFoodInventoryInfo>(DataProvider.Instance().FBProducts_GetByID(moduleId, productID));
        }

        public void FBProducts_Update(FBFoodInventoryInfo info)
        {
            //check we have some content to update
            if (info.ProductCategory != string.Empty)
            {
                DataProvider.Instance().FBProducts_Update(info.ProductID, info.ProductName, info.CasePrice, info.CaseCount, info.ProductCategoryID, info.ModuleId, info.LastModifiedByUserID, info.PortalId, info.CaseWeight, info.IsActive);
            }
        }

        public void FBProducts_Delete(int productID)
        {
            DataProvider.Instance().FBProducts_Delete(productID);
        }

        // Invoices
        //public void FBInvoice_Insert(FBFoodInventoryInfo info)
        //{
        //    //check we have some content to store
        //    if (info.InvoiceNumber != string.Empty)
        //    {
        //        DataProvider.Instance().FBInvoice_Insert(info.InvoiceNumber, info.InvoiceDate, info.SupplierID, info.CreatedByUserID, info.ModuleId, info.PortalId);
        //    }
        //}

        public int FBInvoice_Insert(FBFoodInventoryInfo info)
        {

         //   return Convert.ToInt32(DataProvider.Instance().FBInvoice_Insert(info.InvoiceNumber, info.InvoiceDate, info.SupplierID, info.CreatedByUserID, info.ModuleId, info.PortalId));
            //check we have some content to store
            if (info.InvoiceNumber != string.Empty)
            {
                return Convert.ToInt32(DataProvider.Instance().FBInvoice_Insert(info.InvoiceNumber, info.InvoiceDate, info.SupplierID, info.CreatedByUserID, info.ModuleId, info.PortalId, info.Organization));
            }
            else
            {
                return 0;
            }
        }



        public List<FBFoodInventoryInfo> FBInvoice_List(int moduleId)
        {
            return CBO.FillCollection<FBFoodInventoryInfo>(DataProvider.Instance().FBInvoice_List(moduleId));
        }

        public FBFoodInventoryInfo FBInvoice_GetByID(int moduleId, int invoiceID)
        {
        //    return (FBFoodInventoryInfo)CBO.FillObject(DataProvider.Instance().FBInvoice_GetByID(moduleId, invoiceID), typeof(FBFoodInventoryInfo));
            return CBO.FillObject<FBFoodInventoryInfo>(DataProvider.Instance().FBInvoice_GetByID(moduleId, invoiceID));
        }

        public void FBInvoice_Update(FBFoodInventoryInfo info)
        {
            //check we have some content to update
            if (info.InvoiceNumber != string.Empty)
            {
                DataProvider.Instance().FBInvoice_Update(info.InvoiceID, info.InvoiceNumber, info.InvoiceDate, info.SupplierID, info.ModuleId, info.LastModifiedByUserID, info.PortalId, info.Organization);
            }
        }

        public void FBInvoice_Delete(int invoiceID)
        {
            DataProvider.Instance().FBInvoice_Delete(invoiceID);
        }

        public List<FBFoodInventoryInfo> FBInvoice_GetInvoiceLineItems(int invoiceID)
        {
            return CBO.FillCollection<FBFoodInventoryInfo>(DataProvider.Instance().FBInvoice_GetInvoiceLineItems(invoiceID));
        }

        // LineItems
        public void FBLineItems_Insert(FBFoodInventoryInfo info)
        {
            //check we have some content to store
            if (info.ProductID != 0)
            {
                DataProvider.Instance().FBLineItems_Insert(info.InvoiceID, info.ProductID, info.Cases, info.CountPerCase, info.PricePerCase, info.WeightPerCase, info.ReportType);
            }
        }

        public FBFoodInventoryInfo FBLineItems_GetByID(int lineItemID)
        {
          //  return (FBFoodInventoryInfo)CBO.FillObject(DataProvider.Instance().FBLineItems_GetByID(lineItemID), typeof(FBFoodInventoryInfo));
            return CBO.FillObject<FBFoodInventoryInfo>(DataProvider.Instance().FBLineItems_GetByID(lineItemID));
        }

        public void FBLineItems_Update(FBFoodInventoryInfo info)
        {
            //check we have some content to update
            if (info.ProductID != 0)
            {
                DataProvider.Instance().FBLineItems_Update(info.LineItemID, info.InvoiceID, info.ProductID, info.Cases, info.CountPerCase, info.PricePerCase, info.WeightPerCase, info.ReportType);
            }
        }

        public void FBLineItems_Delete(int itemId)
        {
            DataProvider.Instance().FBLineItems_Delete(itemId);
        }

        // Reports
        public List<FBFoodInventoryInfo> FBReports_Food_Inventory(DateTime startDate, DateTime endDate, int portalId)
        {
            return CBO.FillCollection<FBFoodInventoryInfo>(DataProvider.Instance().FBReports_Food_Inventory(startDate, endDate, portalId));
        }

        #endregion

      

        #region IPortable Members

        /// <summary>
        /// Exports a module to xml
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public string ExportModule(int moduleID)
        {
            StringBuilder sb = new StringBuilder();

            List<FBFoodInventoryInfo> infos = FBSuppliers_List(moduleID);

            if (infos.Count > 0)
            {
                sb.Append("<FBFoodInventorys>");
                foreach (FBFoodInventoryInfo info in infos)
                {
                    sb.Append("<FBFoodInventory>");
                    sb.Append("<content>");
                    sb.Append(XmlUtils.XMLEncode(info.SupplierName));
                    sb.Append("</content>");
                    sb.Append("</FBFoodInventory>");
                }
                sb.Append("</FBFoodInventorys>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// imports a module from an xml file
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="SupplierName"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        public void ImportModule(int ModuleID, string SupplierName, string Version, int UserID)
        {
            XmlNode infos = DotNetNuke.Common.Globals.GetContent(SupplierName, "FBFoodInventorys");

            foreach (XmlNode info in infos.SelectNodes("FBFoodInventory"))
            {
                FBFoodInventoryInfo FBFoodInventoryInfo = new FBFoodInventoryInfo();
                FBFoodInventoryInfo.ModuleId = ModuleID;
                FBFoodInventoryInfo.SupplierName = info.SelectSingleNode("SupplierName").InnerText;
                FBFoodInventoryInfo.CreatedByUserID = UserID;

                FBSuppliers_Insert(FBFoodInventoryInfo);
            }
        }

        #endregion
    }
}
