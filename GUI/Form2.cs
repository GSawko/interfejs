﻿using GUI.GridView;
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

        private List<VehicleListGrid> _avabileVehicleListGrid = new List<VehicleListGrid>();

        public Form2(string login)
        {
            InitializeComponent();
            _login = login;
            timer1.Start();
            LoadCheckedListBox(checkedListBox3);
            LoadClient(_login);

            LoadMakeReservationScreen();
            TimeRangeChange(null, null);
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
            textBox22.Text = _currentClient.DataRejestr.ToString("d");

            foreach (var licence in _currentClient.KATEGORIEPJAZDY)
            {
                checkedListBox3.SetItemChecked(licence.idKatPJ - 1, true);
            }

            textBox19.Text = _currentClient.Login;
            textBox18.Text = _currentClient.Haslo;
        }

        private bool LoadClientFromEditScreen(KLIENCI editClient)
        {
            var tmp = textBox10.TextOrDefault();
            if (tmp == null) return false;
            editClient.Imie = tmp;

            editClient.DrugieImie = textBox11.TextOrDefault();

            tmp = textBox13.TextOrDefault();
            if (tmp == null) return false;
            editClient.Nazwisko = tmp;

            tmp = textBox12.TextOrDefault();
            if (tmp == null) return false;
            editClient.Adres = tmp;

            tmp = TelefonTBoxEDKlienta.TextOrDefault();
            if (tmp == null || tmp.Length < 9) return false;
            editClient.Telefon = tmp;

            editClient.Email = textBox16.TextOrDefault();

            tmp = textBox18.TextOrDefault();
            if (tmp == null) return false;
            editClient.Haslo = tmp;

            editClient.KATEGORIEPJAZDY = new List<KATEGORIEPJAZDY>();
            for (int i = 0; i < checkedListBox3.CheckedIndices.Count; i++)
            {
                var index = checkedListBox3.CheckedIndices[i] + 1;
                editClient.KATEGORIEPJAZDY.Add(new KATEGORIEPJAZDY() { idKatPJ = index });
            }

            return true;
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

        private void button6_Click_1(object sender, EventArgs e)
        {
            FormularzRezerwacjiPanel.BringToFront();
        }

        private void buttonMakeReservation_Click(object sender, EventArgs e)
        {
            LoadMakeReservationScreen();
            TimeRangeChange(null, null);
            FormularzRezerwacjiPanel.BringToFront();
        }

        private void LoadMakeReservationScreen()
        {
            var time = DateTime.Now;
            domainUpDown2.SelectedIndex = 23 - time.Hour;
            domainUpDown3.SelectedIndex = 23 - time.Hour;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowClientOnEditScreen();
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
            var status = LoadClientFromEditScreen(_currentClient);

            if (status == false)
            {
                MessageBox.Show("Sprawdź czy wymagane pola są wypełnione poprawnymi danymi!");
                return;
            }

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

        private void ShowReservationOnDetailsScreen(REZERWACJE reservation)
        {
            //Podstawowe informacje
            textBox17.Text = reservation.DataRez.ToString();
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

            //Opinie
            comboBox2.SelectedIndex = 0;
            richTextBox2.Text = "";
            var isNoOpinion = reservation.OPINIA.Count == 0 && reservation.Wypozycz == 2 ? true : false;
            button1.Enabled = isNoOpinion;
            richTextBox4.Enabled = isNoOpinion;
            comboBox2.Enabled = isNoOpinion;
            if (!isNoOpinion && reservation.OPINIA.Count > 0)
            {
                var opinia = reservation.OPINIA.First();
                richTextBox4.Text = opinia.Opis;
                comboBox2.SelectedIndex = opinia.Ocena - 1;
            }

            //Realizacja
            textBox23.Text = reservation.PRACOWNICY?.ToString();
            textBox24.Text = reservation.PRACOWNICY1?.ToString();
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
            ShowReservationOnDetailsScreen(_currentEditReservation);
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
                newOpinion.Ocena = (short)(comboBox2.SelectedIndex + 1);
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void TimeRangeChange(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            if (GetStartReservationTime() < time)
            {
                dateTimePicker3.Value = time;
                domainUpDown2.SelectedIndex = 23 - time.Hour;
            }

            if (GetStartReservationTime() >= GetEndReservationTime())
                dateTimePicker4.Value = dateTimePicker3.Value.AddDays(1);
            
            FilterByTime();
            FilterByType();
        }
        private DateTime GetStartReservationTime()
        {
            var picker = dateTimePicker3.Value;

            return new DateTime(picker.Year, picker.Month, picker.Day, 23 - Math.Abs(domainUpDown2.SelectedIndex), 0, 0);
        }

        private DateTime GetEndReservationTime()
        {
            var picker = dateTimePicker4.Value;

            return new DateTime(picker.Year, picker.Month, picker.Day, 23 - Math.Abs(domainUpDown3.SelectedIndex), 30, 0);
        }

        private void FilterByTime()
        {
            var vehicles = VehicleService.GetFreeVehicle(GetStartReservationTime(), GetEndReservationTime());

            _avabileVehicleListGrid = new List<VehicleListGrid>();
            foreach (VehicleListGrid vehicle in vehicles)
                _avabileVehicleListGrid.Add(vehicle);

            dataGridView1.DataSource = _avabileVehicleListGrid;
        }

        private void VehicleTypeChange(object sender, EventArgs e)
        {
            FilterByType();
        }

        private void FilterByType()
        {
            var filterList = _avabileVehicleListGrid;
            if (radioButton1.Checked)
                filterList = filterList.Where(v => v.Rodzaj.Equals("Samochód")).ToList();
            else if (radioButton2.Checked)
                filterList = filterList.Where(v => v.Rodzaj.Equals("Motocykl")).ToList();
            else if (radioButton3.Checked)
                filterList = filterList.Where(v => v.Rodzaj.Equals("Motorower")).ToList();

            dataGridView1.DataSource = filterList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Proszę wybrać pojazd z listy dostępnych!");
                return;
            }

            if (!checkBox2.Checked)
            {
                MessageBox.Show("Potwiedź chęć zarezerwowania pojazdu!");
                return;
            }

            var newRes = new REZERWACJE();
            newRes.DataRez = DateTime.Now;
            newRes.DataWypoz = GetStartReservationTime();
            newRes.DataZwrotu = GetEndReservationTime();
            newRes.KLIENCI_idKlient = _currentClient.idKlient;
            newRes.POJAZDY_idPojazd = (int)dataGridView1.SelectedRows[0].Cells["idPojazd"].Value;

            if (ReservationService.Add(newRes))
            {
                MessageBox.Show("Pomyślnie zarezerwowano pojazd.");
                TimeRangeChange(null, null);
            }
            else
                MessageBox.Show("Błąd rezerwacji pojazdu!");

            checkBox2.Checked = false;

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

            dataGridView4.DataSource = filterList;
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                var vehicle = VehicleService.GetVehicle(id);
                FormVehicle formVehicle = new FormVehicle(vehicle);
                formVehicle.ShowDialog();
            }
        }
    }
}
