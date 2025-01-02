using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Data.SqlClient;

namespace GreenLibrary
{
    public partial class LogIn : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog =GreenLibrary; Integrated Security = true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        public LogIn()
        {
            InitializeComponent();
        }

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Bu alan boş bırakılmış.
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Bu alan boş bırakılmış.
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        public bool KullaniciDogrula(string username, string password)
        {
            string query = "SELECT COUNT(1) FROM RegUser WHERE Username = @username AND PasswordGL = @passwordgl";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@passwordgl", password);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            if (count == 1)
            {
                return true; // Kullanıcı varsa true döner
            }
            else
            {
                return false; // Kullanıcı yoksa false döner
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
           

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = ' ';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignUp form2 = new SignUp();
            form2.Show();             // Yeni formu açar
            this.Hide();              // Mevcut formu gizler

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen tüm gerekli alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            string username = textBox1.Text; // Kullanıcı adı
            string password = textBox2.Text; // Şifre

            if (KullaniciDogrula(username, password))
            {
                MessageBox.Show("Giriş başarılı!");
                // Yeni bir form açabilir veya işlemlere devam edebilirsiniz
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ForgotPassword form3 = new ForgotPassword();
            form3.Show();             // Yeni formu açar
            this.Hide();
        }
    }
}