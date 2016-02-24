<%@ Page Title="" Language="C#" MasterPageFile="~/PowerBI.Master" AutoEventWireup="true" CodeBehind="TimeMetrics.aspx.cs" Inherits="PowerBIApplication.TimeMetrics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="/JS/FrameManager.js"></script>       
    <input id="accessTokenTextbox" type="hidden" value="<%= authResult.AccessToken %>" />
    <asp:HiddenField ID="hfMetricTimeDashboardId" runat="server" />
    <div class="row">                        
        <div class="col-xs-12"><strong>Métrica: </strong><span><asp:DropDownList ID="ddlTiles" runat="server" OnSelectedIndexChanged="ddlTiles_SelectedIndexChanged"></asp:DropDownList></span></div>
        <%--<div class="col-md-2"> </div>--%>
    </div>    
    <div class="row">        
        <div class="col-xs-12">
            <iframe ID="iFrameEmbedTile" src="" height="500px" width="500px" frameborder="0" seamless></iframe>
        </div>
        <%--<div class="col-md-2"></div>--%>
    </div>
    
</asp:Content>
