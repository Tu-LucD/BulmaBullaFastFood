using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BulmaBullaFastFood
{
    public partial class Feeback : System.Web.UI.Page
    {
        public string email, message;
        protected void Page_Load(object sender, EventArgs e)
        {
            String constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|BBFastFoodDB.mdf;Integrated Security=False";

            email = Request.QueryString["em"].ToString();
            message = Request.QueryString["mg"].ToString();

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into feedback values ('"+email+"', '"+message+"')";
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}