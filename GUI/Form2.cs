using System;
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
            var saveInCache0 = CurrClient.KATEGORIEPJAZDY;
            var saveInCache1 = CurrClient.REZERWACJE;
            
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
            using (var entities = new DBEntities())
            {
                var klient = entities.KLIENCI.First(k => k.Login == Login);
                LoadKlientDataFromEditScreen(klient);

                for (int i = 0; i < checkedListBox3.Items.Count; i++)
                {
                    var licence = entities.KATEGORIEPJAZDY.FirstOrDefault(k => k.idKatPJ == i + 1);
                    if (checkedListBox3.GetItemCheckState(i) == CheckState.Checked)
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

    }
}
