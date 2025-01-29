using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class im_products_addProduct : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Id FROM [product] ORDER BY Id DESC", conn);           
            SqlDataReader dr = cmd.ExecuteReader();
            bool rf = dr.Read();

            if (rf)
            {
                int record = Int16.Parse(dr["Id"].ToString()) + 1;
                lblID.Text = record.ToString();
            }
            else
            {
                Response.Write("<script>alert('Error occured.');location.href='Default.aspx';</script>");
            }
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();

            string add = "INSERT INTO product (product_name,product_sku,product_price,product_quantity,product_desc,product_brand,product_category,product_status) " +
                         "VALUES(@name,@sku,@price,@quantity,@desc,@brand,@category,@status)";
            SqlCommand cmd = new SqlCommand(add, conn);

            cmd.Parameters.AddWithValue("name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("sku", txtSKU.Text);
            cmd.Parameters.AddWithValue("price", txtPrice.Text);
            cmd.Parameters.AddWithValue("quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("desc", txtDesc.Text);
            cmd.Parameters.AddWithValue("brand", ddlBrand.SelectedValue);
            cmd.Parameters.AddWithValue("category", ddlCategory.SelectedValue);
            cmd.Parameters.AddWithValue("status", ddlStatus.SelectedValue);

            cmd.ExecuteNonQuery();

            conn.Close();

            Response.Write("<script>alert('Product: " + txtName.Text + ". Added Successfully!');location.href='/im/products/Default.aspx';</script>");
        }
    }

}