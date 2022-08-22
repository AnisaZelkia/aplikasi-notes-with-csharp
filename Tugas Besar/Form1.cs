using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tugas_Besar
{
    enum Compound
    {
        Daily,
        Monthly,
        Quarterly,
        SemiAnnually,
        Annually
    }
    public partial class Form1 : Form
    {
        MySqlCommand query;
        string sql;
        MySqlDataAdapter adapter;
        koneksiku sambung;


        void tampiltabel()
        {
            ViewData tampilkan = new ViewData();
            DataTable tabel = new DataTable();
            tabel = tampilkan.bacasemua();
            dataGridView1.DataSource = tabel;
            dataGridView1.Columns[0].HeaderText = "NIM";
            dataGridView1.Columns[1].HeaderText = "NAMA";
            dataGridView1.Columns[2].HeaderText = "JURUSAN";
            dataGridView1.Columns[3].HeaderText = "Nilai Akhir";
            dataGridView1.Columns[0].Width = 400;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 60;
        }


        public Form1()
        {
            InitializeComponent();


        }
        void bersih()
        {
            tx1.Clear();
            tx2.Clear();
        }


        private void Form1_Load(object sender, EventArgs e)

        {
            p8.Show();
            button5.Enabled = false;
            bt4.Enabled = false;
            bt3.Enabled = false;
            bt1.Enabled = true;
            cmb.Items.Add(Compound.Daily);
            cmb.Items.Add(Compound.Monthly);
            cmb.Items.Add(Compound.Quarterly);
            cmb.Items.Add(Compound.SemiAnnually);
            cmb.Items.Add(Compound.Annually);
            cmb.SelectedItem = cmb.Items[0];

            dataGridView1.ReadOnly = true;
            bt3.Enabled = true;
            bt4.Enabled = true;
            wk.Text = DateTime.Now.ToString("HH:mm");
            string[] hari = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jum\'at", "Sabtu" };
            string[] bulan = { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "Nopember", "Desember" };

            string hariIni = hari[(int)DateTime.Today.DayOfWeek];
            string bulanIni = bulan[DateTime.Today.Month - 1];
            string TglIndonesia = string.Format("{0}, {1}-{2}-{3}", hariIni, DateTime.Today.Day, bulanIni, DateTime.Today.Year);
            this.lb2.Text = TglIndonesia;
            DT.Hide();
            DM.Hide();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMenu_Click_1(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 220)
            {

                MenuVertical.Width = 80;

            }
            else

                MenuVertical.Width = 220;


        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bt4.Enabled = false;
            bt3.Enabled = false;
            bt1.Enabled = true;
            p8.Hide();
            panel11.Hide();
            panel6.Show();
            panel7.Show();
            panel8.Hide();
            tampiltabel();
            tx1.Focus();
            DT.Show();
            DM.Hide();

        }
        void tampil()
        {
            ViewData tampilkan1 = new ViewData();
            DataTable tabel1 = new DataTable();
            tabel1 = tampilkan1.uangku();
            dataGridView2.DataSource = tabel1;
            dataGridView2.Columns[0].HeaderText = "Awal";
            dataGridView2.Columns[1].HeaderText = "Bunga(%)";
            dataGridView2.Columns[2].HeaderText = "Period";
            dataGridView2.Columns[3].HeaderText = "JenisPerhitungan";
            dataGridView2.Columns[4].HeaderText = "Total";
            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Width = 60;
            dataGridView2.Columns[2].Width = 60;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
        }
        private void btnprod_Click(object sender, EventArgs e)
        {
            panel11.Show();
            panel8.Show();
            panel6.Hide();
       
            panel7.Hide();

            p8.Hide();

            tampil();
            DM.Show();
            DT.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sambung = new koneksiku();
            sql = "insert into harian values ('" + lb2.Text + "','" + wk.Text + "','" + tx1.Text + "','" + tx2.Text + "');";
            if (tx1.Text == null)
            {
                MessageBox.Show("Anda Belum Mengisi Judul field", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    sambung.buka();
                    query = new MySqlCommand(sql, sambung.koneksi);
                    adapter = new MySqlDataAdapter(query);
                    dataGridView1.Refresh();
                    query.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampiltabel();
                    bersih();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                sambung.tutup();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bersih();
            bt4.Enabled = false;
            bt3.Enabled = false;
            bt1.Enabled = true;
        }

        private void btedt_Click(object sender, EventArgs e)
        {
            sambung = new koneksiku();
            sql = "update harian set Detail = '" + tx2.Text + "',  YOUR_NOTEPAD= '" + tx1.Text + "', Tanggal = '" + lb2.Text + "',Waktu = '" + wk.Text + "' where YOUR_NOTEPAD= '" + tx1.Text + "'";
            try
            {
                bt4.Enabled = false;
                bt3.Enabled = false;
                bt1.Enabled = true;
                sambung.buka();
                query = new MySqlCommand(sql, sambung.koneksi);
                query.ExecuteNonQuery();
                dataGridView1.Refresh();
                MessageBox.Show("Data Berhasil DiUbah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bt4.Enabled = false;
                bt3.Enabled = false;
                bt1.Enabled = true;
                tampiltabel();
                bersih();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sambung.tutup();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button5.Enabled = true;
            bt1.Enabled = false;
            bt3.Enabled = true;
            bt4.Enabled = true;
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

            lb2.Text = row.Cells["Tanggal"].Value.ToString();
            tx1.Text = row.Cells["YOUR_NOTEPAD"].Value.ToString();
            tx2.Text = row.Cells["Detail"].Value.ToString();
            wk.Text = row.Cells["Waktu"].Value.ToString();


         }

        private void button3_Click(object sender, EventArgs e)
        {
            login f2 = new login();
            f2.Show();
            this.Hide();
        }

        private void btn_Click(object sender, EventArgs e)
        {

            DialogResult result;
            result = MessageBox.Show("Apakah Anda Ingin Menyimpan Data Perhitungan???", "Notify", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                decimal decLabelMax = 10000000000M;

                double jumlahAwal = (double)jlhawal.Value;
                double rate = (double)suku.Value;
                int years = (int)prd.Value;
                Compound calcFrequency = (Compound)cmb.SelectedItem;

                decimal totalValue = 0;
                rate = rate / 100;
                int periods = 0;
                switch (calcFrequency)
                {
                    case Compound.Daily:
                        double dailyRate = rate / 365;
                        periods = years * 365;
                        jumlahAwal = jumlahAwal * Math.Pow((1 + dailyRate), (double)periods);
                        break;
                    case Compound.Monthly:
                        double monthlyRate = rate / 12;
                        periods = years * 12;
                        jumlahAwal = jumlahAwal * Math.Pow((1 + monthlyRate), (double)periods);
                        break;
                    case Compound.Quarterly:

                        double quarterlyRate = rate / 4;
                        periods = years * 4;
                        jumlahAwal = jumlahAwal * Math.Pow((1 + quarterlyRate), (double)periods);
                        break;
                    case Compound.SemiAnnually:

                        double semiAnnualRate = rate / 2;

                        periods = years * 2;
                        jumlahAwal = jumlahAwal * Math.Pow((1 + semiAnnualRate), (double)periods);
                        break;
                    case Compound.Annually:

                        double annualRate = rate;

                        periods = years;

                        jumlahAwal = jumlahAwal * Math.Pow((1 + annualRate), (double)periods);
                        break;
                    default:

                        MessageBox.Show(this, "Ada Error");
                        break;
                }
                totalValue = (decimal)jumlahAwal;

                if (totalValue <= decLabelMax)
                {
                    lblTtl.Text = String.Format("Rp. {0:###,###.##}", totalValue);

                }
                else
                {
                    MessageBox.Show("Jumlah melebihi nilai maximum");
                }
                sambung = new koneksiku();

                sql = "insert into uang values ('" + jlhawal.Text + "','" + suku.Text + "','" + prd.Text + "','" + cmb.Text + "','" + lblTtl.Text + "');";
                try
                {
                    sambung.buka();
                    query = new MySqlCommand(sql, sambung.koneksi);
                    adapter = new MySqlDataAdapter(query);
                    dataGridView1.Refresh();
                    query.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ViewData tampilkan1 = new ViewData();
                    DataTable tabel1 = new DataTable();
                    tabel1 = tampilkan1.uangku();
                    dataGridView2.DataSource = tabel1;
                    dataGridView2.Columns[0].HeaderText = "Awal";
                    dataGridView2.Columns[1].HeaderText = "Bunga(%)";
                    dataGridView2.Columns[2].HeaderText = "Period";
                    dataGridView2.Columns[3].HeaderText = "JenisPerhitungan";
                    dataGridView2.Columns[4].HeaderText = "Total";
                    dataGridView2.Columns[0].Width = 60;
                    dataGridView2.Columns[1].Width = 60;
                    dataGridView2.Columns[2].Width = 60;
                    dataGridView2.Columns[3].Width = 60;
                    dataGridView2.Columns[4].Width = 60;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                sambung.tutup();

            }
            else
            {
                decimal decLabelMax = 10000000000M;

                double jumlahAwal = (double)jlhawal.Value;
                double rate = (double)suku.Value;
                int years = (int)prd.Value;
                Compound calcFrequency = (Compound)cmb.SelectedItem;

                decimal totalValue = 0;
                rate = rate / 100;
                int periods = 0;
                switch (calcFrequency)
                {
                    case Compound.Daily:
                        double dailyRate = rate / 365;
                        periods = years * 365;
                        jumlahAwal = jumlahAwal * Math.Pow((1 + dailyRate), (double)periods);
                        break;
                    case Compound.Monthly:
                        double monthlyRate = rate / 12;
                        periods = years * 12;
                        jumlahAwal = jumlahAwal * Math.Pow((1 + monthlyRate), (double)periods);
                        break;
                    case Compound.Quarterly:

                        double quarterlyRate = rate / 4;
                        periods = years * 4;
                        jumlahAwal = jumlahAwal * Math.Pow((1 + quarterlyRate), (double)periods);
                        break;
                    case Compound.SemiAnnually:

                        double semiAnnualRate = rate / 2;

                        periods = years * 2;
                        jumlahAwal = jumlahAwal * Math.Pow((1 + semiAnnualRate), (double)periods);
                        break;
                    case Compound.Annually:

                        double annualRate = rate;

                        periods = years;

                        jumlahAwal = jumlahAwal * Math.Pow((1 + annualRate), (double)periods);
                        break;
                    default:

                        MessageBox.Show(this, "Ada Error");
                        break;
                }
                totalValue = (decimal)jumlahAwal;

                if (totalValue <= decLabelMax)
                {
                    lblTtl.Text = String.Format("Rp. {0:###,###.##}", totalValue);

                }
                else
                {
                    MessageBox.Show("Jumlah melebihi nilai maximum");
                }
            }


        }

        private void btn2_Click(object sender, EventArgs e)
        {
            jlhawal.Value = 0;
            suku.Value = 0;
            cmb.Text = "Daily";
            prd.Value = 0;


            lblTtl.Text = "Rp. 0.00";
        }

        private void btncari_Click(object sender, EventArgs e)
        {


        }


        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void tx11_TextChanged(object sender, EventArgs e)
        {

            {
                string nama = tx11.Text;
                ViewCari cari = new ViewCari();
                DataTable tabel = new DataTable();
                tabel = cari.caridata(nama);
                dataGridView1.DataSource = tabel;


            }
        }






        internal class ViewData
        {
            MySqlCommand query;
            string sql;
            MySqlDataAdapter adapter;
            koneksiku sambung;
            DataTable tabel;
            internal DataTable bacasemua()
            {
                sambung = new koneksiku();
                sql = "select * from harian order by Tanggal asc";
                tabel = new DataTable();
                try
                {
                    sambung.buka();
                    query = new MySqlCommand(sql, sambung.koneksi);
                    adapter = new MySqlDataAdapter(query);
                    query.ExecuteNonQuery();
                    adapter.Fill(tabel);
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
                sambung.tutup();
                return tabel;
            }
            internal DataTable uangku()
            {
                sambung = new koneksiku();
                sql = "select * from uang order by awal asc";
                tabel = new DataTable();
                try
                {
                    sambung.buka();
                    query = new MySqlCommand(sql, sambung.koneksi);
                    adapter = new MySqlDataAdapter(query);
                    query.ExecuteNonQuery();
                    adapter.Fill(tabel);
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
                sambung.tutup();
                return tabel;
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            sambung = new koneksiku();
            sql = "delete from uang where total = '" + lblTtl.Text + "'";
            try
            {
               
                sambung.buka();
                query = new MySqlCommand(sql, sambung.koneksi);
                query.ExecuteNonQuery();
                dataGridView2.Refresh();
                MessageBox.Show("Data Berhasil DiHapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                jlhawal.Value = 0;
                suku.Value = 0;
                cmb.Text = "Daily";
                prd.Value = 0;
                lblTtl.Text = "Rp. 0.00";
                bt4.Enabled = false;
                bt3.Enabled = false;
                bt2.Enabled = true;

                tampil();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sambung.tutup();
            jlhawal.Value = 0;
            suku.Value = 0;
            cmb.Text = "Daily";
            prd.Value = 0;


            lblTtl.Text = "Rp. 0.00";

            bt4.Enabled = false;
            bt3.Enabled = false;
            bt1.Enabled = true;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            sambung = new koneksiku();
            sql = "delete from uang where Total = '" + lblTtl.Text + "'";
            try
            {
                sambung.buka();
                query = new MySqlCommand(sql, sambung.koneksi);
                query.ExecuteNonQuery();
                dataGridView2.Refresh();
                MessageBox.Show("Data Berhasil DiHapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bt4.Enabled = false;
                bt3.Enabled = false;
                bt2.Enabled = true;
                tampil();
                bersih();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sambung.tutup();
            bersih();
            bt4.Enabled = false;
            bt3.Enabled = false;
            bt1.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bt4_Click(object sender, EventArgs e)
        {
            sambung = new koneksiku();
            sql = "delete from harian where YOUR_NOTEPAD = '" + tx1.Text + "'";
            try
            {
                sambung.buka();
                query = new MySqlCommand(sql, sambung.koneksi);
                query.ExecuteNonQuery();
                dataGridView1.Refresh();
                MessageBox.Show("Data Berhasil DiHapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bt4.Enabled = false;
                bt3.Enabled = false;
                bt2.Enabled = true;
                tampiltabel();
                bersih();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sambung.tutup();
            bersih();
            bt4.Enabled = false;
            bt3.Enabled = false;
            bt1.Enabled = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void p8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void DM_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void prd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void suku_ValueChanged(object sender, EventArgs e)
        {

        }

        private void jlhawal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblTtl_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void tx1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void wk_Click(object sender, EventArgs e)
        {

        }

        private void lb2_Click(object sender, EventArgs e)
        {

        }

        private void tx2_TextChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void MenuVertical_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

     
        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ViewData tampilkan1 = new ViewData();
            DataTable tabel1 = new DataTable();
            tabel1 = tampilkan1.uangku();
            dataGridView2.DataSource = tabel1;
            dataGridView2.Columns[0].HeaderText = "Awal";
            dataGridView2.Columns[1].HeaderText = "Bunga(%)";
            dataGridView2.Columns[2].HeaderText = "Period";
            dataGridView2.Columns[3].HeaderText = "JenisPerhitungan";
            dataGridView2.Columns[4].HeaderText = "Total";
            dataGridView2.Columns[0].Width = 60;
            dataGridView2.Columns[1].Width = 60;
            dataGridView2.Columns[2].Width = 60;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            button5.Enabled = true;
            bt1.Enabled = false;
            bt3.Enabled = true;
            bt4.Enabled = true;
            DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

            jlhawal.Text = row.Cells["Awal"].Value.ToString();
            suku.Text = row.Cells["Bunga(%)"].Value.ToString();
            prd.Text = row.Cells["Period"].Value.ToString();
            cmb.Text = row.Cells["JenisPerhitungan"].Value.ToString();
            lblTtl.Text = row.Cells["Total"].Value.ToString();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

            string nama = textBox1.Text;
            ViewCari cari = new ViewCari();
            DataTable tabel = new DataTable();
            tabel = cari.cariuang(nama);
            dataGridView2.DataSource = tabel;

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }



