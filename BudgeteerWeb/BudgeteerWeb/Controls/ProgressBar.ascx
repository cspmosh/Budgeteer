<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ProgressBar.ascx.vb" Inherits="BudgeteerWeb.ProgressBar" %>
<div ID="_progressBarBackground" runat="server">    
    <div ID="_progressBarHighlight" runat="server" style="z-index: 1; position: absolute; background-image:url('/Images/ProgressBarHighlight.png');"></div>    
    <asp:label ID="lblProgress" runat="server" style="z-index: 0; position: relative; text-align:right;"></asp:label>
</div>
    
