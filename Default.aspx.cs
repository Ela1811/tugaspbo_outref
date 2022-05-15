using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tugas_PBO
{
    public partial class Dokter : System.Web.UI.Page
    {
        protected void btnInsertion_Click(object sender, EventArgs e)
        {
            if (SqlDBHelper.ExecuteNonQuery("Insert into dokter values(@id_dokter,@nama,@nomor_hp,@alamat)", CommandType.Text, new NpgsqlParameter("@id_dokter", Convert.ToInt32(txtDokterID.Text)), new NpgsqlParameter("@nama", txtNama.Text), new NpgsqlParameter("@nomor_hp", txtNomor_hp.Text), new NpgsqlParameter("@alamat", txtAlamat.Text)))
            {
                txtDokterID.Text = ""; txtNama.Text = ""; txtNomor_hp.Text = ""; txtAlamat.Text = "";
                GridView1.Visible = false;
                lblmessage2.Text = String.Empty;
                lblmessage.Text = String.Empty;
                lblmsg2.Text = String.Empty;
                lblmsg.Text = "Data Has been Saved";
            }

            else
            {
                txtDokterID.Text = ""; txtNama.Text = ""; txtNomor_hp.Text = ""; txtAlamat.Text = "";
                GridView1.Visible = false;
                lblmessage2.Text = String.Empty;
                lblmessage.Text = String.Empty;
                lblmsg.Text = String.Empty;
                lblmsg2.Text = "Failed, ID Should Be Unique";
            }

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            DataTable dt;
            if (SqlDBHelper.ExecuteDataSet(out dt, "Select * from dokter order by id_dokter", CommandType.Text))
            {
                //if (SqlDBHelper.ExecuteDataSet(out dt, "Select * from account order by id", CommandType.Text))
                lblmsg.Text = String.Empty;
                lblmsg2.Text = String.Empty;
                lblmessage.Text = String.Empty;
                lblmessage2.Text = String.Empty;
                GridView1.DataSource = dt;
                GridView1.Visible = true;
                GridView1.DataBind();
            }
        }
        protected void btnUpdation_Click(object sender, EventArgs e)
        {
            if (SqlDBHelper.ExecuteNonQuery("update dokter set id_dokter=@id_dokter, nama=@nama, nomor_hp=@nomor_hp, alamat=@alamat where id_dokter=@id_dokter", CommandType.Text, new NpgsqlParameter("@id_dokter", Convert.ToInt32(txtDokterID.Text)), new NpgsqlParameter("@nama", txtNama.Text), new NpgsqlParameter("@nomor_hp", txtNomor_hp.Text), new NpgsqlParameter("@alamat", txtAlamat.Text)))
            {
                txtNama.Text = ""; txtDokterID.Text = ""; txtNomor_hp.Text = ""; txtAlamat.Text = "";
                GridView1.Visible = false;
                lblmsg2.Text = String.Empty;
                lblmsg.Text = "Data Has been Updated";
            }
            else
            {
                txtDokterID.Text = ""; txtNama.Text = ""; txtNomor_hp.Text = ""; txtAlamat.Text = "";
                GridView1.Visible = false;
                lblmsg.Text = String.Empty;
                lblmsg2.Text = "Failed, ID and Username Should Be Unique";
            }

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string var;
            var = txtDokterID.Text;
            if (var == "")
            {
                txtDokterID.Text = "";
                GridView1.Visible = false;
                lblmsg.Text = String.Empty;
                lblmsg2.Text = String.Empty;
                lblmessage.Text = String.Empty;
                lblmessage2.Text = "Please Write the Correct ID";
            }
            else if (SqlDBHelper.ExecuteNonQuery("Delete from dokter where id_dokter=@id_dokter", CommandType.Text, new NpgsqlParameter("@id_suster", Convert.ToInt32(txtSustID.Text))))
            {
                txtDokterID.Text = "";
                GridView1.Visible = false;
                lblmsg.Text = String.Empty;
                lblmsg2.Text = String.Empty;
                lblmessage2.Text = String.Empty;
                lblmessage.Text = "Data Has been Deleted";
            }
        }
        public static class SqlDBHelper
        {
            public static bool ExecuteDataSet(out DataTable dt, string sql, CommandType cmdType, params NpgsqlParameter[] parameters)
            {
                using (DataSet ds = new DataSet())
                using (NpgsqlConnection connStr = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
                {
                    cmd.CommandType = cmdType;
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }

                    try
                    {
                        cmd.Connection.Open();
                        new NpgsqlDataAdapter(cmd).Fill(ds);
                    }
                    catch (NpgsqlException ex)
                    {
                        dt = null;
                        return false;

                    }


                    dt = ds.Tables[0];
                    return true;
                }
            }
            public static bool ExecuteNonQuery(string sql, CommandType cmdType, params NpgsqlParameter[] parameters)
            {
                using (DataSet ds = new DataSet())
                using (NpgsqlConnection connStr = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString))
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
                {
                    cmd.CommandType = cmdType;
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }

                    try
                    {

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (NpgsqlException ex)
                    {

                        return false;

                    }

                    return true;

                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

