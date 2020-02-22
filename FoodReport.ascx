<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FoodReport.ascx.cs" Inherits="GIBS.Modules.FBFoodInventory.FoodReport" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FBFoodInventory/Style.css" />
<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css" />

<script type="text/javascript">

    $(function () {
        $("#txtStartDate").datepicker({
            numberOfMonths: 2,
            showButtonPanel: false,
            showCurrentAtPos: 1
        });
        $("#txtEndDate").datepicker({
            numberOfMonths: 2,
            showButtonPanel: false,
            showCurrentAtPos: 1
        });
    });

 </script>

 <div style="position:relative;float:right;padding-right:30px;">
    <asp:Button ID="btnRunReport" runat="server" ResourceKey="btnRunReport" onclick="btnRunReport_Click" CssClass="dnnPrimaryAction" /> 
     
     </div>          
<div class="dnnForm" id="form-demo">
    <fieldset>


		<div class="dnnFormItem">
            <dnn:Label ID="lblStartDate" runat="server" CssClass="dnnFormLabel" AssociatedControlID="txtStartDate" Text="Start Date" />
            <asp:TextBox ID="txtStartDate" runat="server" ClientIDMode="Static" /><asp:RequiredFieldValidator runat="server" id="reqStartDate" controltovalidate="txtStartDate" ResourceKey="RequiredStartDate" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblEndDate" runat="server" CssClass="dnnFormLabel" AssociatedControlID="txtEndDate" Text="Start Date" />
            <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" /><asp:RequiredFieldValidator runat="server" id="reqEndDate" controltovalidate="txtEndDate" ResourceKey="RequiredEndDate" />
        </div>
        <div class="dnnFormItem">
		<dnn:Label runat="server" ID="lblGroupBy" ControlName="rblGroupBy" ResourceKey="lblGroupBy" />
		<asp:RadioButtonList ID="rblGroupBy" runat="server" RepeatDirection="Horizontal" CssClass="rbl">
<asp:ListItem Text="Supplier Name" Value="SupplierName" Selected="True"></asp:ListItem>
<asp:ListItem Text="Product Category" Value="ProductCategory"></asp:ListItem>
<asp:ListItem Text="Report Type" Value="ReportType"></asp:ListItem>
<asp:ListItem Text="Supplier & Category" Value="both"></asp:ListItem>
</asp:RadioButtonList>
		
		</div>
		
    </fieldset>
</div>	

   
   <br clear="all" />




<asp:GridView ID="gv_ClientDetails" runat="server"          
    AutoGenerateColumns="False" CssClass="mGridFlex"  
    resourcekey="gv_ClientDetails" DataKeyNames="LineItemID" EnableViewState="False" OnSorting="gv_ClientDetails_Sorting" >
<AlternatingRowStyle CssClass="alt" />    
<PagerStyle CssClass="pgr" />  
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>

      
       <asp:BoundField HeaderText="ProductCategory" DataField="ProductCategory" SortExpression="ProductCategory" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Supplier Name" DataField="SupplierName" SortExpression="SupplierName" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
		<asp:BoundField HeaderText="Product Name" DataField="ProductName" SortExpression="ProductName" ItemStyle-VerticalAlign="Top" ></asp:BoundField>		
        <asp:BoundField HeaderText="ReportType" DataField="ReportType" SortExpression="ReportType" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

        <asp:BoundField HeaderText="Cases" DataField="Cases" SortExpression="Cases"  ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
		<asp:BoundField HeaderText="PricePerCase" DataField="PricePerCase" SortExpression="PricePerCase" DataFormatString="{0:c}" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
		


        <asp:BoundField HeaderText="Weight Per Case" DataField="WeightPerCase" SortExpression="WeightPerCase" ItemStyle-VerticalAlign="Top" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Total Weight" DataField="TotalProductWeight" SortExpression="TotalProductWeight" ItemStyle-VerticalAlign="Top" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Total Cost" DataField="TotalProductCost" SortExpression="TotalProductCost" DataFormatString="{0:c}" ItemStyle-VerticalAlign="Top" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
        
    </Columns>
</asp:GridView>	