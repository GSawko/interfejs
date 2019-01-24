using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.GridView
{
    class ClientListGrid
    {
        public int idKlient { get; set; }
        public string ImieNazwisko { get; set; }
        public string DataUr { get; set; }
        public string NrDowOsob { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }

        public ClientListGrid() { }

        public ClientListGrid(KLIENCI klient)
        {
            idKlient = klient.idKlient;
            ImieNazwisko = klient.Imie + " " + klient.Nazwisko;
            DataUr = klient.DataUr.ToString("d");
            NrDowOsob = klient.NrDowOsob;
            Telefon = klient.Telefon;
            Email = klient.Email;
            Login = klient.Login;
        }

        public static explicit operator ClientListGrid(KLIENCI klient)
        {
            return new ClientListGrid(klient);
        }
    }
}
