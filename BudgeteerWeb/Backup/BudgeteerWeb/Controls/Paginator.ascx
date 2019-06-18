<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Paginator.ascx.vb" Inherits="BudgeteerWeb.Paginator" %>
<asp:Table id="paginator1" runat="server">
    <asp:TableRow>
        <asp:TableCell>
            <asp:Table ID="innerTable" runat="server" >
                <asp:TableRow>                     
                    <asp:TableCell><asp:ImageButton ID="imgFirst" runat="server" onClick="firstPage"/></asp:TableCell>
                    <asp:TableCell style="padding-right: 10px;"><asp:LinkButton ID="btnFirst" runat="server" Text="&lt;&lt; First" onClick="firstPage" /></asp:TableCell>
                    <asp:TableCell><asp:ImageButton ID="imgPrevious" runat="server" onClick="previousPage" /></asp:TableCell>
                    <asp:TableCell style="padding-right: 10px;"><asp:LinkButton ID="btnPrevious" runat="server" Text="&lt; Previous" onClick="previousPage" /></asp:TableCell>
                    <asp:TableCell id="pageCounter" runat="server" style="padding-left: 10px; padding-right: 10px;">
                        <asp:Label ID="Label1" runat="server" Text="0 of 0"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell style="padding-left: 10px;"><asp:LinkButton ID="btnNext" runat="server" Text="Next &gt;" onClick="nextPage" /></asp:TableCell>
                    <asp:TableCell><asp:ImageButton ID="imgNext" runat="server" onClick="nextPage" /></asp:TableCell>
                    <asp:TableCell style="padding-left: 10px;"><asp:LinkButton ID="btnLast" runat="server" onClick="lastPage" Text="Last &gt;&gt;" /></asp:TableCell>
                    <asp:TableCell><asp:ImageButton ID="imgLast" runat="server" onClick="lastPage" /></asp:TableCell>
                </asp:TableRow>                 
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
    
</asp:Table>  

