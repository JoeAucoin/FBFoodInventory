<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Invoices.ascx.cs" Inherits="GIBS.Modules.FBFoodInventory.Invoices" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/smoothness/jquery-ui.css" />

<script type="text/javascript">

    $(function () {
        $("#txtInvoiceDate").datepicker({
            numberOfMonths: 2,
            showButtonPanel: false,
            showCurrentAtPos: 1
        });

    });
</script>



<div class="dnnForm dnnClear" id="form-lineitems">


<asp:Panel runat="server" ID="panelGrid" Visible="True">
<div style="margin-top:10px;">
<asp:Button runat="server" Text="Button" ID="btnAddNew" ResourceKey="btnAddNew" CssClass="dnnPrimaryAction" 
    onclick="btnAddNew_Click" />
</div>
<asp:GridView ID="gvInvoices" runat="server" 
    DataKeyNames="InvoiceID" 
    OnRowCommand="gvInvoices_RowCommand" 
OnRowEditing="gvInvoices_RowEditing" 
OnRowDeleting="gvInvoices_RowDeleting" 
OnPageIndexChanging="gvInvoices_PageIndexChanging"     
    AutoGenerateColumns="False" 
    GridLines="Horizontal" resourcekey="gvProducts" AllowPaging="True" PageSize="25" CssClass="table table-striped table-bordered table-list">
    
