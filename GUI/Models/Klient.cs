using MySql.Data.MySqlClient;
using System;

namespace GUI.Models
{
    class Klient
    {
        int idKlient;
        public string Imie { get; }
        public string DrugieImie { get; }
        public string Nazwisko { get; }
        public DateTime DataRejestracji { get; }
        public string NrPrawaJazd { get; }
        public string NrDowOsob { get; }
        public string Telefon { get; }
        public string Email { get; }
        public bool Plec { get; }
        public string Login { get; }
        public string Haslo { get; }
        //<typ> zdjecie;

        public string ImieNazwisko => (Imie + " " + Nazwisko);

        public Klient(MySqlDataReader dataReader)
        {
            idKlient = dataReader.GetInt32(0);
            Imie = dataReader.GetString(1);
            DrugieImie = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
            Nazwisko = dataReader.GetString(3);
            DataRejestracji = dataReader.GetDateTime(4);
            NrPrawaJazd = dataReader.GetString(5);
            NrDowOsob = dataReader.GetString(6);
            Telefon = dataReader.GetString(7);
            Email = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8);
            Plec = dataReader.GetBoolean(9);
            Login = dataReader.GetString(10);
            Haslo = dataReader.GetString(11);
        }

    }
}
