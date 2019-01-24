using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.GridView
{
    class ReservationListGrid
    {
        public int idRezerw { get; set; }
        public string DataWypoz { get; set; }
        public string DataZwrotu { get; set; }

        public string Pojazd { get; set; }
        public string Klient { get; set; }

        public ReservationListGrid() { }

        public ReservationListGrid(REZERWACJE rezerwacje)
        {
            idRezerw = rezerwacje.idRezerw;
            DataWypoz = rezerwacje.DataWypoz.ToString("d");
            DataZwrotu = rezerwacje.DataZwrotu.ToString("d");
            Pojazd = rezerwacje.POJAZDY.MARKI.Nazwa;
            Klient = rezerwacje.KLIENCI.Imie + " " + rezerwacje.KLIENCI.Nazwisko;
        }

        public static explicit operator ReservationListGrid(REZERWACJE rezerwacje)
        {
            return new ReservationListGrid(rezerwacje);
        }
    }
}
