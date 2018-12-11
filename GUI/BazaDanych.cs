using GUI.Models;
using MySql.Data.MySqlClient;

namespace GUI
{
    class BazaDanych
    {
        private MySqlConnection connection = new MySqlConnection("SERVER=host.heap.cf;DATABASE=ISDB;UID=isdb;PASSWORD=tg+@8/Sn}HS7[_gV;");

        public bool Zaloguj(string login, string haslo)
        {
            connection.Open();
            string query = $"SELECT * FROM LoginData WHERE login='{login}' AND haslo='{haslo}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            var prawidlowyLoginHaslo = dataReader.HasRows;
            connection.Close();
            return prawidlowyLoginHaslo;
        }

        public Klient WczytajKlienta(string login)
        {
            connection.Open();
            string query = $"SELECT * FROM KLIENCI WHERE login='{login}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Klient klient = null;
            if (dataReader.Read())
                klient = new Klient(dataReader);

            connection.Close();

            return klient;
        }

        public Pracownik WczytajPracownika(string login)
        {
            connection.Open();
            string query = $"SELECT * FROM PRACOWNICY WHERE login='{login}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Pracownik pracownik = null;
            if (dataReader.Read())
                pracownik = new Pracownik(dataReader);

            connection.Close();

            return pracownik;
        }

        public Klient SzukajPoID(string id)
        {
            connection.Open();
            string query = $"SELECT * FROM KLIENCI WHERE idKlient='{id}'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            Klient klient = null;
            if (dataReader.Read())
                klient = new Klient(dataReader);

            connection.Close();

            return klient;
        }
        
    }
}
