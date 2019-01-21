using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Service
{
    static class WorkerService
    {
        public static PRACOWNICY GetWorker(int idWork)
        {
            using (var entities = new DBEntities())
            {
                PRACOWNICY pracownik = entities.PRACOWNICY
                    .Where(p => p.idPrac == idWork)
                    .FirstOrDefault();

                return pracownik;
            }
        }

        public static PRACOWNICY GetWorker(string login)
        {
            using (var entities = new DBEntities())
            {
                PRACOWNICY pracownik = entities.PRACOWNICY
                    .Where(p => p.Login == login)
                    .FirstOrDefault();

                return pracownik;
            }
        }

        public static bool UpdateWorker(PRACOWNICY updatePracownik)
        {
            using (var entities = new DBEntities())
            {
                var pracownik = entities.PRACOWNICY
                    .Where(p => p.idPrac == updatePracownik.idPrac)
                    .First();

                entities.Entry(pracownik).CurrentValues.SetValues(updatePracownik);

                entities.SaveChanges();

                return true;
            }
        }

        public static bool AddWorker(PRACOWNICY newPracownik)
        {
            using (var entities = new DBEntities())
            {
                entities.PRACOWNICY.Add(newPracownik);
                entities.SaveChanges();
            }

            return true;
        }

        public static bool RemoveWorker(int idWorker)
        {
            PRACOWNICY worker = new PRACOWNICY() { idPrac = idWorker };
            using (var entities = new DBEntities())
            {
                try
                {
                    entities.PRACOWNICY.Attach(worker);
                    entities.PRACOWNICY.Remove(worker);
                    entities.SaveChanges();
                }
                catch (Exception)
                {
                    if (!entities.PRACOWNICY.Any(p => p.idPrac == idWorker))
                        return false;
                }
            }

            return true;
        }
    }
}
