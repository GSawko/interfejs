using MySql.Data.MySqlClient;

namespace GUI
{
    class BazaDanych
    {
        private MySqlConnection connection = new MySqlConnection("SERVER=host.heap.cf;DATABASE=ISDB;UID=isdb;PASSWORD=tg+@8/Sn}HS7[_gV;");

        public bool Zaloguj(string login, string haslo)
        {
            
            connection.Open();
            string query = "SELECT * FROM LoginData WHERE login = '" + login + "' AND haslo = '" + haslo + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            var prawidlowyLoginHaslo = dataReader.HasRows;
            connection.Close();
            return prawidlowyLoginHaslo;
        }
        

    }
}
