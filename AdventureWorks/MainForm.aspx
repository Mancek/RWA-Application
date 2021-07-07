<%@ Page Title="" Language="C#" MasterPageFile="~/MainForm.Master" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="AdventureWorks.MainForm1" EnableSessionState="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.addEventListener('DOMContentLoaded', () => {
            const inputs = document.querySelectorAll('input[type=text]');
            const td = document.querySelectorAll('td');

            td.forEach(td => td.classList.add('align-middle'));
            inputs.forEach(input => input.classList.add('form-control'));

            document.querySelector('#ddlSviGradovi').classList.add('form-control');

            document.querySelector('#RegularExpressionValidator1').style.display = 'none';
            document.querySelector('#RequiredFieldValidator1').style.display = 'none';
            document.querySelector('#RequiredFieldValidator2').style.display = 'none';
            document.querySelector('#RequiredFieldValidator3').style.display = 'none';

        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex mb-3">
        <asp:DropDownList runat="server" ID="ddlDrzava" CssClass="dropdown-header me-2" OnSelectedIndexChanged="ddlDrzava_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
        <asp:DropDownList runat="server" ID="ddlGrad" CssClass="dropdown-header" OnSelectedIndexChanged="ddlGrad_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
    </div>
    <asp:GridView runat="server" ID="gvKupci" CssClass="table table-sm" AllowPaging="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnRowEditing="gvKupci_RowEditing" OnRowCancelingEdit="gvKupci_RowCancelingEdit" OnPageIndexChanging="gvKupci_PageIndexChanging" OnRowUpdating="gvKupci_RowUpdating" DataKeyNames="IDKupac" AllowSorting="True" OnSorting="gvKupci_Sorting" PageSize="10" OnSelectedIndexChanging="gvKupci_SelectedIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="Ime" SortExpression="Ime">
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Ime") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Name is required field" ControlToValidate="txtName" runat="server" ClientIDMode="Static" Text="*" ID="RequiredFieldValidator1"/>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Ime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prezime" SortExpression="Prezime">
                <EditItemTemplate>
                    <asp:TextBox ID="txtPrezime" runat="server" Text='<%# Bind("Prezime") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Lastname is required field" ControlToValidate="txtPrezime" runat="server" ClientIDMode="Static" Text="*" ID="RequiredFieldValidator2"/>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Prezime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="E-mail">
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                    <asp:RegularExpressionValidator ControlToValidate="txtEmail" Text="*" ID="RegularExpressionValidator1" ClientIDMode="Static" runat="server" ErrorMessage="Invalid e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Email", "<a href=mailto:{0}>{0}</a>") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Telefon">
                <EditItemTemplate>
                    <asp:TextBox ID="txtTelefon" runat="server" Text='<%# Bind("Telefon") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Number is required field" ControlToValidate="txtTelefon" runat="server" ClientIDMode="Static" Text="*" ID="RequiredFieldValidator3"/>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Telefon") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Grad">
                <EditItemTemplate>
                    <asp:DropDownList runat="server" ID="ddlSviGradovi" ClientIDMode="Static">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Grad") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CssClass="btn btn-sm btn-primary" CommandName="Update" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CssClass="btn btn-sm btn-secondary" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-sm btn-secondary" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-primary" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#999999" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <div>
        <asp:DropDownList runat="server" ID="ddlPageSize" CssClass="dropdown-header" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged1">
            <asp:ListItem Text="10" Selected="True"/>
            <asp:ListItem Text="20" />
            <asp:ListItem Text="30" />
            <asp:ListItem Text="40" />
            <asp:ListItem Text="50" />
        </asp:DropDownList>
    </div>
    <div>
        <asp:ValidationSummary runat="server" />
    </div>
</asp:Content>
