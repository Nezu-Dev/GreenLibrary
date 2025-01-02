using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GreenLibrary
{
    public partial class ForgotPassword : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=GreenLibrary; Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        public ForgotPassword()
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
            string username = textBox1.Text;
            string email = textBox2.Text;
            string favoriteBook = textBox3.Text;

            string query = "SELECT PasswordGL FROM RegUser WHERE Username = @username AND Email = @email AND FavoriteBook = @favoriteBook";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@favoriteBook", favoriteBook);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            var result = cmd.ExecuteScalar();

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox1);

            textBox2.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox2);

            textBox3.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox3);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
