using GUI.GridView;
using GUI.Service;
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
        private List<ClientListGrid> _clientGridList = new List<ClientListGrid>();

        private REZERWACJE _currentEditReservation;
        private List<ReservationListGrid> _reservationListGrid = new List<ReservationListGrid>();

        public Form3(string login)
        {
            InitializeComponent();
            LoadCheckedListBox(KategoriePJazdyCBoxEDKlienta);
            LoadCheckedListBox(checkedListBox3);
            timer1.Start();

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
            textBox11.Text = DateTime.Now.ToString("d");
            DodajKlientaPanel.BringToFront();
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
            ShowClientOnEditScreen(_currentEditKlient);
        }

        private void NrDowOsTBoxWEDK_TextChanged(object sender, EventArgs e)
        {
            string nrDowOsob = ((TextBox)sender).TextOrDefault();
            _currentEditKlient = ClientService.GetClient(nrDowOsob, true) ?? new KLIENCI();
            ShowClientOnEditScreen(_currentEditKlient);
        }

        private void ShowClientOnEditScreen(KLIENCI klient)
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
            LoginTBoxEDKlienta.Text = klient.Login;

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
            editClient.Telefon = TelefonTBoxEDKlienta.TextOrDefault();
            editClient.Email = EmailTBoxEDKlienta.TextOrDefault();
            editClient.NrPrawaJazd = NrPJazdyTBoxEDKlienta.TextOrDefault();
            editClient.NrDowOsob = NrDowOsTBoxEDKlienta.TextOrDefault();
            //editClient.Zdjecie =

            editClient.KATEGORIEPJAZDY = new List<KATEGORIEPJAZDY>();
            for (int i = 0; i < KategoriePJazdyCBoxEDKlienta.CheckedIndices.Count; i++)
            {
                var index = KategoriePJazdyCBoxEDKlienta.CheckedIndices[i] + 1;
                editClient.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = index });
            }
        }

        private KLIENCI GetClientFromAddScreen()
        {
            KLIENCI newClient = new KLIENCI();
            newClient.Imie = textBox13.TextOrDefault();
            newClient.DrugieImie = textBox12.TextOrDefault();
            newClient.Nazwisko = textBox16.TextOrDefault();
            newClient.Adres = textBox20.TextOrDefault();
            newClient.Plec = (sbyte)comboBox1.SelectedIndex;
            newClient.DataUr = DateTime.Parse(maskedTextBox4.Text);
            newClient.Telefon = maskedTextBox1.TextOrDefault();
            newClient.Email = textBox17.TextOrDefault();
            newClient.NrPrawaJazd = textBox15.TextOrDefault();
            newClient.NrDowOsob = textBox14.TextOrDefault();
            newClient.DataRejestr = DateTime.Now;

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

        private void button4_Click(object sender, EventArgs e)
        {
            DodajKlientaPanel.BringToFront();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            LoadClientList();
            ListaKlientowPanel.BringToFront();
        }

        private void LoadClientList()
        {
            var clients = ClientService.GetClients();
            _clientGridList = new List<ClientListGrid>();
            foreach (ClientListGrid client in clients)
                _clientGridList.Add(client);

            dataGridView2.DataSource = _clientGridList;
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                var row = dataGridView2.SelectedRows[0];
                var id = (int)row.Cells["idKlient"].Value;
                ShowSelectedClientOnEditScreen(id);
            }
        }

        private void ShowSelectedClientOnEditScreen(int id)
        {
            _currentEditKlient = ClientService.GetClient(id) ?? new KLIENCI();
            ShowClientOnEditScreen(_currentEditKlient);
            EdytujDaneKlientaPanel.BringToFront();
        }

        private void ClientListFilter(object sender, EventArgs e)
        {
            var filterList = _clientGridList;
            string nameSurname = textBox19.TextOrDefault();
            if (nameSurname != null)
                filterList = filterList.Where(c => c.ImieNazwisko.Contains(nameSurname, StringComparison.CurrentCultureIgnoreCase)).ToList();

            string telefon = textBox24.TextOrDefault();
            if (telefon != null)
                filterList = filterList.Where(c => c.Telefon.StartsWith(telefon, StringComparison.CurrentCultureIgnoreCase)).ToList();

            string numerDowodu = textBox26.TextOrDefault();
            if (numerDowodu != null)
                filterList = filterList.Where(c => c.NrDowOsob.StartsWith(numerDowodu, StringComparison.CurrentCultureIgnoreCase)).ToList();

            dataGridView2.DataSource = filterList;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            LoadReservationList();
            ListaRezerwacjiPanel.BringToFront();
        }

        private void LoadReservationList()
        {
            var reservations = ReservationService.GetReservations();
            _reservationListGrid = new List<ReservationListGrid>();
            foreach (ReservationListGrid reservation in reservations)
            {
                _reservationListGrid.Add(reservation);
            }
            dataGridView4.DataSource = _reservationListGrid;
        }

        private void ShowSelectedReservationOnEditScreen(int id)
        {
            _currentEditReservation = ReservationService.GetReservation(id);
            LoadReservationOnDetailsScreen(_currentEditReservation);
            SzegolyRezerwacjiPanel.BringToFront();
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                var row = dataGridView4.SelectedRows[0];
                var id = (int)row.Cells["idRezerw"].Value;
                ShowSelectedReservationOnEditScreen(id);
            }
        }

        private void ReservationListFilter(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
                dateTimePicker2.Value = dateTimePicker1.Value.Date.AddDays(1);

            var filterList = _reservationListGrid;

            DateTime startWypoz = dateTimePicker1.Value.Date;
            if (checkBox4.Checked)
                filterList = filterList.Where(r => r.DataWypoz >= startWypoz).ToList();

            DateTime startZwrotu = dateTimePicker2.Value.Date;
            if (checkBox5.Checked)
                filterList = filterList.Where(r => r.DataZwrotu <= startZwrotu).ToList();

            string pojazd = textBox48.TextOrDefault();
            if (pojazd != null)
                filterList = filterList.Where(r => r.Pojazd.Contains(pojazd, StringComparison.CurrentCultureIgnoreCase)).ToList();

            string klient = textBox54.TextOrDefault();
            if (klient != null)
                filterList = filterList.Where(r => r.Klient.Contains(klient, StringComparison.CurrentCultureIgnoreCase)).ToList();

            dataGridView4.DataSource = filterList;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            LoadNoTakenReservationList();
            ListaRezerwacjiPanel.BringToFront();
        }

        private void LoadNoTakenReservationList()
        {
            var reservations = ReservationService.GetAllNotTaken();
            _reservationListGrid = new List<ReservationListGrid>();
            foreach (ReservationListGrid reservation in reservations)
            {
                _reservationListGrid.Add(reservation);
            }
            dataGridView4.DataSource = _reservationListGrid;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            LoadNoReturnReservationList();
            ListaRezerwacjiPanel.BringToFront();
        }

        private void LoadNoReturnReservationList()
        {
            var reservations = ReservationService.GetAllNotReturned();
            _reservationListGrid = new List<ReservationListGrid>();
            foreach (ReservationListGrid reservation in reservations)
            {
                _reservationListGrid.Add(reservation);
            }
            dataGridView4.DataSource = _reservationListGrid;
        }

        private void LoadReservationOnDetailsScreen(REZERWACJE reservation)
        {
            //Podstawowe informacje
            textBox10.Text = reservation.DataRez.ToString("dd.MM.yyyy HH:mm");
            textBox32.Text = reservation.DataWypoz.ToString("dd.MM.yyyy HH:mm");
            textBox33.Text = reservation.DataZwrotu.ToString("dd.MM.yyyy HH:mm");
            textBox34.Text = reservation.DataZdania?.ToString("dd.MM.yyyy HH:mm");
            textBox35.Text = ReservationListGrid.GetTextStatus(reservation.Wypozycz);

            //Dane klienta
            textBox36.Text = reservation.KLIENCI.Imie + " " + reservation.KLIENCI.Nazwisko;
            textBox37.Text = reservation.KLIENCI.Plec == 0 ? "Mężczyzna" : "Kobieta";
            textBox38.Text = reservation.KLIENCI.Adres;
            textBox39.Text = reservation.KLIENCI.NrDowOsob;
            textBox40.Text = reservation.KLIENCI.Telefon;
            textBox41.Text = reservation.KLIENCI.Login;

            //Pojazd
            if (reservation.POJAZDY.ZDJECIA.Count > 0)
                pictureBox11.Image = PhotoService.ByteArrayToImage(reservation.POJAZDY.ZDJECIA.First().Zdjecie);
            else
                pictureBox11.Image = Properties.Resources.no_car_image;

            textBox42.Text = VehicleListGrid.GetTextType(reservation.POJAZDY.Rodzaj);
            textBox43.Text = reservation.POJAZDY.MARKI.Nazwa;
            textBox44.Text = reservation.POJAZDY.Kolor;
            textBox45.Text = reservation.POJAZDY.DataProd.ToString("d");
            textBox46.Text = reservation.POJAZDY.Przebieg.ToString() + " km";
            textBox47.Text = reservation.POJAZDY.ZaGodz.ToString("C");
            richTextBox3.Text = reservation.POJAZDY.Opis;

            //Wydaj
            textBox50.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            checkBox6.Enabled = reservation.Wypozycz == 0 ? true : false;
            checkBox6.Checked = false;
            wydajPojazdButton.Enabled = reservation.Wypozycz == 0 ? true : false;

            //Odbierz
            textBox49.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            checkBox3.Enabled = reservation.Wypozycz == 1 ? true : false;
            checkBox3.Checked = false;
            odbierzPojazdButton.Enabled = reservation.Wypozycz == 1 ? true : false;
        }

        private void wydajPojazdButton_Click(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                ReservationService.ChangeStatus(_currentEditReservation.idRezerw, 1);
                ShowSelectedReservationOnEditScreen(_currentEditReservation.idRezerw);
                MessageBox.Show("Status rezerwacji został pomyślnie zmieniony");
            }
            else
            {
                MessageBox.Show("Potwierdź wydanie pojazdu klientowi!");
            }
        }

        private void odbierzPojazdButton_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                ReservationService.ChangeStatus(_currentEditReservation.idRezerw, 2, DateTime.Now);
                ShowSelectedReservationOnEditScreen(_currentEditReservation.idRezerw);
                MessageBox.Show("Status rezerwacji został pomyślnie zmieniony");
            }
            else
            {
                MessageBox.Show("Potwierdź odebranie pojazdu od klienta!");
            }
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void textBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void IDKlientaTBoxWEDK_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var opinions = VehicleService.GetVehicleOpinions(_currentEditReservation.POJAZDY.idPojazd);
            if (opinions.Count == 0)
            {
                MessageBox.Show("Wybrany pojazd nie posiada jeszcze opini.");
                return;
            }

            FormOpinion formOpinion = new FormOpinion(opinions);
            formOpinion.ShowDialog();
        }
    }
}
