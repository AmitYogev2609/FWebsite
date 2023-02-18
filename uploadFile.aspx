<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadFile.aspx.cs" Inherits="FWebsite.uploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
</style>
</head>
<body style="padding:5px 10px 0px 5px">
     <h1>Upload file</h1>
  
    <form id="form1" runat="server" enctype="multipart/form-data">
        <input type="submit" name="logout" value="log out" id="log out" />
        <br />
         <label for="myfile">Select a file:</label>
        <input type="file" id="myfile" name="myfile" />
        <br />
        <br />
      <input type="submit" value="subbmit" name="a" />
        <br />
        <%=messege %>
    </form>
    <br />
    <br />
    <table style="width:50%">
        <tr>
            <th>Name</th>
            <th>Upload Date</th>
            <th>Type</th>
        </tr>
        <%=fileTable %>
    </table>
</body>
</html>
