using GUI.GridView;
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
using OfficeOpenXml;
using System.IO;


namespace GUI
{
    public partial class Form4 : Form
    {
        private KLIENCI _currentEditClient;
        private List<ClientListGrid> _clientListGrid = new List<ClientListGrid>();

        private PRACOWNICY _currentEditWorker;
        private List<WorkerListGrid> _workerListGrid = new List<WorkerListGrid>();

        private POJAZDY _currentEditVehicle;
        private List<VehicleListGrid> _vehicleListGrid = new List<VehicleListGrid>();

        private REZERWACJE _currentEditReservation;
        private List<ReservationListGrid> _reservationListGrid = new List<ReservationListGrid>();

        private Image _lastLoadImage = null;

        public Form4(string Login)
        {
            InitializeComponent();
            LoadMenuData(Login);
            LoadCheckedListBox();
            LoadComboBox(comboBox5);
            ClearVehicleFromAddScreen();
            timer1.Start();
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
            ClearVehicleFromAddScreen();
            DodajPojazdPanel.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DodajKlientaPanel.BringToFront();
            textBox20.Text = DateTime.Now.ToString("d");
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            LoadCarList();
            ListaPojazdowPanel.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void LoadCheckedListBox(CheckedListBox checkedListBox)
        {
            foreach (var licenceType in DrivingLicenceService.GetDrivingLicences())
            {
                checkedListBox.Items.Add(licenceType.Nazwa);
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
            foreach (var model in CarBrandService.GetCarBrands())
            {
                comboBox.Items.Add(model.Nazwa);
            }
            comboBox.SelectedIndex = 0;
        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int id))
            {
                id = -1;
            }
            _currentEditWorker = WorkerService.GetWorker(id) ?? new PRACOWNICY();

            ShowWorkerOnEditScreen(_currentEditWorker);
        }

        private void ShowWorkerOnEditScreen(PRACOWNICY pracownicy)
        {
            textBox32.Text = pracownicy.Imie;
            textBox26.Text = pracownicy.DrugieImie;
            textBox35.Text = pracownicy.Nazwisko;
            textBox38.Text = pracownicy.Adres;
            comboBox2.SelectedIndex = pracownicy.Plec == 1 ? 1 : 0;
            textBox55.Text = pracownicy.Login;
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
            if (_currentEditWorker != null && _currentEditWorker.idPrac > 0)
            {
                LoadWorkerFromEditScreen(_currentEditWorker);
                var isEdit = WorkerService.UpdateWorker(_currentEditWorker);
                if (isEdit)
                    MessageBox.Show("Zaktualizowano dane pracownika!");
                else
                    MessageBox.Show("Błąd podczas aktualizacji danych pracownika");

                _currentEditWorker = WorkerService.GetWorker(_currentEditWorker.idPrac);
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

            _currentEditClient = ClientService.GetClient(id) ?? new KLIENCI();
            ShowClientOnEditScreen(_currentEditClient);
        }

        private void NrDowOsTBoxWEDK_TextChanged(object sender, EventArgs e)
        {
            string nrDowOs = ((TextBox)sender).TextOrDefault();
            _currentEditClient = ClientService.GetClient(nrDowOs, true) ?? new KLIENCI();
            ShowClientOnEditScreen(_currentEditClient);
        }

        private void ShowClientOnEditScreen(KLIENCI klient)
        {
            ImieTBoxEDKlienta.Text = klient.Imie;
            DrugieImieTBoxEDKlienta.Text = klient.DrugieImie;
            NazwiskoTBoxEDKlienta.Text = klient.Nazwisko;
            AdresTBoxEDKlienta.Text = klient.Adres;
            PlecComboEDKlienta.SelectedIndex = klient.Plec == 1 ? 1 : 0;
            textBox18.Text = klient.DataUr.ToString("d");
            TelefonTBoxEDKlienta.Text = klient.Telefon;
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

        private void UpdateClientDataButton_Click(object sender, EventArgs e)
        {
            if (_currentEditClient != null && _currentEditClient.idKlient > 0)
            {
                LoadClientFromEditScreen(_currentEditClient);
                var isEdt = ClientService.UpdateClient(_currentEditClient);
                if (isEdt)
                    MessageBox.Show("Zaktualizowano dane klienta.");
                else
                    MessageBox.Show("Błąd podczas aktualizacji danych klienta!");

                _currentEditClient = ClientService.GetClient(_currentEditClient.idKlient);
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
            newVehicle.MARKA_Nazwa = comboBox5.Text;
            if (_lastLoadImage != null)
                newVehicle.ZDJECIA.Add(new ZDJECIA() { Zdjecie = PhotoService.ImageToByteArray(_lastLoadImage) });

            for (int i = 0; i < checkedListBox6.CheckedIndices.Count; i++)
            {
                var index = checkedListBox6.CheckedIndices[i] + 1;
                newVehicle.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = index });
            }

            return newVehicle;
        }

        private void ClearVehicleFromAddScreen()
        {
            comboBox4.SelectedIndex = 0;
            textBox43.Text = "";
            textBox49.Text = "";
            textBox51.Text = "";
            maskedTextBox1.Text = "";
            checkBox3.Checked = false;
            richTextBox4.Text = "";
            textBox50.Text = "";
            comboBox5.SelectedIndex = 0;
            pictureBox11.Image = Properties.Resources.no_car_image;
            checkedListBox6.ClearItemChecked();

            _lastLoadImage = null;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var addVehicle = GetVehicleFromAddScreen();
            var isAdd = VehicleService.AddVehicle(addVehicle);
            if (isAdd)
            {
                MessageBox.Show("Dodano pojazd.");
                ClearVehicleFromAddScreen();
            }
            else
                MessageBox.Show("Błąd dodania pojazdu!");
        }

        private void LoadVehicleGridList()
        {
            var cars = VehicleService.GetVehicles();
            _vehicleListGrid = new List<VehicleListGrid>();
            foreach (VehicleListGrid car in cars)
            {
                _vehicleListGrid.Add(car);
            }
        }

        private void LoadCarList()
        {
            LoadVehicleGridList();
            dataGridView1.DataSource = _vehicleListGrid;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                var id = (int)row.Cells["idPojazd"].Value;
                ShowSelectedVehicleOnEditScreen(id);
            }
        }

