using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Service
{
    static class ReservationService
    {
        public static List<REZERWACJE> GetAllNotTaken()
        {
            using (var entities = new DBEntities())
            {
                var reservation = entities.REZERWACJE
                    .Where(r => r.PRACOWNICY_idPracWydaj == null)
                    .Include("KLIENCI")
                    .Include("POJAZDY")
                    .ToList();

                return reservation;
            }
        }

        public static List<REZERWACJE> GetAllNotReturned()
        {
            using (var entities = new DBEntities())
            {
                var reservation = entities.REZERWACJE
                    .Where(r => r.PRACOWNICY_idPracOdbier == null)
                    .Include("KLIENCI")
                    .Include("POJAZDY")
                    .Include("PRACOWNICY")
                    .ToList();

                return reservation;
            }
        }
    }
}
