<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageCompany.aspx.cs" Inherits="Lemo.Admin.Pages.ManageCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; padding: 10px;">
        <table width="100%">
            <tr>
            <td><asp:Label runat="server" ID="Label1" Text="Manage Companies" CssClass="TitleInnerPage"></asp:Label></td>
                </tr><tr>
                <td align="center">
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        AllowPaging="true" PageSize="10" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" GridLines="Vertical" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCompany" Text='<%# Eval("CompanyName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStateName" Text='<%# Eval("stateName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCityName" Text='<%# Eval("CityName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Available">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="cbISAvaliable" Checked='<%# (bool) Eval("IsAvailable") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Active">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="cbISActive" Checked='<%# (bool) Eval("IsActive") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Top">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="cbISTop" Checked='<%# (bool) Eval("IsTop") %>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button Text="Edit" runat="server" ID="butEdit" CommandName='<%# Eval("CompanyID") %>'
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
