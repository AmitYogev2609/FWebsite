using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace FWebsite
{
    public partial class LogInOrSignup : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=D:\FWebsite\App_Data\FilesDB.mdf;Integrated Security = True");
        public string message;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ifsign = Request.Form["SignUp"];
            string username ="";
            string pass="";
            if (ifsign!=null)
            {
                con.Open();
                 username = Request.Form["username"];
                 pass = Request.Form["pass"];
                
                string find = $"SELECT * FROM Users WHERE UserName='{username}'";
                SqlCommand cmd = new SqlCommand(find, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                int count = dt.Rows.Count;
                if(count > 0)
                { 
                    message = "user name already exsit";
                    con.Close();
                    return;
                }
                
                SqlCommand sqlcmd = new SqlCommand($"INSERT INTO Users (UserName, Pass) VALUES ('{username}', '{pass}')",con);
                sqlcmd.ExecuteNonQuery();

                find = $"SELECT * FROM Users WHERE UserName='{username}' AND Pass='{pass}'";
                cmd = new SqlCommand(find, con);
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                dt = new DataTable();
                da.Fill(dt);
                con.Close();
                Session["UserID"] = dt.Rows[0]["UserID"];
                Response.Redirect("uploadFile.aspx");

            }
            string iflog = Request.Form["LogIn"];
            if(iflog!=null) 
            {
                con.Open();
               
                username = Request.Form["username"];
                pass = Request.Form["pass"];
                

                string find = $"SELECT * FROM Users WHERE UserName='{username}' AND Pass='{pass}'";
                SqlCommand cmd = new SqlCommand(find, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand= cmd;
                DataTable dt= new DataTable();
                da.Fill(dt);
                int count= dt.Rows.Count;
                if(count == 0)
                {
                    message = "user name already exsit";
                    return;
                }
                con.Close();
                Session["UserID"] = dt.Rows[0]["UserID"];
                Response.Redirect("uploadFile.aspx");


            }

        }
    }
}