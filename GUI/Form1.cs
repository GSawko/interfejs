using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        // komentarz212131231232
        private BazaDanych BD;
        public string Login;
        public Form1()
        {
            InitializeComponent();
            BD = new BazaDanych();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BD.Zaloguj(textBox1.Text, textBox2.Text))
            {
                DialogResult = DialogResult.OK;
                Login = textBox1.Text;
            }
            else
                MessageBox.Show("Nieprawidłowy login lub hasło");
        }
    }
}
