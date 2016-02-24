<%@ Page Title="" Language="C#" MasterPageFile="~/PowerBI.Master" AutoEventWireup="true" CodeBehind="ReportTime.aspx.cs" Inherits="PowerBIApplication.ReportTime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/JS/FrameManagerReport.js"></script>       
    <input id="accessTokenTextbox" type="hidden" value="<%= authResult.AccessToken %>" />
    <input id="reportId" type="hidden" value="<%= reportId %>" />    
    <div> 
        <asp:Panel ID="PanelEmbed" runat="server">                        
            <table>                                                           
                    <tr>
                        <td>
                            <iframe ID="iFrameEmbedReport" src="" height="768px" width="1024px" frameborder="1" seamless></iframe>
                        </td>
                    </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
