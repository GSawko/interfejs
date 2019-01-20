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
    public partial class Form4 : Form

    {
        public Form4(string Login)
        {
            InitializeComponent();
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
            DodajPracownikaPanel.BringToFront();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            EdytujPojazdPanel.BringToFront();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DodajPojazdPanel.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DodajKlientaPanel.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PodgladRezerwacjiPanel.BringToFront();
        }
    }
}
