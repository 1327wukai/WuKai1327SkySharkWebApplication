using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WuKai1327SkySharkWebApplication.BM
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String month, date, year;
            month = ListBox1.SelectedItem.Text.Trim(); 
            year = ListBox1.SelectedItem.Text.Trim(); 
            date = month + "/01/" + year;
            String ConnectionString = ConfigurationManager.ConnectionStrings["ARPODatabaseConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            string selectSql = "SELECT FltNo，SUM(Fare) AS fare from dtDepartedFlights " +
            "where CDateOfJourney>@date) group by FltNo";

            SqlCommand cmd = new SqlCommand(selectSql, conn); 
            cmd.Parameters.AddWithValue("@date", date);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet dataSet = new DataSet(); 
            adapter.Fill(dataSet, "fare"); 
            conn.Close();
            DataView source = new DataView(dataSet.Tables["fare"]);
            GridView1.DataSource = source;
            GridView1.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String ConnectionString = ConfigurationManager.ConnectionStrings["ARPODatabaseConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            string selectSql = "SELECT FltNo，DateOfJourney，SUN(Fare)AS Revenue from dtDepartedFlights " +
            "GROUP BY DateOfJourney,FltNo";

            SqlCommand cmd = new SqlCommand(selectSql, conn);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Usage");
            conn.Close();
            DataView source = new DataView(dataSet.Tables["Usage"]);
            GridView1.DataSource = source;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String ConnectionString = ConfigurationManager.ConnectionStrings["ARPODatabaseConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            string selectSql = "SELECT Top 19Email，FareCollected，TotalTinesFlown from dtPassengerbetails order by TotalTimesFlown" ;

            SqlCommand cmd = new SqlCommand(selectSql, conn);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "FreqFl");
            conn.Close();
            DataView source = new DataView(dataSet.Tables["FreqFl"]);
            GridView1.DataSource = source;
            GridView1.DataBind();
        }
    }
}