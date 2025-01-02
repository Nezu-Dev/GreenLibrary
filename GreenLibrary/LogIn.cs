using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GreenLibrary
{
    public partial class LogIn : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=GreenLibrary; Integrated Security=true;");
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

        private void AdjustTextBoxHeight(TextBox textBox)
        {
            textBox.Height = textBox.PreferredHeight;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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

            return count == 1;
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
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignUp form2 = new SignUp();
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen tüm gerekli alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = textBox1.Text;
            string password = textBox2.Text;

            if (KullaniciDogrula(username, password))
            {
                MessageBox.Show("Giriş başarılı!");
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ForgotPassword form3 = new ForgotPassword();
            form3.Show();
            this.Hide();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox1);

            textBox2.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminLogin admin = new AdminLogin();
            admin.Show();
            this.Hide();
        }
    }
}
