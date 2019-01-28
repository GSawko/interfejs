using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.GridView
{
    class InspectionListGrid
    {
        public int idPrzegl { get; set; }
        public string Data { get; set; }
        public string Opis { get; set; }

        public InspectionListGrid() { }

        public InspectionListGrid(PRZEGLADY inspection)
        {
            idPrzegl = inspection.idPrzegl;
            Data = inspection.Data.ToString("d");
            Opis = inspection.Opis;
        }

        public static explicit operator InspectionListGrid(PRZEGLADY inspection)
        {
            return new InspectionListGrid(inspection);
        }
    }
}
