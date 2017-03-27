<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_034_CasinoMegaChallenge.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="leftImage" runat="server" Height="150px" Width="150px" />
&nbsp;<asp:Image ID="middleImage" runat="server" Height="150px" Width="150px" />
&nbsp;<asp:Image ID="rightImage" runat="server" Height="150px" Width="150px" />
            <br />
            <br />
            Your Bet:
            <asp:TextBox ID="betBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="leverButton" runat="server" OnClick="leverButton_Click" Text="Pull Lever!" />
            <br />
            <br />
            <asp:Label ID="resultLabel" runat="server"></asp:Label>
            <br />
            <br />
            Player&#39;s Money: $<asp:Label ID="moneyLabel" runat="server"></asp:Label>
            <br />
            <br />
            1 Cherry - double your bet!<br />
            2 Cherries - triple your bet!<br />
            3 Cherries - quadruple your bet!<br />
            3 7&#39;s - Jackpot! (100 times your bet)<br />
            <br />
            HOWEVER : if you get even one BAR, you lose your bet!
        </div>
    </form>
</body>
</html>
