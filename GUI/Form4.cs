using GUI.Service;
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
    public partial class Form4 : Form
    {
        private KLIENCI _currentEditKlient;
        private PRACOWNICY _currentEditPracownik;
        private POJAZDY _currentEditPojazd;
        private int _indexPojazd;
        private List<POJAZDY> _listPojazd;

        public Form4(string Login)
        {
            InitializeComponent();
            LoadMenuData(Login);
            LoadCheckedListBox();
            LoadComboBox(comboBox5);
        }

        private void LoadMenuData(string login)
        {
            var owner = WorkerService.GetWorker(login);

            label2.Text = owner.Imie + " " + owner.Nazwisko;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DodajKlientaPanel.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EdytujDaneKlientaPanel.BringToFront();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            EdytujPracownikaPanel.BringToFront();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            maskedTextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DodajPracownikaPanel.BringToFront();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            EdytujPojazdPanel.BringToFront();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DodajPojazdPanel.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DodajKlientaPanel.BringToFront();
            textBox20.Text = DateTime.Now.ToString("d");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PodgladRezerwacjiPanel.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Restart();
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

        private void LoadCheckedListBox()
        {
            LoadCheckedListBox(KategoriePJazdyCBoxEDKlienta);
            LoadCheckedListBox(checkedListBox3);
            LoadCheckedListBox(checkedListBox6);
        }

        private void LoadComboBox(ComboBox comboBox)
        {
            using (var entities = new DBEntities())
            {
                foreach (var model in entities.MARKI)
                {
                    comboBox.Items.Add(model);
                }
                comboBox.SelectedIndex = 0;
            }
        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int id))
            {
                id = -1;
            }
            _currentEditPracownik = WorkerService.GetWorker(id) ?? new PRACOWNICY();

            ShowWorkerOnEditScreen(_currentEditPracownik);
        }

        private void ShowWorkerOnEditScreen(PRACOWNICY pracownicy)
        {
            textBox32.Text = pracownicy.Imie;
            textBox26.Text = pracownicy.DrugieImie;
            textBox35.Text = pracownicy.Nazwisko;
            textBox38.Text = pracownicy.Adres;
            comboBox2.SelectedIndex = pracownicy.Plec == 1 ? 1 : 0;
            maskedTextBox6.Text = pracownicy.Telefon;
            textBox36.Text = pracownicy.Email;
            textBox24.Text = pracownicy.DataZatr.ToString("d");
            textBox19.Text = pracownicy.DataUr.ToString("d");

            //pictureBox9.Image =
        }

        private void LoadWorkerFromEditScreen(PRACOWNICY editWorker)
        {
            editWorker.Imie = textBox32.TextOrDefault();
            editWorker.DrugieImie = textBox26.TextOrDefault();
            editWorker.Nazwisko = textBox35.TextOrDefault();
            editWorker.Adres = textBox38.TextOrDefault();
            editWorker.Telefon = maskedTextBox6.TextOrDefault();
            editWorker.Email = textBox36.TextOrDefault();

            //editWorker.Zdjecie =
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_currentEditPracownik != null && _currentEditPracownik.idPrac > 0)
            {
                LoadWorkerFromEditScreen(_currentEditPracownik);
                var isEdit = WorkerService.UpdateWorker(_currentEditPracownik);
                if (isEdit)
                    MessageBox.Show("Zaktualizowano dane pracownika!");
                else
                    MessageBox.Show("Błąd podczas aktualizacji danych pracownika");

                _currentEditPracownik = WorkerService.GetWorker(_currentEditPracownik.idPrac);
            }
            else
                MessageBox.Show("Najpierw wybierz pracownika do edycji!");
        }

        private void IDKlientaTBoxWEDK_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int id))
            {
                id = -1;
            }

            _currentEditKlient = ClientService.GetClient(id) ?? new KLIENCI();
            ShowClientOnEditScreen(_currentEditKlient);
        }

        private void NrDowOsTBoxWEDK_TextChanged(object sender, EventArgs e)
        {
            string nrDowOs = ((TextBox)sender).TextOrDefault();
            _currentEditKlient = ClientService.GetClient(nrDowOs, true) ?? new KLIENCI();
            ShowClientOnEditScreen(_currentEditKlient);
        }

        private void ShowClientOnEditScreen(KLIENCI klient)
        {
            ImieTBoxEDKlienta.Text = klient.Imie;
            DrugieImieTBoxEDKlienta.Text = klient.DrugieImie;
            NazwiskoTBoxEDKlienta.Text = klient.Nazwisko;
            AdresTBoxEDKlienta.Text = klient.Adres;
            PlecComboEDKlienta.SelectedIndex = klient.Plec == 1 ? 1 : 0;
            textBox18.Text = klient.DataUr.ToString("d");
            maskedTextBox5.Text = klient.Telefon;
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

        private void LoadClientFromEditScreen(KLIENCI editClient)
        {
            editClient.Imie = ImieTBoxEDKlienta.TextOrDefault();
            editClient.DrugieImie = DrugieImieTBoxEDKlienta.TextOrDefault();
            editClient.Nazwisko = NazwiskoTBoxEDKlienta.TextOrDefault();
            editClient.Adres = AdresTBoxEDKlienta.TextOrDefault();
            editClient.Telefon = maskedTextBox5.TextOrDefault();
            editClient.Email = EmailTBoxEDKlienta.TextOrDefault();
            //editClient.Zdjecie =

            editClient.KATEGORIEPJAZDY = new List<KATEGORIEPJAZDY>();
            for (int i = 0; i < KategoriePJazdyCBoxEDKlienta.CheckedIndices.Count; i++)
            {
                var index = KategoriePJazdyCBoxEDKlienta.CheckedIndices[i] + 1;
                editClient.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = index });
            }
        }

        private void UpdateClientDataButton_Click(object sender, EventArgs e)
        {
            if (_currentEditKlient != null && _currentEditKlient.idKlient > 0)
            {
                LoadClientFromEditScreen(_currentEditKlient);
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

        private KLIENCI GetClientFromAddScreen()
        {
            KLIENCI newClient = new KLIENCI();
            newClient.Imie = textBox10.TextOrDefault();
            newClient.DrugieImie = textBox11.TextOrDefault();
            newClient.Nazwisko = textBox13.TextOrDefault();
            newClient.Adres = textBox12.TextOrDefault();
            newClient.Plec = (sbyte)comboBox1.SelectedIndex;
            newClient.DataUr = DateTime.Parse(maskedTextBox4.Text);
            newClient.Telefon = maskedTextBox7.TextOrDefault();
            newClient.Email = textBox16.TextOrDefault();
            newClient.NrPrawaJazd = textBox15.TextOrDefault();
            newClient.NrDowOsob = textBox14.TextOrDefault();
            newClient.DataRejestr = DateTime.Now;
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
            var addClient = GetClientFromAddScreen();
            var isAdd = ClientService.AddClient(addClient);
            if (isAdd)
                MessageBox.Show("Dodano klienta.");
            else
                MessageBox.Show("Błąd dodanie klienta!");
        }

        private PRACOWNICY GetWorkerFromAddScreen()
        {
            PRACOWNICY newWorker = new PRACOWNICY();
            newWorker.Imie = textBox42.TextOrDefault();
            newWorker.DrugieImie = textBox40.TextOrDefault();
            newWorker.Nazwisko = textBox45.TextOrDefault();
            newWorker.Adres = textBox47.TextOrDefault();
            newWorker.Plec = (sbyte)comboBox3.SelectedIndex;
            newWorker.Telefon = maskedTextBox8.TextOrDefault();
            newWorker.Email = textBox46.TextOrDefault();
            newWorker.DataZatr = DateTime.Now;
            newWorker.DataUr = DateTime.Parse(maskedTextBox3.Text);
            //newWorker.Zdjecie =

            newWorker.Login = "p_" + newWorker.Imie + newWorker.DataUr.ToString("dd");
            newWorker.Haslo = newWorker.Login;

            return newWorker;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var addWorker = GetWorkerFromAddScreen();
            var isAdd = WorkerService.AddWorker(addWorker);
            if (isAdd)
                MessageBox.Show("Dodano pracownika.");
            else
                MessageBox.Show("Błąd dodania pracownika!");
        }

        private POJAZDY GetVehicleFromAddScreen()
        {
            POJAZDY newVehicle = new POJAZDY();
            newVehicle.Rodzaj = (sbyte)comboBox4.SelectedIndex;
            newVehicle.NrRejestr = textBox43.TextOrDefault();
            newVehicle.Przebieg = int.Parse(textBox49.Text);
            newVehicle.ZaGodz = float.Parse(textBox51.Text);
            newVehicle.DataProd = DateTime.Parse(maskedTextBox1.Text);
            newVehicle.Sprawny = (sbyte)(checkBox3.Checked == true ? 1 : 0);
            newVehicle.Opis = richTextBox4.TextOrDefault();
            newVehicle.Kolor = textBox50.TextOrDefault();
            newVehicle.MARKI_idMarki = ((MARKI)comboBox5.SelectedItem).idMarki;

            for (int i = 0; i < checkedListBox6.CheckedIndices.Count; i++)
            {
                var index = checkedListBox6.CheckedIndices[i] + 1;
                newVehicle.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = index });
            }

            return newVehicle;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var addVehicle = GetVehicleFromAddScreen();
            var isAdd = VehicleService.AddVehicle(addVehicle);
            if (isAdd)
                MessageBox.Show("Dodano pojazd.");
            else
                MessageBox.Show("Błąd dodania pojazdu!");
        }
    }
}
