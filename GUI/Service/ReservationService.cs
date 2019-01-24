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
        public static List<REZERWACJE> GetReservations()
        {
            using (var entities = new DBEntities())
            {
                var reservation = entities.REZERWACJE
                    .Include("KLIENCI")
                    .Include("POJAZDY")
                    .Include("POJAZDY.MARKI")
                    .ToList();

                return reservation;
            }
        }
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

        public static bool RemoveReservation(int idReservation)
        {
            REZERWACJE reservation = new REZERWACJE() { idRezerw = idReservation };
            using (var entities = new DBEntities())
            {
                try
                {
                    entities.REZERWACJE.Attach(reservation);
                    entities.REZERWACJE.Remove(reservation);
                    entities.SaveChanges();
                }
                catch (Exception)
                {
                    if (!entities.REZERWACJE.Any(r => r.idRezerw == idReservation))
                        return false;
                }
            }

            return true;
        }
    }
}
