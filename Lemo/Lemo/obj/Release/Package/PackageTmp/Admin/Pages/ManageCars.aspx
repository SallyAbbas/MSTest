<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageCars.aspx.cs" Inherits="Lemo.Admin.Pages.ManageCars" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="text-align: center; padding: 10px;">
        <table width="100%">
        <tr><td>            
                <asp:ImageButton runat="server" ID="butAddNewcar12" ImageUrl="~/Image/AddNewCar.png" OnClick="butAddNewcar_Click" />       
        </td></tr>
            <tr>
                <td align="center">
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        AllowPaging="true" PageSize="10" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" GridLines="Vertical" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>                            
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblName" Text='<%# Eval("CarName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price per Hour">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPriceHour" Text='<%# Eval("PricePerHore") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Price per Mile">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPriceMile" Text='<%# Eval("PricePerMaile") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No Passengers">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNoPassengers" Text='<%# Eval("NoPassengers") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button Text="Edit" runat="server" ID="butEdit" CommandName='<%# Eval("CarID") %>'
                                        OnClick="butEdit_onClick123" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
