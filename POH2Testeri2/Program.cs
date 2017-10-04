using POH2Luokat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace POH2Testeri2
{
    class Program
    {
        static void Main(string[] args) {
            var osastot = Osasto.GeneroiData(20).ToArray();
            var tyontekijat = Tyontekija.GeneroiData(1000).ToArray();

            // Palkataan tyontekijät satunnaisiin osastoihin
            var random = new Random();
            var palkkarand = new Random();
            for (int i = 0; i < tyontekijat.Length; i++) {
                osastot[random.Next(0, osastot.Length)].Palkkaa(tyontekijat[i], palkkarand.Next(1000, 8000));
            }

            //// Tulostetaan osastojen tiedot
            //for (int i = 0; i < osastot.Length; i++) {
            //    Console.WriteLine(osastot[i].ToString());
            //}

            int valintamaara = 6;
            int valinta;
            while (true) {
                Console.WriteLine("Vaihtoehdot");
                Console.WriteLine($"1. 50-vuotiaat työntekijät");
                Console.WriteLine($"2. Osastot yli 50 henkilöä");
                Console.WriteLine($"3. Sukunimen työntekijät");
                Console.WriteLine($"4. Osastojen isoimmat palkat");
                Console.WriteLine($"5. 5 Yleisintä sukunimeä");
                Console.WriteLine($"6. Osastojen ikäjakaumat");

                Console.WriteLine($"7. Lopeta");

                Console.Write("Valitse: ");
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out valinta)) {
                    if (valinta >= 0 && valinta <= valintamaara) {
                        switch (valinta) {
                            case 1: {
                                    var tyontekijat50 = tyontekijat.Where(x => x.Ika.Equals(50)).ToList();
                                    TulostaTulos(tyontekijat50);
                                    break;
                                }
                            case 2: {
                                    var osastotyli50hlo = osastot.Where(x => x.HenkiloLkm >= 50)
                                        .OrderByDescending(x => x.HenkiloLkm).ToList();
                                    TulostaTulos(osastotyli50hlo);
                                    break;
                                }
                            case 3: {
                                    Console.Write("Anna sukunimi: ");
                                    var userInput2 = Console.ReadLine();

                                    var tyontekijatsnimi = tyontekijat.Where(
                                        x => x.SukuNimi.Equals(userInput2, StringComparison.CurrentCultureIgnoreCase))
                                        .Select(x => new { Nro = x.Id, Nimi = $"{x.SukuNimi} {x.EtuNimi}" }).ToList();
                                    if(tyontekijatsnimi.Count > 0) {
                                        TulostaTulos(tyontekijatsnimi);
                                    }
                                    else {
                                        Console.WriteLine($"Ei yhtään tyontekijää sukunimellä: {userInput2}");
                                    }
                                    break;
                                }
                            case 4: {
                                    var osastotyontekijat = osastot.SelectMany(o => o.Tyontekijat,
                                        (o, t) => new { OsastoNimi = o.Nimi, Palkka = t.Palkka });
                                    var ryhmitetty = osastotyontekijat.GroupBy(t => t.OsastoNimi);
                                    var korkeapalkkaiset = ryhmitetty.Select(y => new
                                    {
                                        OsastoNimi = y.Key,
                                        Maksimipalkka = y.Max(x => x.Palkka)
                                    }).ToList();
                                    TulostaTulos(korkeapalkkaiset);
                                    break;
                                }
                            case 5: {
                                    var tyontekijatSNimiGroups = tyontekijat.GroupBy(x => x.SukuNimi)
                                        .Select(x => new { SukuNimi = x.Key, Lkm = x.Count() }).OrderByDescending(x => x.Lkm);
                                    var tyontekijatSNimi5suos = tyontekijatSNimiGroups.Take(5).ToList();
                                    TulostaTulos(tyontekijatSNimi5suos);
                                    break;
                                }
                            case 6: {
                                    var results = osastot.Select(x => new
                                    {
                                        Nimi = x.Nimi,
                                        Alle30v = x.Tyontekijat.Count(y => y.Ika < 30),
                                        Välillä30_50v = x.Tyontekijat.Count(z => z.Ika >= 30 && z.Ika <= 50),
                                        Yli50v = x.Tyontekijat.Count(y => y.Ika > 50)
                                    }).ToList();

                                    TulostaTulos(results);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    else if (valinta == valintamaara + 1) {
                        return;
                    }
                }
                else {
                    Console.WriteLine("Väärä valinta. Paina Enter.");
                    Console.WriteLine();
                }
            };

            Console.ReadKey();
        }


        static void TulostaTulos<T>(List<T> tulos) {
            if (tulos is List<Tyontekija>) {
                Console.WriteLine("Id".PadRight(5) + "Etunimi".PadRight(10) + "Sukunimi".PadRight(10) +
    "Ikä".PadRight(4) + "Syntymäaika".PadRight(12) + "Palkka".PadRight(8));
                foreach (object t in tulos) {
                    var ty = (Tyontekija)t;
                    var str = ty.Id.ToString().PadRight(5) +
                                ty.EtuNimi.PadRight(10) +
                                ty.SukuNimi.PadRight(10) +
                                ty.Ika.ToString().PadRight(4) +
                                ty.SyntymaAika.Value.ToShortDateString().PadRight(12) +
                                ty.Palkka.ToString("###0").PadRight(8);

                    Console.WriteLine(str);
                }
            }
            else if (tulos is List<Osasto>) {
                Console.WriteLine("Id".PadRight(5) + "Nimi".PadRight(10) + "HenkilöLkm".PadRight(10));

                foreach (object o in tulos) {
                    var os = (Osasto)o;

                    var str = os.Id.ToString().PadRight(5) +
                                os.Nimi.PadRight(10) +
                                os.HenkiloLkm.ToString().PadRight(10);

                    Console.WriteLine(str);
                }
            }
            else {
                string rivi = "";
                foreach (PropertyInfo ominaisuus in tulos[0].GetType().GetProperties()) {
                    rivi += ominaisuus.Name + " | ";
                }
                Console.WriteLine(rivi);

                foreach (object item in tulos) {
                    rivi = "";
                    foreach (PropertyInfo ominaisuus in item.GetType().GetProperties()) {
                        rivi += ominaisuus.GetValue(item, null).ToString() + " | ";
                    }
                    Console.WriteLine(rivi);
                }
            }

            Console.Write("Paina Enter jatkaaksesi.");
            Console.ReadLine();
        }
    }
}
