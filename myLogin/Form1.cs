using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace myLogin
{
    public partial class Form1 : Form
    {
        static int attempt = 3;
        public Form1()
        {
            InitializeComponent();
            string ruta = System.IO.Path.Combine(Application.StartupPath, "images1.jpeg");
            if (System.IO.File.Exists(ruta))
                pictureBox1.Image = new Bitmap(ruta);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (attempt == 0)
            {
                lbl_Msg.Text = "LOS 3 INTENTOS HAN FALLADO - CONTACTE AL ADMINISTRADOR";
                return;
            }

            string cs = @"Server=localhost\SQLEXPRESS;Database=login;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection scn = new SqlConnection(cs))
            {
                SqlCommand scmd = new SqlCommand(
                    "select count(*) from login_database where username=@usr and password=@pwd", scn);
                scmd.Parameters.AddWithValue("@usr", txt_UserName.Text);
                scmd.Parameters.AddWithValue("@pwd", txt_PWD.Text);
                scn.Open();

                if (scmd.ExecuteScalar().ToString() == "1")
                {
                    pictureBox1.Image = new Bitmap(System.IO.Path.Combine(Application.StartupPath, "granted.png"));
                    MessageBox.Show("SE LE CONCEDE ACCESO");
                }
                else
                {
                    attempt--;
                    pictureBox1.Image = new Bitmap(System.IO.Path.Combine(Application.StartupPath, "denied.jpeg"));
                    MessageBox.Show("NO SE LE CONCEDE ACCESO");
                    lbl_Msg.Text = "Solo tienes " + attempt + " intentos que quedan por probar";
                    txt_UserName.Clear();
                    txt_PWD.Clear();
                }
            }
        }
    }
}
