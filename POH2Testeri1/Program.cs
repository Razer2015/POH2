using POH2Luokat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POH2Testeri1
{
    class Program
    {
        static void Main(string[] args) {
            int tyontekijatLKM = 20;
            var tyontekijat = Tyontekija.GeneroiData(tyontekijatLKM);
            Console.WriteLine($"Luodaan {tyontekijatLKM} työntekijää...");
            foreach (var tyontekija in tyontekijat) {
                Console.WriteLine("{0}, ikä {1}", tyontekija.ToString(), tyontekija.Ika);
            }

            int osastoLKM = 5;
            Console.WriteLine($"\r\nLuodaan {osastoLKM} osastoa...");
            var osastot = Osasto.GeneroiData(osastoLKM);
            foreach (var osasto in osastot) {
                Console.WriteLine(osasto.ToString());
            }

            Console.ReadKey();
        }
    }
}
