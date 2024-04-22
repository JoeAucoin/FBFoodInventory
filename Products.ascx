<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Products.ascx.cs" Inherits="GIBS.Modules.FBFoodInventory.Products" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI.Skins" Assembly="Telerik.Web.UI.Skins" %>

<style type="text/css">
     .dnnFormItem.dnnFormHelp { margin-top: 2em; }
</style>
<script type="text/javascript" lang="javascript" >

 //   jQuery(function ($) { $("#<%= ClientID %>_txtCasePrice").mask("999.99"); });

    
</script>

<asp:Panel runat="server" ID="panelGrid" Visible="True">

<div>
 <asp:DropDownList ID="ddlFilterCategory" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlFilterCategory_SelectedIndexChanged"></asp:DropDownList>
        
        &nbsp;&nbsp;

    <asp:CheckBox ID="cbxShowInActive" runat="server" Text=" &nbsp;Show Inactive Products" 
        AutoPostBack="True" oncheckedchanged="cbxShowInActive_CheckedChanged"  />
</div>

<div><asp:Label ID="lblDebug" runat="server" CssClass="lead" /></div>
<asp:GridView ID="gvProducts" runat="server" 
     AllowPaging="True"
    PageSize="25"
     
    AllowSorting="True"
    PagerSettings-Position="Bottom" 
    OnPageIndexChanging="gvProducts_PageIndexChanging"
    OnRowEditing="gvProducts_RowEditing" 
    OnRowCommand="gvProducts_RowCommand" 
	OnRowDeleting="gvProducts_RowDeleting"
    DataKeyNames="ProductID"       
    AutoGenerateColumns="False" 
     GridLines="Horizontal"
    CellPadding="4" CellSpacing="1" BorderWidth="1px" 
     
    resourcekey="gvProducts">
    
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
             CommandArgument='<%# Eval("ProductID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>


        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonDelete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to inactivate this product?');"      
             CommandArgument='<%# Eval("ProductID") %>' 
             CommandName="Delete" runat="server"><asp:image ID="imgDelete" runat="server" imageurl="~/images/delete.gif" AlternateText="Delete Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>


<asp:BoundField HeaderText="Category" DataField="ProductCategory" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
        <asp:BoundField HeaderText="Product" DataField="ProductName" ></asp:BoundField>
        <asp:BoundField HeaderText="Case Count" DataField="CaseCount" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Case Weight (lbs.)" DataField="CaseWeight" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Case Price" DataField="CasePrice" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
        <asp:BoundField HeaderText="ProductCategoryID" DataField="ProductCategoryID" ItemStyle-HorizontalAlign="Center" Visible="False"></asp:BoundField>
        <asp:BoundField HeaderText="Created By" DataField="CreatedByUserName" Visible="False"></asp:BoundField>
         <asp:BoundField HeaderText="Limit" DataField="Limit" Visible="True" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Active" DataField="IsActive" Visible="True"></asp:BoundField>

    </Columns>
</asp:GridView>	

<div style="color: Blue; font-weight: bold">
            <br />
            <i><asp:Label ID="lblTotalRecordCount" runat="server" Text=""></asp:Label> Records<br />You are viewing page
                <%=gvProducts.PageIndex + 1%>
                of
                <%=gvProducts.PageCount%>
            </i>
        </div>


<div style="margin-top:15px;">
<asp:Button runat="server" Text="Button" ID="btnAddNew" ResourceKey="btnAddNew" CssClass="btn btn-lg btn-primary" 
    onclick="btnAddNew_Click" /></div>



</asp:Panel>


<asp:Panel runat="server" ID="panelEdit" Visible="False">

