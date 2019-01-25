using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.GridView
{
    class ReservationListGrid
    {
        private static string[] Stan = { "Zarezerwowano", "Wydano", "Zakończona", "Anulowana" };
        public int idRezerw { get; set; }
        public DateTime DataWypoz { get; set; }
        public DateTime DataZwrotu { get; set; }
        public string Pojazd { get; set; }
        public string Klient { get; set; }
        public string Status { get ; set; }

        private void SetStatus(int stan)
        {
            Status = Stan[stan];
        }

        public ReservationListGrid() { }

        public ReservationListGrid(REZERWACJE rezerwacje)
        {
            idRezerw = rezerwacje.idRezerw;
            DataWypoz = rezerwacje.DataWypoz;
            DataZwrotu = rezerwacje.DataZwrotu;
            Pojazd = rezerwacje.POJAZDY.MARKI.Nazwa;
            Klient = rezerwacje.KLIENCI.Imie + " " + rezerwacje.KLIENCI.Nazwisko;
            SetStatus(rezerwacje.Wypozycz);
        }

        public static explicit operator ReservationListGrid(REZERWACJE rezerwacje)
        {
            return new ReservationListGrid(rezerwacje);
        }
    }
}
