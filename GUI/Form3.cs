using System;
using System.Data;
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
            {
                id = -1;
            }

            _currentEditKlient = null;
            using (var entites = new DBEntities())
            {
                _currentEditKlient = entites.KLIENCI.FirstOrDefault(k => k.idKlient == id) ?? new KLIENCI();
                var saveToCache = _currentEditKlient?.KATEGORIEPJAZDY;
            }

            LoadKlientDataReservationScreen(_currentEditKlient);
        }

        private void NrDowOsTBox_TextChanged(object sender, EventArgs e)
        {
            string nrDowOsob = ((TextBox)sender).TextOrDefault();

            _currentEditKlient = null;
            using (var entities = new DBEntities())
            {
                _currentEditKlient = entities.KLIENCI.FirstOrDefault(k => k.NrDowOsob.StartsWith(nrDowOsob)) ?? new KLIENCI();
            }

            LoadKlientDataReservationScreen(_currentEditKlient);
        }

        private void LoadKlientDataReservationScreen(KLIENCI klient)
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

            _currentEditKlient = null;
            using (var entities = new DBEntities())
            {
                _currentEditKlient = entities.KLIENCI.FirstOrDefault(k => k.idKlient == id) ?? new KLIENCI();
                var loadToCache = _currentEditKlient?.KATEGORIEPJAZDY;
            }

            LoadKlientDataEditScreen(_currentEditKlient);
        }

        private void NrDowOsTBoxWEDK_TextChanged(object sender, EventArgs e)
        {
            string nrDowOsob = ((TextBox)sender).TextOrDefault();

            _currentEditKlient = null;
            using (var entities = new DBEntities())
            {
                _currentEditKlient = entities.KLIENCI.FirstOrDefault(k => k.NrDowOsob.StartsWith(nrDowOsob)) ?? new KLIENCI();
                var loadToCache = _currentEditKlient?.KATEGORIEPJAZDY;
            }

            LoadKlientDataEditScreen(_currentEditKlient);
        }

        private void LoadKlientDataEditScreen(KLIENCI klient)
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

        private void LoadKlientDataFromEditScreen(KLIENCI klient)
        {
            klient.Imie = ImieTBoxEDKlienta.TextOrDefault();
            klient.DrugieImie = DrugieImieTBoxEDKlienta.TextOrDefault();
            klient.Nazwisko = NazwiskoTBoxEDKlienta.TextOrDefault();
            klient.Adres = AdresTBoxEDKlienta.TextOrDefault();
            klient.Telefon = TelefonTBoxEDKlienta.TextOrDefault();
            klient.Email = EmailTBoxEDKlienta.TextOrDefault();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            KLIENCI newKlient = new KLIENCI();
            newKlient.Imie = textBox10.TextOrDefault();
            newKlient.DrugieImie = textBox11.TextOrDefault();
            newKlient.Nazwisko = textBox13.TextOrDefault();
            newKlient.Adres = textBox12.TextOrDefault();
            newKlient.Plec = (sbyte)comboBox1.SelectedIndex;
            newKlient.Telefon = textBox17.TextOrDefault();
            newKlient.Email = textBox16.TextOrDefault();
            newKlient.NrPrawaJazd = textBox15.TextOrDefault();
            newKlient.NrDowOsob = textBox14.TextOrDefault();
            newKlient.DataRejestr = DateTime.Now;
            newKlient.DataUr = DateTime.Now;
            newKlient.Login = "Test";   //Jak ma być nadawane login i hasło kiedy pracownik tworzy konto dla użytownika??
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
                try
                {
                    entities.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry
                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages

                        string message = "";
                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            message += string.Format("Błąd '{0}' wystąpił w {1} przy {2}\n",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        }
                        MessageBox.Show(message);
                    }
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UpdateClientDataButton_Click(object sender, EventArgs e)
        {
            if (_currentEditKlient != null && _currentEditKlient.idKlient > 0)
            {
                using (var entities = new DBEntities())
                {
                    var klient = entities.KLIENCI.First(k => k.idKlient == _currentEditKlient.idKlient);
                    LoadKlientDataFromEditScreen(klient);

                    for (int i = 0; i < KategoriePJazdyCBoxEDKlienta.Items.Count; i++)
                    {
                        var licence = entities.KATEGORIEPJAZDY.FirstOrDefault(k => k.idKatPJ == i + 1);
                        if (KategoriePJazdyCBoxEDKlienta.GetItemCheckState(i) == CheckState.Checked)
                        {
                            klient.KATEGORIEPJAZDY.Add(licence);
                        }
                        else if (klient.KATEGORIEPJAZDY.Contains(licence))
                        {
                            klient.KATEGORIEPJAZDY.Remove(licence);
                        }    
                    }

                    try
                    {
                        entities.SaveChanges();
                        MessageBox.Show("Zapisano zmiany");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry
                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display error messages
                            string message = "";
                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                message += string.Format("Błąd '{0}' wystąpił w {1} przy {2}\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            }
                            MessageBox.Show(message);
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            else
                MessageBox.Show("Najpierw wybierz klienta do edycji!");
        }
    }
}
