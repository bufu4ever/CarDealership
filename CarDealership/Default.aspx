<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarDealership.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Dealership</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Car Dealership</h1>
            <h2>Add/Edit Car</h2>
            <div class="card">
                <div class="card-body">
                    <table>
                        <tr>
                            <td>Brand:</td>
                            <td><asp:TextBox ID="txtBrand" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Model:</td>
                            <td><asp:TextBox ID="txtModel" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Year:</td>
                            <td><asp:TextBox ID="txtYear" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Price:</td>
                            <td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnAddCar" runat="server" Text="Add Car" OnClick="btnAddCar_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnUpdateCar" runat="server" Text="Update Car" Visible="false" OnClick="btnUpdateCar_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" OnClick="btnCancel_Click" CssClass="btn btn-secondary" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfCarId" runat="server" />
                </div>
            </div>
            <hr />
            <h2>Car List</h2>
          <asp:Repeater ID="rptCars" runat="server">
    <ItemTemplate>
        <div class="card" style="width: 18rem; display: inline-block; margin-right: 10px;">
            <div class="card-body">
                <h5 class="card-title"><%# Eval("Brand") %></h5>
                <h6 class="card-subtitle mb-2 text-muted"><%# Eval("Model") %></h6>
                <p class="card-text">
                    Year: <%# Eval("Year") %><br />
                    Price:  <%# Eval("Price") %>₪
                </p>
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("CarId") %>' OnClick="btnEdit_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("CarId") %>' OnClick="btnDelete_Click" CssClass="btn btn-danger" />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.4/dist/jquery.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
