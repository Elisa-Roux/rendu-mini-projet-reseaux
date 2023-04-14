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

        public string getMostFrequent(List<string> headerValue)
        {
            return headerValue.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
        }


    }
}
