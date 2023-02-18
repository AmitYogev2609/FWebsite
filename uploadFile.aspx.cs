using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace FWebsite
{
    public partial class uploadFile : System.Web.UI.Page
    {
        public DateTime publishDate;
        public string fileTable;
        List<string> filePaths;
        public string messege;
        public int UserID;
       static int count = 0;
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=D:\FWebsite\App_Data\FilesDB.mdf;Integrated Security = True");

        protected void Page_Load(object sender, EventArgs e)
        {
            UserID =(int)Session["UserID"];
            con.Open();
            
            #region upload fail
            object ifnull = Request.Form["a"];
            if (ifnull != null)
            {
                HttpPostedFile postedFile = Request.Files["myfile"];
                try
                {
                    //Check if File is available.
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        //Save the Filet o the server
                        string filePath = Server.MapPath("~/wwwroot/") + Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        
                       
                        SqlCommand sqlcmd = new SqlCommand($"INSERT INTO Files (FilePath ,FilName, UserId) VALUES ('{filePath}', '{Path.GetFileNameWithoutExtension(filePath)}', '{UserID}')", con);
                        sqlcmd.ExecuteNonQuery();
                        
                        filePaths = Directory.GetFiles(Server.MapPath("~/wwwroot/")).ToList<string>();
                        if(filePaths.Count()>count)
                        {
                            messege = "<h3 style='color: green'> succsess </h3>";
                        }
                        else
                        {
                            messege = "<h3 style='color: red'> somthing went wrong </h3>";
                            return;
                        }
                    }
                }
                catch(Exception ex)
                {
                    messege = "<h3 style='color: red'> somthing went wrong </h3>";
                    return;
                }
            }

            #endregion
            string find = $"SELECT * FROM Files WHERE UserId='{UserID}'";
            SqlCommand cmd = new SqlCommand(find, con);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
             count = dt.Rows.Count;


            #region uptade files table
            fileTable = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                fileTable += $"<tr>";
                fileTable += $"<td> <a href='{Path.GetFileName(dt.Rows[i]["FilePath"].ToString())}' download>{dt.Rows[i]["FilName"].ToString()}</a></td> ";
                fileTable += $"<td>{dt.Rows[i]["UploadDate"].ToString()}</td>";
                fileTable += $"<td>{Path.GetExtension(dt.Rows[i]["FilePath"].ToString())}</td>";
                fileTable += "</tr>";

            }
            #endregion
            con.Close();
            string islogout = Request.Form["logout"];
            if (islogout != null)
            {
                Session["UserID"] = null;
                Response.Redirect("LogInOrSignup.aspx");
            }
        }
    }
}