using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CarDealership
{
    public partial class Default : System.Web.UI.Page
    {

        // מחרוזת חיבור למסד הנתונים
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Inventory.mdf;Integrated Security=True;Connect Timeout=30";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCarList();
            }
        }

        // מאחזר נתוני רכב ממסד הנתונים וקשר אותם לממשק המשתמש
        private void BindCarList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cars ORDER BY Brand, Model";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                rptCars.DataSource = dataTable;
                rptCars.DataBind();
            }
        }

        protected void btnAddCar_Click(object sender, EventArgs e)
        {
            //הוסף מכונית חדשה למסד הנתונים 
            string brand = txtBrand.Text;
            string model = txtModel.Text;
            int year = Convert.ToInt32(txtYear.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Cars (Brand, Model, Year, Price) VALUES (@Brand, @Model, @Year, @Price)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Brand", brand);
                    command.Parameters.AddWithValue("@Model", model);
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Price", price);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // נקה את טופס הקלט ועדכן את רשימת המכוניות
            ClearForm();
            BindCarList();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // ערוך פרטי מכונית קיימת
            Button btnEdit = (Button)sender;
            int carId = Convert.ToInt32(btnEdit.CommandArgument);

            // אחזר את פרטי הרכב ממסד הנתונים על סמך ה-carId
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cars WHERE CarId = @CarId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarId", carId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtBrand.Text = reader["Brand"].ToString();
                        txtModel.Text = reader["Model"].ToString();
                        txtYear.Text = reader["Year"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                    }
                    reader.Close();
                }
            }

            // אחסן את ה-cardId בשדה נסתר והחלף את נראות הלחצן
            hfCarId.Value = carId.ToString();
            btnAddCar.Visible = false;
            btnUpdateCar.Visible = true;
        }

        protected void btnUpdateCar_Click(object sender, EventArgs e)
        {
            // עדכן פרטי רכב קיים
            int carId = Convert.ToInt32(hfCarId.Value);
            string brand = txtBrand.Text;
            string model = txtModel.Text;
            int year = Convert.ToInt32(txtYear.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Cars SET Brand = @Brand, Model = @Model, Year = @Year, Price = @Price WHERE CarId = @CarId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Brand", brand);
                    command.Parameters.AddWithValue("@Model", model);
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@CarId", carId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // נקה את טופס הקלט, עדכן את רשימת המכוניות והחלף את נראות הלחצנים
            ClearForm();
            BindCarList();

            btnAddCar.Visible = true;
            btnUpdateCar.Visible = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // מחק מכונית ממסד הנתונים
            Button btnDelete = (Button)sender;
            int carId = Convert.ToInt32(btnDelete.CommandArgument);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Cars WHERE CarId = @CarId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarId", carId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // עדכן את רשימת המכוניות לאחר המחיקה
            BindCarList();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // נקה את טופס הקלט והחלף את נראות הלחצן
            ClearForm();
            btnAddCar.Visible = true;
            btnUpdateCar.Visible = false;
        }
        private void ClearForm()
        {
            // נקה את כל שדות הקלט והשדה הנסתר
            txtBrand.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtYear.Text = string.Empty;
            txtPrice.Text = string.Empty;
            hfCarId.Value = string.Empty;
        }
    }
}
