using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.GridView
{
    class WorkerListGrid
    {
        public int idPrac { get; set; }
        public string ImieNazwisko { get; set; }
        public string DataZatr { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }

        public WorkerListGrid() { }

        public WorkerListGrid(PRACOWNICY pracownik)
        {
            idPrac = pracownik.idPrac;
            ImieNazwisko = pracownik.Imie + " " + pracownik.Nazwisko;
            DataZatr = pracownik.DataZatr.ToString("d");
            Telefon = pracownik.Telefon;
            Email = pracownik.Email;
            Login = pracownik.Login;
        }

        public static explicit operator WorkerListGrid(PRACOWNICY pracownik)
        {
            return new WorkerListGrid(pracownik);
        }
    }
}