        private void ShowSelectedVehicleOnEditScreen(int id)
        {
            _currentEditVehicle = VehicleService.GetVehicle(id) ?? new POJAZDY();
            ShowVehicleOnEditScreen(_currentEditVehicle);
            EdytujPojazdPanel.BringToFront();
        }

        private void ShowVehicleOnEditScreen(POJAZDY vehicle)
        {
            if (vehicle.ZDJECIA.Count > 0)
                pictureBox3.Image = PhotoService.ByteArrayToImage(vehicle.ZDJECIA.First().Zdjecie);
            else
                pictureBox3.Image = Properties.Resources.no_car_image;

            textBox63.Text = vehicle.MARKI.Nazwa;
            maskedTextBox5.Text = vehicle.DataProd.ToString("d");
            textBox62.Text = vehicle.Kolor;
            textBox61.Text = vehicle.Przebieg.ToString("D");
            textBox60.Text = vehicle.ZaGodz.ToString("F");
            textBox59.Text = vehicle.NrRejestr;
            checkBox7.Checked = vehicle.Sprawny == 0 ? false : true;
            comboBox7.SelectedIndex = vehicle.Rodzaj;
            richTextBox1.Text = vehicle.Opis;
        }

        private void LoadVehicleFromEditScreen(POJAZDY editVehicle)
        {
            editVehicle.Kolor = textBox62.TextOrDefault();
            editVehicle.Przebieg = int.Parse(textBox61.Text);
            editVehicle.ZaGodz = float.Parse(textBox60.Text);
            editVehicle.Sprawny = (sbyte)(checkBox7.Checked == true ? 1 : 0);
            editVehicle.Opis = richTextBox1.TextOrDefault();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (_currentEditVehicle != null && _currentEditVehicle.idPojazd > 0)
            {
                LoadVehicleFromEditScreen(_currentEditVehicle);
                var isEdit = VehicleService.UpdateVehicle(_currentEditVehicle);
                if (isEdit)
                    MessageBox.Show("Zaktualizowano dane pojazdu.");
                else
                    MessageBox.Show("Błąd podczas aktualizacji danych klienta!");
            }
            else
                MessageBox.Show("Najpierw wybierz pojazd do edycji!");
        }

