﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    static class ClientService
    {

        public static List<KLIENCI> GetClients()
        {
            using (var entities = new DBEntities())
            {
                var clients = entities.KLIENCI
                    .ToList();

                return clients;
            }
        }
        public static KLIENCI GetClient(string loginOrIDNumb, bool IDNumb = false)
        {
            using (var entities = new DBEntities())
            {
                KLIENCI klient = null;
                if (!IDNumb)
                {
                    klient = entities.KLIENCI
                        .Where(k => k.Login == loginOrIDNumb)
                        .Include("KATEGORIEPJAZDY")
                        .Include("REZERWACJE")
                        .FirstOrDefault();
                }
                else
                {
                    klient = entities.KLIENCI
                        .Where(k => k.NrDowOsob.StartsWith(loginOrIDNumb))
                        .Include("KATEGORIEPJAZDY")
                        .Include("REZERWACJE")
                        .FirstOrDefault();
                }

                return klient;
            }
        }

        public static KLIENCI GetClient(int id)
        {
            using (var entities = new DBEntities())
            {
                var klient = entities.KLIENCI
                    .Where(k => k.idKlient == id)
                    .Include("KATEGORIEPJAZDY")
                    .Include("REZERWACJE")
                    .FirstOrDefault();

                return klient;
            }
        }

        public static bool UpdateClient(KLIENCI updateKlient)
        {
            using (var entities = new DBEntities())
            {
                var klient = entities.KLIENCI
                    .Where(k => k.idKlient == updateKlient.idKlient)
                    .Include("KATEGORIEPJAZDY")
                    .Include("REZERWACJE")
                    .First();

                entities.Entry(klient).CurrentValues.SetValues(updateKlient);

                klient.KATEGORIEPJAZDY = new List<KATEGORIEPJAZDY>();
                foreach (var kat in updateKlient.KATEGORIEPJAZDY)
                {
                    var licence = entities.KATEGORIEPJAZDY.First(k => k.idKatPJ == kat.idKatPJ);
                    klient.KATEGORIEPJAZDY.Add(licence);
                }

                entities.SaveChanges();
                return true;
            }
        }

        public static bool AddClient(KLIENCI newClient)
        {
            using (var entities = new DBEntities())
            {
                var licenses = new List<KATEGORIEPJAZDY>();
                foreach (var lic in newClient.KATEGORIEPJAZDY)
                {
                    var licence = entities.KATEGORIEPJAZDY.First(k => k.idKatPJ == lic.idKatPJ);
                    licenses.Add(licence);
                }

                newClient.KATEGORIEPJAZDY = licenses;
                entities.KLIENCI.Add(newClient);
                entities.SaveChanges();

                return true;
            }
        }

        public static bool RemoveClient(int idClient)
        {
            KLIENCI client = new KLIENCI() { idKlient = idClient };
            using (var entities = new DBEntities())
            {
                try
                {
                    entities.KLIENCI.Attach(client);
                    entities.KLIENCI.Remove(client);
                    entities.SaveChanges();
                }
                catch (Exception)
                {
                    if (!entities.KLIENCI.Any(k => k.idKlient == idClient))
                        return false;
                }
            }

            return true;
        }
    }
}
