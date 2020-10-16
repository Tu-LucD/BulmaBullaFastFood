using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace BulmaBullaFastFood
{
    public partial class Checkout : System.Web.UI.Page
    {
        public String user, food, drink, city, gender, price;
        public int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            String constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|BBFastFoodDB.mdf;Integrated Security=False";

            food = Request.QueryString["ka"].ToString();

            string i = Session["UserID"].ToString();
            id = Convert.ToInt32(i);

            city = Session["City"].ToString();
            gender = Session["Gender"].ToString();

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into orders values ('"+id+"','" + food + "', 'drink', '"+city+"', '"+gender+"', 12.00)";
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}