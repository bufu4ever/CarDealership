using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarDealership
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text.Trim();
            string username = txtUsername.Text.Trim();

            if (password == "123" && username == "admin")
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                Response.Redirect("ViewCar.aspx");
            }
        }

    }
}