        private void VehicleListFilter(object sender, EventArgs e)
        {
            var filterList = _vehicleListGrid;
            string name = textBox53.TextOrDefault();
            if (name != null)
                filterList = filterList.Where(v => v.Marka.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();

            string numerRejestr = textBox52.TextOrDefault();
            if (numerRejestr != null)
                filterList = filterList.Where(v => v.NrRejestr.Contains(numerRejestr, StringComparison.CurrentCultureIgnoreCase)).ToList();

            dataGridView1.DataSource = filterList;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            LoadClientList();
            ListaKlientowPanel.BringToFront();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            var client = ClientService.GetClient(id) ?? new KLIENCI();
            ShowClientOnEditScreen(client);
            _currentEditClient = client;
            EdytujDaneKlientaPanel.BringToFront();
        }

        private void LoadClientList()
        {
            var clients = ClientService.GetClients();
            _clientListGrid = new List<ClientListGrid>();
            foreach (ClientListGrid client in clients)
            {
                _clientListGrid.Add(client);
            }

            dataGridView2.DataSource = _clientListGrid;
        }

        private void ClientListFilter(object sender, EventArgs e)
        {
            var filterList = _clientListGrid;
            string nameSurname = textBox34.TextOrDefault();
            if (nameSurname != null)
                filterList = filterList.Where(c => c.ImieNazwisko.Contains(nameSurname, StringComparison.CurrentCultureIgnoreCase)).ToList();

            string telefon = textBox33.TextOrDefault();
            if (telefon != null)
                filterList = filterList.Where(c => c.Telefon.StartsWith(telefon, StringComparison.CurrentCultureIgnoreCase)).ToList();

            string numerDowodu = textBox17.TextOrDefault();
            if (numerDowodu != null)
                filterList = filterList.Where(c => c.NrDowOsob.StartsWith(numerDowodu, StringComparison.CurrentCultureIgnoreCase)).ToList();

            dataGridView2.DataSource = filterList;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            LoadWorkerList();
            ListaPracownikowPanel.BringToFront();
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                var row = dataGridView3.SelectedRows[0];
                var id = (int)row.Cells["idPrac"].Value;
                ShowSelectedWorkerOnEditScreen(id);
            }
        }

        private void ShowSelectedWorkerOnEditScreen(int id)
        {
            var worker = WorkerService.GetWorker(id) ?? new PRACOWNICY();
            ShowWorkerOnEditScreen(worker);
            _currentEditWorker = worker;
            EdytujPracownikaPanel.BringToFront();
        }

        private void LoadWorkerList()
        {
            var workers = WorkerService.GetWorkers();
            _workerListGrid = new List<WorkerListGrid>();
            foreach (WorkerListGrid worker in workers)
            {
                _workerListGrid.Add(worker);
            }

            dataGridView3.DataSource = _workerListGrid;
        }

        private void WorkerListFilter(object sender, EventArgs e)
        {
            var filterList = _workerListGrid;
            string nameSurname = textBox44.TextOrDefault();
            if (nameSurname != null)
                filterList = filterList.Where(w => w.ImieNazwisko.Contains(nameSurname, StringComparison.CurrentCultureIgnoreCase)).ToList();

            string telefon = textBox39.TextOrDefault();
            if (telefon != null)
                filterList = filterList.Where(w => w.Telefon.StartsWith(telefon, StringComparison.CurrentCultureIgnoreCase)).ToList();

            string login = textBox37.TextOrDefault();
            if (login != null)
                filterList = filterList.Where(w => w.Login.Contains(login, StringComparison.CurrentCultureIgnoreCase)).ToList();

            dataGridView3.DataSource = filterList;
        }

        private void button23_Click(object sender, EventArgs e)
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

