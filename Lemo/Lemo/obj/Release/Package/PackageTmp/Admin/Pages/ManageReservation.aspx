<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageReservation.aspx.cs" Inherits="Lemo.Admin.Pages.ManageReservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; padding: 10px;">
        <table width="100%">
            <tr>
                <td align="center">
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        AllowPaging="true" PageSize="10" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Comapny  Confirmtion">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblComapnyConfirmNum" Text='<%# Eval("ComapnyConfirmNum") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Price">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblTotalPrice" Text='<%# Eval("TotalPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button Text="Edit" runat="server" ID="butEdit" CommandName='<%# Eval("JobID") %>'
                                        OnClick="butEdit_onClick" />
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
