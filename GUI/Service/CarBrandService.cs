using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Service
{
    static class CarBrandService
    {
        public static List<MARKI> GetCarBrands()
        {
            using (var entities = new DBEntities())
            {
                var carBrands = entities.MARKI.ToList();

                return carBrands;
            }
        }

        public static bool AddCarBrand(MARKI newCarBrand)
        {
            using (var entities = new DBEntities())
            {
                entities.MARKI.Add(newCarBrand);
                entities.SaveChanges();
            }

            return true;
        }

        public static bool RemoveCarBrand(int idCarBrand)
        {
            MARKI carBrand = new MARKI() { idMarki = idCarBrand };
            using (var entities = new DBEntities())
            {
                try
                {
                    entities.MARKI.Attach(carBrand);
                    entities.MARKI.Remove(carBrand);
                    entities.SaveChanges();
                }
                catch (Exception)
                {
                    if (!entities.MARKI.Any(i => i.idMarki == idCarBrand))
                        return false;
                }
            }

            return true;
        }
    }
}
