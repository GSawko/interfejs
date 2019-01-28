using System;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        public char UserType { get; private set; }
        public string UserLogin { get; private set; }
        public bool LoginStatus { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;

            using (var entities = new DBEntities())
            {
                var isGoodAuth = false;
                try
                {
                    isGoodAuth = entities.LoginData.Where(u => u.Login == login && u.Haslo == password).Any();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Brak połączenia z internetem!");
                    return;
                }

                if (isGoodAuth == true)
                {
                    UserType = login[0];
                    UserLogin = login;
                    LoginStatus = true;
                    this.Close();
                }
                else
                {
                    LoginStatus = false;
                    MessageBox.Show("Nieprawidłowy login lub hasło");
                }
            }

        }
    }
}
