using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POH2Luokat
{
    public class Henkilo
    {
        public string EtuNimi { get; set; }
        public string SukuNimi { get; set; }
        public DateTime? SyntymaAika { get; set; }
        public int Ika
        {
            get
            {
                if (this.SyntymaAika == null) {
                    return (0);
                }
                else {
                    /// Author: https://stackoverflow.com/a/1404
                    // Save today's date.
                    var today = DateTime.Today;
                    // Calculate the age.
                    var age = today.Year - this.SyntymaAika?.Year ?? 0;
                    // Go back to the year the person was born in case of a leap year
                    if (this.SyntymaAika > today.AddYears(-age)) age--;
                    return (age);
                }
            }
        }

        public Henkilo() {
            this.SyntymaAika = null;
        }

        public Henkilo(string etu, string suku) : this() {
            this.EtuNimi = etu;
            this.SukuNimi = suku;
        }

        public override string ToString() {
            return ($"{this.EtuNimi} {this.SukuNimi}");
        }
    }
}
