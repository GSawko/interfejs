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
    public partial class FormOpinion : Form
    {
        List<OpinionListGrid> _opinionListGrids;
        public FormOpinion(List<OPINIA> opinions)
        {
            InitializeComponent();
            LoadVehicleOpinion(opinions);
        }

        public void LoadVehicleOpinion(List<OPINIA> opinions)
        {
            _opinionListGrids = new List<OpinionListGrid>();
            foreach (OpinionListGrid opinion in opinions)
            {
                _opinionListGrids.Add(opinion);
            }

            dataGridView1.DataSource = _opinionListGrids;
        }
    }
}
