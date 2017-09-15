using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POH2Luokat
{
    class Osasto : IId, INimi
    {
        public int Id;
        public string Nimi;
        public List<Tyontekija> Tyontekijat;
        public int HenkiloLkm;

        public Osasto() {

        }

        public Osasto(int id, string nimi) {

        }

        public override string ToString() {
            return base.ToString();
        }

        public void Palkkaa(Tyontekija tyontekija, double palkka) {

        }

        public void Erota(Tyontekija tyontekija) {

        }
    }
}
