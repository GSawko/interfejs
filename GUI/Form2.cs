using GUI.Models;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form2 : Form
    {
        private BazaDanych BD;
        private Klient klient;
        public Form2(string login)
        {
            InitializeComponent();
            BD = new BazaDanych();
            klient = BD.WczytajKlienta(login);

            ZaladujDaneKlienta();
        }

        private void ZaladujDaneKlienta()
        {
            //boczne menu
            label2.Text = klient.ImieNazwisko;

            //zakładka dane
            textBox10.Text = klient.Imie;
            textBox11.Text = klient.DrugieImie;
            textBox13.Text = klient.Nazwisko;
            //textBox12.Text = klient.Adres;
            comboBox1.Items.AddRange(new object[] { "M", "K" });
            comboBox1.SelectedIndex = klient.Plec ? 1 : 0;
            textBox17.Text = klient.Telefon;
            textBox16.Text = klient.Email;
            textBox15.Text = klient.NrPrawaJazd;
            textBox14.Text = klient.NrDowOsob;
            textBox20.Text = klient.DataRejestracji.ToString("d");

            //checkedListBox3.  //Kategorie
            //pictureBox7.Image = klient.Zdjecie;

            textBox19.Text = klient.Login;
            textBox18.Text = klient.Haslo;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.WybórPojazduPanel.BringToFront();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            FormularzRezerwacjiPanel.BringToFront();
        }

        private void buttonMakeReservation_Click(object sender, EventArgs e)
        {
            FormularzRezerwacjiPanel.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EdytujDaneOsPanel.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KontaktPanel.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void buttonShowReservation_Click(object sender, EventArgs e)
        {

        }
    }
}
