using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mini_projet_reseaux
{
    internal class Stats
    {
        public Stats()
        {

        }

        public double getAverage(List<double> headerValue)
        {
            return headerValue.Average();
        }

        public double getStandardDeviation(List<double> headerValue)
        {
            return Math.Sqrt(headerValue.Average(v => Math.Pow(v - headerValue.Average(), 2)));
        }

    }
}
