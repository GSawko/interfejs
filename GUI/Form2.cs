using GUI.GridView;
using GUI.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form2 : Form
    {
        private string _login;
        private KLIENCI _currentClient;
        private List<POJAZDY> _vehicleList;
        private int _vehicleShow;

        private REZERWACJE _currentEditReservation;
        private List<ReservationListGrid> _reservationListGrid = new List<ReservationListGrid>();

        public Form2(string login)
        {
            InitializeComponent();
            LoadCheckedListBox(checkedListBox3);

            _login = login;
            
            LoadClient(_login);
            ShowClientOnEditScreen();
        }

        private void LoadClient(string login)
        {
            _currentClient = ClientService.GetClient(login);
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

        private void ShowClientOnEditScreen()
        {
            //boczne menu
            label2.Text = _currentClient.Imie + " " + _currentClient.Nazwisko;

            //zakładka dane
            textBox10.Text = _currentClient.Imie;
            textBox11.Text = _currentClient.DrugieImie;
            textBox13.Text = _currentClient.Nazwisko;
            textBox12.Text = _currentClient.Adres;
            comboBox1.SelectedIndex = _currentClient.Plec;
            TelefonTBoxEDKlienta.Text = _currentClient.Telefon;
            textBox16.Text = _currentClient.Email;
            textBox15.Text = _currentClient.NrPrawaJazd;
            textBox14.Text = _currentClient.NrDowOsob;
            textBox20.Text = _currentClient.DataRejestr.ToString("d");
            //pictureBox7.Zdjecie = klient.Zdjecie;

            foreach (var licence in _currentClient.KATEGORIEPJAZDY)
            {
                checkedListBox3.SetItemChecked(licence.idKatPJ - 1, true);
            }

            textBox19.Text = _currentClient.Login;
            textBox18.Text = _currentClient.Haslo;
        }

        private void LoadClientFromEditScreen(KLIENCI editClient)
        {
            editClient.Imie = textBox10.TextOrDefault();
            editClient.DrugieImie = textBox11.TextOrDefault();
            editClient.Nazwisko = textBox13.TextOrDefault();
            editClient.Adres = textBox12.TextOrDefault();
            editClient.Telefon = TelefonTBoxEDKlienta.TextOrDefault();
            editClient.Email = textBox16.TextOrDefault();
            editClient.Haslo = textBox18.TextOrDefault();
            //editClient.Zdjecie =

            editClient.KATEGORIEPJAZDY = new List<KATEGORIEPJAZDY>();
            for (int i = 0; i < checkedListBox3.CheckedIndices.Count; i++)
            {
                var index = checkedListBox3.CheckedIndices[i] + 1;
                editClient.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = index });
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Load all vehicle
            _vehicleList = VehicleService.GetVehicles();
            _vehicleShow = 0;
            ShowVehicleDataToChooseVehicle(_vehicleList[_vehicleShow]);

            
            listView2.LargeImageList.Images.Clear();
            listView2.Items.Clear();
            int i = 0;
            foreach (var vehicle in _vehicleList)
            {
                listView2.LargeImageList.Images.Add(vehicle.MARKI.Nazwa, Properties.Resources.fiat_126p_maluch_pomorskie_gdynia_sprzedam_415772907);
                listView2.Items.Add(vehicle.MARKI.Nazwa, i++);
            }

            this.WybórPojazduPanel.BringToFront();
        }

        private void ShowVehicleDataToChooseVehicle(POJAZDY vehicle)
        {
            label5.Text = vehicle.MARKI.Nazwa;      //Opis pod zjęciem
            var marka = vehicle.MARKI.Nazwa.Split(' ');
            label7.Text = marka.Length > 0 ? marka[0] : "";
            label9.Text = marka.Length > 1 ? marka[1] : "";
            label11.Text = vehicle.Kolor;
            label13.Text = vehicle.Przebieg.ToString() + " km";
            label15.Text = vehicle.DataProd.ToString("yyyy");
            label17.Text = vehicle.Sprawny == 0 ? "Nie" : "Tak";
            label19.Text = vehicle.ZaGodz.ToString() + " zł";
            richTextBox1.Text = vehicle.Opis;

            listBox1.Items.Clear();
            foreach (var check in vehicle.PRZEGLADY)
                listBox1.Items.Add(check.Data.ToString("d"));

            richTextBox2.Text = "";

            var opinions = VehicleService.GetVehicleOpinions(vehicle.idPojazd);
            listView1.Items.Clear();
            foreach (var opinion in opinions)
            {
                listView1.Items.Add(opinion.DataWyst.ToString("d") + " - " + opinion.Ocena + "/10 " + opinion.Opis);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                var check = _vehicleList[_vehicleShow].PRZEGLADY.ToList()[index];

                richTextBox2.Text = check.Data.ToString("d") + '\n' + check.Opis;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            _vehicleShow--;
            if (_vehicleShow < 0)
                _vehicleShow = _vehicleList.Count - 1;

            ShowVehicleDataToChooseVehicle(_vehicleList[_vehicleShow]);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            _vehicleShow++;
            if (_vehicleShow >= _vehicleList.Count)
                _vehicleShow = 0;

            ShowVehicleDataToChooseVehicle(_vehicleList[_vehicleShow]);
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            var index = listView2.SelectedIndices[0];
            if (index >= 0)
            {
                _vehicleShow = index;
                ShowVehicleDataToChooseVehicle(_vehicleList[_vehicleShow]);
            }
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
            LoadReservationList();
            ListaRezerwacjiPanel.BringToFront();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoadClientFromEditScreen(_currentClient);

            if (ClientService.UpdateClient(_currentClient))
            {
                MessageBox.Show("Zapisano zmiany.");
            }
            else
            {
                MessageBox.Show("Nie można zapisać zmian!");
            }

            //Update Klient object from DB
            _currentClient = ClientService.GetClient(_login);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //TODO
            /* Obrazy są niskiej jakości ponieważ ImageList korzysta z bardzo słabego domyślnego algorytmu
             * zmiany rozmiaru. Należy albo zapewnić, że wczytywane obrazy nie wymagają zmiany rozmiaru,
             * albo zmienić rozmiar ręcznie przed wczytaniem do ImageList*/
            ImageList imageList = listView2.LargeImageList;
            for (int i = 0; i < imageList.Images.Count; i++)
            {
                listView2.Items.Add(imageList.Images.Keys[i], i);
            }
            listView2.RedrawItems(0, imageList.Images.Count - 1, false);
        }

        private void LoadReservationList()
        {
            var reservations = ReservationService.GetClientReservation(_currentClient.idKlient);
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
            textBox10.Text = reservation.DataRez.ToString();
            textBox32.Text = reservation.DataWypoz.ToString();
            textBox33.Text = reservation.DataZwrotu.ToString();
            textBox34.Text = reservation.DataZdania?.ToString();
            textBox35.Text = ReservationListGrid.GetTextStatus(reservation.Wypozycz);

            //Dane klienta
            groupBox10.Visible = false;
            textBox36.Text = reservation.KLIENCI.Imie + " " + reservation.KLIENCI.Nazwisko;
            textBox37.Text = reservation.KLIENCI.Plec == 0 ? "Mężczyzna" : "Kobieta";
            textBox38.Text = reservation.KLIENCI.Adres;
            textBox39.Text = reservation.KLIENCI.NrDowOsob;
            textBox40.Text = reservation.KLIENCI.Telefon;
            textBox41.Text = reservation.KLIENCI.Login;

            //Pojazd
            //pictureBox11.Image = reservation.POJAZDY.ZDJECIA[0]
            textBox42.Text = VehicleListGrid.GetTextType(reservation.POJAZDY.Rodzaj);
            textBox43.Text = reservation.POJAZDY.MARKI.Nazwa;
            textBox44.Text = reservation.POJAZDY.Kolor;
            textBox45.Text = reservation.POJAZDY.DataProd.ToString("d");
            textBox46.Text = reservation.POJAZDY.Przebieg.ToString() + " km";
            textBox47.Text = reservation.POJAZDY.ZaGodz.ToString("C");
            richTextBox3.Text = reservation.POJAZDY.Opis;

            //Opinie
            comboBox2.SelectedIndex = 0;
            richTextBox2.Text = "";
            var isNoOpinion = reservation.OPINIA.Count == 0 && reservation.Wypozycz == 2 ? true : false;
            button1.Enabled = isNoOpinion;
            richTextBox4.Enabled = isNoOpinion;
            comboBox2.Enabled = isNoOpinion;
            if (!isNoOpinion && reservation.OPINIA.Count > 1)
            {
                var opinia = reservation.OPINIA.First();
                richTextBox4.Text = opinia.Opis;
                comboBox2.SelectedIndex = opinia.Ocena - 1;
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

        private void ShowSelectedReservationOnEditScreen(int id)
        {
            var reservation = ReservationService.GetReservation(id);
            _currentEditReservation = reservation;
            LoadReservationOnDetailsScreen(_currentEditReservation);
            SzegolyRezerwacjiPanel.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var opinia = richTextBox4.TextOrDefault();
            if (opinia != null && opinia.Length > 10)
            {
                var newOpinion = new OPINIA();
                newOpinion.REZERWACJE_idRezerw = _currentEditReservation.idRezerw;
                newOpinion.DataWyst = DateTime.Now;
                newOpinion.Ocena = (short)(comboBox1.SelectedIndex + 1);
                newOpinion.Opis = opinia;
                ReservationService.AddOpinion(newOpinion);
                ShowSelectedReservationOnEditScreen(_currentEditReservation.idRezerw);
                MessageBox.Show("Dodano opinie do rezerwacji!");
            }
            else
            {
                MessageBox.Show("Opinia musi zawierać przynajmniej 10 znaków!");
            }
        }
    }
}
