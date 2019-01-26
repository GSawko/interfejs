using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.GridView
{
    class VehicleListGrid
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

        public static string GetTextType(int typ)
        {
            return typPojazdu[typ];
        }

        public VehicleListGrid() { }

        public VehicleListGrid(POJAZDY pojazd)
        {
            idPojazd = pojazd.idPojazd;
            Marka = pojazd.MARKI.Nazwa;
            SetRodzaj(pojazd.Rodzaj);
            NrRejestr = pojazd.NrRejestr;
            ZaGodz = pojazd.ZaGodz.ToString("C");
            Sprawny = pojazd.Sprawny == 0 ? "Nie" : "Tak";
        }

        public static explicit operator VehicleListGrid(POJAZDY pojazd)
        {
            return new VehicleListGrid(pojazd);
        }
    }
}
