using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GreenLibrary
{
    public partial class SignUp : Form
    {
        // Windows API importları
        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        public SignUp()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Formu kapatma
            this.Close();
        }

        // Panel tıklama işlemi ile formu taşıma
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Focus();  // TextBox'a odaklanmayı sağlar

        }
    }
}
