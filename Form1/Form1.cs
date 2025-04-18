using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OtobusKartApp
{
    public partial class Form1 : Form
    {
        // ✅ Düzgün bağlanıp bağlanmadığını kontrol etmek için bağlantı dizesini tekrar tanımla
        private string connectionString = @"Server=SERMET\SQLEXPRESS;Database=OtobusKartDB;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "INSERT INTO OtobusKart (Isim, Soyisim, DogumTarihi, Ogrenci) VALUES (@Isim, @Soyisim, @DogumTarihi, @Ogrenci)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        string isim = txtIsim.Text;
                        string soyisim = txtSoyisim.Text;
                        DateTime dogumTarihi = dtpDogumTarihi.Value; // DateTimePicker kontrolünden tarih al
                        bool ogrenciMi = chkOgrenci.Checked; // CheckBox kontrolünden öğrenci olup olmadığını al
                        cmd.Parameters.AddWithValue("@Isim", isim);
                        cmd.Parameters.AddWithValue("@Soyisim", soyisim);
                        cmd.Parameters.AddWithValue("@DogumTarihi", dogumTarihi.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Ogrenci", ogrenciMi);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("✅ Veri başarıyla eklendi!");
                        }
                        else
                        {
                            MessageBox.Show("❌ Veri eklenemedi!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MessageBox.Show("Bağlantı başarılı!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı hatası: " + ex.Message);
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM OtobusKart";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvKartlar.DataSource = dt;
            }
        }

    }
}
