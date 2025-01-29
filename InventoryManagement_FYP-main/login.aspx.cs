using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class login : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        // Ensure the login form is displayed on first load
        if (!IsPostBack)
        {
            pnlLogin.Visible = true;
            pnlRegister.Visible = false;

            // Check and modify database to add role column and default user
            ModifyDatabase();
        }
    }

    private void ModifyDatabase()
    {
        conn.Open();

        // Add the 'role' column if it does not exist
        try
        {
            SqlCommand alterCmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'user' AND COLUMN_NAME = 'role') " +
                                                  "BEGIN ALTER TABLE [user] ADD role NVARCHAR(10) END", conn);
            alterCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur
            Console.WriteLine("Error adding role column: " + ex.Message);
        }

        // Insert default user if it does not exist
        try
        {
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM [user] WHERE username=@user", conn);
            checkCmd.Parameters.AddWithValue("user", "VladimirDemin");
            int userCount = (int)checkCmd.ExecuteScalar();

            if (userCount == 0) // If user does not exist, insert
            {
                SqlCommand insertCmd = new SqlCommand("INSERT INTO [user] (username, password, role) VALUES (@user, @pass, @role)", conn);
                insertCmd.Parameters.AddWithValue("user", "VladimirDemin");
                insertCmd.Parameters.AddWithValue("pass", "111111");
                insertCmd.Parameters.AddWithValue("role", "admin");

                insertCmd.ExecuteNonQuery();
                Console.WriteLine("Default user added successfully.");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur
            Console.WriteLine("Error adding default user: " + ex.Message);
        }

        conn.Close();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM [user] WHERE username=@user AND password=@pass", conn);
        cmd.Parameters.AddWithValue("user", txtUsrname.Text);
        cmd.Parameters.AddWithValue("pass", txtPass.Text);

        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.HasRows) // Check if there's a record
        {
            while (dr.Read())
            {
                Session["username"] = dr["username"].ToString();
                Session["Id"] = dr["Id"].ToString();
                Response.Redirect("/im/dashboard.aspx");
            }
        }
        else
        {
            Response.Write("<script>alert('EMAIL AND PASSWORD IS NOT FOUND!');</script>");
        }

        conn.Close();
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        conn.Open();

        SqlCommand insertCmd = new SqlCommand("INSERT INTO [user] (username, password, role) VALUES (@user, @pass, @role)", conn);
        insertCmd.Parameters.AddWithValue("user", txtRegUsrname.Text);
        insertCmd.Parameters.AddWithValue("pass", txtRegPass.Text);
        insertCmd.Parameters.AddWithValue("role", "user"); // Default role for new users

        int rowsAffected = insertCmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Response.Write("<script>alert('Registration successful!');</script>");
            pnlLogin.Visible = true;
            pnlRegister.Visible = false;
        }
        else
        {
            Response.Write("<script>alert('Registration failed!');</script>");
        }

        conn.Close();
    }

    protected void btnShowRegister_Click(object sender, EventArgs e)
    {
        // Navigate to the registration form
        pnlLogin.Visible = false;
        pnlRegister.Visible = true;
    }

    protected void btnCancelRegister_Click(object sender, EventArgs e)
    {
        // Cancel registration and return to the login form
        pnlLogin.Visible = true;
        pnlRegister.Visible = false;
    }
}
