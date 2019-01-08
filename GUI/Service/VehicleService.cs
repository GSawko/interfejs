using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    static class VehicleService
    {

        public static List<POJAZDY> GetVehicles()
        {
            using (var entities = new DBEntities())
            {
                var vehicles = entities.POJAZDY
                    .Include("MARKI")
                    .Include("PRZEGLADY")
                    .Include("REZERWACJE")
                    .Include("ZDJECIA")
                    .Include("KATEGORIEPJAZDY")
                    .ToList();

                return vehicles;
            }
        }

        public static List<POJAZDY> GetVehicle(int idVehicle)
        {
            using (var entities = new DBEntities())
            {
                var vehicles = entities.POJAZDY
                    .Where(p => p.idPojazd == idVehicle)
                    .Include("MARKI")
                    .Include("PRZEGLADY")
                    .Include("REZERWACJE")
                    .Include("ZDJECIA")
                    .Include("KATEGORIEPJAZDY")
                    .ToList();

                return vehicles;
            }
        }
    }
}
