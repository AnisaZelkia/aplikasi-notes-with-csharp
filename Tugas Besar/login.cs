using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tugas_Besar
    
{
    public partial class login : Form
    {

        MySqlCommand query;
        koneksiku sambung;
        MySqlDataReader dr;

        public login()
        {
            InitializeComponent();
        }
        private void login_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
  
       
            private void button1_Click(object sender, EventArgs e)
        {
         
           
            sambung = new koneksiku ();
            try
            {
                sambung.buka();
                query = new MySqlCommand("select * from login where user = '" + textBox1.Text + "' && pass = '" + textBox2.Text + "'",sambung.koneksi);
                dr = query.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    this.Hide();
                    Form1 menu = new Form1();
                    menu.ShowDialog();
                   
                }
                else if (textBox1.Text == "")
                {
                    MessageBox.Show("Username Masih Kosong", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Password Masih Kosong", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                }
                else
                {
                    MessageBox.Show("Login Gagal, Periksa Username Dan Password Anda","Informasi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sambung.tutup();  
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)

            {
                sambung = new koneksiku();
                try
                {
                    sambung.buka();
                    query = new MySqlCommand("select * from login where user = '" + textBox1.Text + "' && pass = '" + textBox2.Text + "'", sambung.koneksi);
                    dr = query.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        this.Hide();
                        Form1 menu = new Form1();
                        menu.ShowDialog();

                    }
                    else if (textBox1.Text == "")
                    {
                        MessageBox.Show("Username Masih Kosong", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Focus();
                    }
                    else if (textBox2.Text == "")
                    {
                        MessageBox.Show("Password Masih Kosong", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox2.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Login Gagal, Periksa Username Dan Password Anda", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                sambung.tutup();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextBox2_StyleChanged(object sender, EventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
