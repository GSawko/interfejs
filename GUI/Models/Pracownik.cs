using MySql.Data.MySqlClient;
using System;

namespace GUI.Models
{
    class Pracownik
    {
        int idPrac;
        public string imie;
        public string drugieImie;
        string nazwisko;
        DateTime dataUrodzenia;
        DateTime dataZatrudnienia;
        string adres;
        string telefon;
        string email;
        bool plec;
        string login;
        //string haslo;
        //<typ> zdjecie;

        public string ImieNazwisko => (imie + " " + nazwisko);

        public Pracownik(MySqlDataReader dataReader)
        {
            idPrac = dataReader.GetInt32(0);
            imie = dataReader.GetString(2);
            drugieImie = dataReader.IsDBNull(3) ? "" : dataReader.GetString(3);
            nazwisko = dataReader.GetString(4);
            dataUrodzenia = dataReader.GetDateTime(5);
            dataZatrudnienia = dataReader.GetDateTime(6);
            adres = dataReader.GetString(7);
            telefon = dataReader.GetString(8);
            email = dataReader.IsDBNull(9) ? "" : dataReader.GetString(9);
            plec = dataReader.GetBoolean(10);
            login = dataReader.GetString(11);
        }
    }
}
