using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POH2Luokat
{
    class Tyontekija : IId, INimi
    {
        private double _palkka;
        public int Id;
        public string Nimi;
        public double Palkka;
        public DateTime PalkkausPvm;
        public DateTime? PaattymisPvm;

        public Tyontekija() {

        }

        public Tyontekija(int id, string etu, string suku, DateTime sa) {

        }

        public Tyontekija(int id, string etu, string suku, DateTime sa, double palkka) {

        }

        public override string ToString() {
            return base.ToString();
        }
    }
}
