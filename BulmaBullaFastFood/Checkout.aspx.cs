using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BulmaBullaFastFood
{
    public partial class Checkout : System.Web.UI.Page
    {
        public String user, food, drink, city, gender, price;
        public int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            String constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|BBFastFoodDB.mdf;Integrated Security=False";

            food += Request.QueryString["sz"].ToString() + " ";
            food += Request.QueryString["go"].ToString() + " ";
            food += Request.QueryString["kr"].ToString() + " ";
            food += Request.QueryString["rn"].ToString() + " ";
            food += Request.QueryString["on"].ToString() + " ";
            food += Request.QueryString["ha"].ToString() + " ";
            food += Request.QueryString["ka"].ToString() + " ";
            food += Request.QueryString["ri"].ToString() + " ";
            food += Request.QueryString["be"].ToString() + " ";
            food += Request.QueryString["eg"].ToString();
            drink += Request.QueryString["re"].ToString() + " ";
            drink += Request.QueryString["po"].ToString();

            string sendFood = "";
            string sendDrink = "";


            string i = Session["UserID"].ToString();
            id = Convert.ToInt32(i);

            city = Session["City"].ToString();
            gender = Session["Gender"].ToString();

            double oPrice = 0;
            int itemQuantity = 0;

            string[] foodItems = food.Split(' ');

            int counter = 0;
            foreach(string foodItem in foodItems)
            {
                itemQuantity = Int32.Parse(Regex.Match(foodItem, @"\d").Value);
                if (itemQuantity > 0)
                {
                    sendFood += foodItem + " ";
                }
                switch(counter)
                {
                    //Senzu Bean
                    case 0: oPrice += Convert.ToDouble(itemQuantity) * 999.99;
                        break;
                    //Gotcha Pork
                    case 1: oPrice += Convert.ToDouble(itemQuantity) * 39.99;
                        break;
                    //Krabby Patty
                    case 2: oPrice += Convert.ToDouble(itemQuantity) * 4.99;
                        break;
                    //Hidden Leaf Ramen
                    case 3: oPrice += Convert.ToDouble(itemQuantity) * 0.99;
                        break;
                    //Brock's Onigiri
                    case 4: oPrice += Convert.ToDouble(itemQuantity) * 19.99;
                        break;
                    //Happy Meal
                    case 5: oPrice += Convert.ToDouble(itemQuantity) * 3.99;
                        break;
                    //Karaage Roll
                    case 6: oPrice += Convert.ToDouble(itemQuantity) * 12.99;
                        break;
                    //Rainbow Terrine
                    case 7: oPrice += Convert.ToDouble(itemQuantity) * 39.99;
                        break;
                    //Beef Bouguignon
                    case 8: oPrice += Convert.ToDouble(itemQuantity) * 69.99;
                        break;
                    //Erina's Egg Benedict
                    case 9: oPrice += Convert.ToDouble(itemQuantity) * 49.99;
                        break;
                }                
                counter++;
            }

            counter = 0;
            string[] drinkItems = drink.Split(' ');
            foreach (string drinkItem in drinkItems)
            {
                itemQuantity = Int32.Parse(Regex.Match(drinkItem, @"\d").Value);
                if (itemQuantity > 0)
                {
                    sendDrink += drinkItem + " ";
                }
                switch (counter)
                {
                    //Ramune
                    case 0: oPrice += Convert.ToDouble(itemQuantity) * 2.99;
                        break;
                    //Pocari Sweat
                    case 1: oPrice += Convert.ToDouble(itemQuantity) * 2.99;
                        break;
                }
                counter++;
            }

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into orders values ('"+id+"','" + sendFood + "', '" + sendDrink + "', '"+city+"', '"+gender+"', '"+oPrice+"')";
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}