        private void LoadReservationOnDetailsScreen(REZERWACJE reservation)
        {
            //Podstawowe informacje
            textBox28.Text = reservation.DataRez.ToString("dd.MM.yyyy HH:mm");
            textBox27.Text = reservation.DataWypoz.ToString("dd.MM.yyyy HH:mm");
            textBox25.Text = reservation.DataZwrotu.ToString("dd.MM.yyyy HH:mm");
            textBox23.Text = reservation.DataZdania?.ToString("dd.MM.yyyy HH:mm");
            textBox22.Text = ReservationListGrid.GetTextStatus(reservation.Wypozycz);

            //Dane klienta
            textBox58.Text = reservation.KLIENCI.Imie + " " + reservation.KLIENCI.Nazwisko;
            textBox57.Text = reservation.KLIENCI.Plec == 0 ? "Mężczyzna" : "Kobieta";
            textBox56.Text = reservation.KLIENCI.Adres;
            textBox31.Text = reservation.KLIENCI.NrDowOsob;
            textBox30.Text = reservation.KLIENCI.Telefon;
            textBox29.Text = reservation.KLIENCI.Login;

            //Pojazd
            if (reservation.POJAZDY.ZDJECIA.Count > 0)
                pictureBox2.Image = PhotoService.ByteArrayToImage(reservation.POJAZDY.ZDJECIA.First().Zdjecie);
            else
                pictureBox2.Image = Properties.Resources.no_car_image;

            textBox70.Text = VehicleListGrid.GetTextType(reservation.POJAZDY.Rodzaj);
            textBox69.Text = reservation.POJAZDY.MARKI.Nazwa;
            textBox68.Text = reservation.POJAZDY.Kolor;
            textBox67.Text = reservation.POJAZDY.DataProd.ToString("d");
            textBox66.Text = reservation.POJAZDY.Przebieg.ToString() + " km";
            textBox65.Text = reservation.POJAZDY.ZaGodz.ToString("C");
            richTextBox3.Text = reservation.POJAZDY.Opis;

            //Wydaj
            textBox71.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            checkBox6.Enabled = reservation.Wypozycz == 0 ? true : false;
            checkBox6.Checked = false;
            wydajPojazdButton.Enabled = reservation.Wypozycz == 0 ? true : false;

            //Odbierz
            textBox72.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            checkBox2.Enabled = reservation.Wypozycz == 1 ? true : false;
            checkBox2.Checked = false;
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
            if (checkBox2.Checked)
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

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void textBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File|*.xlsx";
            saveFileDialog.Title = "Utwórz raport z listy pracowników";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName.Equals(""))
                return;

            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Klienci");

                  var headerRow = new List<string[]>()
                  {
                    new string[] { "Imię Nazwisko", "Data Urodzenia", "Numer dowodu osobistego", "Telefon", "Email" }
                  };

                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                var worksheet = excel.Workbook.Worksheets["Klienci"];
                worksheet.Cells[headerRange].Style.Font.Bold = true;
                worksheet.Cells[headerRange].Style.Font.Size = 14;
                worksheet.Cells[headerRange].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                var cellData = new List<Object[]>();
                ;
                foreach (ClientListGrid clg in _clientListGrid)
                    cellData.Add(new Object[] { clg.ImieNazwisko, clg.DataUr, clg.NrDowOsob, clg.Telefon, clg.Email });
                worksheet.Cells[2, 1].LoadFromArrays(cellData);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                excel.SaveAs(excelFile);
                MessageBox.Show("Raport został wygenerowany i zapisany.");
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File|*.xlsx";
            saveFileDialog.Title = "Utwórz raport z listy pracowników";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName.Equals(""))
                return;

            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Pracownicy");

                var headerRow = new List<string[]>()
                  {
                    new string[] { "Imię Nazwisko", "Data Zatrudnienia", "Email", "Telefon" }
                  };

                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                var worksheet = excel.Workbook.Worksheets["Pracownicy"];
                worksheet.Cells[headerRange].Style.Font.Bold = true;
                worksheet.Cells[headerRange].Style.Font.Size = 14;
                worksheet.Cells[headerRange].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                var cellData = new List<Object[]>();

                foreach (WorkerListGrid clg in _workerListGrid)
                    cellData.Add(new Object[] { clg.ImieNazwisko, clg.DataZatr, clg.Email, clg.Telefon });

                worksheet.Cells[2, 1].LoadFromArrays(cellData);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                excel.SaveAs(excelFile);
                MessageBox.Show("Raport został wygenerowany i zapisany.");
            }
        }

        private void button20_Click_1(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _lastLoadImage = PhotoService.LoadFileToImage();

            if (_lastLoadImage == null)
                return;

            pictureBox11.Image = _lastLoadImage;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var opinions = VehicleService.GetVehicleOpinions(_currentEditVehicle.idPojazd);
            if (opinions.Count == 0)
            {
                MessageBox.Show("Wybrany pojazd nie posiada jeszcze opini.");
                return;
            }

            FormOpinion formOpinion = new FormOpinion(opinions);
            formOpinion.ShowDialog();
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

        private void button12_Click(object sender, EventArgs e)
        {
            FormInspection formInspection = new FormInspection(_currentEditVehicle.idPojazd);
            formInspection.ShowDialog();
        }
    }
}
