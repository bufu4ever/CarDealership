using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace CarDealership
{
    public partial class ViewCar : System.Web.UI.Page
    {
        // מחרוזת החיבור משמשת לחיבור למסד הנתונים. זה כולל את שם השרת, שם מסד הנתונים, פרטי אימות ועוד.
        private string connectionString
        {
            get { return Application["ConnectionString"].ToString(); }
        }
        // CarId הוא משתנה שיאחסן את המזהה של המכונית שעבורה להציג פרטים
        private int CarId;


        //אם ה-CarId אינו null, הדף אמור להציג פרטים עבור המכונית הזו.
        //אם הוא null, זה אומר שהמשתמש לא ציין מכונית לצפייה, אז הדף יציג את כל המכוניות.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CarId"] != null)
                {
                    CarId = Convert.ToInt32(Request.QueryString["CarId"]);
                    PopulateCarDetails(CarId);
                }
                else
                {
                    ShowAllCarData();
                }
            }
        }

        // PopulateCarDetails מקבלת את הפרטים של מכונית מהמאגר ומציגה אותם בעמוד.
        //זה יוצר SqlConnection חדש כדי להתחבר למסד הנתונים, ואז יוצר SqlCommand כדי להפעיל שאילתת SQL.
        //שאילתת SQL מקבלת את כל הפרטים של המכונית עם ה-CarId הנתון.
        private void PopulateCarDetails(int carId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cars WHERE CarId = @CarId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarId", carId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    lblBrand.Text = reader["Brand"].ToString();
                    lblModel.Text = reader["Model"].ToString();
                    lblYear.Text = reader["Year"].ToString();
                    lblPrice.Text = reader["Price"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        // ShowAllCarData מקבלת את כל המכוניות ממסד הנתונים ומציגה אותן בעמוד.
        private void ShowAllCarData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cars ORDER BY Brand, Model";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                rptCars.DataSource = dataTable;
                rptCars.DataBind();

                rptCars.Visible = dataTable.Rows.Count > 0;
            }
        }

        // אם קיימת מכונית עם מונח חיפוש זה, היא מריץ שיטה לחיפוש מכוניות עם מונח זה.
        //אם לא, הוא מציג התראת JavaScript האומרת שהמכונית לא קיימת.
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = TextBox1.Text;
            if (CarExists(searchTerm))
            {
                SearchCars(searchTerm);
            }
            else
            {
                string script = "alert('The car does not exist.');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CarNotFound", script, true);
            }
        }

        // CarExists בודקת אם קיימת מכונית עם אותו השם החיפוש הנתון.
        private bool CarExists(string searchTerm)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Cars WHERE Brand = @SearchTerm OR Model = @SearchTerm";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchTerm", searchTerm);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        // SearchCars מחפשת מכוניות עם מונח החיפוש הנתון ומציגה אותן בעמוד.
        private void SearchCars(string searchTerm)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cars WHERE Brand LIKE @SearchTerm OR Model LIKE @SearchTerm";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                rptCars.DataSource = dataTable;
                rptCars.DataBind();

                // Show the repeater control if there are cars available
                rptCars.Visible = dataTable.Rows.Count > 0;
            }
        }
    }
}
