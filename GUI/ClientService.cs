using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class ClientService
    {
        public KLIENCI GetClient(string loginOrIDNumb, bool IDNumb = false)
        {
            if (!IDNumb)
            {
                using (var entities = new DBEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    return entities.KLIENCI.Where(k => k.Login == loginOrIDNumb).First();
                }
            }
            else
                using (var entities = new DBEntities())
                {
                    entities.Configuration.LazyLoadingEnabled = false;
                    return entities.KLIENCI.FirstOrDefault(k => k.NrDowOsob.StartsWith(loginOrIDNumb));
                }

        }

        public KLIENCI GetClient(int id)
        {
            using (var entities = new DBEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.KLIENCI.FirstOrDefault(k => k.idKlient == id);
            }
        }
    }
}
