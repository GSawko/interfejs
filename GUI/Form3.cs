using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form3 : Form
    {
        private KLIENCI _currentEditKlient;

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


        private void button5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        
        private void IDKlientaTBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int id))
            {
                id = -1;
            }
            _currentEditKlient = ClientService.GetClient(id) ?? new KLIENCI();

            ShowKlientDataOnReservationScreen(_currentEditKlient);
        }

        private void NrDowOsTBox_TextChanged(object sender, EventArgs e)
        {
            string nrDowOsob = ((TextBox)sender).TextOrDefault();

            _currentEditKlient = null;
            _currentEditKlient = ClientService.GetClient(nrDowOsob, true) ?? new KLIENCI();
            ShowKlientDataOnReservationScreen(_currentEditKlient);
        }

        private void ShowKlientDataOnReservationScreen(KLIENCI klient)
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

        private void IDKlientaTBoxWEDK_TextChanged(object sender, EventArgs e)
        {

            if (!int.TryParse(((TextBox)sender).Text, out int id))
            {
                id = -1;
            }

            _currentEditKlient = ClientService.GetClient(id) ?? new KLIENCI();
            UpdateKlientDataFromEditScreen(_currentEditKlient);
        }

        private void NrDowOsTBoxWEDK_TextChanged(object sender, EventArgs e)
        {
            string nrDowOsob = ((TextBox)sender).TextOrDefault();
            _currentEditKlient = ClientService.GetClient(nrDowOsob, true) ?? new KLIENCI();
            UpdateKlientDataFromEditScreen(_currentEditKlient);
        }

        private void UpdateKlientDataFromEditScreen(KLIENCI klient)
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

            KategoriePJazdyCBoxEDKlienta.ClearItemChecked();
            foreach (var licence in klient.KATEGORIEPJAZDY)
            {
                KategoriePJazdyCBoxEDKlienta.SetItemChecked(licence.idKatPJ - 1, true);
            }
        }

        private void GetKlientDataFromEditScreen(KLIENCI klient)
        {
            klient.Imie = ImieTBoxEDKlienta.TextOrDefault();
            klient.DrugieImie = DrugieImieTBoxEDKlienta.TextOrDefault();
            klient.Nazwisko = NazwiskoTBoxEDKlienta.TextOrDefault();
            klient.Adres = AdresTBoxEDKlienta.TextOrDefault();
            klient.Telefon = TelefonTBoxEDKlienta.TextOrDefault();
            klient.Email = EmailTBoxEDKlienta.TextOrDefault();

            klient.KATEGORIEPJAZDY = new List<KATEGORIEPJAZDY>();
            for (int i = 0; i < KategoriePJazdyCBoxEDKlienta.CheckedIndices.Count; i++)
            {
                var index = KategoriePJazdyCBoxEDKlienta.CheckedIndices[i] + 1;
                klient.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = index });
            }
        }

        private KLIENCI LoadKlientDataFromAddScreen()
        {
            KLIENCI newClient = new KLIENCI();
            newClient.Imie = textBox10.TextOrDefault();
            newClient.DrugieImie = textBox11.TextOrDefault();
            newClient.Nazwisko = textBox13.TextOrDefault();
            newClient.Adres = textBox12.TextOrDefault();
            newClient.Plec = (sbyte)comboBox1.SelectedIndex;
            newClient.Telefon = textBox17.TextOrDefault();
            newClient.Email = textBox16.TextOrDefault();
            newClient.NrPrawaJazd = textBox15.TextOrDefault();
            newClient.NrDowOsob = textBox14.TextOrDefault();
            newClient.DataRejestr = DateTime.Now;
            newClient.DataUr = DateTime.Now;
            //newClient.Zdjecie = 

            for (int i = 0; i < checkedListBox3.CheckedIndices.Count; i++)
            {
                var index = checkedListBox3.CheckedIndices[i] + 1;
                newClient.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = index });
            }

            newClient.Login = "k_" + newClient.Imie + newClient.DataUr.ToString("dd");
            newClient.Haslo = newClient.Login;

            return newClient;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var addClient = LoadKlientDataFromAddScreen();
            var isAdd = ClientService.AddClient(addClient);
            if (isAdd)
                MessageBox.Show("Dodano klienta.");
            else
                MessageBox.Show("Błąd dodanie klienta!");
        }

        private void UpdateClientDataButton_Click(object sender, EventArgs e)
        {
            if (_currentEditKlient != null && _currentEditKlient.idKlient > 0)
            {
                GetKlientDataFromEditScreen(_currentEditKlient);
                var isEdt = ClientService.UpdateClient(_currentEditKlient);
                if (isEdt)
                    MessageBox.Show("Zaktualizowano dane klienta.");
                else
                    MessageBox.Show("Błąd podczas aktualizacji danych klienta!");

                _currentEditKlient = ClientService.GetClient(_currentEditKlient.idKlient);
            }
            else
                MessageBox.Show("Najpierw wybierz klienta do edycji!");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //TODO
            /* Obrazy są niskiej jakości ponieważ ImageList korzysta z bardzo słabego domyślnego algorytmu
             * zmiany rozmiaru. Należy albo zapewnić, że wczytywane obrazy nie wymagają zmiany rozmiaru,
             * albo zmienić rozmiar ręcznie przed wczytaniem do ImageList*/
            ImageList imageList = listView2.LargeImageList;
            for(int i=0;i<imageList.Images.Count;i++)
            {
                listView2.Items.Add(imageList.Images.Keys[i], i);
            }
            listView2.RedrawItems(0, imageList.Images.Count - 1, false);
        }

        private void listView2_ItemActivate(object sender, EventArgs e)
        {
            PodgladRezerwacjiPanel.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PodgladRezerwacjiPanel.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DodajKlientaPanel.BringToFront();
        }
    }
}
