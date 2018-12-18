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
        private KLIENCI klient;
        public Form2(string login)
        {
            InitializeComponent();
            LoadCheckedListBox(checkedListBox3);

            Login = login;

            LoadClient(Login);
            LoadClientData();
        }

        private void LoadClient(string login)
        {
            using (var entities = new DBEntities())
            {
                klient = entities.KLIENCI.Where(k => k.Login == login).First();
                var saveInCache0 = klient.KATEGORIEPJAZDY;
                var saveInCache1 = klient.REZERWACJE;
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

        private void LoadClientData()
        {
            //boczne menu
            label2.Text = klient.Imie + " " + klient.Nazwisko;

            //zakładka dane
            textBox10.Text = klient.Imie;
            textBox11.Text = klient.DrugieImie;
            textBox13.Text = klient.Nazwisko;
            textBox12.Text = klient.Adres;
            comboBox1.SelectedIndex = klient.Plec;
            textBox17.Text = klient.Telefon;
            textBox16.Text = klient.Email;
            textBox15.Text = klient.NrPrawaJazd;
            textBox14.Text = klient.NrDowOsob;
            textBox20.Text = klient.DataRejestr.ToString("d");

            foreach (var licence in klient.KATEGORIEPJAZDY)
            {
                checkedListBox3.SetItemChecked(licence.idKatPJ - 1, true);
            }
            //pictureBox7.Image = klient.Zdjecie;

            textBox19.Text = klient.Login;
            textBox18.Text = klient.Haslo;
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
