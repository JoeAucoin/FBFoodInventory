<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Suppliers.ascx.cs" Inherits="GIBS.Modules.FBFoodInventory.Suppliers" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<script type="text/javascript" language="javascript" >

    //jQuery(function ($) { $("#<%= ClientID %>_txtSupplierPhone").mask("(999) 999-9999? x99999"); });
    jQuery(function ($) { $("#<%= txtSupplierPhone.ClientID %>").mask("(999) 999-9999? x99999"); });
   
    jQuery(function ($) { $("#<%= ClientID %>_txtSalesmanPhone").mask("(999) 999-9999? x99999"); });
</script>


<div class="dnnForm" id="form-supplier">


<asp:Panel runat="server" ID="panelGrid" Visible="True">

    <asp:CheckBox ID="cbxShowInActive" runat="server" Text=" &nbsp;Show Inactive Suppliers" 
        AutoPostBack="True" oncheckedchanged="cbxShowInActive_CheckedChanged"  />

<asp:GridView ID="gvSuppliers" runat="server" 
    DataKeyNames="SupplierID" OnRowEditing="gvSuppliers_RowEditing"     
    AutoGenerateColumns="False" 
    GridLines="Horizontal"
    CellPadding="4" CellSpacing="1" BorderWidth="1px" 
    CssClass="dnnGrid" 
    resourcekey="gvSuppliers">
        <AlternatingRowStyle cssclass="dnnGridAltItem" />
    <FooterStyle cssclass="dnnGridFooter" />
    <HeaderStyle cssclass="dnnGridHeader" />
    <PagerStyle cssclass="dnnGridPager" />
    <RowStyle cssclass="dnnGridItem" />
    <PagerStyle HorizontalAlign="Left"  BackColor="DodgerBlue" Height="20px" Font-Size="Small" ForeColor="Snow" />  
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>
        
       <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("SupplierID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>        
        
        

        <asp:BoundField HeaderText="Name" DataField="SupplierName"></asp:BoundField>
        <asp:BoundField HeaderText="Phone" DataField="SupplierPhone"></asp:BoundField>
        <asp:BoundField HeaderText="Salesman" DataField="Salesman"></asp:BoundField>
        <asp:BoundField HeaderText="SalesmanPhone" DataField="SalesmanPhone"></asp:BoundField>
        <asp:BoundField HeaderText="Created By" DataField="CreatedByUserName"></asp:BoundField>
        <asp:BoundField HeaderText="Active" DataField="IsActive" Visible="True"></asp:BoundField>
    </Columns>
</asp:GridView>	
    <ul class="dnnActions dnnClear">
        <li><asp:LinkButton ID="btnAddNewRecord" runat="server" CssClass="dnnPrimaryAction" 
                ResourceKey="btnAddNewRecord" onclick="btnAddNewRecord_Click" /></li>
       
    </ul>
</asp:Panel>



<asp:Panel runat="server" ID="panelEdit" Visible="False">



    <asp:Label ID="lblFormMessage" runat="server" CssClass="dnnFormMessage dnnFormInfo" ResourceKey="lblFormMessage" Visible="False" />
    <div class="dnnFormItem dnnFormHelp dnnClear">
        <p class="dnnFormRequired"><asp:Label ID="lblRequiredIndicator" runat="server" ResourceKey="Required Indicator" /></p>
    </div>
    <fieldset>

        <div class="dnnFormItem">
           <dnn:Label runat="server" ID="lblSupplierName" ControlName="txtSupplierName" ResourceKey="lblSupplierName" />
            <asp:TextBox runat="server" ID="txtSupplierName" CssClass="dnnFormRequired" Width="200px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSupplierName" 
                CssClass="dnnFormMessage dnnFormError" ResourceKey="SupplierName.Required" />
        </div>
		
		
		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblAddress" ControlName="txtAddress" ResourceKey="lblAddress" />
            <asp:TextBox runat="server" ID="txtAddress" Width="150px" />
        </div>
		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblCityStateZip" ControlName="txtCity" ResourceKey="lblCityStateZip" />
            <asp:TextBox runat="server" ID="txtCity" Width="120px" /> <asp:DropDownList ID="ddlState" runat="server" Width="120px"></asp:DropDownList> <asp:TextBox runat="server" ID="txtZip" Width="60px" />
        </div>		

		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblSupplierPhone" ControlName="txtSupplierPhone" ResourceKey="lblSupplierPhone" />
            <asp:TextBox runat="server" ID="txtSupplierPhone" Width="150px" />
        </div>

		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblSalesman" ControlName="txtSalesman" ResourceKey="lblSalesman" />
            <asp:TextBox runat="server" ID="txtSalesman" Width="150px" />
        </div>

		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblSalesmanPhone" ControlName="txtSalesmanPhone" ResourceKey="lblSalesmanPhone" />
            <asp:TextBox runat="server" ID="txtSalesmanPhone" Width="150px" />
        </div>		

       <div class="dnnFormItem">
          <dnn:Label runat="server" ID="lblIsActive" ControlName="rblIsActive" ResourceKey="lblIsActive" Suffix=":" />
                <asp:RadioButtonList ID="rblIsActive" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="True" Value="True" Selected="True" />
                <asp:ListItem Text="False" Value="False" />
                </asp:RadioButtonList>
        </div>
		
    </fieldset>
    <asp:HiddenField ID="txtSupplierID" runat="server" />

    <ul class="dnnActions dnnClear">
        <li><asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" 
                ResourceKey="Save" onclick="btnSave_Click" /></li>
        <li><asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-default" CausesValidation="False" 
        ResourceKey="btnCancel" onclick="btnCancel_Click" /></li>
    </ul>



</asp:Panel>


<div style="text-align:right"><asp:Button ID="btnReturnToFrontDesk" resourcekey="btnReturnToFrontDesk" CausesValidation="False" runat="server" CssClass="btn btn-default" 
                Text="Return" onclick="btnReturnToFrontDesk_Click" /></div>

</div>