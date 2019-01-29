using GUI.GridView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Service
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

        public static POJAZDY GetVehicle(int idVehicle)
        {
            using (var entities = new DBEntities())
            {
                var vehicle = entities.POJAZDY
                    .Where(p => p.idPojazd == idVehicle)
                    .Include("MARKI")
                    .Include("PRZEGLADY")
                    .Include("REZERWACJE")
                    .Include("ZDJECIA")
                    .Include("KATEGORIEPJAZDY")
                    .FirstOrDefault();

                return vehicle;
            }
        }

        public static List<POJAZDY> GetFreeVehicle(DateTime startReserv, DateTime endReserv)
        {
            using (var entities = new DBEntities())
            {
                var vehicles = entities.POJAZDY
                            .Where(v => v.Sprawny == 1)
                            .Where(v => !v.REZERWACJE.Any(r => (startReserv > r.DataWypoz && startReserv < r.DataZwrotu) ||
                                                                (endReserv > r.DataWypoz && endReserv < r.DataZwrotu) ||
                                                                (r.DataWypoz > startReserv && r.DataWypoz < endReserv)))
                            .Include("MARKI")
                            .ToList();

                return vehicles;
            }
        }

        public static List<OPINIA> GetVehicleOpinions(int idVehicle)
        {
            using (var entities = new DBEntities())
            {
                var opinions = entities.OPINIA
                    .Where(o => o.REZERWACJE.POJAZDY_idPojazd == idVehicle)
                    .ToList();

                return opinions;
            }
        }

        public static List<PRZEGLADY> GetVehicleInspection(int idVehicle)
        {
            using (var entities = new DBEntities())
            {
                var inspection = entities.PRZEGLADY
                    .Where(i => i.POJAZDY_idPojazd == idVehicle)
                    .ToList();

                return inspection;
            }

            return new List<PRZEGLADY>();
        }

        public static bool UpdateVehicle(POJAZDY updateVehicle)
        {
            using (var entities = new DBEntities())
            {
                var vehicle = entities.POJAZDY
                    .First(v => v.idPojazd == updateVehicle.idPojazd);

                entities.Entry(vehicle).CurrentValues.SetValues(updateVehicle);
                if (updateVehicle.ZDJECIA.First()?.idZdjecia == 0)
                    entities.ZDJECIA.Add(updateVehicle.ZDJECIA.First());

                entities.SaveChanges();

                return true;
            }
            return false;
        }

        public static bool AddVehicle(POJAZDY newVehile)
        {
            using (var entities = new DBEntities())
            {
                var licences = new List<KATEGORIEPJAZDY>();
                foreach (var lic in newVehile.KATEGORIEPJAZDY)
                {
                    var licence = entities.KATEGORIEPJAZDY.First(k => k.idKatPJ == lic.idKatPJ);
                    licences.Add(licence);
                }

                newVehile.KATEGORIEPJAZDY = licences;

                if (newVehile.MARKI_idMarki == 0)
                {
                    var brandID = CarBrandService.GetCarBrandId(newVehile.MARKA_Nazwa);
                    if (brandID == -1)
                    {
                        brandID = CarBrandService.AddCarBrand(new MARKI() { Nazwa = newVehile.MARKA_Nazwa });
                    }

                    newVehile.MARKI_idMarki = brandID;
                }

                entities.POJAZDY.Add(newVehile);
                entities.SaveChanges();

                return true;
            }
        }

        public static bool AddVehicleInspection(PRZEGLADY newInspection)
        {
            using (var entities = new DBEntities())
            {
                entities.PRZEGLADY.Add(newInspection);
                entities.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool RemoveVehicle(int idVehicle)
        {
            POJAZDY vehicle = new POJAZDY() { idPojazd = idVehicle };
            using (var entities = new DBEntities())
            {
                try
                {
                    entities.POJAZDY.Attach(vehicle);
                    entities.POJAZDY.Remove(vehicle);
                    entities.SaveChanges();
                }
                catch (Exception)
                {
                    if (!entities.POJAZDY.Any(p => p.idPojazd == idVehicle))
                        return false;
                }
            }

            return true;
        }
    }
}
