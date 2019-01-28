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

namespace GUI
{
    public partial class FormInspection : Form
    {
        int _vehicleId;
        List<InspectionListGrid> _inspectionLists;

        public FormInspection(int vehicleId)
        {
            InitializeComponent();
            _vehicleId = vehicleId;
            LoadInspectionList();
        }

        public void LoadInspectionList()
        {
            var inspections = VehicleService.GetVehicleInspection(_vehicleId);
            _inspectionLists = new List<InspectionListGrid>();

            foreach (InspectionListGrid inspection in inspections)
            {
                _inspectionLists.Add(inspection);
            }

            dataGridView1.DataSource = _inspectionLists;
        }

        public PRZEGLADY LoadInspectionFromAddForm()
        {
            var inspection = new PRZEGLADY();
            inspection.Data = dateTimePicker1.Value.Date;
            inspection.Opis = richTextBox1.TextOrDefault();
            inspection.POJAZDY_idPojazd = _vehicleId;

            return inspection;
        }

        public void ClearInspectionAddForm()
        {
            richTextBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var inspection = LoadInspectionFromAddForm();
            var isAdd = VehicleService.AddVehicleInspection(inspection);
            if (isAdd)
            {
                LoadInspectionList();
                ClearInspectionAddForm();
                MessageBox.Show("Przegląd został pomyślnie dodany");
            }
            else
            {
                MessageBox.Show("Błąd dodawania nowego przeglądu!");
            }
        }
    }
}
