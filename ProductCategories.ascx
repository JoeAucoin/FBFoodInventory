<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductCategories.ascx.cs" Inherits="GIBS.Modules.FBFoodInventory.ProductCategories" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm" id="form-supplier">  


<asp:Panel runat="server" ID="panelGrid" Visible="True">

    <asp:CheckBox ID="cbxShowInActive" runat="server" Text=" &nbsp;Show Inactive Categories" 
        AutoPostBack="True" oncheckedchanged="cbxShowInActive_CheckedChanged"  />

<asp:GridView ID="gvProductCategory" runat="server" 
    DataKeyNames="ProductCategoryID" OnRowEditing="gvProductCategory_RowEditing" 
    
         AllowPaging="True"
    PageSize="20"
     
    AllowSorting="True"
    
    OnPageIndexChanging="gvProducts_PageIndexChanging" 
    AutoGenerateColumns="False" 
    GridLines="Horizontal" 
     CssClass="dnnGrid"
    resourcekey="gvProductCategory">

    <AlternatingRowStyle cssclass="dnnGridAltItem" />
    <FooterStyle cssclass="dnnGridFooter" />
    <HeaderStyle cssclass="dnnGridHeader" />
    <PagerStyle cssclass="dnnGridPager" />
    <RowStyle cssclass="dnnGridItem" />
    <PagerStyle HorizontalAlign="Left"  BackColor="DodgerBlue" Height="20px" Font-Size="Small" ForeColor="Snow" />  
<PagerSettings Mode="NumericFirstLast" Position="Bottom" /> 
    
    
    <Columns>
        
        
        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("ProductCategoryID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>





        <asp:BoundField HeaderText="Category" DataField="ProductCategory" ItemStyle-Font-Bold="True"></asp:BoundField>
         <asp:BoundField HeaderText="Active" DataField="IsActive" Visible="True"></asp:BoundField>
        
        <asp:BoundField HeaderText="Last Modified" DataField="LastModifiedOnDate" dataformatstring="{0:g}" HtmlEncode="False" ></asp:BoundField>
         <asp:BoundField HeaderText="By" DataField="LastModifiedByUserName"></asp:BoundField>
        
        

    </Columns>
</asp:GridView>	

<div style="color: Blue; font-weight: bold">
            <br />
            <i>
                <asp:Label ID="lblTotalRecordCount" runat="server" Text=""></asp:Label> Records<br />You are viewing page
                <%=gvProductCategory.PageIndex + 1%>
                of
                <%=gvProductCategory.PageCount%>
            </i>
</div>


<div style="margin-top:15px;">
<asp:Button runat="server" Text="Button" ID="btnAddNew" ResourceKey="btnAddNew" CssClass="dnnPrimaryAction" 
    onclick="btnAddNew_Click" /></div>

</asp:Panel>



<asp:Panel runat="server" ID="panelEdit" Visible="False">


    <asp:Label ID="lblFormMessage" runat="server" CssClass="dnnFormMessage dnnFormInfo" ResourceKey="lblFormMessage" Visible="False" />
    <div class="dnnFormItem dnnFormHelp dnnClear">
        <p class="dnnFormRequired"><asp:Label ID="lblRequiredIndicator" runat="server" ResourceKey="Required Indicator" Visible="False" /></p>
    </div>
    <fieldset>

        <div class="dnnFormItem">
           <dnn:Label runat="server" ID="lblProductCategory" ControlName="txtProductCategory" ResourceKey="lblProductCategory" />
            <asp:TextBox runat="server" ID="txtProductCategory" CssClass="dnnFormRequired" Width="200px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductCategory" 
                CssClass="dnnFormMessage dnnFormError" ResourceKey="ProductCategory.Required" />
        </div>

        <div class="dnnFormItem">
          <dnn:Label runat="server" ID="lblIsActive" ControlName="rblIsActive" ResourceKey="lblIsActive" Suffix=":" />
                <asp:RadioButtonList ID="rblIsActive" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="True" Value="True" />
                <asp:ListItem Text="False" Value="False" />
                </asp:RadioButtonList>
        </div>
		
    </fieldset>
    <asp:HiddenField ID="txtProductCategoryID" runat="server" />

    <ul class="dnnActions dnnClear">
        <li><asp:LinkButton ID="btnSave" runat="server" CssClass="dnnPrimaryAction" 
                ResourceKey="Save" onclick="btnSave_Click" /></li>
        <li><asp:LinkButton ID="btnCancel" runat="server" CssClass="dnnSecondaryAction" CausesValidation="False" 
        ResourceKey="btnCancel" onclick="btnCancel_Click" /></li>
    </ul>
</asp:Panel>


<div style="text-align:right"><asp:Button ID="btnReturnToFrontDesk" CausesValidation="False" resourcekey="btnReturnToFrontDesk" runat="server" CssClass="dnnSecondaryAction" 
                Text="Return" onclick="btnReturnToFrontDesk_Click" /></div>


</div>