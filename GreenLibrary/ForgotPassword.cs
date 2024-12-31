using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; // Windows API için gerekli
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenLibrary
{
    public partial class ForgotPassword : Form
    {

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog =GreenLibrary; Integrated Security = true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        public ForgotPassword()
        {
            InitializeComponent();
        }

        
        


        // Windows API işlevlerini tanımlıyoruz
        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Panel üzerine MouseDown olayı ekliyoruz
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text; // Kullanıcı adı
            string email = textBox2.Text;   // E-posta
            string favoriteBook = textBox3.Text; // Favori kitap

            // SQL sorgusu
            string query = "SELECT PasswordGL FROM RegUser WHERE Username = @username AND Email = @email AND FavoriteBook = @favoriteBook";

            // SQL komutu oluştur
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@favoriteBook", favoriteBook);

            // Veritabanı bağlantısını aç
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            // Sorguyu çalıştır ve sonucu al
            var result = cmd.ExecuteScalar();

            // Bağlantıyı kapat
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            // Sonucu kontrol et
            if (result != null)
            {
                string password = result.ToString();
                MessageBox.Show($"Şifreniz: {password}", "Şifre Bulundu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Girilen bilgilerle eşleşen bir hesap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
