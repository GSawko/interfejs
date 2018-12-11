using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    class KategoriaPrawaJazdy
    {
        int idKatPJ;
        string Nazwa;
        string Opis;

        KategoriaPrawaJazdy(MySqlDataReader dataReader)
        {
            idKatPJ = dataReader.GetInt32(0);
            Nazwa = dataReader.GetString(1);
            Opis = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2);
        }
    }
}
