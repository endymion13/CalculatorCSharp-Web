<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCalculator.aspx.cs" Inherits="Calculator.frmCalculator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calculator</title>
    <script>
    function validateInput(e){
       key = e.keyCode || e.which;
       keys = String.fromCharCode(key).toLowerCase();
       character = " 1234567890.sqrt()+-*/^";
       special = "8-37-39-46";

       special_key = false
       for (var i in special) {
           if (key == special[i]) {
               special_key = true;
                break;
            }
        }

       if (character.indexOf(keys) == -1 && !special_key) {
            return false;
        }
    }
</script>

 </head>
<body>
    <form id="form1" runat="server">
    <div>
        <table >
            <tr>
                <td>
                     <asp:TextBox ID="txtOperation" onkeypress="return validateInput(event)" runat="server" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnEqual" runat="server" Text="=" Width="500px" OnClick="btnEqual_Click"/>

                </td>
            </tr>
             <tr>
                <td>
                    <asp:Button ID="btnClean" runat="server" Text="C" Width="500px" OnClick="btnClean_Click" />

                </td>
            </tr>
             <tr>
                <td>
                     <asp:TextBox ID="txtErrorMessage" ForeColor="Red" Width="500px" runat="server" Visible="false" ></asp:TextBox>
                </td>
            </tr>

        </table>
    
       
    
    </div>
    </form>
</body>
</html>
