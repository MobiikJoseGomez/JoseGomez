<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Elementos.aspx.cs" Inherits="PBIWebApp.Elementos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Power BI</h1>
    <br/>
    <div>
        <h2>Seleccione tipo de elemento a visualizar</h2>
        <hr/>
    </div>
    <div>        
         <asp:RadioButtonList ID="rblElementos" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblElementos_SelectedIndexChanged">
             <asp:ListItem Text="Iconos" Value="icon" Selected="True"></asp:ListItem>
             <asp:ListItem Text="Reportes" Value="report"></asp:ListItem>
         </asp:RadioButtonList>
    </div>
    <input id="accessTokenTextbox" type="hidden" runat="server" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div ID="divIcons" runat="server" Visible="True">                    
                    <div>
                        Seleccione un Dashbords: <asp:DropDownList ID="ddlDasbords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDasbords_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div>
                        Seleccione un ícono: <asp:DropDownList ID="ddlIconos" runat="server"></asp:DropDownList>
                    </div>
                    <!-- Embed Tile-->  
                    <div> 
                    <asp:Panel ID="PanelEmbedTile" runat="server" Visible="true">                    
                        <table>                        
                        
                            <tr>
                                <td>
                                    <iframe ID="iFrameEmbedTile" src="" height="500px" width="500px" frameborder="0" seamless></iframe>
                                </td>
                            </tr>                        

                    </table>
                    </asp:Panel>
            

                </div>
            </div>
            <div ID="divReports" runat="server" Visible="False">
                <div>
                    Seleccione un reporte: <asp:DropDownList ID="ddlReports" runat="server"></asp:DropDownList>
                </div>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblElementos" EventName="SelectedIndexChanged"/>
            <asp:AsyncPostBackTrigger ControlID="ddlDasbords" EventName="SelectedIndexChanged"/>
        </Triggers>
        
    </asp:UpdatePanel>
</asp:Content>
