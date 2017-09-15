using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POH2Luokat
{
    class Henkilo
    {
        public string EtuNimi;
        public string SukuNimi;
        public DateTime? SyntymaAika;
        public int Ika;

        public Henkilo() {

        }

        public Henkilo(string etu, string suku) {

        }

        public override string ToString() {
            return base.ToString();
        }
    }
}
