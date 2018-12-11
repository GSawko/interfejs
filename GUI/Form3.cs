using GUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form3 : Form
    {
        private BazaDanych BD;
        public Form3(string login)
        {
            InitializeComponent();
            BD = new BazaDanych();
            Pracownik pracownik = BD.WczytajPracownika(login);

            label2.Text = pracownik.ImieNazwisko;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DodajKlientaPanel.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WyszukiwanieRezerwacjiPanel.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EdytujDaneKlientaPanel.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PrzeglądanieRezerwacjiPanel.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void IDKlientaTBox_TextChanged(object sender, EventArgs e)
        {
            string id = ((TextBox)sender).Text;
            Klient klient = BD.SzukajPoID(id);

            if(klient != null)
            {
                ImieDispLabel.Text = klient.Imie;
                DrugieImieDispLabel.Text = klient.DrugieImie;
                NazwiskoDispLabel.Text = klient.Nazwisko;
                //DataUrDispLabel.Text = klient.DataUrodzenia.ToString("d");
                TelefonDispLabel.Text = klient.Telefon;
                PlecDispLabel.Text = klient.Plec ? "Kobieta" : "Mężczyzna";
                //AdresDispLabel.Text = klient.Adres;
                EmailDispLabel.Text = klient.Email;
            }
            else
            {
                ImieDispLabel.Text = "";
                DrugieImieDispLabel.Text = "";
                NazwiskoDispLabel.Text = "";
                DataUrDispLabel.Text = "";
                TelefonDispLabel.Text = "";
                PlecDispLabel.Text = "";
                AdresDispLabel.Text = "";
                EmailDispLabel.Text = "";
            }
        }
    }
}
