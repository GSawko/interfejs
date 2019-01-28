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
    public partial class FormVehicle : Form
    {
        POJAZDY _vehicle;

        public FormVehicle(POJAZDY vehicle)
        {
            InitializeComponent();
            _vehicle = vehicle;
            ShowVehicleOnForm(_vehicle);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var opinions = VehicleService.GetVehicleOpinions(_vehicle.idPojazd);
            if (opinions.Count == 0)
            {
                MessageBox.Show("Wybrany pojazd nie posiada jeszcze opini.");
                return;
            }

            FormOpinion formOpinion = new FormOpinion(opinions);
            formOpinion.ShowDialog();
        }

        private void ShowVehicleOnForm(POJAZDY vehicle)
        {
            if (vehicle.ZDJECIA.Count > 0)
                pictureBox3.Image = PhotoService.ByteArrayToImage(vehicle.ZDJECIA.First().Zdjecie);
            else
                pictureBox3.Image = Properties.Resources.no_car_image;

            textBox63.Text = vehicle.MARKI.Nazwa;
            maskedTextBox5.Text = vehicle.DataProd.ToString("d");
            textBox62.Text = vehicle.Kolor;
            textBox61.Text = vehicle.Przebieg.ToString("D");
            textBox60.Text = vehicle.ZaGodz.ToString("C");
            textBox59.Text = vehicle.NrRejestr;
            checkBox7.Checked = vehicle.Sprawny == 0 ? false : true;
            comboBox7.SelectedIndex = vehicle.Rodzaj;
            richTextBox1.Text = vehicle.Opis;
        }
    }
}
