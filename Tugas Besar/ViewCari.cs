using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Tugas_Besar

{
    class ViewCari
    {

        

        MySqlCommand query;
            string sql;
            MySqlDataAdapter adapter;
            koneksiku sambung;
            DataTable tabel;

            public DataTable caridata(string x)
            {
                sambung = new koneksiku();
                 sql = "select * from harian where  YOUR_NOTEPAD like '%"+x+"%'" ;
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
        public DataTable cariuang(string x)
        {
            sambung = new koneksiku();
            sql = "select * from uang where  awal like '%" + x + "%'";
            tabel = new DataTable ();
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
}
