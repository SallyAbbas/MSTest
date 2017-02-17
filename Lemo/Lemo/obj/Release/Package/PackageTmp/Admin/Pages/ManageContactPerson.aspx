<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageContactPerson.aspx.cs" Inherits="Lemo.Admin.Pages.ManageContactPerson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; padding: 10px;">
        <table width="100%">
        <tr><td>            
                <asp:ImageButton runat="server" ID="butAddNewContactPerson123" ImageUrl="~/Image/AddcontactPerson.png" OnClick="butSignUp_Click" />     
        </td></tr>
            <tr>
                <td align="center">
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        AllowPaging="true" PageSize="10" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" GridLines="Vertical" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFirstName" Text='<%# Eval("FirstName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPhone" Text='<%# Eval("Phone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEmail" Text='<%# Eval("Email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCityName" Text='<%# Eval("Job") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button Text="Edit" runat="server" ID="butEdit" CommandName='<%# Eval("ContactPersonID") %>'
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
