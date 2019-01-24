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
        public DateTime DateWypoz { get; set; }
        public string DataZwrotu { get; set; }
        public DateTime DateZwrotu { get; set; }
        public string Pojazd { get; set; }
        public string Klient { get; set; }

        public ReservationListGrid() { }

        public ReservationListGrid(REZERWACJE rezerwacje)
        {
            idRezerw = rezerwacje.idRezerw;
            DataWypoz = rezerwacje.DataWypoz.ToString("d");
            DateWypoz = rezerwacje.DataWypoz;
            DataZwrotu = rezerwacje.DataZwrotu.ToString("d");
            DateZwrotu = rezerwacje.DataZwrotu;
            Pojazd = rezerwacje.POJAZDY.MARKI.Nazwa;
            Klient = rezerwacje.KLIENCI.Imie + " " + rezerwacje.KLIENCI.Nazwisko;
        }

        public static explicit operator ReservationListGrid(REZERWACJE rezerwacje)
        {
            return new ReservationListGrid(rezerwacje);
        }
    }
}
