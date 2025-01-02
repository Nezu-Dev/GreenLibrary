using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GreenLibrary
{
    public partial class BookManagement : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=GreenLibrary; Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        public void Doldur()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("SELECT BookID, KitapAdi, Yazar, Yayinevi, BasimYili, Tur, SayfaSayisi, Dil, RafKonumu, KapakTuru, ISBN FROM Books ORDER BY BookID DESC", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;

            // Adjust column headers
            dataGridView1.Columns["BookID"].Visible = false; // Hides the primary key
            dataGridView1.Columns["KitapAdi"].HeaderText = "Kitap Adı";
            dataGridView1.Columns["Yazar"].HeaderText = "Yazar";
            dataGridView1.Columns["Yayinevi"].HeaderText = "Yayınevi";
            dataGridView1.Columns["BasimYili"].HeaderText = "Basım Yılı";
            dataGridView1.Columns["Tur"].HeaderText = "Tür";
            dataGridView1.Columns["SayfaSayisi"].HeaderText = "Sayfa Sayısı";
            dataGridView1.Columns["Dil"].HeaderText = "Dil";
            dataGridView1.Columns["RafKonumu"].HeaderText = "Raf Konumu";
            dataGridView1.Columns["KapakTuru"].HeaderText = "Kapak Türü";
            dataGridView1.Columns["ISBN"].HeaderText = "ISBN";

            con.Close();
        }

        private void AdjustTextBoxHeight(TextBox textBox)
        {
            textBox.Height = textBox.PreferredHeight;
        }

        public BookManagement()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private void AdjustColumnWidths()
        {
            dataGridView1.Columns["KitapAdi"].Width = 150;
            dataGridView1.Columns["Yazar"].Width = 100;
            dataGridView1.Columns["Yayinevi"].Width = 120;
            dataGridView1.Columns["BasimYili"].Width = 80;
            dataGridView1.Columns["Tur"].Width = 100;
            dataGridView1.Columns["SayfaSayisi"].Width = 80;
            dataGridView1.Columns["Dil"].Width = 70;
            dataGridView1.Columns["RafKonumu"].Width = 90;
            dataGridView1.Columns["KapakTuru"].Width = 100;
            dataGridView1.Columns["ISBN"].Width = 150;
        }

        private void BookManagement_Load(object sender, EventArgs e)
        {
            Doldur();

            textBox1.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox1);

            textBox2.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox2);

            textBox3.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox3);

            textBox4.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox4);

            textBox5.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox5);

            textBox6.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox6);

            textBox7.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox7);

            textBox8.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox8);

            textBox9.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox9);

            textBox10.Font = new Font("Poppins", 10, FontStyle.Bold);
            AdjustTextBoxHeight(textBox10);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
            cmd = new SqlCommand(
                "INSERT INTO Books (KitapAdi, Yazar, Yayinevi, BasimYili, Tur, SayfaSayisi, Dil, RafKonumu, KapakTuru, ISBN) " +
                "VALUES (@kitapAdi, @yazar, @yayinevi, @basimYili, @tur, @sayfaSayisi, @dil, @rafKonumu, @kapakTuru, @isbn)",
                con
            );

            con.Open();
            cmd.Parameters.AddWithValue("@kitapAdi", textBox1.Text);
            cmd.Parameters.AddWithValue("@yazar", textBox2.Text);
            cmd.Parameters.AddWithValue("@yayinevi", textBox3.Text);
            cmd.Parameters.AddWithValue("@basimYili", textBox4.Text);
            cmd.Parameters.AddWithValue("@tur", textBox5.Text);
            cmd.Parameters.AddWithValue("@sayfaSayisi", textBox6.Text);
            cmd.Parameters.AddWithValue("@dil", textBox7.Text);
            cmd.Parameters.AddWithValue("@rafKonumu", textBox8.Text);
            cmd.Parameters.AddWithValue("@kapakTuru", textBox9.Text);
            cmd.Parameters.AddWithValue("@isbn", textBox10.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            Doldur();
        }
    }
}
