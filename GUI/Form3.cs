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
        public Form3(string login)
        {
            InitializeComponent();
            LoadCheckedListBox(KategoriePJazdyCBoxEDKlienta);
            LoadCheckedListBox(checkedListBox3);

            using (var entities = new DBEntities())
            {
                var pracownik = entities.PRACOWNICY.Where(p => p.Login == login).First();
                label2.Text = pracownik.Imie + " " + pracownik.Nazwisko;
            }
        }

        private void LoadCheckedListBox(CheckedListBox checkedListBox)
        {
            using (var entities = new DBEntities())
            {
                var driveLicenceType = entities.KATEGORIEPJAZDY.ToList();

                foreach (var licenceType in driveLicenceType)
                {
                    checkedListBox.Items.Add(licenceType.Nazwa);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox20.Text = DateTime.Now.ToString("d");
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
            if (!int.TryParse(((TextBox)sender).Text, out int id))
                return;

            KLIENCI klient = null;
            using (var entities = new DBEntities())
            {
                klient = entities.KLIENCI.FirstOrDefault(k => k.idKlient == id);
            }

            if (klient != null)
            {
                ImieDispLabel.Text = klient.Imie;
                DrugieImieDispLabel.Text = klient.DrugieImie;
                NazwiskoDispLabel.Text = klient.Nazwisko;
                DataUrDispLabel.Text = klient.DataUr.ToString("d");
                TelefonDispLabel.Text = klient.Telefon;
                PlecDispLabel.Text = klient.Plec == 1 ? "Kobieta" : "Mężczyzna";
                AdresDispLabel.Text = klient.Adres;
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

        private void IDKlientaTBoxWEDK_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int id))
                return;

            KLIENCI klient = null;
            using (var entites = new DBEntities())
            {
                klient = entites.KLIENCI.FirstOrDefault(k => k.idKlient == id);
                var saveToCache = klient?.KATEGORIEPJAZDY;
            }

            if (klient != null)
            {
                ImieTBoxEDKlienta.Text = klient.Imie;
                DrugieImieTBoxEDKlienta.Text = klient.DrugieImie;
                NazwiskoTBoxEDKlienta.Text = klient.Nazwisko;
                DataUrDispLabel.Text = klient.DataUr.ToString("d");
                TelefonTBoxEDKlienta.Text = klient.Telefon;
                PlecComboEDKlienta.SelectedIndex = klient.Plec == 1 ? 1 : 0;
                AdresTBoxEDKlienta.Text = klient.Adres;
                EmailTBoxEDKlienta.Text = klient.Email;
                NrPJazdyTBoxEDKlienta.Text = klient.NrPrawaJazd;
                NrDowOsTBoxEDKlienta.Text = klient.NrDowOsob;
                DataRejestracjiTBoxEDKlienta.Text = klient.DataRejestr.ToString("d");
                foreach (var licence in klient.KATEGORIEPJAZDY)
                {
                    KategoriePJazdyCBoxEDKlienta.SetItemChecked(licence.idKatPJ - 1, true);
                }
            }
            else
            {
                ImieTBoxEDKlienta.Text = "";
                DrugieImieTBoxEDKlienta.Text = "";
                NazwiskoTBoxEDKlienta.Text = "";
                DataUrDispLabel.Text = "";
                TelefonTBoxEDKlienta.Text = "";
                PlecComboEDKlienta.SelectedIndex = 0;
                AdresTBoxEDKlienta.Text = "";
                EmailTBoxEDKlienta.Text = "";
                NrPJazdyTBoxEDKlienta.Text = "";
                NrDowOsTBoxEDKlienta.Text = "";
                DataRejestracjiTBoxEDKlienta.Text = "";
                while (KategoriePJazdyCBoxEDKlienta.CheckedIndices.Count > 0)
                    KategoriePJazdyCBoxEDKlienta.SetItemChecked(KategoriePJazdyCBoxEDKlienta.CheckedIndices[0], false);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            KLIENCI newKlient = new KLIENCI();
            newKlient.Imie = textBox10.Text;
            newKlient.DrugieImie = textBox11.Text;
            newKlient.Nazwisko = textBox13.Text;
            newKlient.Adres = textBox12.Text;
            newKlient.Plec = (sbyte)comboBox1.SelectedIndex;
            newKlient.Telefon = textBox17.Text;
            newKlient.Email = textBox16.Text;
            newKlient.NrPrawaJazd = textBox15.Text;
            newKlient.NrDowOsob = textBox14.Text;
            newKlient.DataRejestr = DateTime.Now;
            newKlient.DataUr = DateTime.Now;
            newKlient.Login = "Test";
            newKlient.Haslo = "Test";

            using (var entities = new DBEntities())
            {
                for (int i = 0; i < checkedListBox3.CheckedIndices.Count; i++)
                {
                    var index = checkedListBox3.CheckedIndices[i];
                    var licence = entities.KATEGORIEPJAZDY.FirstOrDefault(k => k.idKatPJ == index + 1);
                    if (licence != null)
                        newKlient.KATEGORIEPJAZDY.Add(licence);
                }

                entities.KLIENCI.Add(newKlient);
                entities.SaveChanges();
            }
        }
    }
}
