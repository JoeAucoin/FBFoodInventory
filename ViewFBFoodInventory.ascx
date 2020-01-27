<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFBFoodInventory.ascx.cs" Inherits="GIBS.Modules.FBFoodInventory.ViewFBFoodInventory" %>


<div style="width: 100%; text-align: center; padding: 10px;">

<asp:Button ID="btnInvoices" runat="server" CssClass="btn btn-lg btn-primary" resourcekey="btnInvoices" Text="Invoices" onclick="btnInvoices_Click" />
     &nbsp; &nbsp; &nbsp;

<asp:Button ID="btnProducts" runat="server" Text="Products" resourcekey="btnProducts" CssClass="btn btn-lg btn-primary" onclick="btnProducts_Click" />
</div>

<div style="width: 100%; text-align: center; padding: 10px;">
<asp:Button ID="btnSuppliers" resourcekey="btnSuppliers" runat="server" CssClass="btn btn-sm btn-default" Text="Suppliers" onclick="btnSuppliers_Click" />
 &nbsp;
<asp:Button ID="btnProductCategories" runat="server" Text="ProductCategories" resourcekey="btnProductCategories" CssClass="btn btn-sm btn-default" onclick="btnProductCategories_Click" /></div>


