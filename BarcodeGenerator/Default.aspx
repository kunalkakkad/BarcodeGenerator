<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BarcodeGenerator.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Printing Barcode Labels to Thermal Printers from ASP.NET</title>

    <style type="text/css">
        #pdf {
            width: 600px;
            height: 400px;
            border: 5px solid #ccc;
        }

        h3 {
            font-family: Sans-Serif, Arial, Tahoma;
        }

        fieldset {
            font-family: Sans-Serif, Arial, Tahoma;
            font-size: 16px;
        }
    </style>

</head>
<body>
    <form id="Form2" runat="server">
        <h3>Generate Barcode and print</h3>

        <table>
            <tr>
                <td valign="top">
                    <fieldset>
                        <legend>Fill the form:</legend>
                        <br />
                        Product Name:
                        <asp:TextBox ID="TextBox1" runat="server" Text="Sample Product"></asp:TextBox>
                        <%--<br />
                        Product Code:
                        <asp:TextBox ID="TextBox2" runat="server" Text="ABCD12345"></asp:TextBox>
                        <br />--%>
                        <%--Printer DPI:
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Text="203" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="300"></asp:ListItem>
                            <asp:ListItem Text="600"></asp:ListItem>
                        </asp:DropDownList>--%>
                        <%--<br />--%>
                        <br />
                        <%--<asp:Button ID="Button1" runat="server" Text="Refresh Preview" />--%>
                        <asp:Button ID="PrintBtn" runat="server" Text="Print" />
                    </fieldset>
                </td>
                <td>
                    <%--<div id="pdf">
                        <asp:Literal ID="pdfViewer" runat="server"></asp:Literal>
                    </div>--%>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
