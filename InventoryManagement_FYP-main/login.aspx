<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous">
    <link href="/assets/css/signin.css" rel="stylesheet">
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet">
    <link rel="shortcut icon" type="image/ico" href="/assets/image/login.png" />
</head>

<body class="text-center">
    <form class="form-signin" runat="server">
        <h1 class="h3 mb-3 font-weight-normal">IT-assets Management</h1>

        <!-- Форма авторизации -->
        <asp:Panel ID="pnlLogin" runat="server" Visible="true">
            <asp:TextBox ID="txtUsrname" class="form-control" runat="server" placeholder="Username" required autofocus></asp:TextBox>
            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" class="form-control" placeholder="Password" required></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-lg btn-primary btn-block" Text="Login" OnClick="btnLogin_Click" />
            <asp:Button ID="btnShowRegister" runat="server" CssClass="btn btn-lg btn-secondary btn-block mt-2" Text="Create New Account" OnClick="btnShowRegister_Click" />
        </asp:Panel>

        <!-- Форма регистрации -->
        <asp:Panel ID="pnlRegister" runat="server" Visible="false">
            <asp:TextBox ID="txtRegUsrname" runat="server" CssClass="form-control mt-3" placeholder="New Username" required></asp:TextBox>
            <asp:TextBox ID="txtRegPass" runat="server" TextMode="Password" CssClass="form-control mt-2" placeholder="New Password" required></asp:TextBox>
            <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-lg btn-success btn-block mt-2" Text="Register" OnClick="btnRegister_Click" />
            <asp:Button ID="btnCancelRegister" runat="server" CssClass="btn btn-lg btn-secondary btn-block mt-2" Text="Cancel" OnClick="btnCancelRegister_Click" />
        </asp:Panel>

        <p class="mt-5 mb-3 text-muted">© 2024-2025 Vladimir Demin</p>
    </form>
</body>

</html>