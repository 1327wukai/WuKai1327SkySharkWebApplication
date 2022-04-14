using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WuKai1327SkySharkWebApplication
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String username;
                String password;
                username = txtUserName.Text;
                password = txtPassword.Text;
                //get conntion
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ARPDatabaseConnectionstring"].ConnectionString);
                conn.Open();
                //create dataadapter
                string queryString = "Select UserName，Password,role from dtUsers where UserName='" + username + " '";
                SqlDataAdapter adapter = new SqlDataAdapter();
                //create commnad
                adapter.SelectCommand = new SqlCommand(queryString, conn);

                DataSet ds1 = new DataSet();
                adapter.Fill(ds1, "dtUsers");

                if(ds1.Tables["dtUsers"].Rows.Count == 0)
                {
                    lblMessage.Text = "Invalid Username";
                }
                else
                {
                    if(ds1.Tables["dtUsers"].Rows[0][1].ToString().Trim() == txtPassword.Text.Trim())
                    {
                        String Role;
                        Role = ds1.Tables["dtUsers"].Rows[0][2].ToString().Trim();
                        Session["userName"] = username;
                        Session["userRole"] = Role;
                        if (Role == "Disabled")
                        {
                            lblMessage.Text = "Your account has been disabled.Please contact the network Administrator"; 
                            return;
                        }
                        switch (Role)
                        {
                            case "Admin":
                                Response.Redirect(" .\\NAN\\ManageUsers1.aspx"); 
                                break;
                            case "BM":
                                Response.Redirect(".\\BM\\AddFI.aspx"); 
                                break;
                            case "LOB":
                                Response.Redirect(".\\OB\\CreateRes.aspx"); 
                                break;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Invalid password";
                    }
                }
            }
        }
    }
}