<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogInOrSignup.aspx.cs" Inherits="FWebsite.LogInOrSignup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1> please log in</h1>
    <h2>if you dont have an account we will register with the username and password you enterd</h2>
    <form id="form1" runat="server" style="padding:20px">
        <div>
             <label for="myfile">user name:</label>
        <input type="text"  name="username" />
        </div>
        <br />
        <br />
        <div>
             <label for="myfile">password:</label>
        <input type="text"  name="pass" />
        </div>
        <br /> 
        <input type="submit" name="LogIn" value="log in" />     
        <input type="submit" name="SignUp" value="sign up" />
        <br />
        <%=message %>
    </form>
</body>
</html>
