using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POH2Luokat
{
    class Henkilo
    {
        public string EtuNimi { get; set; }
        public string SukuNimi { get; set; }
        public DateTime? SyntymaAika { get; set; }
        public int Ika
        {
            get
            {
                return ();
            }
        };

        public Henkilo() {

        }

        public Henkilo(string etu, string suku) {

        }

        public override string ToString() {
            return base.ToString();
        }
    }
}
