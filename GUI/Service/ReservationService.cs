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
                    .Include("OPINIA")
                    .ToList();

                return reservation;
            }
        }
        public static List<REZERWACJE> GetAllNotTaken()
        {
            using (var entities = new DBEntities())
            {
                var reservation = entities.REZERWACJE
                    .Where(r => r.Wypozycz == 0)
                    .Include("KLIENCI")
                    .Include("POJAZDY")
                    .Include("POJAZDY.MARKI")
                    .Include("OPINIA")
                    .ToList();

                return reservation;
            }
        }

        public static List<REZERWACJE> GetAllNotReturned()
        {
            using (var entities = new DBEntities())
            {
                var reservation = entities.REZERWACJE
                    .Where(r => r.Wypozycz == 1)
                    .Include("KLIENCI")
                    .Include("POJAZDY")
                    .Include("POJAZDY.MARKI")
                    .Include("PRACOWNICY")
                    .Include("OPINIA")
                    .ToList();

                return reservation;
            }
        }

        public static REZERWACJE GetReservation(int id)
        {
            using (var entities = new DBEntities())
            {
                var reservation = entities.REZERWACJE
                    .Where(r => r.idRezerw == id)
                    .Include("KLIENCI")
                    .Include("POJAZDY")
                    .Include("POJAZDY.MARKI")
                    .Include("POJAZDY.ZDJECIA")
                    .Include("PRACOWNICY")
                    .Include("PRACOWNICY1")
                    .Include("OPINIA")
                    .First();

                return reservation;
            }
        }

        public static List<REZERWACJE> GetClientReservation(int id)
        {
            using (var entities = new DBEntities())
            {
                var reservation = entities.REZERWACJE
                    .Where(r => r.KLIENCI_idKlient == id)
                    .Include("KLIENCI")
                    .Include("POJAZDY")
                    .Include("POJAZDY.MARKI")
                    .Include("PRACOWNICY")
                    .Include("OPINIA")
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

        public static bool ChangeStatus(int idReservation, sbyte status , DateTime? dataZdania = null)
        {
            using (var entities = new DBEntities())
            {
                var reservation = entities.REZERWACJE
                    .Where(r => r.idRezerw == idReservation)
                    .FirstOrDefault();

                reservation.Wypozycz = status;
                if (status == 2 && dataZdania != null)
                    reservation.DataZdania = dataZdania;

                entities.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool AddOpinion(OPINIA newOpinia)
        {
            using (var entities = new DBEntities())
            {
                entities.OPINIA.Add(newOpinia);
                entities.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool Add(REZERWACJE newReservation)
        {
            using (var entities = new DBEntities())
            {
                entities.REZERWACJE.Add(newReservation);
                entities.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
