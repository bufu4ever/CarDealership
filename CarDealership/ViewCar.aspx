<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCar.aspx.cs" Inherits="CarDealership.ViewCar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Car Dealership - View Car</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">
    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.4/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
           <center><h2>Car Details</h2></center> 
            <div>
                <br />
                <br />
                <asp:PlaceHolder ID="phCarDetails" runat="server" Visible="false">
                    <label>Brand:</label>
                    <asp:Label ID="lblBrand" runat="server"></asp:Label>
                    <br />
                    <label>Model:</label>
                    <asp:Label ID="lblModel" runat="server"></asp:Label>
                    <br />
                    <label>Year:</label>
                    <asp:Label ID="lblYear" runat="server"></asp:Label>
                    <br />
                    <label>Price:</label>
                    <asp:Label ID="lblPrice" runat="server"></asp:Label>
                    <br />
                </asp:PlaceHolder>
                <div class="bg-light border mb-3 d-inline-flex p-2">
                    <div class="col-md-auto">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-auto">
                      <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-sm btn-outline-primary btn-block" />
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <asp:Repeater ID="rptCars" runat="server">
                        <ItemTemplate>
                            <div class="col-md-4 mb-3">
                                <div class="card">
                                    <div class="card-body">
                                        <h3><%# Eval("Brand") %> <%# Eval("Model") %></h3>
                                        <p>Year: <%# Eval("Year") %></p>
                                        <p>Price: <%# Eval("Price") %> ₪</p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <br />
                <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="Login.aspx" Text="Back to Login Page" />
            </div>
        </div>
    </form>
</body>
</html>
