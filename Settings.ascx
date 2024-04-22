<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.FBFoodInventory.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>



<div class="dnnForm" id="form-settings">
    <fieldset>
         <div class="dnnFormItem">
<dnn:label id="lblGoogleTranslateAPIKey" runat="server" suffix=":" controlname="txtGoogleTranslateAPIKey"></dnn:label>
 <asp:textbox id="txtGoogleTranslateAPIKey" maxlength="150" runat="server" />
             </div>
         <div class="dnnFormItem">
<dnn:label id="lblTemplate" runat="server" controlname="txtTemplate" suffix=":"></dnn:label>
<asp:textbox id="txtTemplate" cssclass="NormalTextBox"  columns="30" textmode="MultiLine" rows="10" maxlength="2000" runat="server" />
             </div>
    </fieldset>
</div>