<PagerStyle  CssClass="pagination-ys" />  
<PagerSettings Mode="NumericFirstLast" /> 

    
    <Columns>



               <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("InvoiceID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>


        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonDelete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this invoice? All line items for this invoice will also be deleted!');"      
             CommandArgument='<%# Eval("InvoiceID") %>' 
             CommandName="Delete" runat="server"><asp:image ID="imgDelete" runat="server" imageurl="~/images/delete.gif" AlternateText="Delete Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>
       
       <asp:BoundField HeaderText="Entity" DataField="Organization"></asp:BoundField>
      <asp:BoundField HeaderText="Date" DataField="InvoiceDate" DataFormatString="{0:d}"></asp:BoundField>
       <asp:BoundField HeaderText="Supplier" DataField="SupplierName"></asp:BoundField>
        <asp:BoundField HeaderText="Invoice #" DataField="InvoiceNumber"></asp:BoundField>
    
        <asp:BoundField HeaderText="Created By" DataField="CreatedByUserName"></asp:BoundField>

    </Columns>
</asp:GridView>	

<div style="color: Blue; font-weight: bold">
            <br />
            <i>
                <asp:Label ID="lblTotalRecordCount" runat="server" Text=""></asp:Label> Records<br />You are viewing page
                <%=gvInvoices.PageIndex + 1%>
                of
                <%=gvInvoices.PageCount%>
            </i>
</div>

    

</asp:Panel>






    <asp:Panel ID="panelEdit" runat="server" Visible="False">



    <asp:Label ID="lblFormMessage" runat="server" CssClass="dnnFormMessage dnnFormInfo" ResourceKey="lblFormMessage" Visible="False" />
    <div class="dnnFormItem dnnFormHelp dnnClear">
        <div class="dnnFormRequired"><asp:Label ID="lblRequiredIndicator" runat="server" ResourceKey="Required Indicator" Visible="False"/></div>
    </div>



    <ul class="dnnActions dnnClear" style="float:right;">
        <li><asp:LinkButton ID="btnSave" runat="server" CssClass="dnnPrimaryAction" 
                ResourceKey="btnSave" onclick="btnSave_Click" /></li>
        <li><asp:LinkButton ID="btnCancel" runat="server" CssClass="dnnSecondaryAction" CausesValidation="False" 
                ResourceKey="btnCancel" onclick="btnCancel_Click" /></li>
    </ul>
<div class="dnnForm" id="form-invoice">

    <fieldset>

		<div class="dnnFormItem">						
					 <dnn:Label runat="server" ID="lblOrganization" ControlName="ddlOrganization" ResourceKey="lblOrganization" Suffix=":" /> 
 <asp:DropDownList ID="ddlOrganization" runat="server" EnableViewState="true" /><asp:RequiredFieldValidator runat="server" id="reqOrganization" 
            controltovalidate="ddlOrganization" InitialValue="" CssClass="dnnFormMessage dnnFormError" ResourceKey="OrganizationError" /> 
		</div>	


		<div class="dnnFormItem">
<dnn:Label runat="server" ID="lblInvoiceNumber" ControlName="txtInvoiceNumber" ResourceKey="lblInvoiceNumber" Suffix=":" />
<asp:TextBox runat="server" ID="txtInvoiceNumber"  />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInvoiceNumber" 
                CssClass="dnnFormError dnnFormMessage" ResourceKey="InvoiceNumber.Required" Display="Dynamic" />
        </div>
        <div class="dnnFormItem">
<dnn:Label runat="server" ID="lblInvoiceDate" ControlName="txtInvoiceDate" ResourceKey="lblInvoiceDate" Suffix=":" />

<asp:TextBox ID="txtInvoiceDate" runat="server" ClientIDMode="Static" />
<asp:RequiredFieldValidator runat="server" id="reqInvoiceDate" Display="Dynamic" 
            controltovalidate="txtInvoiceDate" CssClass="dnnFormError dnnFormMessage" ResourceKey="InvoiceDate.Required" />
        </div>		

		<div class="dnnFormItem">
<dnn:Label runat="server" ID="lblSupplier" ControlName="ddlSupplier" ResourceKey="lblSupplier" Suffix=":" />
<asp:DropDownList ID="ddlSupplier" runat="server" ></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSupplier" 
                CssClass="dnnFormError dnnFormMessage" ResourceKey="Supplier.Required" InitialValue="0" Display="Dynamic" />
        </div>

	
		
    </fieldset>


</div>









    


    <asp:HiddenField ID="txtInvoiceID" runat="server" />


<div class="dnnClear">&nbsp;</div>
<asp:Panel ID="panelAddLineItems" runat="server" Visible="False">

<h4>Manage Line Items</h4>
<div style="text-align:left;">

<asp:RequiredFieldValidator ID="rfvddlProducts" runat="server" ControlToValidate="ddlProducts"  
             InitialValue="0"  ErrorMessage="You must select a product!"
              ResourceKey="Product.Required" CssClass="dnnFormMessage dnnFormError" Width="200px" Display="Dynamic"
             ValidationGroup="AddLineItem">  
        </asp:RequiredFieldValidator> 
<asp:RequiredFieldValidator ID="rfvtxtCasesAddNewEdit" runat="server" ControlToValidate="txtCasesAddNewEdit" 
                 ResourceKey="txtCasesAddNewEdit.Required" CssClass="dnnFormMessage dnnFormError" Width="150px" Display="Dynamic"
                 
                ValidationGroup="AddLineItem" />        
         
</div>

<div class="dnnForm dnnClear" id="form-CategoryFilter">
    <fieldset>

        <div class="dnnFormItem">
           <dnn:Label runat="server" ID="lblCategoryFilter" ControlName="ddlFilterCategory" ResourceKey="lblCategoryFilter" Suffix=":" />
            <asp:DropDownList ID="ddlFilterCategory" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlFilterCategory_SelectedIndexChanged"></asp:DropDownList>
        </div>
		

    </fieldset>
    

</div>



        <table border="1" id="LineItemTable" style="background-color:#dddddd;margin-left: auto;margin-right: auto;">
            <tr>
                <td style="width:50px; text-align:center; font-weight:bold;">&nbsp;</td>
                
                <td style="width:180px; text-align:center; font-weight:bold;">Product</td>
                <td style="width:50px; text-align:center; font-weight:bold;">Cases</td>
                
                <td style="width:80px; text-align:center; font-weight:bold;"># Per Case</td>
                <td style="width:80px; text-align:center; font-weight:bold;">Lbs. Per Case</td>
                <td style="width:80px; text-align:center; font-weight:bold;">Case Price</td>
                <td style="width:80px; text-align:center; font-weight:bold;">Report Type</td>
                <td>&nbsp;</td>
               
            </tr>
            <tr>
                <td>&nbsp;</td>
                
                <td style="text-align:center;"><asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="True" Width="200px" 
        onselectedindexchanged="ddlProducts_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td><asp:TextBox ID="txtCasesAddNewEdit" runat="server" Width="50px"></asp:TextBox></td>
                <td style="text-align:center;"><asp:TextBox ID="txtCountPerCaseAddNewEdit" runat="server" Width="50px"></asp:TextBox></td>
                <td style="text-align:center;"><asp:TextBox ID="txtWeightPerCaseAddNewEdit" runat="server" Width="50px"></asp:TextBox></td>

                 <td style="text-align:center;"><asp:TextBox ID="txtPricePerCaseAddNewEdit" runat="server"  Width="50px"></asp:TextBox></td>
                 <td style="text-align:center;"><asp:DropDownList ID="ddlReportType" runat="server"></asp:DropDownList></td>
                 <td> <asp:Button ID="btnAdd" runat="server" Text="Add New Item" OnClick="AddNewLineItem" ValidationGroup="AddLineItem" CssClass="dnnPrimaryAction" /></td>
            </tr>

        </table>
        <asp:HiddenField ID="txtLineItemID_Edit" runat="server" />
</asp:Panel>

<br />
<asp:GridView ID="GridViewLineItems" runat="Server" 
OnRowCommand="GridView1_RowCommand" 
OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" Caption="<b>Items Ordered</b>" CaptionAlign="Top"     
    AutoGenerateColumns="False" GridLines="Horizontal"
  CssClass="dnnGrid" EnableViewState="True" HorizontalAlign="Center"
DataKeyNames="LineItemID" >
    <AlternatingRowStyle cssclass="dnnGridAltItem" />
    <FooterStyle cssclass="dnnGridFooter" />
    <HeaderStyle cssclass="dnnGridHeader" />
    <PagerStyle cssclass="dnnGridPager" />
    <RowStyle cssclass="dnnGridItem" />

<Columns>
        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("LineItemID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>


        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonDelete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this line item?');"      
             CommandArgument='<%# Eval("LineItemID") %>' 
             CommandName="Delete" runat="server"><asp:image ID="imgDelete" runat="server" imageurl="~/images/delete.gif" AlternateText="Delete Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>
      
       <asp:BoundField HeaderText="Product" DataField="ProductName" ItemStyle-Width="200px"></asp:BoundField>
       <asp:BoundField HeaderText="Cases" DataField="Cases"  ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

        <asp:BoundField HeaderText="# Per Case" DataField="CountPerCase" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Total Lbs." DataField="TotalWeightPerCase" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Case Price" DataField="PricePerCase" DataFormatString="{0:c}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ></asp:BoundField>
        <asp:BoundField HeaderText="Report Type" DataField="ReportType" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Total Cost" DataField="TotalCostExtended" DataFormatString="{0:c}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ></asp:BoundField>
        



 </Columns>

</asp:GridView> 
       
        
</asp:Panel>


<div style="text-align:right"><asp:Button ID="btnReturnToFrontDesk" resourcekey="btnReturnToFrontDesk" CausesValidation="False" runat="server" CssClass="dnnSecondaryAction" 
                Text="Return" onclick="btnReturnToFrontDesk_Click" /></div>


</div>