using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GreenLibrary
{
    public partial class SignUp : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=GreenLibrary; Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        private void AdjustTextBoxHeight(TextBox textBox)
        {
            textBox.Height = textBox.PreferredHeight;
        }

        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        public void UserAdd()
        {
            cmd = new SqlCommand(
                "Insert Into RegUser(Username, PasswordGL, Email, FavoriteBook) Values(@username, @passwordgl, @email, @favoritebook)",
                con
            );
            con.Open();
            cmd.Parameters.AddWithValue("@username", textBox1.Text);
            cmd.Parameters.AddWithValue("@passwordgl", textBox2.Text);
            cmd.Parameters.AddWithValue("@email", textBox3.Text);
            cmd.Parameters.AddWithValue("@favoritebook", textBox4.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public SignUp()
        {
            InitializeComponent();
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

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                DialogResult msjsonuc = MessageBox.Show(
                    "Lütfen tüm gerekli alanları doldurun.",
                    "Uyarı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            else
            {
                DialogResult msjsonuc2 = MessageBox.Show(
                    "Başarıyla kayıt oldunuz.",
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                if (msjsonuc2 == DialogResult.OK)
                {
                    LogIn Grs = new LogIn();
                    Grs.Show();
                    this.Hide();
                    UserAdd();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox1);

            textBox2.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox2);

            textBox3.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox3);

            textBox4.Font = new Font("Poppins", 14, FontStyle.Bold);
            AdjustTextBoxHeight(textBox4);
        }
    }
}
