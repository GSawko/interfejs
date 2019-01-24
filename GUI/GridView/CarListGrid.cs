using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.GridView
{
    class CarListGrid
    {
        private static string[] typPojazdu = { "Samochód", "Motocykl", "Motorower" };
        public int idPojazd { get; set; }
        public string Marka { get; set; }
        public string Rodzaj { get; set; }
        public string NrRejestr { get; set; }
        public string ZaGodz { get; set; }
        public string Sprawny { get; set; }

        public void SetRodzaj(int typ)
        {
            Rodzaj = typPojazdu[typ];
        }

        public CarListGrid() { }

        public CarListGrid(POJAZDY pojazd)
        {
            idPojazd = pojazd.idPojazd;
            Marka = pojazd.MARKI.ToString();
            SetRodzaj(pojazd.Rodzaj);
            NrRejestr = pojazd.NrRejestr;
            ZaGodz = pojazd.ZaGodz.ToString("C");
            Sprawny = pojazd.Sprawny == 0 ? "Nie" : "Tak";
        }

        public static explicit operator CarListGrid(POJAZDY pojazd)
        {
            return new CarListGrid(pojazd);
        }
    }
}
