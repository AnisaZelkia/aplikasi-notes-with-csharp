using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Tugas_Besar
{
    class koneksiku
       
    {
        string konek = "Server=localhost;Database=tubes;Uid=root;Pwd=";

        public MySqlConnection koneksi;

        public void buka()
        {
            koneksi = new MySqlConnection(konek);
            koneksi.Open();
        }
        public void tutup()
        {
            koneksi = new MySqlConnection(konek);
            koneksi.Close();
        }
    }
}
