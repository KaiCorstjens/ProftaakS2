<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TramBeheerSysteemASP.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
       
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="Gebruiker" Value="Gebruiker"></asp:MenuItem>
                <asp:MenuItem Text="Tram" Value="Tram">
                    <asp:MenuItem Text="Voeg toe" Value="Voeg toe"></asp:MenuItem>
                    <asp:MenuItem Text="Wijzig status" Value="Wijzig status"></asp:MenuItem>
                    <asp:MenuItem Text="Tram info" Value="Tram info"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Spoor" Value="Spoor">
                    <asp:MenuItem Text="Status veranderen" Value="Status veranderen"></asp:MenuItem>
                    <asp:MenuItem Text="Spoor info" Value="Spoor info"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Onderhoud" Value="Onderhoud"></asp:MenuItem>
                <asp:MenuItem Text="Onderhoudlijsten" Value="Onderhoudlijsten"></asp:MenuItem>
            </Items>
        </asp:Menu>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                     <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                     <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
                     <br />
                     <asp:Panel ID="PanelTBS" runat="server" BorderStyle="None">
                         <table>
                         <tr>
                             <td style="vertical-align: top;">
                                 <asp:Table ID="TableTrack1" runat="server" BorderColor="Black" BorderStyle="Solid" Visible="False">
                         </asp:Table>
                             </td>
                             <td style="vertical-align: top;">
                                 <asp:Table ID="TableTrack2" runat="server" BorderColor="Black" BorderStyle="Solid" Visible="False">
                         </asp:Table>
                             </td>
                             <td style="vertical-align: top;">
                                 <asp:Table ID="TableTrack3" runat="server" BorderColor="Black" BorderStyle="Solid" Visible="False">
                         </asp:Table>
                             </td>
                             </table>
                         </tr>
                     </asp:Panel>
                </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
