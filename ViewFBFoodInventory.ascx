<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFBFoodInventory.ascx.cs" Inherits="GIBS.Modules.FBFoodInventory.ViewFBFoodInventory" %>



<asp:Button ID="btnInvoices" runat="server"  CssClass="dnnPrimaryAction" 
    resourcekey="btnInvoices" Text="Invoices" onclick="btnInvoices_Click" />
    <br />

<asp:Button ID="btnProducts" runat="server" Text="Products"  
    resourcekey="btnProducts" CssClass="dnnPrimaryAction" 
    onclick="btnProducts_Click" /><br />
<asp:Button ID="btnSuppliers" resourcekey="btnSuppliers" runat="server" CssClass="dnnPrimaryAction" Text="Suppliers" onclick="btnSuppliers_Click" />
<br />
<asp:Button ID="btnProductCategories" runat="server" Text="ProductCategories" 
    resourcekey="btnProductCategories" CssClass="dnnPrimaryAction" 
    onclick="btnProductCategories_Click" /><br />




