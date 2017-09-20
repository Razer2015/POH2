using POH2Luokat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace POH2Testeri1
{
    class Program
    {
        static void Main(string[] args) {
            //int tyontekijatLKM = 20;
            //var tyontekijat = Tyontekija.GeneroiData(tyontekijatLKM);
            //Console.WriteLine($"Luodaan {tyontekijatLKM} työntekijää...");
            //foreach (var tyontekija in tyontekijat) {
            //    Console.WriteLine("{0}, ikä {1}", tyontekija.ToString(), tyontekija.Ika);
            //}

            //int osastoLKM = 5;
            //Console.WriteLine($"\r\nLuodaan {osastoLKM} osastoa...");
            //var osastot = Osasto.GeneroiData(osastoLKM);
            //foreach (var osasto in osastot) {
            //    Console.WriteLine(osasto.ToString());
            //}

            var osastot = Osasto.GeneroiData(4).ToArray();
            var tyontekijat = Tyontekija.GeneroiData(80).ToArray();

            osastot[0].Palkkaaminen += PalkkausKasittelija;
            osastot[0].Erottaminen += ErottamisKasittelija;
            osastot[1].Palkkaaminen += PalkkausKasittelija;
            osastot[2].Erottaminen += ErottamisKasittelija;

            // Palkataan 20 työntekijää joka osastoon
            int index = 0;
            for (int i = 0; i < osastot.Length; i++) {
                for (int j = 0; j < tyontekijat.Length / osastot.Length; j++) {
                    osastot[i].Palkkaa(tyontekijat[index++], 0);
                }
            }

            // Erotetaan sukunimi "nen" loppuiset
            for (int i = 0; i < osastot.Length; i++) {
                var t = osastot[i].Tyontekijat;
                var erotettavat = t.FindAll(x => x.SukuNimi.EndsWith("nen"));
                foreach (var erotettava in erotettavat) {
                    osastot[i].Erota(erotettava);
                }
            }

            // Tulostetaan osastojen tiedot
            for (int i = 0; i < osastot.Length; i++) {
                Console.WriteLine(osastot[i].ToString());
                for (int j = 0; j < osastot[i].Tyontekijat.Count; j++) {
                    Console.WriteLine(osastot[i].Tyontekijat[j].ToString());
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private static void KirjoitaLokiin(string loki, string rivi) {
            try {
                using (var sw = new StreamWriter(loki, true)) {
                    sw.WriteLine(rivi);
                }
            }
            catch { }
        }

        private static void PalkkausKasittelija(object sender, Tyontekija t, CancelEventArgs e) {
            KirjoitaLokiin("POH2PalkkausLoki.txt", 
                $"{((Osasto)sender).Nimi} {DateTime.Now.ToString("dd.MM.yyyy")} {t.Nimi} {t.SyntymaAika.Value.ToString("dd.MM.yyyy")}");
        }


        private static void ErottamisKasittelija(object sender, Tyontekija t, EventArgs e) {
            KirjoitaLokiin("POH2ErottamisLoki.txt", string.Format("{0} {1} {2} {3}",
                ((Osasto)sender).Nimi,
                DateTime.Now.ToString("dd.MM.yyyy"),
                t.Nimi,
                t.SyntymaAika.Value.ToString("dd.MM.yyyy")));
        }
    }
}
