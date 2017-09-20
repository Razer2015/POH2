using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace POH2Luokat
{
    public delegate void PalkkaaminenHandler(object sender, Tyontekija t, CancelEventArgs e);
    public delegate void ErottaminenHandler(object sender, Tyontekija t, EventArgs e);

    public class Osasto : IId, INimi
    {
        public event PalkkaaminenHandler Palkkaaminen;
        public event ErottaminenHandler Erottaminen;

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
            var paluu = ($"{this.Nimi} ({this.HenkiloLkm})");
            paluu += Palkkaaminen != null ? " +" : "";
            paluu += Erottaminen != null ? " -" : "";
            return (paluu);
        }

        public void Palkkaa(Tyontekija tyontekija, double palkka) {
            if(Palkkaaminen != null) {
                var cancel = new CancelEventArgs();
                Palkkaaminen(this, tyontekija, cancel);

                if (cancel.Cancel.Equals(true)) {
                    return;
                }
            }

            tyontekija.Palkka = palkka;
            tyontekija.PalkkausPvm = DateTime.Now;
            this.Tyontekijat.Add(tyontekija);

        }

        public void Erota(Tyontekija tyontekija) {
            if (Erottaminen != null) {
                var eventArgs = new EventArgs();
                Erottaminen(this, tyontekija, eventArgs);
            }

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
