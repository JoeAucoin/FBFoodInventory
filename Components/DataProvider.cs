using System;
using System.Data;
using DotNetNuke;
using DotNetNuke.Framework;

namespace GIBS.FBFoodInventory.Components
{
    public abstract class DataProvider
    {

        #region common methods

        /// <summary>
        /// var that is returned in the this singleton
        /// pattern
        /// </summary>
        private static DataProvider instance = null;

        /// <summary>
        /// private static cstor that is used to init an
        /// instance of this class as a singleton
        /// </summary>
        static DataProvider()
        {
            instance = (DataProvider)Reflection.CreateObject("data", "GIBS.FBFoodInventory.Components", "");
        }

        /// <summary>
        /// Exposes the singleton object used to access the database with
        /// the conrete dataprovider
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return instance;
        }

        #endregion


        #region Abstract methods

        /* implement the methods that the dataprovider should */

      //  public abstract IDataReader GetFBFoodInventorys(int moduleId);
        
        //Suppliers
        public abstract IDataReader FBSuppliers_List(int moduleId);
        public abstract IDataReader FBSuppliers_GetByID(int moduleId, int itemId);
        public abstract void FBSuppliers_Insert(int createdByUserID, int moduleId, string supplierName, bool gbfb, string address, string city, string state, string zip, string supplierPhone, string salesman, string salesmanPhone, int portalId, bool isActive);
        public abstract void FBSuppliers_Update(int supplierID, int moduleId, string supplierName, bool gbfb, string address, string city, string state, string zip, string supplierPhone, string salesman, string salesmanPhone, int lastModifiedByUserID, int portalId, bool isActive);
                //UNUSED
        public abstract void DeleteFBFoodInventory(int moduleId, int itemId);

        // Products
        public abstract void FBProducts_Insert(string productName, double casePrice, int caseCount, int productCategoryID, int createdByUserID, int moduleId, int portalId, double caseWeight, bool isActive);
        public abstract IDataReader FBProducts_List(int moduleId);
        public abstract IDataReader FBProducts_GetByID(int moduleId, int productID);
        public abstract void FBProducts_Update(int productID, string productName, double casePrice, int caseCount, int productCategoryID, int moduleId, int lastModifiedByUserID, int portalId, double caseWeight, bool isActive);
        public abstract void FBProducts_Delete(int productID);
        
        
        //ProductCategories
        public abstract void FBProductCategories_Insert(int createdByUserID, int moduleId, string productCategory, int portalId, bool isActive);
        public abstract IDataReader FBProductCategory_List(int moduleId);
        public abstract IDataReader FBProductCategory_GetByID(int moduleId, int productCategoryID);
        public abstract void FBProductCategory_Update(int productCategoryID, int moduleId, string productCategory, int lastModifiedByUserID, int portalId, bool isActive);

        //Invoices
        //public abstract void FBInvoice_Insert(string invoiceNumber, DateTime invoiceDate, int supplierID, int createdByUserID, int moduleId, int portalId);

        public abstract int FBInvoice_Insert(string invoiceNumber, DateTime invoiceDate, int supplierID, int createdByUserID, int moduleId, int portalId, string organization);

        public abstract IDataReader FBInvoice_List(int moduleId);
        public abstract IDataReader FBInvoice_GetByID(int moduleId, int invoiceID);
        public abstract void FBInvoice_Update(int invoiceID, string invoiceNumber, DateTime invoiceDate, int supplierID, int moduleId, int lastModifiedByUserID, int portalId, string organization);
        public abstract IDataReader FBInvoice_GetInvoiceLineItems(int invoiceID);
        public abstract void FBInvoice_Delete(int invoiceID);

        //LineItems
        public abstract void FBLineItems_Insert(int invoiceID, int productID, int Cases, int countPerCase, double pricePerCase, double weightPerCase, string reportType);
        public abstract void FBLineItems_Update(int lineItemID, int invoiceID, int productID, int cases, int countPerCase, double pricePerCase, double weightPerCase, string reportType);
        public abstract void FBLineItems_Delete(int lineItemID);
        public abstract IDataReader FBLineItems_GetByID(int lineItemID);

        // Reports
  //      public abstract IDataReader FBReports_Food_Inventory(DateTime startDate, DateTime endDate, int portalId);

        #endregion

    }



}
