using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POH2Luokat
{
    public class Osasto : IId, INimi
    {
        public int Id { get; private set; }
        public string Nimi { get; set; }
        public List<Tyontekija> Tyontekijat { get; set; }
        public int HenkiloLkm { get { return (this.Tyontekijat.Count); } }

        public Osasto() {
            this.Tyontekijat = new List<Tyontekija>();
        }

        public Osasto(int id, string nimi) : this() {
            this.Id = id;
            this.Nimi = nimi;
        }

        public override string ToString() {
            return ($"{this.Nimi} ({this.HenkiloLkm})");
        }

        public void Palkkaa(Tyontekija tyontekija, double palkka) {
            tyontekija.Palkka = palkka;
            tyontekija.PalkkausPvm = DateTime.Now;
            this.Tyontekijat.Add(tyontekija);

        }

        public void Erota(Tyontekija tyontekija) {
            tyontekija.PaattymisPvm = DateTime.Now;
            this.Tyontekijat.Remove(tyontekija);
        }

        /// <summary>
        /// Generoi Dataa
        /// </summary>
        /// <param name="lkm"></param>
        public static List<Osasto> GeneroiData(int lkm) {
            var data = new List<Osasto>();
            for (int i = 0; i < lkm; i++) {
                data.Add(new Osasto(i + 1, $"Osasto {i + 1}"));
            }
            return (data);
        }
    }
}
