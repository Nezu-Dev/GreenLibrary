using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GreenLibrary
{
    public partial class AdminLogin : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=GreenLibrary; Integrated Security=true;");
        SqlCommand cmd;

        private void AdjustTextBoxHeight(TextBox textBox)
        {
            textBox.Height = textBox.PreferredHeight;
        }

        public AdminLogin()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void AdminLogin_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox1);

            textBox2.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox2);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private bool ValidateAdmin(string adminName, string adminPassword)
        {
            string query = "SELECT COUNT(1) FROM Admin WHERE AdminName = @adminName AND AdminPassword = @adminPassword";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@adminName", adminName);
            cmd.Parameters.AddWithValue("@adminPassword", adminPassword);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return count == 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LogIn login = new LogIn();
            login.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string adminName = textBox1.Text.Trim(); // Get the Name input
            string adminPassword = textBox2.Text.Trim(); // Get the Password input

            if (string.IsNullOrEmpty(adminName) || string.IsNullOrEmpty(adminPassword))
            {
                MessageBox.Show("Lütfen tüm gerekli alanları doldurun.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateAdmin(adminName, adminPassword)) // Call the validation method
            {
                MessageBox.Show("Giriş başarılı!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Redirect to admin dashboard or next form
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