<div class="dnnForm" id="form-products">

    <asp:Label ID="lblFormMessage" runat="server" CssClass="dnnFormMessage dnnFormInfo" ResourceKey="lblFormMessage" Visible="False" />
    <div class="dnnFormItem dnnFormHelp dnnClear">
        <p class="dnnFormRequired"><asp:Label ID="lblRequiredIndicator" runat="server" ResourceKey="RequiredIndicator" /></p>
    </div>
    <fieldset>

        <div class="dnnFormItem">
           <dnn:Label runat="server" ID="lblProductName" ControlName="txtProductName" ResourceKey="lblProductName" Suffix=":" />
            <asp:TextBox runat="server" ID="txtProductName" CssClass="dnnFormRequired" MaxLength="100" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductName" ValidationGroup="grAddProduct" 
                CssClass="dnnFormMessage dnnFormError" ResourceKey="txtProductName.Required" />
        </div>
		
	
		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblCasePrice" ControlName="txtCasePrice" ResourceKey="lblCasePrice" Suffix=":" />
            <asp:TextBox runat="server" ID="txtCasePrice" Width="90px" Text="0" />
        </div>


		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblCaseCount" ControlName="txtCaseCount" ResourceKey="lblCaseCount" Suffix=":" />
            <asp:TextBox runat="server" ID="txtCaseCount" Width="90px" Text="0" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblCaseWeight" ControlName="txtCaseWeight" ResourceKey="lblCaseWeight" Suffix=":" />
            <asp:TextBox runat="server" ID="txtCaseWeight" Width="90px" Text="0" />
        </div>
        

		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblLimit" ControlName="ddlLimit" ResourceKey="lblLimit" Suffix=":" />
            <asp:DropDownList ID="ddlLimit" runat="server">
                <asp:ListItem Text="0" Value="0" />
                <asp:ListItem Text="1" Value="1" />
                <asp:ListItem Text="2" Value="2" />
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="4" Value="4" />
                <asp:ListItem Text="5" Value="5" />
                 <asp:ListItem Text="6" Value="6" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlLimit" runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="grAddProduct" ControlToValidate="ddlLimit" InitialValue="0" ErrorMessage="Required"></asp:RequiredFieldValidator>
        </div>

		<div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblProductCategory" ControlName="ddlProductCategory" ResourceKey="lblProductCategory" Suffix=":" />
            <asp:DropDownList ID="ddlProductCategory" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ForeColor="Red" ValidationGroup="grAddProduct" ControlToValidate="ddlProductCategory" InitialValue="0" ErrorMessage="Required"></asp:RequiredFieldValidator>
        </div>
        <div class="dnnFormItem">
          <dnn:Label runat="server" ID="lblIsActive" ControlName="rblIsActive" ResourceKey="lblIsActive" Suffix=":" />
                <asp:RadioButtonList ID="rblIsActive" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="True" Value="True" Selected="True" />
                <asp:ListItem Text="False" Value="False" />
                </asp:RadioButtonList>
        </div>
	
		
    </fieldset>
    <asp:HiddenField ID="txtProductID" runat="server" />

    <ul class="dnnActions dnnClear">
        <li><asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" ValidationGroup="grAddProduct" 
                ResourceKey="btnSave" onclick="btnSave_Click" /></li>
        <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-warning" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to inactivate this product?');" 
                ResourceKey="btnDelete" onclick="btnDelete_Click" /></li>

        <li><asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-default" CausesValidation="False" 
                ResourceKey="btnCancel" onclick="btnCancel_Click" /></li>
    </ul>

</div>



<!--- <h2>Translations</h2> --->
    <fieldset>

        <div class="dnnFormItem">
             <dnn:Label runat="server" ID="lblAddLanguage" ControlName="ddlLanguage" Visible="false" ResourceKey="lblAddLanguage" Suffix=":" />
				 <asp:DropDownList ID="ddlLanguage" runat="server" Visible="false"></asp:DropDownList>
            </div>

        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblTranslation" ControlName="txtTranslation" Visible="false" ResourceKey="lblTranslation" Suffix=":" />
            <asp:TextBox ID="txtTranslation" runat="server" Visible="false" MaxLength="100"></asp:TextBox>
        </div>
        </fieldset>

        <ul class="dnnActions dnnClear" style="float:right;">
        <li><asp:LinkButton ID="btnSaveTranslation" runat="server" Visible="false" CssClass="dnnPrimaryAction" 
                ResourceKey="btnSaveTranslation" OnClick="btnSaveTranslation_Click" /></li>
        <li><asp:LinkButton ID="btnCancelTranslation" runat="server" Visible="false" CssClass="dnnSecondaryAction" CausesValidation="False" 
        ResourceKey="btnCancelTranslation" OnClick="btnCancelTranslation_Click"/></li>
    </ul>

    	<asp:GridView ID="gvLanguages" runat="server" Visible="false" 
    DataKeyNames="ProductCategoryID" OnRowEditing="gvLanguages_RowEditing"
    
    AutoGenerateColumns="False" 
    GridLines="Horizontal" 
     CssClass="dnnGrid"
    resourcekey="gvLanguages">

    <AlternatingRowStyle cssclass="dnnGridAltItem" />
    <FooterStyle cssclass="dnnGridFooter" />
    <HeaderStyle cssclass="dnnGridHeader" />
   
    <RowStyle cssclass="dnnGridItem" />
   
    <Columns>
               
        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("ProductCategoryID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

        <asp:BoundField HeaderText="Language" DataField="LanguageCode" ItemStyle-Font-Bold="True"></asp:BoundField>
         <asp:BoundField HeaderText="Translation" DataField="ProductName" Visible="True"></asp:BoundField>

    </Columns>
</asp:GridView>	


</asp:Panel>


<div style="text-align:right"><asp:Button ID="btnReturnToFrontDesk" resourcekey="btnReturnToFrontDesk" CausesValidation="False" runat="server" CssClass="btn btn-default" 
                Text="Return" onclick="btnReturnToFrontDesk_Click" /></div>