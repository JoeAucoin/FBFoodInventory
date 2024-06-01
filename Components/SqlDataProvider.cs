using System;
using System.Data;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace GIBS.FBFoodInventory.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "GIBS_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {
            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion

        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region override methods

        public override IDataReader FBSuppliers_List(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBSuppliers_List"), moduleId);
        }

        public override IDataReader FBSuppliers_GetByID(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBSuppliers_GetByID"), moduleId, itemId);
        }

        public override void FBSuppliers_Insert(int createdByUserID, int moduleId, string supplierName, bool gbfb, string address, string city, string state, string zip, string supplierPhone, string salesman, string salesmanPhone, int portalId, bool isActive)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBSuppliers_Insert"), createdByUserID, moduleId, supplierName, gbfb, address, city, state, zip, supplierPhone, salesman, salesmanPhone, portalId, isActive);
        }

        public override void FBSuppliers_Update(int supplierID, int moduleId, string supplierName, bool gbfb, string address, string city, string state, string zip, string supplierPhone, string salesman, string salesmanPhone, int lastModifiedByUserID, int portalId, bool isActive)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBSuppliers_Update"), supplierID, moduleId, supplierName, gbfb, address, city, state, zip, supplierPhone, salesman, salesmanPhone, lastModifiedByUserID, portalId, isActive);
        }

        public override void DeleteFBFoodInventory(int moduleId, int itemId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("DeleteFBFoodInventory"), moduleId, itemId);
        }

        
        //ProductCategory
        public override IDataReader FBProductCategory_List(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBProductCategory_List"), moduleId);
        }

        public override IDataReader FBProductCategory_Translations(int productCategoryID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBProductCategory_Translations"), productCategoryID);
        }
        // //int productCategoryID, string productCategory, string languageCode
        public override void FBProductCategoryTranslate_InsertUpdate(int productCategoryID, string productCategory, string languageCode)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBProductCategoryTranslate_InsertUpdate"), productCategoryID, productCategory, languageCode);
        }

        public override IDataReader FBProducts_Translations(int productID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBProducts_Translations"), productID);
        }
        // //int productCategoryID, string productCategory, string languageCode
        public override void FBProductsTranslate_InsertUpdate(int productID, string productName, string languageCode)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBProductsTranslate_InsertUpdate"), productID, productName, languageCode);
        }

        public override void FBProductCategories_Insert(int createdByUserID, int moduleId, string productCategory, int portalId, bool isActive)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBProductCategories_Insert"), createdByUserID, moduleId, productCategory, portalId, isActive);
        }

        public override IDataReader FBProductCategory_GetByID(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBProductCategory_GetByID"), moduleId, itemId);
        }

        public override void FBProductCategory_Update(int productCategoryID, int moduleId, string productCategory, int lastModifiedByUserID, int portalId, bool isActive)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBProductCategory_Update"), productCategoryID, moduleId, productCategory, lastModifiedByUserID, portalId, isActive);
        }

        //Products
        public override IDataReader FBProducts_List(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBProducts_List"), moduleId);
        }

        public override void FBProducts_Insert(string productName, double casePrice, int caseCount, int productCategoryID, int createdByUserID, int moduleId, int portalId, double caseWeight, bool isActive, int limit, string limitQuantities)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBProducts_Insert"), productName, casePrice, caseCount, productCategoryID, createdByUserID, moduleId, portalId, caseWeight, isActive, limit, limitQuantities);
        }

        public override IDataReader FBProducts_GetByID(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBProducts_GetByID"), moduleId, itemId);
        }

        public override void FBProducts_Update(int productID, string productName, double casePrice, int caseCount, int productCategoryID, int moduleId, int lastModifiedByUserID, int portalId, double caseWeight, bool isActive, int limit, string limitQuantities)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBProducts_Update"), productID, productName, casePrice, caseCount, productCategoryID, moduleId, lastModifiedByUserID, portalId, caseWeight, isActive, limit, limitQuantities);
        }

        public override void FBProducts_Delete(int productID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBProducts_Delete"), productID);
        }

        //Invoices
        public override IDataReader FBInvoice_List(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBInvoice_List"), moduleId);
        }

        //public override void FBInvoice_Insert(string invoiceNumber, DateTime invoiceDate, int supplierID, int createdByUserID, int moduleId, int portalId)
        //{
        //    SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBInvoice_Insert"), invoiceNumber, invoiceDate, supplierID, createdByUserID, moduleId, portalId);
        //}

        public override int FBInvoice_Insert(string invoiceNumber, DateTime invoiceDate, int supplierID, int createdByUserID, int moduleId, int portalId, string organization)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(connectionString, GetFullyQualifiedName("FBInvoice_Insert"), invoiceNumber, invoiceDate, supplierID, createdByUserID, moduleId, portalId, organization));
        }

        public override IDataReader FBInvoice_GetByID(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBInvoice_GetByID"), moduleId, itemId);
        }

        public override void FBInvoice_Update(int invoiceID, string invoiceNumber, DateTime invoiceDate, int supplierID, int moduleId, int lastModifiedByUserID, int portalId, string organization)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBInvoice_Update"), invoiceID, invoiceNumber, invoiceDate, supplierID, moduleId, lastModifiedByUserID, portalId, organization);
        }

        public override void FBInvoice_Delete(int invoiceID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBInvoice_Delete"), invoiceID);
        }

        public override IDataReader FBInvoice_GetInvoiceLineItems(int invoiceID)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBInvoice_GetInvoiceLineItems"), invoiceID);
        }

        //LineItems
        public override void FBLineItems_Insert(int invoiceID, int productID, int Cases, int countPerCase, double pricePerCase, double weightPerCase, string reportType)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBLineItems_Insert"), invoiceID, productID, Cases, countPerCase, pricePerCase, weightPerCase, reportType);
        }

        public override IDataReader FBLineItems_GetByID(int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBLineItems_GetByID"), itemId);
        }

        public override void FBLineItems_Update(int lineItemID, int invoiceID, int productID, int cases, int countPerCase, double pricePerCase, double weightPerCase, string reportType)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBLineItems_Update"), lineItemID, invoiceID, productID, cases, countPerCase, pricePerCase, weightPerCase, reportType);
        }	
        public override void FBLineItems_Delete(int itemId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FBLineItems_Delete"), itemId);
        }

        // Reports
        public override IDataReader FBReports_Food_Inventory(DateTime startDate, DateTime endDate, int portalId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FBReports_Food_Inventory"), startDate, endDate, portalId);
        }

        #endregion
    }
}
