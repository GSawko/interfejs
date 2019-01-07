using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form2 : Form
    {
        private string Login;
        private KLIENCI CurrClient;
        private ClientService CS;
        public Form2(string login)
        {
            InitializeComponent();
            LoadCheckedListBox(checkedListBox3);

            Login = login;
            CS = new ClientService();
            LoadClient(Login);
            LoadClientData();
        }

        private void LoadClient(string login)
        {
            CurrClient = CS.GetClient(login);
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

        private void LoadClientData()
        {
            //boczne menu
            label2.Text = CurrClient.Imie + " " + CurrClient.Nazwisko;

            //zakładka dane
            textBox10.Text = CurrClient.Imie;
            textBox11.Text = CurrClient.DrugieImie;
            textBox13.Text = CurrClient.Nazwisko;
            textBox12.Text = CurrClient.Adres;
            comboBox1.SelectedIndex = CurrClient.Plec;
            textBox17.Text = CurrClient.Telefon;
            textBox16.Text = CurrClient.Email;
            textBox15.Text = CurrClient.NrPrawaJazd;
            textBox14.Text = CurrClient.NrDowOsob;
            textBox20.Text = CurrClient.DataRejestr.ToString("d");

            foreach (var licence in CurrClient.KATEGORIEPJAZDY)
            {
                checkedListBox3.SetItemChecked(licence.idKatPJ - 1, true);
            }
            //pictureBox7.Image = klient.Zdjecie;

            textBox19.Text = CurrClient.Login;
            textBox18.Text = CurrClient.Haslo;
        }

        private void LoadKlientDataFromEditScreen(KLIENCI klient)
        {
            klient.Imie = textBox10.TextOrDefault();
            klient.DrugieImie = textBox11.TextOrDefault();
            klient.Nazwisko = textBox13.TextOrDefault();
            klient.Adres = textBox12.TextOrDefault();
            klient.Telefon = textBox17.TextOrDefault();
            klient.Email = textBox16.TextOrDefault();
            klient.Haslo = textBox18.TextOrDefault();
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

        private void button8_Click(object sender, EventArgs e)
        {
            LoadKlientDataFromEditScreen(CurrClient);

            CurrClient.KATEGORIEPJAZDY = new List<KATEGORIEPJAZDY>();
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (checkedListBox3.GetItemCheckState(i) == CheckState.Checked)
                {
                    CurrClient.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = i + 1 });
                }
            }

            if (CS.UpdateClient(CurrClient))
            {
                MessageBox.Show("Zapisano zmiany.");
            }
            else
            {
                MessageBox.Show("Nie można zapisać zmian!");
            }

            //Update Klient object from DB
            CurrClient = CS.GetClient(Login);
        }

    }
}
