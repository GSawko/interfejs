using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.GridView
{
    class OpinionListGrid
    {
        public int idOpinia { get; set; }
        public string Data { get; set; }
        public short Ocena { get; set; }
        public string Opis { get; set; }

        public OpinionListGrid() { }

        public OpinionListGrid(OPINIA opinion)
        {
            idOpinia = opinion.idOpinia;
            Data = opinion.DataWyst.ToString("d");
            Ocena = opinion.Ocena;
            Opis = opinion.Opis;
        }

        public static explicit operator OpinionListGrid(OPINIA opinion)
        {
            return new OpinionListGrid(opinion);
        }
    }
}
