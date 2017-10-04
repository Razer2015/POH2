using System;
using System.Collections.Generic;

namespace POH2Luokat
{
    public class Tyontekija : Henkilo, IId, INimi
    {
        private double _palkka;
        public int Id { get; private set; }
        public string Nimi { get { return ($"{this.SukuNimi} {this.EtuNimi}"); } set { this.Nimi = value; } }
        public double Palkka
        {
            get { return (this._palkka); }
            set
            {
                if (value >= 0) {
                    this._palkka = Math.Round(value, 2);
                }
                else {
                    throw new ApplicationException("Negatiivinen palkka");
                }
            }
        }
        public DateTime PalkkausPvm { get; set; }
        public DateTime? PaattymisPvm { get; set; }

        public Tyontekija() : base() {
            this.PalkkausPvm = DateTime.Now;
            this.PaattymisPvm = null;
        }

        public Tyontekija(int id, string etu, string suku, DateTime sa) : base() {
            this.Id = id;
            this.EtuNimi = etu;
            this.SukuNimi = suku;
            this.SyntymaAika = sa;
        }

        public Tyontekija(int id, string etu, string suku, DateTime sa, double palkka) : this(id, etu, suku, sa) {
            this.Palkka = palkka;
        }

        public override string ToString() {
            return ($"{this.EtuNimi} {this.SukuNimi} [{this.Id}]");
        }

        public static List<Tyontekija> GeneroiData(int lkm) {
            var data = new List<Tyontekija>();
            for (int i = 0; i < lkm; i++) {
                data.Add(new Tyontekija(i + 1, etunimet[rand.Next(0, etunimet.Length)],
                    $"{sukualku[rand.Next(0, sukualku.Length)]}{sukuloppu[rand.Next(0, sukuloppu.Length)]}", GetRandomDateTime(new DateTime(1950, 1, 1), new DateTime(1995, 12, 31))));
            }
            return (data);
        }

        /// <summary>
        /// Author: https://stackoverflow.com/a/1483677
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        static DateTime GetRandomDateTime(DateTime startDate, DateTime endDate) {
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, rand.Next(0, (int)timeSpan.TotalMinutes), 0);
            return (startDate + newSpan);
        }

        static string[] etunimet =
        {
            "Antti", "Aulis", "Aulikki", "Bertta", "Eemeli", "Eetu", "Esko", "Hannele", "Hannu", "Heikki",
            "Harri", "Heli", "Helena", "Iines", "Ilkka", "Ilmari", "Ida", "Irene", "Jaakko", "Jari", "Juha", "Jukka", "Jaana",
            "Juhani", "Kalevi", "Kikka", "Kaarina", "Kalle", "Kauko", "Kimmo", "Liisa", "Leevi", "Lilli", "Lumi", "Martti", "Matti",
            "Mauno", "Melina", "Miisa", "Merja", "Marja", "Niilo", "Nina", "Niina", "Ninna", "Oona", "Olavi", "Oskari", "Pentti",
            "Panu", "Pertti", "Petri", "Paula", "Pauliina", "Pirkko", "Raimo", "Rauli", "Riikka", "Ritva", "Raili", "Sara", "Saara",
            "Sauli", "Sakari", "Seppo", "Taina", "Tiina", "Tuuli", "Tuula", "Timo", "Tino", "Tomas", "Tuomas", "Unto", "Uolevi",
            "Veikko", "Veera", "Vesa", "Väinö", "Yrjö"
        };
        static string[] sukualku =
        {
            "Aalto", "Joki", "Meri", "Järvi", "Virta", "Vuori", "Lehto", "Leppi", "Koivu",
            "Salo","Niemi", "Lahti", "Laiho", "Haka", "Ala", "Ylä", "Väli", "Keski"
        };
        static string[] sukuloppu = { "", "nen", "talo", "mäki", "pää", "kari", "la" };
        static Random rand = new Random();
    }
}
