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
        String user, food;
        protected void Page_Load(object sender, EventArgs e)
        {
            String constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|BBFastFoodDB.mdf;Integrated Security=False";

            food = Request.QueryString["ka"].ToString();
            
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into orders values (2,'" + food + "', 'drink', 'montreal', 'Male', 12.00)";
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}