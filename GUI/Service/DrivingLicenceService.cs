using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Service
{
    static class DrivingLicenceService
    {
        public static List<KATEGORIEPJAZDY> GetDrivingLicences()
        {
            using (var entities = new DBEntities())
            {
                var driveLicenceType = entities.KATEGORIEPJAZDY.ToList();

                return driveLicenceType;
            }
        }

        public static bool AddDrivingLicence(KATEGORIEPJAZDY newDrivingLicence)
        {
            using (var entities = new DBEntities())
            {
                entities.KATEGORIEPJAZDY.Add(newDrivingLicence);
                entities.SaveChanges();
            }

            return true;
        }

        public static bool RemoveDrivingLicence(int idDrivingLicence)
        {
            KATEGORIEPJAZDY drivingLicence = new KATEGORIEPJAZDY() { idKatPJ = idDrivingLicence };
            using (var entities = new DBEntities())
            {
                try
                {
                    entities.KATEGORIEPJAZDY.Attach(drivingLicence);
                    entities.KATEGORIEPJAZDY.Remove(drivingLicence);
                    entities.SaveChanges();
                }
                catch (Exception)
                {
                    if (!entities.KATEGORIEPJAZDY.Any(i => i.idKatPJ == idDrivingLicence))
                        return false;
                }
            }

            return true;
        }
    }
}
