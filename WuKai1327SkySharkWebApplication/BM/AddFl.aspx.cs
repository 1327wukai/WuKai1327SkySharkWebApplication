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
    public partial class AddFl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String ConnectionString = ConfigurationManager.ConnectionStrings["ARPDatabaseConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            string selectSql = "SELECT FltNo FROM dtFltDetails";
            SqlCommand cmd = new SqlCommand(selectSql, conn);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet,"FlightNO");
            conn.Close();
            foreach (DataRow row in dataSet.Tables["FlightNO"].Rows)
            {
                lblMessage.Text = "The Flight already exists. Please try another fligh number";
                return;
            }
            TimeSpan deptime, arrtime;
            try
            {
                deptime = Convert.ToDateTime(txtDepartureTime.Text).TimeOfDay;
                arrtime = Convert.ToDateTime(txtArrivalTime.Text).TimeOfDay;
                if (deptime >= arrtime)
                {
                    lblMessage.Text = "Departure Time cant be greater than or equal to arrival time";
                    return;
                }
            }
            catch 
            {
                lblMessage.Text = "Invalid departure or arrival time";
                return;
            }

            try
            {
                conn.Open();
                string updateSql = "INSERT INTO [dtFltDetails]([FltNo]，[origin]，[Destination]，[Deptime]，[Arrtime]," +
                "[AircraftType], " +
                " [SeatsExec]，[SeatsBn]，[FareExec]，[FareBn]，[LaunchDate])" +
                " VALUES (@FltNo，@Origin，@Destination，@Deptime，@Arrtime，@AircraftType," + 
                " @SeatsExec，@SeatsBn，@FareExec，@FareBn，@LaunchDate)";
                SqlCommand cmd2 = new SqlCommand(updateSql, conn);
                cmd2.Parameters.AddWithValue("@FltNo",txtFlightNumber.Text.Trim());
                cmd2.Parameters.AddWithValue("@Origin", txtOriginPlace.Text.Trim());
                cmd2.Parameters.AddWithValue("@Destination", txtDestination.ToString());
                cmd2.Parameters.AddWithValue("@Deptime", deptime.ToString());
                cmd2.Parameters.AddWithValue("@Arrtime", arrtime.ToString());
                cmd2.Parameters.AddWithValue("@AircraftType", txtAircraftType.Text.Trim());
                cmd2.Parameters.AddWithValue("@SeatsExec", Convert.ToInt32(txtExecFare.Text.Trim()));
                cmd2.Parameters.AddWithValue("@SeatsBn", Convert.ToInt32(txtNoOfBusiSeats.Text.Trim()));
                cmd2.Parameters.AddWithValue("@FareExec", Convert.ToInt32(txtExecFare.Text.Trim()));
                cmd2.Parameters.AddWithValue("@FareBn", Convert.ToInt32(txtBusiClassFare.Text.Trim()));
                cmd2.Parameters.AddWithValue("@LaunchDate", DateTime.Today.Date.ToShortDateString());
                int n= cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message; 
                conn.Close();
                return;
            }
            conn.Close();
            lblMessage.Text = "Flight add Successfully"; 
            txtFlightNumber.Text = "";
            txtOriginPlace.Text = "";
            txtAircraftType.Text = ""; 
            txtArrivalTime.Text = "";
            txtNoOfBusiSeats.Text = ""; 
            txtDepartureTime.Text = "";
            txtDestination.Text = "";
            txtExecFare.Text = ""; 
            txtFlightNumber.Text = "";
            txtNoOfBusiSeats.Text = "";
            txtNoOfBusiSeats.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            txtFlightNumber.Text = "";
            txtOriginPlace.Text = "";
            txtAircraftType.Text = "";
            txtArrivalTime.Text = "";
            txtNoOfBusiSeats.Text = "";
            txtDepartureTime.Text = "";
            txtDestination.Text = "";
            txtExecFare.Text = "";
            txtFlightNumber.Text = "";
            txtNoOfBusiSeats.Text = "";
            txtNoOfBusiSeats.Text = "";
        }
    }
}